using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using Nirvana.ViewModels;

namespace Nirvana.Models.BotModels
{
    public class My_Windows : INotifyPropertyChanged
    {
        /// <summary>
        /// Выбранный персонаж
        /// </summary>
        public static int selected_window = -1;

        //забытое поле, надо вспомнить, зачем оно тут!
        public int old_count_res;

        //перенести коллекцию в другой класс
        /// <summary>
        /// Временный список для алгоритма определения открытия новых и закрытия старых окон
        /// </summary>
        public static ObservableCollection<My_Windows> my_windows_selecting_temp = new ObservableCollection<My_Windows>();

        string name;
        int processID;
        int classID;
        ChatReader chatRead;
        Bitmap icon;

        /// <summary>
        /// Геттер - конвертер из Bitmap в ImageSource
        /// </summary>
        public ImageSource img
        {
            get
            {
                IntPtr hBitmap = icon.GetHbitmap();
                ImageSource src;
                try
                {
                    src = Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                    );
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
                return src;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int ProcessID
        {
            get
            {
                return processID;
            }
            set
            {
                processID = value;
                OnPropertyChanged("processID");
            }
        }
        public int ClassID
        {
            get
            {
                return classID;
            }

            set
            {
                classID = value;
                OnPropertyChanged("classID");
            }
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public IntPtr Handle { get; private set; }

        public BackgroundWorker BackgroundWorker { get; set; }

        internal StateEnum StateThread { get; set; }
        internal SkillArray SkillArray { get; set; }
        internal ChatReader ChatRead
        {
            get
            {
                return chatRead;
            }
            set
            {
                chatRead = value;
                OnPropertyChanged("ChatRead");
            }
        }
        internal Packets PacketSend { get; private set; }

        //wid персонажа
        public int Wid { get; set; }
        //флаг, указывающий, открывашка ли это
        public int Rule { get; set; }
        //дескриптор запущенного процесса
        public IntPtr Oph { get; private set; }

        //деструктор
        ~My_Windows()
        {
            if (Oph != IntPtr.Zero)
                WinApi.CloseHandle(Oph);
            
        }

        //конструктор
        public My_Windows(IntPtr handle)
        {
            this.Handle = handle;
            //получаем id процесса по хендлу
            WinApi.GetWindowThreadProcessId(handle, out processID);
            //запускаем процесс и получаем его дескриптор
            Oph = WinApi.OpenProcess(WinApi.ProcessAccessFlags.All, false, ProcessID);
            //создаем класс для отправки пакетов
            PacketSend = new Packets(Oph);
            //получаем имя персонажа для данного клиента
            Name = CalcMethods.ReadString(Oph, Offsets.BaseAdress, Offsets.OffsetsName);
            //узнаем wid
            Wid = CalcMethods.ReadInt(Oph, Offsets.BaseAdress, Offsets.OffsetsWid);
            //узнаем класс нашего персонажа
            classID = CalcMethods.ReadInt(Oph, Offsets.BaseAdress, Offsets.OffsetsClassId);
            //узнаем координаты
            X = CalcMethods.ReadFloat(Oph, Offsets.BaseAdress, Offsets.OffsetsX) / 10 + 400;
            Y = CalcMethods.ReadFloat(Oph, Offsets.BaseAdress, Offsets.OffsetsY) / 10 + 550;
            Z = CalcMethods.ReadFloat(Oph, Offsets.BaseAdress, Offsets.OffsetsZ) / 10;
            //выбираем картинку для нашего персонажа
            Select_Image(classID);
            //указываем состояние
            StateThread = StateEnum.stop;
            //анализируем доступные скиллы
            this.SkillArray = new SkillArray(Oph);
            //создаем поток для окна
            this.BackgroundWorker = new BackgroundWorker();
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker5_DoWork);
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Выбор иконки класса
        /// </summary>
        /// <param name="classID"></param>
        void Select_Image(int classID)
        {
            switch (classID)
            {
                case 0:
                    icon = new Bitmap(@"Resources\var.png");
                    break;
                case 1:
                    icon = new Bitmap(@"Resources\mag.png");
                    break;
                case 2:
                    icon = new Bitmap(@"Resources\sham.png");
                    break;
                case 3:
                    icon = new Bitmap(@"Resources\dru.png");
                    break;
                case 4:
                    icon = new Bitmap(@"Resources\tank.png");
                    break;
                case 5:
                    icon = new Bitmap(@"Resources\sin.png");
                    break;
                case 6:
                    icon = new Bitmap(@"Resources\luk.png");
                    break;
                case 7:
                    icon = new Bitmap(@"Resources\prist.png");
                    break;
                case 8:
                    icon = new Bitmap(@"Resources\sik.png");
                    break;
                case 9:
                    icon = new Bitmap(@"Resources\mist.png");
                    break;
                case 10:
                    icon = new Bitmap(@"Resources\prizrak.png");
                    break;
                case 11:
                    icon = new Bitmap(@"Resources\kosa.png");
                    break;
                default: break;
            }
        }

        public event LogHandler Log_event;
        public void Logging(params TaskBar.FormatText[] text_log) => Log_event?.Invoke(text_log);

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            while (StateThread == StateEnum.run)
            {
                switch (Rule){
                    case 0:
                        ChatRead.ReadChat_2();
                        break;
                    case 1:
                        ChatRead.ReadChat();
                        break;
                    case 2:
                        ChatRead.ReadChat_3();
                        break;
                }
            }
        }

    }
}
