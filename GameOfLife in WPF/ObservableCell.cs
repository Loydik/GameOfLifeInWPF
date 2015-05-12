using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using System.Text;
using System.ComponentModel;

namespace GameOfLife_in_WPF
{
    public class ObservableCell : Cell, INotifyPropertyChanged
    {

        public override Boolean Alive {
            get { return _alive; } 
            set { 
                _alive = value; 
                OnPropertyChanged("Alive");
            } 
        }

        public ObservableCell(Boolean alive) : base(alive)
        {
            //this.Alive = alive;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
