using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Models.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WPFView
{
    public partial class GameWindow : Window
    {
        private GameView _gameView;
        private Dictionary<string, Type> _weaponTypes;

        public GameWindow(Dictionary<string, Type> weaponTypes)
        {
            InitializeComponent();
            _weaponTypes = weaponTypes;
        }
        
        public void StartGame()
        {
            Time.TimeScale = 1;
            bool marker = false;
            Subscribe();
            Screen.Width = (int)viewBox.DesiredSize.Width;
            Screen.Height = (int)viewBox.DesiredSize.Height;
            if (_gameView is null)
            {
                marker = true;
                _gameView = new GameView(new Invasion.Models.Configurations.GameConfiguration(3), _weaponTypes);
            }
            else
            {
                _gameView.Restart();
            }

            if (marker)
            {
                formPlacement.Child = _gameView.RenderForm;
                _gameView.Run();
            }
        }

        public void LoseGame(GameLoseEvent loseEvent)
        {
            Time.TimeScale = 0;
            UnSubscribe();
            _gameView.Stop();
            LoseWindow loseWindow = new LoseWindow();
            loseWindow.Show();
            loseWindow.OnRestart += StartGame;
            loseWindow.OnExit += CloseGame;
        }

        public void CloseGame()
        {
            _gameView.Dispose();
            formPlacement.Child = null;
            Close();
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
            UnSubscribe();
            _gameView.Stop();
            WinWindow winWindow = new WinWindow(gameWinEvent.Score);
            winWindow.Show();
            winWindow.OnRestart += StartGame;
            winWindow.OnExit += CloseGame;
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
