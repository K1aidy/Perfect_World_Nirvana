using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Nirvana.Models;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Hardcodet.Wpf.TaskbarNotification;
using System.IO;
using System.Xml.Serialization;
using Nirvana.Models.TaskBar;
using Nirvana.Models.Login;
using Nirvana.Views;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Threading;
using Nirvana.Models.BotModels;

namespace Nirvana.ViewModels
{
    public delegate void Log(params FormatText[] textLog);

    public class HeadViewModel : INotifyPropertyChanged
    {
        //коллекция комбобоксов
        ObservableCollection<ComboBox> comboBoxes;
        //переменная для синхронизации логирования
        Object thread_object;
        //диспетчер
        public Dispatcher dispatcher { get; private set; }

        //объявляем объект иконки и объект класса всплывающих сообщений
        public TaskbarIcon Tb { get; private set; }
        MyBalloon mb;

        //объявляем переменную для хранения офсетов из бд
        Offset offsetsFromDb;

        //объявляем переменные для контекстов БД
        AccountContext dbAccounts;
        SettingContext dbSettings;
        OffsetContext dbOffsets;

        //объявляем команды
        RelayCommand deleteAccountCommand;
        RelayCommand addAccountCommand;
        RelayCommand loginSettingsOpneCommand;
        RelayCommand playCommand;
        RelayCommand openLoginWindowCommand;
        RelayCommand taskBarCommand;
        RelayCommand openOffsetWindowCommand;
        RelayCommand saveSettingsCommand;
        RelayCommand startAndStopButtonCommand;
        RelayCommand dragMoveCommand;
        RelayCommand refreshClientCollectionCommand;
        RelayCommand headComboboxSelectionChangedCommand;
        RelayCommand loadedCommand;
        RelayCommand changeFlagSCommand;
        RelayCommand changeFlagBCommand;
        RelayCommand changeFlagDCommand;

        //объявляем переменную для хранения настроек окна
        public static MySettings CheckBoxSettings { get; set; }
        //объявляем сериализатор
        XmlSerializer formatter;

        /// <summary>
        /// Конструктор, принимает объект иконки трея
        /// </summary>
        /// <param name="tb"></param>
        public HeadViewModel(TaskbarIcon tb)
        {
            try
            {
                dispatcher = Dispatcher.CurrentDispatcher;
                thread_object = new Object();
                //инициализируем объект иконки и объект класса всплывающих сообщений
                this.Tb = tb;
                mb = new MyBalloon(tb.GetPopupTrayPosition().X, tb.GetPopupTrayPosition().Y);
                mb.baloon_panel.ItemsSource = FormatText.baloon_msg;
                mb.Show();
                //инициализируем переменную для хранения настроек окна
                CheckBoxSettings = new MySettings();
                //инициализируем сериализатор
                formatter = new XmlSerializer(typeof(MySettings));
                //загружаем данные из xml при открытии приложения
                Deserializable();
                //выгружаем из бд список аккаунтов
                dbAccounts = new AccountContext();
                dbAccounts.Accounts.Load();
                Accounts = dbAccounts.Accounts.Local.ToBindingList();
                //выгружаем из бд офсеты
                dbOffsets = new OffsetContext();
                dbOffsets.Offsets.Load();
                offsetsFromDb = dbOffsets.Offsets.FirstOrDefault((p) => p.Version == "1.5.5_2591");
                if (offsetsFromDb != null)
                    OpenOffsets();
                //выгружаем настройки из бд, в будующем планируется выполнять этот шаг через вебсервис 
                dbSettings = new SettingContext();
                dbSettings.Settings.Load();
                //генерируем уникльный ключ компьютера
                String serial = CalcMethods.GenerateSerialNumber();
                //проверяем, есть ли в бд настройки, привязанные к этому ключу
                settings = dbSettings.Settings.FirstOrDefault((p) => p.Serialnumber == serial);
                //если настройки отсутствуют, то создаем новые и заносим в бд
                if (settings == null)
                {
                    settings = new Models.Login.Setting
                    {
                        Downloader = "Downloader/12650 MailRuGameCenter/1265",
                        Serialnumber = CalcMethods.GenerateSerialNumber(),
                        UserId_1 = CalcMethods.RandomStringValue(20),
                        UserId_2 = CalcMethods.RandomStringValue(20),
                        Filepath = String.Empty
                    };
                    dbSettings.Settings.Add(settings);
                    dbSettings.SaveChanges();
                }
                ApplySettings();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Объект для хранения настроек авторизации
        /// </summary>
        Setting settings;
        public Setting Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                OnPropertyChanged("Settings");
            }
        }

