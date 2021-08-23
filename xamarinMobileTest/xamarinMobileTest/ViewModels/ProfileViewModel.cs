using System;
using System.Collections.Generic;
using System.Text;
using xamarinMobileTest.UserSettings;

namespace xamarinMobileTest.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public ProfileViewModel()
        {
            Title = "Profile";
            Username = SignedInUserDetails.Username;
        }
    }
}
