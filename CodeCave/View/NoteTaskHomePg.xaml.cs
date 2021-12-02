using CodeCave.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeCave.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteTaskHomePg : ContentPage
    {
        private readonly NoteTaskHomeVM vm = new();
        public NoteTaskHomePg()
        {
            InitializeComponent();
            BindingContext = vm;
            MessagingCenter.Subscribe<AddNoteTaskViewModel>(this, "Refresh", async (sender) => await vm.GetNotes());
        }

        private async void LifecycleEffect_Loaded(object sender, System.EventArgs e)
        {
            await vm.GetNotes();
        }
    }
}