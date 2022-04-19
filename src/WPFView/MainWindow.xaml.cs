using Invasion.Engine;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WPFView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Type> _weaponTypes;

        public MainWindow()
        {
            InitializeComponent();
            _weaponTypes = new Dictionary<string, Type>
            {
                {"Knife", typeof(Knife)},
                {"Pistol", typeof(Pistol)},
            };

            player1Choose.ItemsSource = _weaponTypes.Keys.ToList();
            player1Choose.SelectedIndex = 0;

            player2Choose.ItemsSource = _weaponTypes.Keys.ToList();
            player2Choose.SelectedIndex = 0;
        }

        private void StartGame()
        {
            Time.TimeScale = 1;
            GameWindow gameWindow = new GameWindow(new Dictionary<string, Type>
                {
                    { "Player1", _weaponTypes[player1Choose.SelectedItem.ToString()] },
                    { "Player2", _weaponTypes[player2Choose.SelectedItem.ToString()] },
                });

            gameWindow.Show();
            gameWindow.StartGame();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
    }
}