        /// <summary>
        /// Коллекция для хранения аккаунтов
        /// </summary>
        IEnumerable<Account> accounts;
        public IEnumerable<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        //команда выполняемая при загрузке главного окна
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ??
                    (loadedCommand = new RelayCommand(
                        (o) => {
                            try
                            {
                                MainWindow wnd = o as MainWindow;
                                if (wnd == null) return;
                                //заполняем коллекцию комбобоксов для дальнейшей работы
                                if (comboBoxes == null)
                                    comboBoxes = new ObservableCollection<ComboBox> { wnd.cmbx_pl, wnd.cmbx_otkr_1, wnd.cmbx_otkr_2, wnd.cmbx_otkr_3, wnd.cmbx_otkr_4, wnd.cmbx_otkr_5,
                                                                                wnd.cmbx_otkr_6, wnd.cmbx_otkr_7, wnd.cmbx_otkr_8, wnd.cmbx_otkr_9, wnd.cmbx_sham};
                                //задаем фильтр для каждого комбобокса
                                foreach (ComboBox box in comboBoxes)
                                {
                                    Int32 index = comboBoxes.IndexOf(box);
                                    ICollectionView view = CollectionViewSource.GetDefaultView(ListClients.my_windows_clients[index]);

                                    view.Filter = str => !ListClients.exList.Where(p => (ListClients.exList.IndexOf(p) != index)).Contains((str as My_Windows).Name);

                                    box.ItemsSource = view;
                                    //запускаем таймер для чтения цепочки сообщений для всплывающих окон
                                    if (!FormatText.Timer_1_State())
                                        FormatText.Start();
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                            
                        }));
            }
        }
        //открыть окно для авторизации
        public RelayCommand OpenLoginWindowCommand
        {
            get
            {
                return openLoginWindowCommand ??
                    (openLoginWindowCommand = new RelayCommand(
                        (headWindow) =>
                        {
                            try
                            {
                                MainWindow hw = headWindow as MainWindow;
                                if (headWindow == null) return;

                                if (hw.grid_loging.Visibility == Visibility.Hidden)
                                {
                                    for (Int32 i = 0; i < 24; i++)
                                    {
                                        hw.Width = hw.Width + 10;
                                        if (hw.Left > 0)
                                            hw.Left = hw.Left - 10;
                                    }
                                    hw.grid_loging.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    for (Int32 i = 0; i < 24; i++)
                                    {
                                        hw.Width = hw.Width - 10;
                                        hw.Left = hw.Left + 10;
                                    }
                                    hw.grid_loging.Visibility = Visibility.Hidden;
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //открыть окно для настройки офсетов
        public RelayCommand OpenOffsetWindowCommand
        {
            get
            {
                return openOffsetWindowCommand ??
                    (openOffsetWindowCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                if (offsetsFromDb == null) return;

                                OffsetWindow off = new OffsetWindow(offsetsFromDb.Clone());
                                if (off.ShowDialog() == true)
                                {
                                    Offset tempOfset = new Offset();
                                    tempOfset = dbOffsets.Offsets.Find(off.TempOffsets.Id);
                                    if (tempOfset != null)
                                    {
                                        if (tempOfset.Version == off.TempOffsets.Version)
                                        {
                                            tempOfset.BA = off.TempOffsets.BA;
                                            tempOfset.GA = off.TempOffsets.GA;
                                            tempOfset.Version = off.TempOffsets.Version;
                                            tempOfset.GuiAdd = off.TempOffsets.GuiAdd;
                                            tempOfset.SendPacket = off.TempOffsets.SendPacket;
                                            tempOfset.AutoAttack = off.TempOffsets.AutoAttack;
                                            tempOfset.UseSkill = off.TempOffsets.UseSkill;
                                            tempOfset.Action_1 = off.TempOffsets.Action_1;
                                            tempOfset.Action_2 = off.TempOffsets.Action_2;
                                            tempOfset.Action_3 = off.TempOffsets.Action_3;
                                            tempOfset.InviteCount = off.TempOffsets.InviteCount;
                                            tempOfset.InviteStruct = off.TempOffsets.InviteStruct;
                                            tempOfset.ChatStart = off.TempOffsets.ChatStart;
                                            tempOfset.ChatNumber = off.TempOffsets.ChatNumber;
                                            tempOfset.InviteWidPlayer = off.TempOffsets.InviteWidPlayer;
                                            tempOfset.InviteWidParty = off.TempOffsets.InviteWidParty;
                                            tempOfset.OffsetToGameAdress = off.TempOffsets.OffsetToGameAdress;
                                            tempOfset.OffsetToPersStruct = off.TempOffsets.OffsetToPersStruct;
                                            tempOfset.OffsetToParty = off.TempOffsets.OffsetToParty;
                                            tempOfset.OffsetToCountParty = off.TempOffsets.OffsetToCountParty;
                                            tempOfset.OffsetToName = off.TempOffsets.OffsetToName;
                                            tempOfset.OffsetToClassID = off.TempOffsets.OffsetToClassID;
                                            tempOfset.OffsetToMiningState = off.TempOffsets.OffsetToMiningState;
                                            tempOfset.OffsetToWidWin_QuickAction = off.TempOffsets.OffsetToWidWin_QuickAction;
                                            tempOfset.OffsetToX = off.TempOffsets.OffsetToX;
                                            tempOfset.OffsetToY = off.TempOffsets.OffsetToY;
                                            tempOfset.OffsetToZ = off.TempOffsets.OffsetToZ;
                                            tempOfset.OffsetToWalkMode = off.TempOffsets.OffsetToWalkMode;
                                            tempOfset.OffsetToWid = off.TempOffsets.OffsetToWid;
                                            tempOfset.OffsetToTargetWid = off.TempOffsets.OffsetToTargetWid;
                                            tempOfset.OffsetToStructParty = off.TempOffsets.OffsetToStructParty;
                                            tempOfset.OffsetsLocationName_0 = off.TempOffsets.OffsetsLocationName_0;
                                            tempOfset.OffsetsLocationName_1 = off.TempOffsets.OffsetsLocationName_1;
                                            tempOfset.OffsetsLocationName_2 = off.TempOffsets.OffsetsLocationName_2;
                                            tempOfset.OffsetsLocationName_3 = off.TempOffsets.OffsetsLocationName_3;
                                            tempOfset.OffsetsLocationName_4 = off.TempOffsets.OffsetsLocationName_4;
                                            tempOfset.OffsetToSkillsCount = off.TempOffsets.OffsetToSkillsCount;
                                            tempOfset.OffsetToSkillsArray = off.TempOffsets.OffsetToSkillsArray;
                                            tempOfset.OffsetToCurrentSkill = off.TempOffsets.OffsetToCurrentSkill;
                                            tempOfset.OffsetToIdSkill = off.TempOffsets.OffsetToIdSkill;
                                            tempOfset.OffsetToCdSkill = off.TempOffsets.OffsetToCdSkill;
                                            tempOfset.OffsetToCountBufs = off.TempOffsets.OffsetToCountBufs;
                                            tempOfset.OffsetToBufsArray = off.TempOffsets.OffsetToBufsArray;
                                            tempOfset.OffsetToHashTables = off.TempOffsets.OffsetToHashTables;
                                            tempOfset.OffsetToMobsStruct = off.TempOffsets.OffsetToMobsStruct;
                                            tempOfset.OffsetToBeginMobsStruct = off.TempOffsets.OffsetToBeginMobsStruct;
                                            tempOfset.OffsetToMobsCount = off.TempOffsets.OffsetToMobsCount;
                                            tempOfset.OffsetToMobName = off.TempOffsets.OffsetToMobName;
                                            tempOfset.OffsetToMobWid = off.TempOffsets.OffsetToMobWid;
                                            tempOfset.OffsetToPlayersStruct = off.TempOffsets.OffsetToPlayersStruct;
                                            tempOfset.OffsetToBeginPlayersStruct = off.TempOffsets.OffsetToBeginPlayersStruct;
                                            tempOfset.OffsetToPlayersCount = off.TempOffsets.OffsetToPlayersCount;
                                            tempOfset.MsgId = off.TempOffsets.MsgId;
                                            tempOfset.MsgType = off.TempOffsets.MsgType;
                                            tempOfset.Msg_form1 = off.TempOffsets.Msg_form1;
                                            tempOfset.Msg_form2 = off.TempOffsets.Msg_form2;
                                            tempOfset.MsgWid = off.TempOffsets.MsgWid;
                                            tempOfset.Invent_struct = off.TempOffsets.Invent_struct;
                                            tempOfset.Invent_struct_2 = off.TempOffsets.Invent_struct_2;
                                            tempOfset.CellsCount = off.TempOffsets.CellsCount;
                                            tempOfset.ItemInCellType = off.TempOffsets.ItemInCellType;
                                            tempOfset.ItemInCellID = off.TempOffsets.ItemInCellID;
                                            tempOfset.ItemInCellCount = off.TempOffsets.ItemInCellCount;
                                            tempOfset.ItemInCellPrice = off.TempOffsets.ItemInCellPrice;
                                            tempOfset.ItemInCellName = off.TempOffsets.ItemInCellName;

                                            dbOffsets.Entry(tempOfset).State = EntityState.Modified;
                                            dbOffsets.SaveChanges();
                                        }
                                        else
                                        {
                                            dbOffsets.Offsets.Add(off.TempOffsets);
                                            dbOffsets.SaveChanges();
                                        }

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //удаление аккаунта
        public RelayCommand DeleteAccountCommand
        {
            get
            {
                return deleteAccountCommand ??
                    (deleteAccountCommand = new RelayCommand(
                        (selectedItem) =>
                        {
                            try
                            {
                                if (selectedItem == null) return;
                                Account acc = selectedItem as Account;
                                dbAccounts.Accounts.Remove(acc);
                                dbAccounts.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //добавить аккаунт
        public RelayCommand AddAccountCommand
        {
            get
            {
                return addAccountCommand ??
                    (addAccountCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                AddAccount aw = new AddAccount(new Account());
                                if (aw.ShowDialog() == true)
                                {
                                    Account acc = aw.Account;
                                    dbAccounts.Accounts.Add(acc);
                                    dbAccounts.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //открыть настройки 
        public RelayCommand LoginSettingsOpneCommand
        {
            get
            {
                return loginSettingsOpneCommand ??
                    (loginSettingsOpneCommand = new RelayCommand(
                        (selectedItem) =>
                        {
                            try
                            {
                                LoginSettings ls = new LoginSettings(settings);
                                if (ls.ShowDialog() == true)
                                {
                                    ApplySettings();
                                    Setting set = new Setting();
                                    set = dbSettings.Settings.Find(ls.setting.ID);
                                    if (set != null)
                                    {
                                        set.Downloader = ls.setting.Downloader;
                                        set.UserId_1 = ls.setting.UserId_1;
                                        set.UserId_2 = ls.setting.UserId_2;
                                        set.Serialnumber = ls.setting.Serialnumber;
                                        set.Filepath = ls.setting.Filepath;

                                        dbSettings.Entry(set).State = EntityState.Modified;
                                        dbSettings.SaveChanges();
                                        ApplySettings();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //запустить клиент playCommand
        public RelayCommand PlayCommand
        {
            get
            {
                return playCommand ??
                    (playCommand = new RelayCommand(
                        async (selectedItem) =>
                        {
                            try
                            {
                                if (selectedItem == null) return;
                                Account acc = selectedItem as Account;
                                await Loging.AuthAsync(acc, dbAccounts);
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //команда для обработки нажатия на иконку трея
        public RelayCommand TaskBarCommand
        {
            get
            {
                return taskBarCommand ??
                    (taskBarCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                if (o as MainWindow != null)
                                {
                                    if (((MainWindow)o).Visibility == Visibility.Hidden)
                                        ((MainWindow)o).Show();
                                    else
                                        ((MainWindow)o).Hide();
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }        
                        }));
            }
        }
        //команда для сохранения значения чекбоксов
        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return saveSettingsCommand ??
                    (saveSettingsCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                Serializable();
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //команда для кнопки Старт/Стоп
        public RelayCommand StartAndStopButtonCommand
        {
            get
            {
                return startAndStopButtonCommand ??
                    (startAndStopButtonCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                Button btn = o as Button;
                                if (btn == null) return;

                                switch (btn.Content.ToString())
                                {
                                    case "Старт":
                                        if (ListClients.work_collection[10] != null && ListClients.work_collection[0] != null)
                                            foreach (My_Windows mw in ListClients.work_collection)
                                            {
                                                if (mw != null)
                                                {
                                                    mw.StateThread = StateEnum.run;
                                                    if (!mw.BackgroundWorker5.IsBusy)
                                                        mw.BackgroundWorker5.RunWorkerAsync();
                                                    btn.Content = "Стоп";
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
                                        btn.Content = "Старт";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            } 
                        }));
            }
        }
        //команда для перетаскивания главного окна
        public RelayCommand DragMoveCommand
        {
            get
            {
                return dragMoveCommand ??
                    (dragMoveCommand = new RelayCommand(
                        (headWindow) =>
                        {
                            try
                            {
                                MainWindow hw = headWindow as MainWindow;
                                if (headWindow == null) return;

                                hw.DragMove();
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        //команда для обновления коллекции запущенных клиентов
        public RelayCommand RefreshClientCollectionCommand
        {
            get
            {
                return refreshClientCollectionCommand ??
                    (refreshClientCollectionCommand = new RelayCommand(
                        (o) => {
                            try
                            {
                                MainWindow wnd = o as MainWindow;
                                if (wnd == null) return;

                                ListClients.CanRefresh = false;
                                ListClients.Count_Clients();
                                for (int i = 0; i < ListClients.exList.Count(); i++)
                                {
                                    ListClients.exList[i] = null;
                                }
                                foreach (ComboBox box in comboBoxes)
                                    box.SelectedIndex = -1;

                                for (int i = 0; i < ListClients.work_collection.Count(); i++)
                                {
                                    if (ListClients.work_collection[i] != null)
                                        if (ListClients.work_collection[i].BackgroundWorker5.IsBusy)
                                        {
                                            ListClients.work_collection[i].StateThread = StateEnum.stop;
                                            ListClients.work_collection[i].BackgroundWorker5.CancelAsync();
                                        }
                                    ListClients.work_collection[i] = null;
                                }
                                wnd.start_stop_btn.Content = "Старт";

                                RefreshComboboxes(wnd);
                            }
                            catch (Exception ex) { CalcMethods.ViewException(ex.Message); }
                            finally { ListClients.CanRefresh = true; }
                            
                        }, (o) => { return ListClients.CanRefresh; }));
            }
        }
        //команды для смены флагов твинков
        public RelayCommand ChangeFlagSCommand
        {
            get
            {
                return changeFlagSCommand ??
                    (changeFlagSCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                My_Windows mw = o as My_Windows;
                                if (mw == null) return;
                                mw.ChatRead.S = !mw.ChatRead.S;
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        public RelayCommand ChangeFlagBCommand
        {
            get
            {
                return changeFlagBCommand ??
                    (changeFlagBCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                My_Windows mw = o as My_Windows;
                                if (mw == null) return;
                                mw.ChatRead.B = !mw.ChatRead.B;
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }
        public RelayCommand ChangeFlagDCommand
        {
            get
            {
                return changeFlagDCommand ??
                    (changeFlagDCommand = new RelayCommand(
                        (o) =>
                        {
                            try
                            {
                                My_Windows mw = o as My_Windows;
                                if (mw == null) return;
                                mw.ChatRead.D = !mw.ChatRead.D;
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }));
            }
        }

        private void Deserializable()
        {
            FileStream fs = new FileStream("settings.xml", FileMode.OpenOrCreate);
            try
            {
                CheckBoxSettings = (MySettings)formatter.Deserialize(fs);
            }
            catch
            {
                CalcMethods.ViewException("Пока не сохранено настроек");
                CheckBoxSettings = new MySettings();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        private void Serializable()
        {
            try
            {
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\settings.xml", FileMode.Truncate))
                {
                    formatter.Serialize(fs, CheckBoxSettings);
                }
            }
            catch (Exception ex)
            {
                CalcMethods.ViewException(ex.Message);
            }
            
        }

        //метод обновления комбобоксов
        private void RefreshComboboxes(MainWindow wnd)
        {
            try
            {
                if (wnd == null) return;

                foreach (ComboBox box in comboBoxes)
                    if (box.ItemsSource is ICollectionView)
                        (box.ItemsSource as ICollectionView).Refresh();
            }
            catch (Exception ex)
            {
                CalcMethods.ViewException(ex.Message);
            }
        }

        //применяем настройки
        private void ApplySettings()
        {
            try
            {
                Loging.Downloader = settings.Downloader;
                Loging.UserId_1 = settings.UserId_1;
                Loging.UserId_2 = settings.UserId_2;
                Loging.PathToClient = settings.Filepath;
            }
            catch (Exception ex)
            {
                CalcMethods.ViewException(ex.Message);
            }
        }

        //формат офсетов (String -> Int32)
        private void OpenOffsets()
        {
            try
            {
                Offsets.BaseAdress = Convert.ToInt32(offsetsFromDb.BA, 16);
                Offsets.GameAdress = Convert.ToInt32(offsetsFromDb.GA, 16);
                Offsets.GuiAdress = Convert.ToInt32(offsetsFromDb.GuiAdd, 16);
                Offsets.SendPacket = Convert.ToInt32(offsetsFromDb.SendPacket, 16);
                Offsets.AutoAttack = Convert.ToInt32(offsetsFromDb.AutoAttack, 16);
                Offsets.UseSkill = Convert.ToInt32(offsetsFromDb.UseSkill, 16);
                Offsets.Action_1 = Convert.ToInt32(offsetsFromDb.Action_1, 16);
                Offsets.Action_2 = Convert.ToInt32(offsetsFromDb.Action_2, 16);
                Offsets.Action_3 = Convert.ToInt32(offsetsFromDb.Action_3, 16);
                Offsets.InviteCount = Convert.ToInt32(offsetsFromDb.InviteCount, 16);
                Offsets.InviteStruct = Convert.ToInt32(offsetsFromDb.InviteStruct, 16);
                Offsets.СhatStart = Convert.ToInt32(offsetsFromDb.ChatStart, 16);
                Offsets.СhatNumber = Convert.ToInt32(offsetsFromDb.ChatNumber, 16);
                Offsets.InviteWidParty = Convert.ToInt32(offsetsFromDb.InviteWidParty, 16);
                Offsets.InviteWidPlayer = Convert.ToInt32(offsetsFromDb.InviteWidPlayer, 16);
                Offsets.OffsetToGameAdress = Convert.ToInt32(offsetsFromDb.OffsetToGameAdress, 16);
                Offsets.OffsetToPersStruct = Convert.ToInt32(offsetsFromDb.OffsetToPersStruct, 16);
                Offsets.OffsetToParty = Convert.ToInt32(offsetsFromDb.OffsetToParty, 16);
                Offsets.OffsetToCountParty = Convert.ToInt32(offsetsFromDb.OffsetToCountParty, 16);
                Offsets.OffsetToName = Convert.ToInt32(offsetsFromDb.OffsetToName, 16);
                Offsets.OffsetToClassID = Convert.ToInt32(offsetsFromDb.OffsetToClassID, 16);
                Offsets.OffsetToMiningState = Convert.ToInt32(offsetsFromDb.OffsetToMiningState, 16);
                Offsets.OffsetToWidWin_QuickAction = Convert.ToInt32(offsetsFromDb.OffsetToWidWin_QuickAction, 16);
                Offsets.OffsetToX = Convert.ToInt32(offsetsFromDb.OffsetToX, 16);
                Offsets.OffsetToY = Convert.ToInt32(offsetsFromDb.OffsetToY, 16);
                Offsets.OffsetToZ = Convert.ToInt32(offsetsFromDb.OffsetToZ, 16);
                Offsets.OffsetToWalkMode = Convert.ToInt32(offsetsFromDb.OffsetToWalkMode, 16);
                Offsets.OffsetToWid = Convert.ToInt32(offsetsFromDb.OffsetToWid, 16);
                Offsets.OffsetToTargetWid = Convert.ToInt32(offsetsFromDb.OffsetToTargetWid, 16);
                Offsets.OffsetToStructParty = Convert.ToInt32(offsetsFromDb.OffsetToStructParty, 16);
                Offsets.OffsetToSkillsCount = Convert.ToInt32(offsetsFromDb.OffsetToSkillsCount, 16);
                Offsets.OffsetToCdSkill = Convert.ToInt32(offsetsFromDb.OffsetToCdSkill, 16);
                Offsets.OffsetToIdSkill = Convert.ToInt32(offsetsFromDb.OffsetToIdSkill, 16);
                Offsets.OffsetToSkillsArray = Convert.ToInt32(offsetsFromDb.OffsetToSkillsArray, 16);
                Offsets.OffsetsLocationName = new Int32[5];
                Offsets.OffsetsLocationName[0] = Convert.ToInt32(offsetsFromDb.OffsetsLocationName_0, 16);
                Offsets.OffsetsLocationName[1] = Convert.ToInt32(offsetsFromDb.OffsetsLocationName_1, 16);
                Offsets.OffsetsLocationName[2] = Convert.ToInt32(offsetsFromDb.OffsetsLocationName_2, 16);
                Offsets.OffsetsLocationName[3] = Convert.ToInt32(offsetsFromDb.OffsetsLocationName_3, 16);
                Offsets.OffsetsLocationName[4] = Convert.ToInt32(offsetsFromDb.OffsetsLocationName_4, 16);
                Offsets.OffsetToCurrentSkill = Convert.ToInt32(offsetsFromDb.OffsetToCurrentSkill, 16);
                Offsets.OffsetToCountBufs = Convert.ToInt32(offsetsFromDb.OffsetToCountBufs, 16);
                Offsets.OffsetToBufsArray = Convert.ToInt32(offsetsFromDb.OffsetToBufsArray, 16);
                Offsets.OffsetToBeginMobsStruct = Convert.ToInt32(offsetsFromDb.OffsetToBeginMobsStruct, 16);
                Offsets.OffsetToMobsCount = Convert.ToInt32(offsetsFromDb.OffsetToMobsCount, 16);
                Offsets.OffsetToMobWid = Convert.ToInt32(offsetsFromDb.OffsetToMobWid, 16);
                Offsets.OffsetToMobName = Convert.ToInt32(offsetsFromDb.OffsetToMobName, 16);
                Offsets.OffsetToMobsStruct = Convert.ToInt32(offsetsFromDb.OffsetToMobsStruct, 16);
                Offsets.OffsetToHashTables = Convert.ToInt32(offsetsFromDb.OffsetToHashTables, 16);
                Offsets.OffsetToPlayersCount = Convert.ToInt32(offsetsFromDb.OffsetToPlayersCount, 16);
                Offsets.OffsetToBeginPlayersStruct = Convert.ToInt32(offsetsFromDb.OffsetToBeginPlayersStruct, 16);
                Offsets.OffsetToPlayersStruct = Convert.ToInt32(offsetsFromDb.OffsetToPlayersStruct, 16);
                Offsets.MsgId = Convert.ToInt32(offsetsFromDb.MsgId, 16);
                Offsets.MsgType = Convert.ToInt32(offsetsFromDb.MsgType, 16);
                Offsets.Msg_form1 = Convert.ToInt32(offsetsFromDb.Msg_form1, 16);
                Offsets.Msg_form2 = Convert.ToInt32(offsetsFromDb.Msg_form2, 16);
                Offsets.MsgWid = Convert.ToInt32(offsetsFromDb.MsgWid, 16);
                Offsets.Invent_struct = Convert.ToInt32(offsetsFromDb.Invent_struct, 16);
                Offsets.Invent_struct_2 = Convert.ToInt32(offsetsFromDb.Invent_struct_2, 16);
                Offsets.CellsCount = Convert.ToInt32(offsetsFromDb.CellsCount, 16);
                Offsets.ItemInCellCount = Convert.ToInt32(offsetsFromDb.ItemInCellCount, 16);
                Offsets.ItemInCellID = Convert.ToInt32(offsetsFromDb.ItemInCellID, 16);
                Offsets.ItemInCellName = Convert.ToInt32(offsetsFromDb.ItemInCellName, 16);
                Offsets.ItemInCellPrice = Convert.ToInt32(offsetsFromDb.ItemInCellPrice, 16);
                Offsets.ItemInCellType = Convert.ToInt32(offsetsFromDb.ItemInCellType, 16);
                //определяем цепочки смещений
                Offsets.RefreshOffsets();
            }
            catch (Exception ex)
            {
                CalcMethods.ViewException(ex.Message);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //всплывающее окно
        public void Logging(params FormatText[] text_log)
        {
            dispatcher.Invoke (DispatcherPriority.Background, new Action(() =>
                             {
                                 try
                                 {
                                     lock (thread_object)
                                     {
                                         string tray_text = "";
                                         foreach (FormatText log in text_log)
                                             tray_text = log.Text != null ? log.Text : "пустое сообщение";

                                         if (FormatText.baloon_msg.Count() < 5)
                                         {
                                             FormatText.baloon_msg.Add(new FormatText(tray_text, Brushes.Black, 13, text_log[0].Type));
                                         }
                                         else
                                         {
                                             FormatText.baloon_msg.RemoveAt(0);
                                             FormatText.baloon_msg.Add(new FormatText(tray_text, Brushes.Black, 13, text_log[0].Type));
                                         }
                                     }
                                 }
                                 catch (Exception ex)
                                 {
                                     CalcMethods.ViewException(ex.Message);
                                 }
                             }));
        }

        //Команда смены итема для первого комбобокса
        public RelayCommand HeadComboboxSelectionChangedCommand
        {
            get
            {
                return headComboboxSelectionChangedCommand ??
                    (headComboboxSelectionChangedCommand = new RelayCommand(
                        (o) => {
                            try
                            {
                                ComboBox box = o as ComboBox;
                                if (box == null) return;

                                Int32 indexBox = comboBoxes.IndexOf(box);
                                if (box.SelectedIndex > -1)
                                {
                                    if (ListClients.work_collection[indexBox] != null)
                                    {
                                        if (ListClients.work_collection[indexBox].BackgroundWorker5.IsBusy)
                                            ListClients.work_collection[indexBox].BackgroundWorker5.CancelAsync();
                                        ListClients.work_collection[indexBox].StateThread = StateEnum.stop;
                                        ListClients.work_collection[indexBox].Rule = 0;
                                    }
                                    ListClients.exList[indexBox] = ((My_Windows)box.SelectedItem).Name;
                                    ListClients.work_collection[indexBox] = (My_Windows)(box.SelectedItem);
                                    ListClients.work_collection[indexBox].Rule = (indexBox == 0) ? 2 : ((indexBox == 10) ? 1 : 0);
                                    ListClients.work_collection[indexBox].ChatRead = (indexBox == 0) ?
                                    new ChatReader(ListClients.work_collection[indexBox]) :
                                    new ChatReader(ListClients.work_collection[indexBox],
                                    CheckBoxSettings.GetValueCheckBox(1, indexBox), CheckBoxSettings.GetValueCheckBox(2, indexBox), CheckBoxSettings.GetValueCheckBox(3, indexBox));
                                    ListClients.work_collection[indexBox].Log_event += Logging;
                                }
                                foreach (ComboBox cmbx in comboBoxes.Where(p => p != box))
                                    if (cmbx.ItemsSource is ICollectionView)
                                        (cmbx.ItemsSource as ICollectionView).Refresh();
                            }
                            catch (Exception ex)
                            {
                                CalcMethods.ViewException(ex.Message);
                            }
                        }
                    ));
            }
        }

    }
}