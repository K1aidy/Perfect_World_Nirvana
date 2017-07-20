using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Nirvana.Models;
using System.Windows;

namespace Nirvana.ViewModels
{
    public class HeadViewModel : INotifyPropertyChanged
    {
        
        RelayCommand deleteAccountCommand;

        public HeadViewModel()
        {
            
        }
        //IEnumerable<Account> accounts;
        //public IEnumerable<Account> Accounts
        //{
        //    get { return accounts; }
        //    set
        //    {
        //        accounts = value;
        //        OnPropertyChanged("Accounts");
        //    }
        //}

        //удаление аккаунта
        public RelayCommand DeleteAccountCommand
        {
            get
            {
                return deleteAccountCommand ??
                    (deleteAccountCommand = new RelayCommand(
                        (selectedItem) =>
                        {
                            MessageBox.Show("111");
                            if (selectedItem == null) return;

                            Account acc = selectedItem as Account;
                            Collection.accounts.Remove(acc);
                        }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}