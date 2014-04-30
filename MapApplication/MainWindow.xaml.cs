using System.ComponentModel;
using System.Windows;

namespace MapApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MariaViewModel();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            MariaUserControl.Dispose();
        }
    }
}
