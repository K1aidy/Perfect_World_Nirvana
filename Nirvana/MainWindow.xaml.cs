using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Hardcodet.Wpf.TaskbarNotification;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Net;

namespace Nirvana
{

    public delegate void Log(params FormatText[] textLog);
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ставим костыль для общего доступа к логу из любого дочернего класса
        public static RichTextBox loging_box;
        XmlSerializer formatter;
        XmlSerializer formatter_for_offsets;
        XmlSerializer formatter_for_pass;
        public static MySettings settings;
        public Offset ofset;
        public object thread_object;
        MyBalloon mb;
        AddAccount addAccounWindow;
        LoginSettings loginSetWondow;
        GetSmsCode windowGetSms;

        public MainWindow()
        {
            InitializeComponent();
            
            mb = new MyBalloon(tbi.GetPopupTrayPosition().X, tbi.GetPopupTrayPosition().Y);
            
            mb.baloon_panel.ItemsSource = FormatText.baloon_msg;
            if (!FormatText.Timer_1_State())
                FormatText.Start();
            thread_object = new object();
            loging_box = log_box;
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            settings = new MySettings();
            ofset = new Offset();
            //определяем тип для сериализатора
            formatter_for_offsets = new XmlSerializer(typeof(Offset));
            formatter = new XmlSerializer(typeof(MySettings));
            formatter_for_pass = new XmlSerializer(typeof(ObservableCollection<Account>));
            //загружаем данные из xml при открытии приложения
            Deserializable();
            listAccount.ItemsSource = Collection.accounts;
            //this.DataContext = new ViewModels.HeadViewModel();
            //устанавливаем чекбоксы как в сохраненном файле
            ApplySettings();
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

        /// <summary>
        /// Обработчик события для логирования, принимает сообщение в качестве аргумента
        /// </summary>
        /// <param name="text_log"></param>
        public void Logging(params FormatText[] text_log)
        {
            log_box.Dispatcher.Invoke
                        (System.Windows.Threading.DispatcherPriority.Background, new
                             Action(() =>
                             {
                                 lock (thread_object)
                                 {
                                     
                                     string tray_text = "";
                                     foreach (FormatText log in text_log)
                                     {
                                         TextRange tr = new TextRange(log_box.Document.ContentEnd, log_box.Document.ContentEnd);
                                         tr.Text = log.Text != null ? log.Text : "пустое сообщение";
                                         tr.Text += " ";
                                         tr.ApplyPropertyValue(TextElement.ForegroundProperty, log.Brushes);
                                         tr.ApplyPropertyValue(TextElement.FontSizeProperty, log.FontSize);
                                         tray_text += tr.Text;
                                     }
                                     if (FormatText.baloon_msg.Count() < 5)
                                     {
                                         FormatText.baloon_msg.Add(new FormatText(tray_text, Brushes.Black, 13, text_log[0].Type));
                                     }
                                     else
                                     {
                                         FormatText.baloon_msg.RemoveAt(0);
                                         FormatText.baloon_msg.Add(new FormatText(tray_text, Brushes.Black, 13, text_log[0].Type));
                                     }
                                     mb.Show();
                                     log_box.AppendText("\n");
                                     log_box.ScrollToEnd();
                                 }                              
                             }
                             )
                        );
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
            if (FormatText.Timer_1_State())
                FormatText.Stop();
            tbi.Dispose();
            this.Width = 640;
            Nirvana.Properties.Settings.Default.Save();
            base.OnClosing(e);
        }

        /// <summary>
        /// При загрузке MainWindow включаем фильтры комбобоксов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mb.Owner = this;
            tbi.LeftClickCommandParameter = this;
           
            #region 1й combobox
            ICollectionView view = CollectionViewSource.GetDefaultView(ListClients.my_windows_pl);
                view.Filter = str => !(((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

            cmbx_pl.ItemsSource = view;
            #endregion

            #region 2й combobox
            ICollectionView view_2 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_1);
                view_2.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

            cmbx_otkr_1.ItemsSource = view_2;
            #endregion

            #region 3й combobox
            ICollectionView view_3 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_2);
            view_3.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );
            cmbx_otkr_2.ItemsSource = view_3;
            #endregion

            #region 4й combobox
            ICollectionView view_4 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_3);
                view_4.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

            cmbx_otkr_3.ItemsSource = view_4;
            #endregion

