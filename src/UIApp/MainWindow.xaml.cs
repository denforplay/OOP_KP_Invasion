using Invasion.View;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using System.Windows;
using System.Windows.Interop;

namespace UIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            Loaded += (s,e) =>
            {
                GameView gameView = new GameView(this);
                gameView.Run();
            };
        }
    }
}
