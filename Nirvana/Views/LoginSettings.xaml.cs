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

namespace Nirvana.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginSettings.xaml
    /// </summary>
    public partial class LoginSettings : Window
    {
        public Models.Login.Setting setting { get; private set; }

        public LoginSettings(Models.Login.Setting settings)
        {
            InitializeComponent();
            this.setting = new Models.Login.Setting { ID = settings.ID, Downloader = settings.Downloader, Serialnumber = settings.Serialnumber, UserId_1 = settings.UserId_1, UserId_2 = settings.UserId_2 };
            this.DataContext = setting;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (UserID_txt.Text.Length > 0 && UserID2_txt.Text.Length > 0 && Downloader_txt.Text.Length > 0)
            {
                DialogResult = true;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
