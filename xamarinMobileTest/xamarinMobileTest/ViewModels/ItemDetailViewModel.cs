using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using xamarinMobileTest.Models;

namespace xamarinMobileTest.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        
        private DateTime dueDate;
        public int Id { get; set; }
        public Command<Item> SaveChangesCommand { get; }
        public ItemDetailViewModel()
        {
            SaveChangesCommand = new Command<Item>(OnSaveChanges);
        }

        private async void OnSaveChanges(Item item)
        {
                Text = Text;
                Description = Description;
                DueDate = DueDate;

            await LocalDatabaseService.UpdateItem(item);

            await Shell.Current.GoToAsync("..");
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public DateTime DueDate
        {
            get => dueDate;
            set => SetProperty(ref dueDate, value);
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(Int32.Parse(value));
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await LocalDatabaseService.GetItem(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                DueDate = item.DueDate;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
