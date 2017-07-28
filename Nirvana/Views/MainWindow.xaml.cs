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

                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            for (int i = 0; i < ListClients.work_collection.Count(); i++)
            {
                if (ListClients.work_collection[i] != null)
                {
                    if (ListClients.work_collection[i].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[i].BackgroundWorker5.CancelAsync();
                }
            }
            if (FormatText.Timer_1_State()) FormatText.Stop();
            tbi.Dispose();
            this.Width = 640;
            Nirvana.Properties.Settings.Default.Save();
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        #region Чекбоксы

        #region Следовать
        //private void ckbx_1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[1] != null)
        //        ListClients.work_collection[1].ChatRead.S = (bool)ckbx_1_1.IsChecked;
        //}

        //private void ckbx_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[2] != null)
        //        ListClients.work_collection[2].ChatRead.S = (bool)ckbx_2_1.IsChecked;
        //}

        //private void ckbx_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[3] != null)
        //        ListClients.work_collection[3].ChatRead.S = (bool)ckbx_3_1.IsChecked;
        //}

        //private void ckbx_4_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[4] != null)
        //        ListClients.work_collection[4].ChatRead.S = (bool)ckbx_4_1.IsChecked;
        //}

        //private void ckbx_5_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[5] != null)
        //        ListClients.work_collection[5].ChatRead.S = (bool)ckbx_5_1.IsChecked;
        //}

        //private void ckbx_6_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[6] != null)
        //        ListClients.work_collection[6].ChatRead.S = (bool)ckbx_6_1.IsChecked;
        //}

        //private void ckbx_7_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[7] != null)
        //        ListClients.work_collection[7].ChatRead.S = (bool)ckbx_7_1.IsChecked;
        //}

        //private void ckbx_8_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[8] != null)
        //        ListClients.work_collection[8].ChatRead.S = (bool)ckbx_8_1.IsChecked;
        //}

        //private void ckbx_9_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[9] != null)
        //        ListClients.work_collection[9].ChatRead.S = (bool)ckbx_9_1.IsChecked;
        //}

        //private void ckbx_10_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[10] != null)
        //        ListClients.work_collection[10].ChatRead.S = (bool)ckbx_10_1.IsChecked;
        //}
        #endregion

        #region Бафать

        //private void ckbx_1_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[1] != null)
        //        ListClients.work_collection[1].ChatRead.B = (bool)ckbx_1_2.IsChecked;
        //}

        //private void ckbx_2_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[2] != null)
        //        ListClients.work_collection[2].ChatRead.B = (bool)ckbx_2_2.IsChecked;
        //}

        //private void ckbx_3_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[3] != null)
        //        ListClients.work_collection[3].ChatRead.B = (bool)ckbx_3_2.IsChecked;
        //}

        //private void ckbx_4_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[4] != null)
        //        ListClients.work_collection[4].ChatRead.B = (bool)ckbx_4_2.IsChecked;
        //}

        //private void ckbx_5_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[5] != null)
        //        ListClients.work_collection[5].ChatRead.B = (bool)ckbx_5_2.IsChecked;
        //}

        //private void ckbx_6_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[6] != null)
        //        ListClients.work_collection[6].ChatRead.B = (bool)ckbx_6_2.IsChecked;
        //}

        //private void ckbx_7_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[7] != null)
        //        ListClients.work_collection[7].ChatRead.B = (bool)ckbx_7_2.IsChecked;
        //}

        //private void ckbx_8_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[8] != null)
        //        ListClients.work_collection[8].ChatRead.B = (bool)ckbx_8_2.IsChecked;
        //}

        //private void ckbx_9_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[9] != null)
        //        ListClients.work_collection[9].ChatRead.B = (bool)ckbx_9_2.IsChecked;
        //}

        //private void ckbx_10_2_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[10] != null)
        //        ListClients.work_collection[10].ChatRead.B = (bool)ckbx_10_2.IsChecked;
        //}

        #endregion

        #region Дебафать

        //private void ckbx_1_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[1] != null)
        //        ListClients.work_collection[1].ChatRead.R = (bool)ckbx_1_3.IsChecked;
        //}

        //private void ckbx_2_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[2] != null)
        //        ListClients.work_collection[2].ChatRead.R = (bool)ckbx_2_3.IsChecked;
        //}

        //private void ckbx_3_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[3] != null)
        //        ListClients.work_collection[3].ChatRead.R = (bool)ckbx_3_3.IsChecked;
        //}

        //private void ckbx_4_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[4] != null)
        //        ListClients.work_collection[4].ChatRead.R = (bool)ckbx_4_3.IsChecked;
        //}

        //private void ckbx_5_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[5] != null)
        //        ListClients.work_collection[5].ChatRead.R = (bool)ckbx_5_3.IsChecked;
        //}

        //private void ckbx_6_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[6] != null)
        //        ListClients.work_collection[6].ChatRead.R = (bool)ckbx_6_3.IsChecked;
        //}

        //private void ckbx_7_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[7] != null)
        //        ListClients.work_collection[7].ChatRead.R = (bool)ckbx_7_3.IsChecked;
        //}

        //private void ckbx_8_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[8] != null)
        //        ListClients.work_collection[8].ChatRead.R = (bool)ckbx_8_3.IsChecked;
        //}

        //private void ckbx_9_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[9] != null)
        //        ListClients.work_collection[9].ChatRead.R = (bool)ckbx_9_3.IsChecked;
        //}

        //private void ckbx_10_3_Click(object sender, RoutedEventArgs e)
        //{
        //    if (ListClients.work_collection[10] != null)
        //        ListClients.work_collection[10].ChatRead.R = (bool)ckbx_10_3.IsChecked;
        //}

        #endregion

        #endregion

    }

}
