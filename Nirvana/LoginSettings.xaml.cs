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

namespace Nirvana
{
    /// <summary>
    /// Логика взаимодействия для LoginSettings.xaml
    /// </summary>
    public partial class LoginSettings : Window
    {
        public LoginSettings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserID_txt.Text = Loging.UserId_1;
            UserID2_txt.Text = Loging.UserId_2;
            Downloader_txt.Text = Loging.Downloader;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (UserID_txt.Text.Length > 0 && UserID2_txt.Text.Length > 0 && Downloader_txt.Text.Length > 0)
            {
                MainWindow.settings.Downloader = Downloader_txt.Text;
                Loging.Downloader = Downloader_txt.Text;

                MainWindow.settings.UserId_1 = UserID_txt.Text;
                Loging.UserId_1 = UserID_txt.Text;

                MainWindow.settings.UserId_2 = UserID2_txt.Text;
                Loging.UserId_2 = UserID2_txt.Text;
            }
            this.Hide();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
