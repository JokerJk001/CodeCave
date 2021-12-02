using CodeCave.Model;
using CodeCave.Service;
using CodeCave.View;
using CodeCave.ViewModel.Base;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CodeCave.ViewModel
{
    public class NoteTaskHomeVM : BaseVM
    {
        private bool _IsLoading;
        public bool IsLoading { get => _IsLoading; set => SetProperty(ref _IsLoading, value); }
        public ICommand RefreshDataCommand => new AsyncCommand(RefreshDataAsync);
        public ICommand DeleteNoteCommand => new AsyncCommand<Note>(DeleteNoteAsync);
        public ICommand LoadPageCommand => new AsyncCommand(LoadPageAsync);
        public ObservableRangeCollection<Note> NotesCollection { get; set; } = new();
        private readonly List<Note> notesList = new();

        private async Task DeleteNoteAsync(Note note)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var ans = await Application.Current.MainPage.DisplayAlert("", $"Delete {note.Title}?", "Yes", "No");
                if (ans)
                {
                    await LocalData.DeleteNoteAsync(note.ID);
                    await GetNotes();
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshDataAsync()
        {
            IsLoading = true;
            await GetNotes();
            IsLoading = false;
        }
        public async Task GetNotes()
        {
            List<Note> notes = await LocalData.GetNotesAsync();
            notesList.Clear();
            foreach (Note item in notes)
            {
                if (item.Date == DateTime.Now.ToString("ddd MMM yyyy"))
                    item.Date = "Today";
                else if (item.Date == DateTime.Now.AddDays(1).ToString("ddd MMM yyyy"))
                    item.Date = "Tomorrow";
                else if (item.Date == DateTime.Now.AddDays(-1).ToString("ddd MMM yyyy"))
                    item.Date = "Yesterday";

                if (item.CanSendNotification)
                {
                    if (item.Date.Equals("Today"))
                    {
                        if (!item.RecievedNotification)
                        {
                            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                            {
                                if (item.Time == DateTime.Now.ToString("HH:mm"))
                                {
                                    Task.Run(async () =>
                                    {
                                        item.RecievedNotification = true;
                                        await LocalData.UpdateNoteAsync(item);
                                        await GetNotes();

                                        // C + P explaining this code will take time + advanced
                                        SendNotification(item);
                                    });
                                    return false;
                                }
                                return true;
                            });
                        }
                    }
                }

                notesList.Add(item);
            }
            NotesCollection.ReplaceRange(notesList);
        }

        private void SendNotification(Note note)
        {
            var notification = new NotificationRequest
            {
                NotificationId = 5134,
                Title = $"🔔 {note.Title}",
                Description = note.Detail,
                Android =
                {
                    Priority = NotificationPriority.Max,
                },
            };
            NotificationCenter.Current.Show(notification);
        }

        private async Task LoadPageAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new AddNoteTaskPage());
            IsBusy = false;
        }
    }
}
