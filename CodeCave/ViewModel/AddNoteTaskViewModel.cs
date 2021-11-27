using CodeCave.Model;
using CodeCave.Service;
using CodeCave.ViewModel.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CodeCave.ViewModel
{
    public class AddNoteTaskViewModel : BaseVM
    {
        private string _Title = "", _Detail = "", _ImagePath = "";
        private bool _CanRecieveNotification, _ShowImageView;
        private DateTime _Date;
        private TimeSpan _Time;

        public string Title { get => _Title; set => SetProperty(ref _Title, value); }
        public string Detail { get => _Detail; set => SetProperty(ref _Detail, value); }
        public string ImagePath { get => _ImagePath; set => SetProperty(ref _ImagePath, value); }
        public bool CanRecieveNotification { get => _CanRecieveNotification; set => SetProperty(ref _CanRecieveNotification, value); }
        public bool ShowImageView { get => _ShowImageView; set => SetProperty(ref _ShowImageView, value); }
        public DateTime Date { get => _Date; set => SetProperty(ref _Date, value); }
        public TimeSpan Time { get => _Time; set => SetProperty(ref _Time, value); }

        public ICommand PickImageCmd => new AsyncCommand(PickImageAsync, allowsMultipleExecutions: false);
        public ICommand SaveNoteCmd => new AsyncCommand(SaveNoteAsync, allowsMultipleExecutions: false);
        public ICommand RemoveImageCmd => new Command(RemoveImage);

        public AddNoteTaskViewModel()
        {
            DateTime date = DateTime.Now;
            Date = date;
            Time = date.TimeOfDay;
        }

        private async Task PickImageAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                FileResult file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick an Image"
                });

                if (file == null)
                {
                    if (string.IsNullOrWhiteSpace(ImagePath))
                    return;
                }

                ImagePath = file.FullPath;
                ShowImageView = true;
            }
            catch (Exception x) { Console.WriteLine($"Error: {x.Message}"); }
            finally { IsBusy = false; }
        }

        private void RemoveImage()
        {
            ImagePath = "";
            ShowImageView = false;
        }

        private async Task SaveNoteAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (string.IsNullOrWhiteSpace(Title))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(Detail))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Detail cannot be mpty!", "OK");
                    return;
                }

                await LocalData.InsertNewNoteAsync(ProcessNote());
                await Application.Current.MainPage.Navigation.PopAsync();
                SendMessage();
            }
            finally
            {
                IsBusy = false;
            }

        }

        private Note ProcessNote()
        {
            var note = new Note()
            {
                ID = DateTime.Now.Ticks.ToString(),
                Title = Title,
                Detail = Detail,
                ImagePath = ImagePath,
                CanSendNotification = CanRecieveNotification,
                // There is an alt chk if Image != null note.CanShowImage == true.
                CanShowImage = ShowImageView,
                //
                Date = Date.ToString("ddd MMM yyyy"),
                Time = $"{Time.Hours:00}:{Time.Minutes:00}",
            };

            return note;
        }

        private void SendMessage() => MessagingCenter.Send(this, "Refresh");
    }
}
