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
        private ObservableCollection<Currency> _currencies = new ObservableCollection<Currency>();

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

        public void AddCurrency(Currency item)
        {
            _currencies.Add(item);
        }

        public void InsertCurrency(int index, Currency item)
        {
            _currencies.Insert(index, item);
        }

        public void RemoveCurrency(Currency item)
        {
            _currencies.Remove(item);
        }

        public void RemoveCurrencyAt(int index)
        {
            _currencies.RemoveAt(index);
        }

        public int IndexOfCurrency(Currency item)
        {
            return _currencies.IndexOf(item);
        }

        public bool ContainsCurrency(Currency item)
        {
            foreach (Currency currency in _currencies)
            {
                if (currency.Code == item.Code)
                    return true;
            }

            return false;
        }

        public bool ContainsCurrency(String code)
        {
            foreach (Currency currency in _currencies)
            {
                if (currency.Code == code)
                    return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
