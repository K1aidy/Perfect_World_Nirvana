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
    /// Логика взаимодействия для AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //нельзя добавлять пустые поля
            if (textBox.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                Collection.accounts.Add(new Account(textBox.Text, textBox1.Text, textBox2.Text));
                textBox.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
            }
            this.Hide();
        }
    }
}
