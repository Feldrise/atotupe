using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Atotupe.Data
{
    public class Wallet : INotifyPropertyChanged
    {
        private string _name;
        private ObservableCollection<Currency> _currencies;

        public int Count => _currencies.Count;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged("Currencies");
            }
        }

        public void AddCurrency(Currency toAdd)
        {
            _currencies.Add(toAdd);
        }

        public void InsertCurrency(int index, Currency toInsert)
        {
            _currencies.Insert(index, toInsert);
        }

        public void RemoveCurrency(Currency toRemove)
        {
            _currencies.Remove(toRemove);
        }

        public void RemoveCurrencyAt(int index)
        {
            _currencies.RemoveAt(index);
        }

        public int IndexOfCurrency(Currency currency)
        {
            return _currencies.IndexOf(currency);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
