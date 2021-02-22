using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;


namespace SimpleGame.Presentation
{
    public class GameViewModel : ObservableObjects
    {


        
        Random random = new Random();
        private List<string> _icons = new List<string>();
        private int _coin;
        private int _betting;
        private int _won;
        private string _result;
        private string _reel1;
        private string _reel2;
        private string _reel3;
        DispatcherTimer dispatchTimer = new DispatcherTimer();
        bool spinning = true;
        
        private int spins = 0;

        public int Spins
        {
            get { return spins = 0; }
            set 
            {
                spins = value;
                OnPropertyChanged(nameof(Spins));
            }
        }


        private bool _spinning;

        public bool Spinning
        {
            get { return _spinning; }
            set 
            {
                _spinning = value;
                OnPropertyChanged(nameof(Spinning));
            }
        }



        public string Reel1
        {
            get { return _reel1; }
            set
            {
                _reel1 = value;
                OnPropertyChanged(nameof(Reel1));
            }
        }



        public string Reel2
        {
            get { return _reel2; }
            set
            {
                _reel2 = value;
                OnPropertyChanged(nameof(Reel2));
            }
        }

        public string Reel3
        {
            get { return _reel3; }
            set
            {
                _reel3 = value;
                OnPropertyChanged(nameof(Reel3));
            }
        }


        public List<string> Icons
        {
            get { return _icons; }
            set
            {
                _icons = value;
                OnPropertyChanged(nameof(Icons));
            }
        }


        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }


        public int Won
        {
            get { return _won; }
            set
            {
                _won = value;
                OnPropertyChanged(nameof(Won));
            }
        }


        public int Betting
        {
            get { return _betting; }
            set 
            {
                _betting = value;
                OnPropertyChanged(nameof(Betting));
            }
        }


        public int Coin
        {
            get { return _coin; }
            set
            {
                _coin = value;
                OnPropertyChanged(nameof(Coin));
            }
        }
        public GameViewModel()
        {
            
            _betting = 1;
            _coin = 200;
            _icons.Add("/SimpleGame;component/Media/1.png");
            _icons.Add("/SimpleGame;component/Media/2.png");
            _icons.Add("/SimpleGame;component/Media/3.png");
            _icons.Add("/SimpleGame;component/Media/4.png");
            _icons.Add("/SimpleGame;component/Media/5.png");

            int reel1 = random.Next(0, 5);
            int reel2 = random.Next(0, 5);
            int reel3 = random.Next(0, 5);
            _reel1 = _icons[reel1];
            _reel2 = _icons[reel2];
            _reel3 = _icons[reel3];

        }



        public void Spin()
        {
 
            if (spinning)
            {
                if (Betting > Coin)
                {
                    Result = "No Coin";
                    return;
                }
                spinning = false;
                spins = 0;
                Coin -= Betting;
                DispatcherTimer dispatchTimer = new DispatcherTimer();
                dispatchTimer.Tick += new EventHandler(Run_Reels);
                dispatchTimer.Interval = new TimeSpan(0, 0, 0, 1);
                dispatchTimer.Start();
            }


        }

        private void Calculate_Win()
        {
            int reel1 = random.Next(0, 101);
            int reel2 = random.Next(0, 101);
            int reel3 = random.Next(0, 101);

            reel1 = Odds(reel1);
            reel2 = Odds(reel2);
            reel3 = Odds(reel3);



            Update_Reels(reel1, reel2, reel3);


            if (reel1 == reel2 && reel1 == reel3)
            {
                _result = "You Won!";
                switch (reel1)
                {
                    case 0:
                        Won = Betting * 5;
                        break;
                    case 1:
                        Won = Betting * 2;
                        break;
                    case 2:
                        Won = Betting * 3;
                        break;
                    case 3:
                        Won = Betting * 10;
                        break;
                    case 4:
                        Won = Betting * 20;
                        break;
                    default:
                        break;
                }
            }
            else if (reel1 == 1 || reel2 == 1 || reel3 == 1)
            {
                Result = "You Won!";
                Won = 1;
            }
            else
            {
                Result = "No Win";
                Won = 0;
            }
            if (_result == "You Won!")
            {
                Result = "You Won!";
                Coin += Won;                
            }
            else
            {
                Result = "No Win";
            }
            spinning = true;

        }

        private void Update_Reels(int reel1,  int reel2, int reel3)
        {
            Reel1 = _icons[reel1];
            Reel2 = _icons[reel2];
            Reel3 = _icons[reel3];
        }

        internal void Bets(string upDown)
        {
            switch (upDown)
            {
                case "up":
                    Betting += 1;
                    break;
                case "down":
                    if (Betting == 1)
                        break;
                    else
                    {
                        Betting--;
                        break;
                    }
            };

        }

        private void Run_Reels(object sender, EventArgs e)
        {
            
            Won = 0;
            DispatcherTimer timer = sender as DispatcherTimer;            
            spins++;            
            Result = "Spinning";
            int reel1 = random.Next(0, 5);
            int reel2 = random.Next(0, 5);
            int reel3 = random.Next(0, 5);
            Update_Reels(reel1, reel2, reel3);       
            
            if (spins >= 4)
            {
                
                timer.Stop();
                Calculate_Win();
            }
        }
        private int Odds(int reel)
        {
            if (reel <= 50)
            {
                reel = 3;
            }
            if (reel <= 70 && reel >= 51)
            {
                reel = 1;
            }
            if (reel <= 85 && reel >= 71)
            {
                reel = 0;
            }
            if (reel <= 95 && reel >= 86)
            {
                reel = 2;
            }
            if (reel <= 100 && reel >= 96)
            {
                reel = 4;
            }
            return reel;
        }
        internal void Reset_Game()
        {
            Coin = 200;
            Result = "Coin Added";
        }
    }
}
