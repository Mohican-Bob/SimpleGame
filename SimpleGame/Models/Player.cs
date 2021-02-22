using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleGame.Presentation;

namespace SimpleGame.Models 
{
    public class Player : ObservableObjects
    {
        private int _coin;

        public int Coin
        {
            get { return _coin; }
            set
            { 
                _coin = value;
                OnPropertyChanged(nameof(Coin));
            }
        }

    }
}
