using CodeCave.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodeCave
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            entry0.Text = "Welcome";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entry1.Text) && !string.IsNullOrWhiteSpace(entry2.Text))
            {
                outputLabel.Text = $"Welcome {entry1.Text} your password: {entry2.Text}"; 
            }
            else
            {
                outputLabel.Text = "Something went wrong.😐";
                outputLabel.TextColor = Color.Red;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (IsBusy)
            {
                outputLabel.Text = "Did noy execute!";
                return;
            }
            IsBusy = true;
            await Navigation.PushAsync(new AddNoteTaskPage());
            IsBusy = false;
        }
    }
}