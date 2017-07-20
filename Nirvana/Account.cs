using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nirvana
{
    public class Collection
    {
        /// <summary>
        /// Коллекция логопасов
        /// </summary>
        public static ObservableCollection<Account> accounts = new ObservableCollection<Account>();
    }

    [Serializable]
    public class Account : INotifyPropertyChanged
    {      
        /// <summary>
        /// Мыло
        /// </summary>
        String mail;
        public String Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
                OnPropertyChanged("mail");
            }
        }

        /// <summary>
        /// Пароль
        /// </summary>
        String password;
        public String Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("password");
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
            }
        }

        /// <summary>
        /// Никнейм персонажа
        /// </summary>
        String nickName;
        public String NickName
        {
            get
            {
                return nickName;
            }

            set
            {
                nickName = value;
                OnPropertyChanged("nickName");
            }
        }

        Boolean cheked;
        public Boolean Cheked
        {
            get
            {
                return cheked;
            }
            set
            {
                cheked = value;
                OnPropertyChanged("cheked");
            }
        }

        /// <summary>
        /// Пустой конструктор для сериализатора xml
        /// </summary>
        public Account() { }

        /// <summary>
        /// Конструктор, принимающий 3 параметра
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="comment"></param>
        public Account(String mail, String password, String nickName)
        {
            this.mail = mail;
            this.password = password;
            this.nickName = nickName;
            this.tsaToken = String.Empty;
        }

        /// <summary>
        /// Конструктор, принимающий 3 параметра
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="password"></param>
        /// <param name="comment"></param>
        public Account(String mail, String password, String nickName, String tsaToken)
        {
            this.mail = mail;
            this.password = password;
            this.nickName = nickName;
            this.tsaToken = tsaToken;
        }

        /// <summary>
        /// Событие для реализации интерфейса (автосинхронизация коллекции логопасов и комбобокса)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]String prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
