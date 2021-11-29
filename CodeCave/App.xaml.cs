using CodeCave.View;
using Xamarin.Forms;

namespace CodeCave
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // To navigate between pages always change this code to the one below.
            /*MainPage = new NoteTaskHomePg();*/
            MainPage = new NavigationPage(new Page1());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
