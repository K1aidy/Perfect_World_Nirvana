using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace Nirvana
{
    public class FormatText : INotifyPropertyChanged
    {
        string text;
        Brush brushes;
        double fontSize;
        byte type;
        int time_live = 0;

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public Brush Brushes
        {
            get
            {
                return brushes;
            }
            set
            {
                brushes = value;
                OnPropertyChanged("Brushes");
            }            
        }

        public double FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        /// <summary>
        /// Тип сообщения: 1 - ошибка, 2 - информация, 3 - предупреждение.
        /// </summary>
        public byte Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public int Time_live
        {
            get
            {
                return time_live;
            }

            set
            {
                time_live = value;
            }
        }

        public FormatText(string text, Brush brushes, double fontSize, byte type)
        {
            this.text = text;
            this.brushes = brushes;
            this.fontSize = fontSize;
            this.type = type;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Коллекция всплывающих подсказок для трея, 
        /// обновляется каждый тик таймера
        /// </summary>
        public static ObservableCollection<FormatText> baloon_msg = new ObservableCollection<FormatText>();

        /// <summary>
        /// Таймер, по которому обновляется коллекция baloon_msg
        /// </summary>
        private static DispatcherTimer timer_1 = new DispatcherTimer();

        /// <summary>
        /// Функция обратного вызова для таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Callback(object sender, EventArgs e)
        {
            TimeLivePlus();
        }

        /// <summary>
        /// Узнаем состояние таймера
        /// </summary>
        /// <returns>true если запущен, false если не запущен</returns>
        public static bool Timer_1_State()
        {
            if (timer_1.IsEnabled == true)
                return true;
            else return false;
        }

        /// <summary>
        /// Запуск таймера 
        /// </summary>
        public static void Start()
        {
            timer_1.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer_1.Tick += new EventHandler(Timer_Callback);
            timer_1.Start();
        }

        /// <summary>
        /// Остановка таймера
        /// </summary>
        public static void Stop()
        {
            timer_1.Stop();
        }

        /// <summary>
        /// Метод добавляющий время жизни сообщения, по истечении 5000мс, сообщение удаляется из коллекции
        /// </summary>
        private static void TimeLivePlus()
        {
            for (int i = baloon_msg.Count() - 1; i > -1; i--)
            {
                baloon_msg[i].time_live += 100;
                if (baloon_msg[i].time_live > 5000)
                    baloon_msg.RemoveAt(i);
            }
        }

    }
}
