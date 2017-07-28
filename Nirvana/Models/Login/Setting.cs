using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana.Models.Login
{
    public class Setting : INotifyPropertyChanged
    {

        Int32 id;
        public Int32 ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        String downloader;
        public String Downloader
        {
            get
            {
                return downloader;
            }

            set
            {
                downloader = value;
                OnPropertyChanged("Downloader");
            }
        }

        String userId_1;
        public String UserId_1
        {
            get
            {
                return userId_1;
            }

            set
            {
                userId_1 = value;
                OnPropertyChanged("UserId_1");
            }
        }

        String userId_2;
        public String UserId_2
        {
            get
            {
                return userId_2;
            }

            set
            {
                userId_2 = value;
                OnPropertyChanged("UserId_2");
            }
        }

        String serialnumber;
        public String Serialnumber
        {
            get
            {
                return serialnumber;
            }

            set
            {
                serialnumber = value;
                OnPropertyChanged("Serialnumber");
            }
        }

        String filepath;
        public String Filepath
        {
            get
            {
                return filepath;
            }

            set
            {
                filepath = value;
                OnPropertyChanged("Filepath");
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
