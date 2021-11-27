using CodeCave.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeCave.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNoteTaskPage : ContentPage
    {
        private readonly AddNoteTaskViewModel vm = new();
        public AddNoteTaskPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}