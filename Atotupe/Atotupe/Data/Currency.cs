using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Atotupe.Data
{
    public class Currency : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CurrencyValueUpdateArgs> ValueUpdated;

        private string _code;
        private string _name;
        private double _number;
        private double _value;
        private double _price;

        public Currency()
        {
            App.CurrencyPriceUpdated += OnPriceUpdated;
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
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

        public double Number
        {
            get => _number;
            set
            {
                _number = value;
                Value = _price * _number;
                OnPropertyChanged("Number");
            }
        }

        public double Value
        {
            get => _value;
            set
            {
                ValueUpdated?.Invoke(this, new CurrencyValueUpdateArgs { OldValue = _value, NewValue = value });
                _value = value;
                ValueLine = "Update";
                OnPropertyChanged("Value");
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                Value = _number * _price;
                OnPropertyChanged("Price");
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

        public string PriceLine
        {
            get
            {
                if (App.CurrenciesMode == "eur")
                    return $"{_price:0.0000}" + "€";
                else
                    return "$" + $"{_price:0.0000}";
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnPriceUpdated(object sender, CurrencyPriceUpdatedArgs args)
        {
            if (args.CurrencyCode == _code)
            {
                Price = args.NewPrice;
            }
        }
    }

    public class CurrencyValueUpdateArgs : EventArgs
    {
        public double OldValue = 0.0d;
        public double NewValue = 0.0d;
    }
}
