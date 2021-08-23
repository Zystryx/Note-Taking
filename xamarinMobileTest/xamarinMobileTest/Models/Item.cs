using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace xamarinMobileTest.Models
{
    public class Item : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        string _text;
        public string Text
        {
            set { SetProperty(ref _text, value); }

            get { return _text; }
        }

        string _description;
        public string Description
        {
            set { SetProperty(ref _description, value); }

            get { return _description; }
        }
        public DateTime DueDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}