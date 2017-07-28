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
    /// Логика взаимодействия для MyBalloon.xaml
    /// </summary>
    public partial class MyBalloon : Window
    {
        int x_tray_pos;
        int y_tray_pos;
        public MyBalloon(int X, int Y)
        {
            x_tray_pos = X;
            y_tray_pos = Y;
            Loaded += OnLoaded;
            InitializeComponent();
        }
        private void OnLoaded(object sender, EventArgs e)
        {
            Loaded -= OnLoaded;
            this.Top = y_tray_pos - this.ActualHeight;
            this.Left = x_tray_pos - this.ActualWidth;
        }

        private void ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Opacity = 1;
        }

        private void ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Ellipse)sender).Opacity = 0;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Top = y_tray_pos - this.ActualHeight;
            this.Left = x_tray_pos - this.ActualWidth;
        }
    }
    
}
