using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using xamarinMobileTest.Services;
using xamarinMobileTest.UserSettings;
using xamarinMobileTest.Views;

namespace xamarinMobileTest.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Command LoginCommand { get; }
        public DatabaseService DatabaseService { get; set; }

        public LoginViewModel()
        {
            DatabaseService = new DatabaseService();

            LoginCommand = new Command(OnLoginClicked);
        }
        private void OnLoginClicked(object obj)
        {
            UserModel user = new UserModel
            {
                Username = this.Username,
                Password = this.Password
            };

            if (DatabaseService.FindUser(user))
            {
                // User exists, sign in & Save User Details
                SignedInUserDetails.Username = this.Username; // This is saved from user input
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                // Display error message, user does not exist
                App.Current.MainPage.DisplayAlert("User does not exist", "Login Failed", "OK");
            }

        }
    }
}
