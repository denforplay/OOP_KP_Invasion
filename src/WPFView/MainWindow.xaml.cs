using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Models.Events;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPFView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Type> _weaponTypes;
        private GameView _gameView;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (formPlacement.Child is null)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            Time.TimeScale = 1;
            Subscribe();
            Screen.Width = (int)viewBox.DesiredSize.Width;
            Screen.Height = (int)viewBox.DesiredSize.Height;

            _gameView = new GameView(new Invasion.Models.Configurations.GameConfiguration(3), new Dictionary<string, Type>
                {
                    { "Player1", _weaponTypes[player1Choose.SelectedItem.ToString()] },
                    { "Player2", _weaponTypes[player2Choose.SelectedItem.ToString()] },
                });
            formPlacement.Child = _gameView.RenderForm;
            _gameView.Run();
        }

        public void LoseGame(GameLoseEvent loseEvent)
        {
            Time.TimeScale = 0;
            LoseWindow loseWindow = new LoseWindow();
            loseWindow.Show();
            loseWindow.OnRestart += StartGame;
            loseWindow.OnExit += CloseGame;
            UnSubscribe();
        }

        public void CloseGame()
        {
            _gameView.Dispose();
            formPlacement.Child = null;
        }

        private void Subscribe()
        {
            SingletonEventBus.GetInstance.Subscribe<GameLoseEvent>(LoseGame);
            SingletonEventBus.GetInstance.Subscribe<GameWinEvent>(WinGame);
        }
        
        private void UnSubscribe()
        {
            SingletonEventBus.GetInstance.Unsubscribe<GameLoseEvent>(LoseGame);
            SingletonEventBus.GetInstance.Unsubscribe<GameWinEvent>(WinGame);
        }

        public void WinGame(GameWinEvent gameWinEvent)
        {
            WinWindow winWindow = new WinWindow(gameWinEvent.Score);
            winWindow.Show();
            winWindow.OnRestart += StartGame;
            winWindow.OnExit += CloseGame;
            UnSubscribe();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (formPlacement.Child is null)
                {
                    Close();
                }
                else
                {
                    UnSubscribe();
                    CloseGame();
                }
            }
        }
    }
}
