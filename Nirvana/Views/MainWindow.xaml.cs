using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Nirvana.ViewModels;
using Nirvana.Models.BotModels;
using Nirvana.Models.TaskBar;

namespace Nirvana.Views
{

    
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HeadViewModel hvModel;

        public MainWindow()
        {
            InitializeComponent();
            hvModel = new HeadViewModel(tbi);
            this.DataContext = hvModel;
            //Test();
        }
        /// <summary>
        /// Метод для быстрого дебага функционала
        /// </summary>
        void Test()
        {
            Int32 processID;
            IntPtr hwnd = IntPtr.Zero;
            while (true)
            {
                //очищаем коллекцию клиентов и начинаем заполнять заново
                //получаем следующее окно с классом ElementClient Window. 
                hwnd = WinApi.FindWindowEx(IntPtr.Zero, hwnd, "ElementClient Window", null);
                //Если наткнулись на ноль - значит выходим 
                if (hwnd == IntPtr.Zero) break;
                WinApi.GetWindowThreadProcessId(hwnd, out processID);
                //запускаем процесс и получаем его дескриптор
                IntPtr oph = WinApi.OpenProcess(WinApi.ProcessAccessFlags.All, false, processID);
                //Int32[] temp_mas;
                if (oph != IntPtr.Zero)
                {
                    //My_Windows mw = new My_Windows(oph);
                    Injects.OpenWindow(oph);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            for (int i = 0; i < ListClients.work_collection.Count(); i++)
            {
                if (ListClients.work_collection[i] != null)
                {
                    if (ListClients.work_collection[i].BackgroundWorker.IsBusy)
                        ListClients.work_collection[i].BackgroundWorker.CancelAsync();
                }
            }
            if (FormatText.Timer_1_State()) FormatText.Stop();
            tbi.Dispose();
            this.Width = 640;
            Nirvana.Properties.Settings.Default.Save();
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

    }

}
