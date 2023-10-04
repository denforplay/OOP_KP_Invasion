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

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для WinWindow.xaml
    /// </summary>
    public partial class WinWindow : Window
    {
        public event Action OnExit;
        public event Action OnRestart;

        public WinWindow(int score)
        {
            InitializeComponent();
            scoreText.Text = score.ToString();
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
