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
    /// Логика взаимодействия для OffsetWindow.xaml
    /// </summary>
    public partial class OffsetWindow : Window
    {
        public Models.BotModels.Offset TempOffsets { get; private set; }

        public OffsetWindow(object tempOffsets)
        {
            InitializeComponent();
            this.TempOffsets = tempOffsets as Models.BotModels.Offset;
            this.DataContext = this.TempOffsets;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
