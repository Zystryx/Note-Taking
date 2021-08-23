using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarinMobileTest.Services;
using xamarinMobileTest.Views;

namespace xamarinMobileTest
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<DatabaseService>();

            Device.SetFlags(new string[] { "SwipeView_Experimental", "AppTheme_Experimental" });

            MainPage = new NavigationPage(new LoginPage());

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
