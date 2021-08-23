using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using xamarinMobileTest.Models;
using xamarinMobileTest.Views;

namespace xamarinMobileTest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> DeleteItemCommand { get; }

        public ItemsViewModel()
        {
            Title = "Notes";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DeleteItemCommand = new Command<Item>(async x => await OnDeleteItem(x));

            LocalDatabaseService.OnDeletedItem += LocalDatabaseService_OnDeletedItem;
            LocalDatabaseService.OnUpdatedItem += LocalDatabaseService_OnUpdatedItem;
        }

        private void LocalDatabaseService_OnUpdatedItem()
        {
            _ = ExecuteLoadItemsCommand();
        }

        private void LocalDatabaseService_OnDeletedItem()
        {
            _ = ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await LocalDatabaseService.GetAllItems();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private async Task OnDeleteItem(Item selectedItem)
        {
            await LocalDatabaseService.DeleteItem(selectedItem);
        }
    }
}