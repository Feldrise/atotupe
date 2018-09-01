using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Atotupe.Data
{
    public class Currency : INotifyPropertyChanged
    {
        private string _code;
        private string _name;
        private double _number;
        private double _value;

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
                OnPropertyChanged("Number");
            }
        }

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
