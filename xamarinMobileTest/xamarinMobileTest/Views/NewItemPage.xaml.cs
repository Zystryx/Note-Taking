using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using xamarinMobileTest.Models;
using xamarinMobileTest.ViewModels;

namespace xamarinMobileTest.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
        }

        private void DueDateClicked(object sender, EventArgs e)
        {
            DisplayAlert("Due Date Clicked", "You can now selected a due date", "OK");
        }
    }
}