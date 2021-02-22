using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SimpleGame.Presentation;

namespace SimpleGame.Presentation
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        GameViewModel _gameViewModel;



        public GameView(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            
            InitializeComponent();
        }
        public GameView()
        {

        }

        private void Spin_Click(object sender, RoutedEventArgs e)
        {
            
            Spin.IsEnabled = false;


            _gameViewModel.Spin();
            Spin.IsEnabled = true;

        }

        private void Bet_Click(object sender, RoutedEventArgs e)
        {
            string upDown;
            Button betButtons = sender as Button;
            upDown = betButtons.Tag.ToString();
            _gameViewModel.Bets(upDown);
        }

        private void Game_Info_Click(object sender, RoutedEventArgs e)
        {
            GameInfo gameInfo = new GameInfo();

            gameInfo.ShowDialog();
        }

        private void Reset_Game_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.Reset_Game();
        }
    }
}
