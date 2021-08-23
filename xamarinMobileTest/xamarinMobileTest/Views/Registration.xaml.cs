using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarinMobileTest.Views
{
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void AccountRegister(object sender, EventArgs e)
        {
            DisplayAlert("Registered!", "", "OK");
        }
    }
}