using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace Atotupe.Data
{
    public class Wallet : INotifyPropertyChanged
    {
        private string _name;
        private double _value;
        private ObservableCollection<Currency> _currencies = new ObservableCollection<Currency>();
        
        public int Count => _currencies.Count;

        public Wallet()
        {
            _currencies.CollectionChanged += OnCurrenciesChanged;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueLine = "Update";
                OnPropertyChanged("Value");
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

        public string ValueLine
        {
            get
            {
                if (App.CurrenciesMode == "eur")
                    return $"{_value:0.0000}" + "€";
                else
                    return "$" + $"{_value:0.0000}";
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                OnPropertyChanged("ValueLine");
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

        private void OnCurrenciesChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Remove)
            {
                if (args.OldItems != null)
                {
                    foreach (var argsOldItem in args.OldItems)
                        ((Currency) (argsOldItem)).ValueUpdated -= OnCurrencyValueUpdated;
                }

                if (args.NewItems != null)
                {
                    foreach (var argsNewItem in args.NewItems)
                        ((Currency) (argsNewItem)).ValueUpdated += OnCurrencyValueUpdated;
                }
            }
        }

        private void OnCurrencyValueUpdated(object sender, CurrencyValueUpdateArgs args)
        {
            Value = (_value - args.OldValue) + args.NewValue;
        }
    }
}
