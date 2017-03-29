using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows.Threading;

namespace Nirvana
{
    public class My_Windows : INotifyPropertyChanged
    {
        /// <summary>
        /// Выбранный персонаж
        /// </summary>
        public static int selected_window = -1;

        public int old_count_res;

        public event Log Log_event;

        public void Logging(params FormatText[] text_log)
        {
            Log_event(text_log);
        }

        /// <summary>
        /// Временный список для алгоритма определения открытия новых и закрытия старых окон
        /// </summary>
        public static ObservableCollection<My_Windows> my_windows_selecting_temp =
            new ObservableCollection<My_Windows>();
        private static List<int> skills_for_buf = new List<int> {
            #region мист
            #endregion
            #region сикер
            1361,           //Железная плоть
            1696,           //Светлая железная плоть
            1697,           //Темная железная плоть
            1342,           //Магия клинка вселенной
            //1343,           //Магия клинка империи
            1344,           //Техника черного воина
            //1345,           //Техника кровавого меча
            //1346,           //Техника каменного змея
            #endregion
            #region вар
            77,             //Аура стали
            422,            //Светлая аура стали
            423,            //Темная аура стали
            #endregion
            #region маг
            2258,           //Божественный ледяной доспех
            2259,           //Демонический ледяной доспех
            180,            //Ледяной доспех
            458,            //Светлый ледяной доспех
            459,            //Темный ледяной доспех
            466,            //Светлое обморожение
            467,            //Темное обморожение
            2262,           //Божественное обморожение
            2263,           //Демоническое обморожение
            91,             //Обморожение
            #endregion
            #region танк
            112,            //Облик тигра
            518,            //Светлый облик тигра
            519,            //Темный облик тигра
            1980,           //Светлый облик тигра (усиленное)
            1981,           //Темный облик тигра (усиленное)
            512,            //Светлый рев главы стаи
            513,            //Темный рев главы стаи
            82,             //Рев главы стаи
            83,             //Невероятная сила
            516,            //Светлая невероятная сила
            517,            //Темная невероятная сила
            #endregion
            #region дру
            312,            //Обращение в лисицу
            656,            //Светлое обращение в лисицу
            657,            //Темное обращение в лисицу
            1984,           //Светлое обращение в лисицу (усиленное)
            1985,           //Темное обращение в лисицу (усиленное)
            306,            //Стена шипов
            648,            //Светлая стена шипов
            649,            //Темная стена шипов
            762,            //Наряд из цветов
            763,            //Светлый наряд из цветов
            764,            //Темный наряд из цветов
            #endregion
            #region прист
            2222,           //Милость богов
            121,            //Оплот духа
            122,            //Мудрость небес
            124,            //Благословенный символ
            194,            //Оплот тела
            1811,           //Крылья - Ночной танец
            1869,           //Ночной танец
            #endregion
            #region лук
            #endregion
            #region син
            1094,           //Метка крови
            1276,           //Светлая метка крови
            1277,           //Темная метка крови
            1282,           //Светлая печать бешеного волка
            #endregion
            #region шам
            1824,           //Призыв
            #endregion
            #region призрак
            2561,           //Лунный стих
            2756,           //Светлый лунный стих
            2757            //Темный лунный стих
            #endregion
            #region коса
            #endregion
        };

        private float x;
        private float y;
        private float z;
        private string name;
        private IntPtr handle;
        private int processID;
        private int classID;
        private StateEnum stateThread;
        private Packets packetSend;
        private int wid;
        private List<Skill> my_skills_for_buf = new List<Skill>();
        private Skill changeForm;
        private Skill shamansCall;


        private BackgroundWorker backgroundWorker5;

        private Thread thread;

        private ChatReader chatRead;