            #region 5й combobox
            ICollectionView view_5 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_4);
                view_5.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

            cmbx_otkr_4.ItemsSource = view_5;
            #endregion

            #region 6й combobox
            ICollectionView view_6 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_5);
                view_6.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[6])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

                cmbx_otkr_5.ItemsSource = view_6;
            #endregion

            #region 7й combobox
            ICollectionView view_7 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_6);
                view_7.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
                || ((str as My_Windows).Name == ListClients.exList[1])
                || ((str as My_Windows).Name == ListClients.exList[2])
                || ((str as My_Windows).Name == ListClients.exList[3])
                || ((str as My_Windows).Name == ListClients.exList[4])
                || ((str as My_Windows).Name == ListClients.exList[5])
                || ((str as My_Windows).Name == ListClients.exList[7])
                || ((str as My_Windows).Name == ListClients.exList[8])
                || ((str as My_Windows).Name == ListClients.exList[9])
                || ((str as My_Windows).Name == ListClients.exList[10])
                );

            cmbx_otkr_6.ItemsSource = view_7;
            #endregion

            #region 8й combobox
            ICollectionView view_8 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_7);
            view_8.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
            || ((str as My_Windows).Name == ListClients.exList[1])
            || ((str as My_Windows).Name == ListClients.exList[2])
            || ((str as My_Windows).Name == ListClients.exList[3])
            || ((str as My_Windows).Name == ListClients.exList[4])
            || ((str as My_Windows).Name == ListClients.exList[5])
            || ((str as My_Windows).Name == ListClients.exList[6])
            || ((str as My_Windows).Name == ListClients.exList[8])
            || ((str as My_Windows).Name == ListClients.exList[9])
            || ((str as My_Windows).Name == ListClients.exList[10])
            );

            cmbx_otkr_7.ItemsSource = view_8;
            #endregion

            #region 9й combobox
            ICollectionView view_9 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_8);
            view_9.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
            || ((str as My_Windows).Name == ListClients.exList[1])
            || ((str as My_Windows).Name == ListClients.exList[2])
            || ((str as My_Windows).Name == ListClients.exList[3])
            || ((str as My_Windows).Name == ListClients.exList[4])
            || ((str as My_Windows).Name == ListClients.exList[5])
            || ((str as My_Windows).Name == ListClients.exList[6])
            || ((str as My_Windows).Name == ListClients.exList[7])
            || ((str as My_Windows).Name == ListClients.exList[9])
            || ((str as My_Windows).Name == ListClients.exList[10])
            );

            cmbx_otkr_8.ItemsSource = view_9;
            #endregion

            #region 10й combobox
            ICollectionView view_10 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_9);
            view_10.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
            || ((str as My_Windows).Name == ListClients.exList[1])
            || ((str as My_Windows).Name == ListClients.exList[2])
            || ((str as My_Windows).Name == ListClients.exList[3])
            || ((str as My_Windows).Name == ListClients.exList[4])
            || ((str as My_Windows).Name == ListClients.exList[5])
            || ((str as My_Windows).Name == ListClients.exList[6])
            || ((str as My_Windows).Name == ListClients.exList[7])
            || ((str as My_Windows).Name == ListClients.exList[8])
            || ((str as My_Windows).Name == ListClients.exList[10])
            );

            cmbx_otkr_9.ItemsSource = view_10;
            #endregion

            #region 11й combobox
            ICollectionView view_11 = CollectionViewSource.GetDefaultView(ListClients.my_windows_otkr_shaman);
            view_11.Filter = str => !(((str as My_Windows).Name == ListClients.exList[0])
            || ((str as My_Windows).Name == ListClients.exList[1])
            || ((str as My_Windows).Name == ListClients.exList[2])
            || ((str as My_Windows).Name == ListClients.exList[3])
            || ((str as My_Windows).Name == ListClients.exList[4])
            || ((str as My_Windows).Name == ListClients.exList[5])
            || ((str as My_Windows).Name == ListClients.exList[6])
            || ((str as My_Windows).Name == ListClients.exList[7])
            || ((str as My_Windows).Name == ListClients.exList[8])
            || ((str as My_Windows).Name == ListClients.exList[9])
            );

            cmbx_sham.ItemsSource = view_11;
            #endregion
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ListClients.Count_Clients();

            for (int i = 0; i < ListClients.exList.Count(); i++)
            {
                ListClients.exList[i] = null;
            }
            cmbx_pl.SelectedIndex = -1;
            cmbx_sham.SelectedIndex = -1;
            cmbx_otkr_1.SelectedIndex = -1;
            cmbx_otkr_2.SelectedIndex = -1;
            cmbx_otkr_3.SelectedIndex = -1;
            cmbx_otkr_4.SelectedIndex = -1;
            cmbx_otkr_5.SelectedIndex = -1;
            cmbx_otkr_6.SelectedIndex = -1;
            cmbx_otkr_7.SelectedIndex = -1;
            cmbx_otkr_8.SelectedIndex = -1;
            cmbx_otkr_9.SelectedIndex = -1;

            for (int i = 0; i< ListClients.work_collection.Count(); i++)
            {
                if(ListClients.work_collection[i]!=null)
                    if (ListClients.work_collection[i].BackgroundWorker5.IsBusy)
                    {
                        ListClients.work_collection[i].StateThread = StateEnum.stop;
                        ListClients.work_collection[i].BackgroundWorker5.CancelAsync();
                    }
                ListClients.work_collection[i] = null;
            }
            start_stop_btn.Content = "Старт";

            Refresh_Cmbx();
        }

        private void cmbx_pl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbx_pl.SelectedIndex > -1)
            {
                if (ListClients.work_collection[0] != null)
                {
                    if (ListClients.work_collection[0].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[0].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[0].StateThread = StateEnum.stop;
                    ListClients.work_collection[0].Rule = 0;
                }

                ListClients.exList[0] = ((My_Windows)cmbx_pl.SelectedItem).Name;
                ListClients.work_collection[0] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[0].Rule = 2;
                ListClients.work_collection[0].ChatRead = new ChatReader(ListClients.work_collection[0]);
                ListClients.work_collection[0].Log_event += Logging;
            }
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_1.SelectedIndex > -1)
            {
                if (ListClients.work_collection[1] != null)
                {
                    if (ListClients.work_collection[1].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[1].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[1].StateThread = StateEnum.stop;
                    ListClients.work_collection[1].Rule = 0;
                }

                ListClients.exList[1] = ((My_Windows)cmbx_otkr_1.SelectedItem).Name;
                ListClients.work_collection[1] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[1].ChatRead = new ChatReader(ListClients.work_collection[1],
                    settings.Checkbox_set[1][0], settings.Checkbox_set[1][1], settings.Checkbox_set[1][2]);
                ListClients.work_collection[1].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_2.SelectedIndex > -1)
            {
                if (ListClients.work_collection[2] != null)
                {
                    if (ListClients.work_collection[2].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[2].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[2].StateThread = StateEnum.stop;
                    ListClients.work_collection[2].Rule = 0;
                }

                ListClients.exList[2] = ((My_Windows)cmbx_otkr_2.SelectedItem).Name;
                ListClients.work_collection[2] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[2].ChatRead = new ChatReader(ListClients.work_collection[2],
                    settings.Checkbox_set[2][0], settings.Checkbox_set[2][1], settings.Checkbox_set[2][2]);
                ListClients.work_collection[2].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_3.SelectedIndex > -1)
            {
                if (ListClients.work_collection[3] != null)
                {
                    if (ListClients.work_collection[3].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[3].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[3].StateThread = StateEnum.stop;
                    ListClients.work_collection[3].Rule = 0;
                }

                ListClients.exList[3] = ((My_Windows)cmbx_otkr_3.SelectedItem).Name;
                ListClients.work_collection[3] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[3].ChatRead = new ChatReader(ListClients.work_collection[3],
                    settings.Checkbox_set[3][0], settings.Checkbox_set[3][1], settings.Checkbox_set[3][2]);
                ListClients.work_collection[3].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_4.SelectedIndex > -1)
            {
                if (ListClients.work_collection[4] != null)
                {
                    if (ListClients.work_collection[4].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[4].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[4].StateThread = StateEnum.stop;
                    ListClients.work_collection[4].Rule = 0;
                }

                ListClients.exList[4] = ((My_Windows)cmbx_otkr_4.SelectedItem).Name;
                ListClients.work_collection[4] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[4].ChatRead = new ChatReader(ListClients.work_collection[4],
                    settings.Checkbox_set[4][0], settings.Checkbox_set[4][1], settings.Checkbox_set[4][2]);
                ListClients.work_collection[4].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_5.SelectedIndex > -1)
            {
                if (ListClients.work_collection[5] != null)
                {
                    if (ListClients.work_collection[5].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[5].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[5].StateThread = StateEnum.stop;
                    ListClients.work_collection[5].Rule = 0;
                }

                ListClients.exList[5] = ((My_Windows)cmbx_otkr_5.SelectedItem).Name;
                ListClients.work_collection[5] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[5].ChatRead = new ChatReader(ListClients.work_collection[5],
                    settings.Checkbox_set[5][0], settings.Checkbox_set[5][1], settings.Checkbox_set[5][2]);
                ListClients.work_collection[5].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_6.SelectedIndex > -1)
            {
                if (ListClients.work_collection[6] != null)
                {
                    if (ListClients.work_collection[6].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[6].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[6].StateThread = StateEnum.stop;
                    ListClients.work_collection[6].Rule = 0;
                }

                ListClients.exList[6] = ((My_Windows)cmbx_otkr_6.SelectedItem).Name;
                ListClients.work_collection[6] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[6].ChatRead = new ChatReader(ListClients.work_collection[6],
                    settings.Checkbox_set[6][0], settings.Checkbox_set[6][1], settings.Checkbox_set[6][2]);
                ListClients.work_collection[6].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_7.SelectedIndex > -1)
            {
                if (ListClients.work_collection[7] != null)
                {
                    if (ListClients.work_collection[7].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[7].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[7].StateThread = StateEnum.stop;
                    ListClients.work_collection[7].Rule = 0;
                }

                ListClients.exList[7] = ((My_Windows)cmbx_otkr_7.SelectedItem).Name;
                ListClients.work_collection[7] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[7].ChatRead = new ChatReader(ListClients.work_collection[7],
                    settings.Checkbox_set[7][0], settings.Checkbox_set[7][1], settings.Checkbox_set[7][2]);
                ListClients.work_collection[7].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_8.SelectedIndex > -1)
            {
                if (ListClients.work_collection[8] != null)
                {
                    if (ListClients.work_collection[8].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[8].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[8].StateThread = StateEnum.stop;
                    ListClients.work_collection[8].Rule = 0;
                }

                ListClients.exList[8] = ((My_Windows)cmbx_otkr_8.SelectedItem).Name;
                ListClients.work_collection[8] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[8].ChatRead = new ChatReader(ListClients.work_collection[8],
                    settings.Checkbox_set[8][0], settings.Checkbox_set[8][1], settings.Checkbox_set[8][2]);
                ListClients.work_collection[8].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_otkr_9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_otkr_9.SelectedIndex > -1)
            {
                if (ListClients.work_collection[9] != null)
                {
                    if (ListClients.work_collection[9].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[9].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[9].StateThread = StateEnum.stop;
                    ListClients.work_collection[9].Rule = 0;
                }

                ListClients.exList[9] = ((My_Windows)cmbx_otkr_9.SelectedItem).Name;
                ListClients.work_collection[9] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[9].ChatRead = new ChatReader(ListClients.work_collection[9],
                    settings.Checkbox_set[9][0], settings.Checkbox_set[9][1], settings.Checkbox_set[9][2]);
                ListClients.work_collection[9].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void cmbx_sham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbx_sham.SelectedIndex > -1)
            {
                if (ListClients.work_collection[10] != null)
                {
                    if (ListClients.work_collection[10].BackgroundWorker5.IsBusy)
                        ListClients.work_collection[10].BackgroundWorker5.CancelAsync();
                    ListClients.work_collection[10].StateThread = StateEnum.stop;
                    ListClients.work_collection[10].Rule = 0;
                }

                ListClients.exList[10] = ((My_Windows)cmbx_sham.SelectedItem).Name;
                ListClients.work_collection[10] = (My_Windows)(((ComboBox)sender).SelectedItem);
                ListClients.work_collection[10].Rule = 1;
                ListClients.work_collection[10].ChatRead = new ChatReader(ListClients.work_collection[10],
                    settings.Checkbox_set[10][0], settings.Checkbox_set[10][1], settings.Checkbox_set[10][2]);
                ListClients.work_collection[10].Log_event += Logging;
            }
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
        }

        private void Refresh_Cmbx()
        {
            if (cmbx_pl.ItemsSource is ICollectionView)
                (cmbx_pl.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_1.ItemsSource is ICollectionView)
                (cmbx_otkr_1.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_2.ItemsSource is ICollectionView)
                (cmbx_otkr_2.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_3.ItemsSource is ICollectionView)
                (cmbx_otkr_3.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_4.ItemsSource is ICollectionView)
                (cmbx_otkr_4.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_5.ItemsSource is ICollectionView)
                (cmbx_otkr_5.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_6.ItemsSource is ICollectionView)
                (cmbx_otkr_6.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_7.ItemsSource is ICollectionView)
                (cmbx_otkr_7.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_8.ItemsSource is ICollectionView)
                (cmbx_otkr_8.ItemsSource as ICollectionView).Refresh();
            if (cmbx_otkr_9.ItemsSource is ICollectionView)
                (cmbx_otkr_9.ItemsSource as ICollectionView).Refresh();
            if (cmbx_sham.ItemsSource is ICollectionView)
                (cmbx_sham.ItemsSource as ICollectionView).Refresh();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }

        private void start_stop_btn_Click(object sender, RoutedEventArgs e)
        {
            switch (start_stop_btn.Content.ToString())
            {
                case "Старт":
                    if (ListClients.work_collection[10] != null && ListClients.work_collection[0] != null)
                        foreach (My_Windows mw in ListClients.work_collection)
                        {
                            if (mw != null )
                            {
                                mw.StateThread = StateEnum.run;
                                if (!mw.BackgroundWorker5.IsBusy)
                                    mw.BackgroundWorker5.RunWorkerAsync();
                                start_stop_btn.Content = "Стоп";
                            }
                        }
                    break;
                case "Стоп":
                    for (int i = 0; i < ListClients.work_collection.Count(); i++)
                    {
                        if (ListClients.work_collection[i] != null)
                        {
                            ListClients.work_collection[i].StateThread = StateEnum.stop;
                            if (ListClients.work_collection[i].BackgroundWorker5.IsBusy)
                                ListClients.work_collection[i].BackgroundWorker5.CancelAsync();
                        }
                    }
                    start_stop_btn.Content = "Старт";
                    break;
                default:
                    break;
            }
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            Serializable();
        }

        private void ApplySettings()
        {
            if (settings.Checkbox_set != null)
            {
                change_pl_chk_box.IsChecked = settings.ChangePl;

                if (settings.Checkbox_set.Count() >0)
                {
                    #region Включение чексбоксов "следовать" из настроек
                    ckbx_1_1.IsChecked = settings.Checkbox_set[1][0];
                    ckbx_2_1.IsChecked = settings.Checkbox_set[2][0];
                    ckbx_3_1.IsChecked = settings.Checkbox_set[3][0];
                    ckbx_4_1.IsChecked = settings.Checkbox_set[4][0];
                    ckbx_5_1.IsChecked = settings.Checkbox_set[5][0];
                    ckbx_6_1.IsChecked = settings.Checkbox_set[6][0];
                    ckbx_7_1.IsChecked = settings.Checkbox_set[7][0];
                    ckbx_8_1.IsChecked = settings.Checkbox_set[8][0];
                    ckbx_9_1.IsChecked = settings.Checkbox_set[9][0];
                    ckbx_10_1.IsChecked = settings.Checkbox_set[10][0];
                    #endregion

                    #region Включение чексбоксов "бафать" из настроек
                    ckbx_1_2.IsChecked = settings.Checkbox_set[1][1];
                    ckbx_2_2.IsChecked = settings.Checkbox_set[2][1];
                    ckbx_3_2.IsChecked = settings.Checkbox_set[3][1];
                    ckbx_4_2.IsChecked = settings.Checkbox_set[4][1];
                    ckbx_5_2.IsChecked = settings.Checkbox_set[5][1];
                    ckbx_6_2.IsChecked = settings.Checkbox_set[6][1];
                    ckbx_7_2.IsChecked = settings.Checkbox_set[7][1];
                    ckbx_8_2.IsChecked = settings.Checkbox_set[8][1];
                    ckbx_9_2.IsChecked = settings.Checkbox_set[9][1];
                    ckbx_10_2.IsChecked = settings.Checkbox_set[10][1];
                    #endregion

                    #region Включение чексбоксов "дебафать" из настроек
                    ckbx_1_3.IsChecked = settings.Checkbox_set[1][2];
                    ckbx_2_3.IsChecked = settings.Checkbox_set[2][2];
                    ckbx_3_3.IsChecked = settings.Checkbox_set[3][2];
                    ckbx_4_3.IsChecked = settings.Checkbox_set[4][2];
                    ckbx_5_3.IsChecked = settings.Checkbox_set[5][2];
                    ckbx_6_3.IsChecked = settings.Checkbox_set[6][2];
                    ckbx_7_3.IsChecked = settings.Checkbox_set[7][2];
                    ckbx_8_3.IsChecked = settings.Checkbox_set[8][2];
                    ckbx_9_3.IsChecked = settings.Checkbox_set[9][2];
                    ckbx_10_3.IsChecked = settings.Checkbox_set[10][2];
                    #endregion
                }
                else
                {
                    settings.ChangePl = new bool();
                    settings.Checkbox_set = new List<List<bool>>
                    {
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()},
                        new List<bool>{new bool(), new bool(), new bool()}
                    };
                }

            }
            #region Заполнение полей с коммандами из настроек
            peresbor_v_nirku.Text = settings.Peresbor_v_nirku;
            party_and_pl.Text = settings.Party_and_pl;
            rebaf.Text = settings.Rebaf;
            peresbor.Text = settings.Peresbor;
            to_him.Text = settings.To_him;
            pechat.Text = settings.Pechat;
            stop.Text = settings.Stop;
            #endregion
            #region Заполнение настроек для входа в игру
            Loging.Downloader = settings.Downloader;
            Loging.UserId_1 = (settings.UserId_1 != String.Empty && settings.UserId_1.Length > 18) ? settings.UserId_1 : CalcMethods.RandomStringValue(20);
            settings.UserId_1 = Loging.UserId_1;
            Loging.UserId_2 = (settings.UserId_2 != String.Empty && settings.UserId_2.Length > 18) ? settings.UserId_2 : CalcMethods.RandomStringValue(20);
            settings.UserId_2 = Loging.UserId_2;
            Serializable();
            #endregion

            OpenOffsets();
        }

        private void Deserializable()
        {
            FileStream fs = new FileStream("settings.xml", FileMode.OpenOrCreate);
            FileStream fs_2 = new FileStream("offsets.xml", FileMode.OpenOrCreate);
            FileStream fs_3 = new FileStream("password.xml", FileMode.OpenOrCreate);
            try
            {
                settings = (MySettings)formatter.Deserialize(fs);
                ofset = (Offset)formatter_for_offsets.Deserialize(fs_2);
            }
            catch
            {
                MessageBox.Show("Пока не сохранено настроек");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (fs_2 != null)
                    fs_2.Close();
            }
            try
            {
                Collection.accounts = (ObservableCollection<Account>)formatter_for_pass.Deserialize(fs_3);
               //var temp_temp = formatter.Deserialize(fs_3);
            }
            catch
            {
                MessageBox.Show("Пока не сохранено ни одного пароля.");
            }
            finally
            {
                if (fs_3 != null)
                    fs_3.Close();
            }
        }

        private void Serializable()
        {
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\settings.xml", FileMode.Truncate))
            {
                formatter.Serialize(fs, settings);
                fs.Close();
            }
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\offsets.xml", FileMode.OpenOrCreate))
            {
                formatter_for_offsets.Serialize(fs, ofset);
                fs.Close();
            }
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\password.xml", FileMode.Truncate))
            {
                formatter_for_pass.Serialize(fs, Collection.accounts);
                fs.Close();
            }
        }

        #region Чекбоксы

        #region Менять ПЛ
        private void change_pl_chk_box_Click(object sender, RoutedEventArgs e)
        {
            settings.ChangePl = (bool)change_pl_chk_box.IsChecked;
        }
        #endregion

        #region Следовать
        private void ckbx_1_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[1][0]= (bool)ckbx_1_1.IsChecked;
            if (ListClients.work_collection[1] != null)
                ListClients.work_collection[1].ChatRead.S = (bool)ckbx_1_1.IsChecked;
        }

        private void ckbx_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[2][0] = (bool)ckbx_2_1.IsChecked;
            if (ListClients.work_collection[2] != null)
                ListClients.work_collection[2].ChatRead.S = (bool)ckbx_2_1.IsChecked;
        }

        private void ckbx_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[3][0] = (bool)ckbx_3_1.IsChecked;
            if (ListClients.work_collection[3] != null)
                ListClients.work_collection[3].ChatRead.S = (bool)ckbx_3_1.IsChecked;
        }

        private void ckbx_4_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[4][0] = (bool)ckbx_4_1.IsChecked;
            if (ListClients.work_collection[4] != null)
                ListClients.work_collection[4].ChatRead.S = (bool)ckbx_4_1.IsChecked;
        }

        private void ckbx_5_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[5][0] = (bool)ckbx_5_1.IsChecked;
            if (ListClients.work_collection[5] != null)
                ListClients.work_collection[5].ChatRead.S = (bool)ckbx_5_1.IsChecked;
        }

        private void ckbx_6_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[6][0] = (bool)ckbx_6_1.IsChecked;
            if (ListClients.work_collection[6] != null)
                ListClients.work_collection[6].ChatRead.S = (bool)ckbx_6_1.IsChecked;
        }

        private void ckbx_7_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[7][0] = (bool)ckbx_7_1.IsChecked;
            if (ListClients.work_collection[7] != null)
                ListClients.work_collection[7].ChatRead.S = (bool)ckbx_7_1.IsChecked;
        }

        private void ckbx_8_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[8][0] = (bool)ckbx_8_1.IsChecked;
            if (ListClients.work_collection[8] != null)
                ListClients.work_collection[8].ChatRead.S = (bool)ckbx_8_1.IsChecked;
        }

        private void ckbx_9_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[9][0] = (bool)ckbx_9_1.IsChecked;
            if (ListClients.work_collection[9] != null)
                ListClients.work_collection[9].ChatRead.S = (bool)ckbx_9_1.IsChecked;
        }

        private void ckbx_10_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[10][0] = (bool)ckbx_10_1.IsChecked;
            if (ListClients.work_collection[10] != null)
                ListClients.work_collection[10].ChatRead.S = (bool)ckbx_10_1.IsChecked;
        }
        #endregion

        #region Бафать

        private void ckbx_1_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[1][1] = (bool)ckbx_1_2.IsChecked;
            if (ListClients.work_collection[1] != null)
                ListClients.work_collection[1].ChatRead.B = (bool)ckbx_1_2.IsChecked;
        }

        private void ckbx_2_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[2][1] = (bool)ckbx_2_2.IsChecked;
            if (ListClients.work_collection[2] != null)
                ListClients.work_collection[2].ChatRead.B = (bool)ckbx_2_2.IsChecked;
        }

        private void ckbx_3_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[3][1] = (bool)ckbx_3_2.IsChecked;
            if (ListClients.work_collection[3] != null)
                ListClients.work_collection[3].ChatRead.B = (bool)ckbx_3_2.IsChecked;
        }

        private void ckbx_4_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[4][1] = (bool)ckbx_4_2.IsChecked;
            if (ListClients.work_collection[4] != null)
                ListClients.work_collection[4].ChatRead.B = (bool)ckbx_4_2.IsChecked;
        }

        private void ckbx_5_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[5][1] = (bool)ckbx_5_2.IsChecked;
            if (ListClients.work_collection[5] != null)
                ListClients.work_collection[5].ChatRead.B = (bool)ckbx_5_2.IsChecked;
        }

        private void ckbx_6_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[6][1] = (bool)ckbx_6_2.IsChecked;
            if (ListClients.work_collection[6] != null)
                ListClients.work_collection[6].ChatRead.B = (bool)ckbx_6_2.IsChecked;
        }

        private void ckbx_7_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[7][1] = (bool)ckbx_7_2.IsChecked;
            if (ListClients.work_collection[7] != null)
                ListClients.work_collection[7].ChatRead.B = (bool)ckbx_7_2.IsChecked;
        }

        private void ckbx_8_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[8][1] = (bool)ckbx_8_2.IsChecked;
            if (ListClients.work_collection[8] != null)
                ListClients.work_collection[8].ChatRead.B = (bool)ckbx_8_2.IsChecked;
        }

        private void ckbx_9_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[9][1] = (bool)ckbx_9_2.IsChecked;
            if (ListClients.work_collection[9] != null)
                ListClients.work_collection[9].ChatRead.B = (bool)ckbx_9_2.IsChecked;
        }

        private void ckbx_10_2_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[10][1] = (bool)ckbx_10_2.IsChecked;
            if (ListClients.work_collection[10] != null)
                ListClients.work_collection[10].ChatRead.B = (bool)ckbx_10_2.IsChecked;
        }

        #endregion

        #region Дебафать

        private void ckbx_1_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[1][2] = (bool)ckbx_1_3.IsChecked;
            if (ListClients.work_collection[1] != null)
                ListClients.work_collection[1].ChatRead.R = (bool)ckbx_1_3.IsChecked;
        }

        private void ckbx_2_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[2][2] = (bool)ckbx_2_3.IsChecked;
            if (ListClients.work_collection[2] != null)
                ListClients.work_collection[2].ChatRead.R = (bool)ckbx_2_3.IsChecked;
        }

        private void ckbx_3_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[3][2] = (bool)ckbx_3_3.IsChecked;
            if (ListClients.work_collection[3] != null)
                ListClients.work_collection[3].ChatRead.R = (bool)ckbx_3_3.IsChecked;
        }

        private void ckbx_4_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[4][2] = (bool)ckbx_4_3.IsChecked;
            if (ListClients.work_collection[4] != null)
                ListClients.work_collection[4].ChatRead.R = (bool)ckbx_4_3.IsChecked;
        }

        private void ckbx_5_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[5][2] = (bool)ckbx_5_3.IsChecked;
            if (ListClients.work_collection[5] != null)
                ListClients.work_collection[5].ChatRead.R = (bool)ckbx_5_3.IsChecked;
        }

        private void ckbx_6_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[6][2] = (bool)ckbx_6_3.IsChecked;
            if (ListClients.work_collection[6] != null)
                ListClients.work_collection[6].ChatRead.R = (bool)ckbx_6_3.IsChecked;
        }

        private void ckbx_7_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[7][2] = (bool)ckbx_7_3.IsChecked;
            if (ListClients.work_collection[7] != null)
                ListClients.work_collection[7].ChatRead.R = (bool)ckbx_7_3.IsChecked;
        }

        private void ckbx_8_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[8][2] = (bool)ckbx_8_3.IsChecked;
            if (ListClients.work_collection[8] != null)
                ListClients.work_collection[8].ChatRead.R = (bool)ckbx_8_3.IsChecked;
        }

        private void ckbx_9_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[9][2] = (bool)ckbx_9_3.IsChecked;
            if (ListClients.work_collection[9] != null)
                ListClients.work_collection[9].ChatRead.R = (bool)ckbx_9_3.IsChecked;
        }

        private void ckbx_10_3_Click(object sender, RoutedEventArgs e)
        {
            settings.Checkbox_set[10][2] = (bool)ckbx_10_3.IsChecked;
            if (ListClients.work_collection[10] != null)
                ListClients.work_collection[10].ChatRead.R = (bool)ckbx_10_3.IsChecked;
        }

        #endregion

        #endregion

        #region Методы текстбоксов, вызывающиеся изменением в них текста

        private void peresbor_v_nirku_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings !=null)
                settings.Peresbor_v_nirku = peresbor_v_nirku.Text;
        }

        private void party_and_pl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.Party_and_pl = party_and_pl.Text;
        }

        private void rebaf_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.Rebaf = rebaf.Text;
        }

        private void to_pl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.Peresbor = peresbor.Text;
        }

        private void pechat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.Pechat = pechat.Text;
        }

        private void to_him_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.To_him = to_him.Text;
        }

        private void stop_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (settings != null)
                settings.Stop = stop.Text;
        }

        #endregion

        private void test_btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Height>390)
                this.Height = 390;
            else
                this.Height = 590;     
        } 

        private void OpenOffsets()
        {
            Offsets.BaseAdress = ofset.baseAdress;
            Offsets.GameAdress = ofset.gameAdress;
            Offsets.GuiAdress = ofset.guiAdress;
            Offsets.SendPacket = ofset.sendPacket;
            Offsets.AutoAttack = ofset.autoAttack;
            Offsets.UseSkill = ofset.useSkill;
            Offsets.Action_1 = ofset.action_1;
            Offsets.Action_2 = ofset.action_2;
            Offsets.Action_3 = ofset.action_3;
            Offsets.InviteCount = ofset.inviteCount;
            Offsets.InviteStruct = ofset.inviteStruct;
            Offsets.СhatStart = ofset.сhatStart;
            Offsets.СhatNumber = ofset.сhatNumber;
            Offsets.InviteWidParty = ofset.inviteWidParty;
            Offsets.InviteWidPlayer = ofset.inviteWidPlayer;
            Offsets.OffsetToGameAdress = ofset.offsetToGameAdress;
            Offsets.OffsetToPersStruct = ofset.offsetToPersStruct;
            Offsets.OffsetToParty = ofset.offsetToParty;
            Offsets.OffsetToCountParty = ofset.offsetToCountParty;
            Offsets.OffsetToName = ofset.offsetToName;
            Offsets.OffsetToClassID = ofset.offsetToClassID;
            Offsets.OffsetToMiningState = ofset.offsetToMiningState;
            Offsets.OffsetToWidWin_QuickAction = ofset.offsetToWidWin_QuickAction;
            Offsets.OffsetToX = ofset.offsetToX;
            Offsets.OffsetToY = ofset.offsetToY;
            Offsets.OffsetToZ = ofset.offsetToZ;
            Offsets.OffsetToWalkMode = ofset.offsetToWalkMode;
            Offsets.OffsetToWid = ofset.offsetToWid;
            Offsets.OffsetToTargetWid = ofset.offsetToTargetWid;
            Offsets.OffsetToStructParty = ofset.offsetToStructParty;
            Offsets.OffsetToSkillsCount = ofset.offsetToSkillsCount;
            Offsets.OffsetToCdSkill = ofset.offsetToCdSkill;
            Offsets.OffsetToIdSkill = ofset.offsetToIdSkill;
            Offsets.OffsetToSkillsArray = ofset.offsetToSkillsArray;
            Offsets.OffsetsLocationName = ofset.offsetsLocationName;
            Offsets.OffsetToCurrentSkill = ofset.offsetToCurrentSkill;
            Offsets.OffsetToCountBufs = ofset.offsetToCountBufs;
            Offsets.OffsetToBufsArray = ofset.offsetToBufsArray;
            Offsets.OffsetToBeginMobsStruct = ofset.offsetToBeginMobsStruct;
            Offsets.OffsetToMobsCount = ofset.offsetToMobsCount;
            Offsets.OffsetToMobWid = ofset.offsetToMobWid;
            Offsets.OffsetToMobName = ofset.offsetToMobName;
            Offsets.OffsetToMobsStruct = ofset.offsetToMobsStruct;
            Offsets.OffsetToHashTables = ofset.offsetToHashTables;
            Offsets.OffsetToPlayersCount = ofset.offsetToPlayersCount;
            Offsets.OffsetToBeginPlayersStruct = ofset.offsetToBeginPlayersStruct;
            Offsets.OffsetToPlayersStruct = ofset.offsetToPlayersStruct;
            Offsets.MsgId = ofset.msgId;
            Offsets.MsgType = ofset.msgType;
            Offsets.Msg_form1 = ofset.msg_form1;
            Offsets.Msg_form2 = ofset.msg_form2;
            Offsets.MsgWid = ofset.msgWid;
            Offsets.Invent_struct = ofset.invent_struct;
            Offsets.Invent_struct_2 = ofset.invent_struct_2;
            Offsets.CellsCount = ofset.cellsCount;
            Offsets.ItemInCellCount = ofset.itemInCellCount;
            Offsets.ItemInCellID = ofset.itemInCellID;
            Offsets.ItemInCellName = ofset.itemInCellName;
            Offsets.ItemInCellPrice = ofset.itemInCellPrice;
            Offsets.ItemInCellType = ofset.itemInCellType;
            //определяем цепочки смещений
            Offsets.RefreshOffsets();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

            WindowState = WindowState.Normal;
            this.Hide();
        }

        private void hide_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void loging_btn_Click(object sender, RoutedEventArgs e)
        {
            if (grid_loging.Visibility == Visibility.Hidden)
            {
                for (Int32 i = 0; i < 24; i++)
                {
                    this.Width = this.Width + 10;
                    if (this.Left > 0)
                        this.Left = this.Left - 10;
                }
                grid_loging.Visibility = Visibility.Visible;
            }
            else
            {
                for (Int32 i = 0; i < 24; i++)
                {
                    this.Width = this.Width - 10;
                    this.Left = this.Left + 10;
                }
                grid_loging.Visibility = Visibility.Hidden;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (listAccount.SelectedItem != null)
            {
                Collection.accounts.Remove((Account)(listAccount.SelectedItem));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (addAccounWindow == null)
            {
                addAccounWindow = new AddAccount();
                addAccounWindow.Owner = this;
            }
            addAccounWindow.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (loginSetWondow == null)
            {
                loginSetWondow = new LoginSettings();
                loginSetWondow.Owner = this;
            }
            loginSetWondow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (listAccount.SelectedItem != null)
            {
               Loging.AuthAsync((Account)(listAccount.SelectedItem), this);
            }
        }

        public void ShowQuestions()
        {
            if (windowGetSms == null)
            {
                windowGetSms = new GetSmsCode();
                windowGetSms.Owner = this;
            }
            windowGetSms.ShowDialog();
        }
    }

    
}
