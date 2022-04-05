using System;
using System.Windows;

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для LoseWindow.xaml
    /// </summary>
    public partial class LoseWindow : Window
    {
        public event Action OnRestart;
        public event Action OnExit;

        public LoseWindow()
        {
            InitializeComponent();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            OnRestart?.Invoke();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            OnExit?.Invoke();
        }
    }
}
