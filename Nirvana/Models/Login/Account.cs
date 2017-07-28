using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nirvana.Models.Login
{
    public class Collection
    {
        /// <summary>
        /// Коллекция логопасов
        /// </summary>
        public static ObservableCollection<Account> accounts = new ObservableCollection<Account>();
    }

    public class Account : INotifyPropertyChanged
    {

        /// <summary>
        /// Первичный ключ для db
        /// </summary>
        private Int32 id;
        public Int32 ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Почта
        /// </summary>
        private String mail;
        public String Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }

        /// <summary>
        /// Пароль
        /// </summary>
        private String password;
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Никнейм
        /// </summary>
        private String nickname;
        public String Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                OnPropertyChanged("Nickname");
            }
        }

        /// <summary>
        /// Сервер
        /// </summary>
        private String server;
        public String Server
        {
            get { return server; }
            set
            {
                server = value;
                OnPropertyChanged("Server");
            }
        }

        /// <summary>
        /// Токен для запоминания двухфакторной аутентификации
        /// </summary>
        String tsaToken;
        public String TsaToken
        {
            get
            {
                return tsaToken;
            }

            set
            {
                tsaToken = value;
                OnPropertyChanged("TsaToken");
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
