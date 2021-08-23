using System.ComponentModel;
using Xamarin.Forms;
using xamarinMobileTest.ViewModels;

namespace xamarinMobileTest.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}