        //флаг, указывающий, открывашка ли это
        private int rule = 0;

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
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                    );
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
                return src;
            }
            set
            {
                //OnPropertyChanged("img");
            }
        }

        //дескриптор запущенного процесса
        IntPtr oph;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("name");
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

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public float Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public IntPtr Handle
        {
            get
            {
                return handle;
            }
        }

        internal StateEnum StateThread
        {
            get
            {
                return stateThread;
            }

            set
            {
                stateThread = value;
            }
        }

        public Thread Thread
        {
            get
            {
                return thread;
            }

            set
            {
                thread = value;
            }
        }

        public BackgroundWorker BackgroundWorker5
        {
            get
            {
                return backgroundWorker5;
            }

            set
            {
                backgroundWorker5 = value;
            }
        }

        internal ChatReader ChatRead
        {
            get
            {
                return chatRead;
            }

            set
            {
                chatRead = value;
            }
        }

        internal Packets PacketSend
        {
            get
            {
                return packetSend;
            }

        }

        public int Wid
        {
            get
            {
                return wid;
            }
        }

        public int Rule
        {
            get
            {
                return rule;
            }

            set
            {
                rule = value;
            }
        }

        public List<Skill> My_skills_for_buf
        {
            get
            {
                return my_skills_for_buf;
            }
        }

        public IntPtr Oph
        {
            get
            {
                return oph;
            }
        }

        public Skill ChangeForm
        {
            get
            {
                return changeForm;
            }
        }

        public Skill ShamansCall
        {
            get
            {
                return shamansCall;
            }
        }

        //деструктор
        ~My_Windows()
        {
            if (Oph != IntPtr.Zero)
                WinApi.CloseHandle(Oph);
            
        }

        //конструктор
        public My_Windows(IntPtr handle)
        {
            this.handle = handle;
            //получаем id процесса по хендлу
            WinApi.GetWindowThreadProcessId(handle, out processID);
            //запускаем процесс и получаем его дескриптор
            oph = WinApi.OpenProcess(WinApi.ProcessAccessFlags.All, false, ProcessID);
            //создаем класс для отправки пакетов
            packetSend = new Packets(oph);
            //получаем имя персонажа для данного клиента
            name = CalcMethods.ReadString(oph, Offsets.BaseAdress, Offsets.OffsetsName);
            //узнаем wid
            wid = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWid);
            //узнаем класс нашего персонажа
            classID = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsClassId);
            //читаем доступные скиллы-бафы
            int skillCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsSkillsCount);
            for (int s = 0; s < skillCount; s++)
            {
                int id = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToIdSkill(s));
                if (skills_for_buf.Contains(id))
                {
                    if (id == 112 || id == 518 || id == 519 || id == 1980 || id == 1981 ||  //танк
                        id == 312 || id == 656 || id == 657 || id == 1984 || id == 1985 ||  //друид
                        id == 1811 || id == 1869)                                           //прист
                    {
                        changeForm = new Skill(id, s);
                        continue;
                    }
                    if (id == 1824)
                    {
                        shamansCall = new Skill(id, s);
                        continue;
                    }
                    my_skills_for_buf.Add(new Skill(id, s));
                }
                    
            }
            //узнаем координаты
            x = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsX) / 10 + 400;
            y = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsY) / 10 + 550;
            z = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsZ) / 10;
            //выбираем картинку для нашего персонажа
            Select_Image(classID);
            //указываем состояние
            stateThread = StateEnum.stop;
            //создаем поток для окна
            this.BackgroundWorker5 = new BackgroundWorker();
            this.BackgroundWorker5.WorkerSupportsCancellation = true;
            this.BackgroundWorker5.DoWork += new DoWorkEventHandler(this.backgroundWorker5_DoWork);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            while (StateThread == StateEnum.run)
            {
                switch (rule){
                    case 0:
                        chatRead.ReadChat_2();
                        break;
                    case 1:
                        chatRead.ReadChat();
                        break;
                    case 2:
                        chatRead.ReadChat_3();
                        break;
                }
                Thread.Sleep(3000);
            }
        }

    }
}
