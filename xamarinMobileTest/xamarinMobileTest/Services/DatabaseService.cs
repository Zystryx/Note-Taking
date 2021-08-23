using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarinMobileTest.Models;

namespace xamarinMobileTest.Services
{
    public class DatabaseService : IUserService, ILocalDatabaseService
    {
        #region IUserService
        List<UserModel> ListOfUsers = new List<UserModel>();

        public DatabaseService()
        {
            // Sample Data of users
            ListOfUsers.Add(new UserModel { UserId = 1, Username = "John123", Password = "Password" });
            ListOfUsers.Add(new UserModel { UserId = 2, Username = "Sarah321", Password = "Password2" });
            ListOfUsers.Add(new UserModel { UserId = 3, Username = "a", Password = "b" });
        }


        public bool FindUser(UserModel user)
        {
            // Checking if the given user exists in list
            // If it does, return true
            if (ListOfUsers.Where(x => x.Username == user.Username && x.Password == user.Password).Count() == 1)
            {
                return true;
            }
            // Else return false, user does not exist.
            else
            {
                return false;
            }
        }


        #endregion

        #region ILocalDatabaseService
        public event Action OnUpdatedItem;
        public event Action OnDeletedItem;

        public async Task<List<Item>> GetAllItems()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData3.db");
            var db = new SQLiteAsyncConnection(databasePath);
            var data = await db.Table<Item>().ToListAsync();
            return data.ToList();
        }

        public async Task<Item> GetItem(int id)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData3.db");
            var db = new SQLiteAsyncConnection(databasePath);
            var data = await db.Table<Item>().Where(item => item.Id == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task UpdateItem(Item item)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData3.db");
            var db = new SQLiteAsyncConnection(databasePath);

            await db.UpdateAsync(item);
            OnUpdatedItem?.Invoke();
        }

        public async Task CreateItem(Item item)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData3.db");
            var db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Item>();
            await db.InsertAsync(item);

        }

        public async Task DeleteItem(Item item)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData3.db");
            var db = new SQLiteAsyncConnection(databasePath);

            await db.DeleteAsync(item);
            OnDeletedItem?.Invoke();
        }

        #endregion
    }

    #region Interfaces
    public interface IUserService
    {
        bool FindUser(UserModel user);
    }

    public interface ILocalDatabaseService
    {
        event Action OnUpdatedItem;
        event Action OnDeletedItem;
        Task<List<Item>> GetAllItems();
        Task<Item> GetItem(int id);
        Task CreateItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(Item item);
    }
    #endregion

    #region Models
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
    #endregion
}
