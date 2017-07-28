using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Nirvana.Models.Login;

namespace Nirvana.Views
{
    /// <summary>
    /// Логика взаимодействия для AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public Account Account { get; private set; }

        public AddAccount(Account acc)
        {
            InitializeComponent();
            Account = acc;
            this.DataContext = Account;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
