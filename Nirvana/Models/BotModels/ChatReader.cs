using Nirvana.Models.TaskBar;
using Nirvana.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Media;

namespace Nirvana.Models.BotModels
{
    /// <summary>
    /// Класс для работы с чатом
    /// </summary>
    class ChatReader : INotifyPropertyChanged
    {
        //чекбокс следовать
        bool s;
        //чекбокс бафать
        bool b;
        //чекбокс дебафать
        bool d;
        My_Windows mw;
        IntPtr oph;
        int ChNold;
        List<Skill> My_skills_for_buf;
        List<Message> messages;

        Packets packet;
        //переменная, чтобы не считывать чат в первую итерацию
        bool temp = false;

        public bool S
        {
            get
            {
                return s;
            }

            set
            {
                s = value;
            }
        }

        public bool B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public bool D
        {
            get
            {
                return d;
            }

            set
            {
                d = value;
            }
        }

        //Конструктор для пати-лидера
        public ChatReader(My_Windows mw)
        {
            this.mw = mw;

            this.packet = mw.PacketSend;
            this.oph = mw.Oph;
            messages = new List<Message>(5);
        }

        //Конструктор для твинов
        public ChatReader(My_Windows mw, bool s, bool b, bool d)
        {
            this.mw = mw;
            this.My_skills_for_buf = mw.My_skills_for_buf;
            this.packet = mw.PacketSend;
            this.oph = mw.Oph;
            this.S = s;
            this.B = b;
            this.D = d;
            messages = new List<Message>(5);
        }

        /// <summary>
        /// Метод для чтения сообщений в чате пересборщиком
        /// </summary>
        public void ReadChat()
        {
            //узнаем начало чата
            int ChS = CalcMethods.CalcInt32Value(oph, Offsets.СhatStart);
            //узнаем количество сообщений в памяти клиента
            int ChN = CalcMethods.CalcInt32Value(oph, Offsets.СhatNumber);
            //чистим массив сообщений и заполняем его null
            messages.Clear();
            messages = new List<Message>() { null, null, null, null, null };
            //в цикле заполняем массив сообщений последними 5ю сообщениями
            for (int i = ChN-5, j = 0; i< ChN; i++, j++)
            {
                //id сообщения (каждое следующее имеет +1 к предыдущему)
                int messageID = CalcMethods.CalcInt32Value(oph,Offsets.OffsetsMsgId(ChS, i));
                //уникальный wid человека, оправившего сообщение
                int wid = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgWid(ChS, i));
                //тип сообщения: 1 - мирчат, 2 - группчат, 3 - кланчат, 4 - пм
                byte messageType = CalcMethods.CalcByteValue(oph, Offsets.OffsetsMsgType(ChS, i));
                //адрес и текст первой формы сообщения
                int message_1_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm1(ChS, i));
                string message_1 = CalcMethods.CalcStringValue(oph, message_1_address);
                //адрес и текст второй формы сообщения
                int message_2_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm2(ChS, i));
                string message_2 = CalcMethods.CalcStringValue(oph, message_2_address);
                //заполняем j-й элемент массива сообщений
                messages[j] = new Message(i, messageType, messageID, message_1, message_2, (uint)wid);
                //если сообщение новой и ранее не обрабатывалось, то проверяем его на наличие служебной команды
                if (messageID > ChNold)
                {


                    #region Пересбор в нирку
                    try
                    {
                        //реакция на команду "пересбор"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Peresbor_v_nirku) != -1
                            && messages[j].Type == 4
                            && messages[j].Wid == ListClients.work_collection[0].Wid
                            && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 14, 2));
                            //все боты выходят из пати (если они в пати)
                            SimonSayMethods.QuitFromParty();
                            //пересборщик кидает всем ботам пати
                            SimonSayMethods.InvitetoParty();
                            //все боты принимают пати
                            SimonSayMethods.AcceptInviteToParty();
                            //пересборщик отдает патилидера первому боту
                            SimonSayMethods.ChangePl();
                            //пересборщик кидает всем призыв по очереди и боты его принимают
                            //проверки на класс у пересборщика нет, если пересборщик по каким-то причинам
                            //не может кинуть призыв, то ничего не происходит
                            //все боты, кроме пересборщика заходят в нирвану
                            SimonSayMethods.CallShaman(mw.ShamansCall);
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Физнирка
                    try
                    {
                        //реакция на команду "пересбор"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Fiznirka) != -1 && messages[j].Type == 4 && messages[j].Wid == ListClients.work_collection[0].Wid && temp)
                        {
                            //mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2), new FormatText(message_1, Brushes.Black, 13, 2));
                            ////все боты выходят из пати (если они в пати)
                            //SimonSayMethods.QuitFromParty();
                            ////пересборщик кидает всем ботам пати
                            //SimonSayMethods.InvitetoParty();
                            ////все боты принимают пати
                            //SimonSayMethods.AcceptInviteToParty();
                            ////пересборщик отдает патилидера первому боту
                            //SimonSayMethods.ChangePl();
                            ////пересборщик кидает всем призыв по очереди и боты его принимают
                            ////проверки на класс у пересборщика нет, если пересборщик по каким-то причинам
                            ////не может кинуть призыв, то ничего не происходит
                            ////все боты, кроме пересборщика заходят в нирвану
                            //SimonSayMethods.CallShaman(mw.ShamansCall);
                            //mw.PacketSend.leaveParty();
                            //SimonSayMethods.Rebuf(MainWindow.settings.Rebaf);
                            ////дадим время ботам на ребаф
                            //Thread.Sleep(5000);
                            ////проверяем всех ботов на наличие самоцвета и берем квест на открытие
                            //SimonSayMethods.OpenFizNirvana();
                            //убиваем первого босса
                            KillBoss.KillBossForAll(1);
                            Thread.Sleep(100);
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Пати и пл
                    try
                    {
                        //реакция на команду "пересбор"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Party_and_pl) != -1
                            && messages[j].Type == 4
                            && temp)
                        {
                            mw.Logging(new FormatText(mw.Name + " " + message_1, Brushes.Red, 14, 2));
                            SimonSayMethods.PartyAndPl((int)messages[j].Wid);
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region За целью
                    try
                    {
                        //реакция на команду "следовать за целью"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.To_him) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && s && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл
                            if (messages[j].Wid == ListClients.work_collection[0].Wid)
                            {
                                //проверим, находится ли пл рядом
                                bool near_player_state = CalcMethods.SearchPlayerNearby(oph, (int)messages[j].Wid);
                                if (near_player_state)
                                {
                                    //возьмем пла в ассист
                                    packet.selectNpc((int)messages[j].Wid);
                                    Thread.Sleep(300);
                                    //узнаем вид в таргете
                                    int target_wid = CalcMethods.TargetPlayerWid(oph, messages[j].Wid);
                                    //узнаем наш wid
                                    int my_wid = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWid);
                                    //проверим, игрок ли это и находится ли он рядом
                                    bool near_player_target_state = CalcMethods.SearchPlayerNearby(oph, target_wid);
                                    if (near_player_state && target_wid != my_wid)
                                    {
                                        //возьмем таргет пла в ассист
                                        packet.selectNpc(target_wid);
                                        Thread.Sleep(300);
                                        //запишем в структуру окна wid пла
                                        int[] result = CalcMethods.CalcControlAddress(oph, "Win_QuickAction", "Follow", 1);
                                        CalcMethods.WriteProcessBytes(oph, target_wid, result[0] + Offsets.OffsetToWidWin_QuickAction);
                                        SimonSayMethods.FollowMe(result, oph);
                                    }
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Стоп
                    try
                    {
                        //реакция на команду "следовать за целью"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Stop) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && s && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл
                            if (messages[j].Wid == ListClients.work_collection[0].Wid)
                            {
                                SimonSayMethods.Stop(oph);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Ребаф
                    try
                    {
                        //реакция на команду "бафнуть"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Rebaf) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && b && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл или Щирое ))
                            if (messages[j].Wid == ListClients.work_collection[0].Wid ||
                                messages[j].Msg_2.IndexOf("&Щирое&") != -1)
                            {
                                SimonSayMethods.UseSkillMassive(My_skills_for_buf, oph);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }
                    
                    #endregion

                    //временная переменная, чтобы не обрабатывать 1 сообщение более 1 раза
                    ChNold = messageID;
                }         
            }
            //делаем true после первой итерации
            temp = true;
        }

        /// <summary>
        /// Метод для чтения сообщений в чате ботами
        /// </summary>
        public void ReadChat_2()
        {
            //узнаем начало чата
            int ChS = CalcMethods.CalcInt32Value(oph, Offsets.СhatStart);
            //узнаем количество сообщений в памяти клиента
            int ChN = CalcMethods.CalcInt32Value(oph, Offsets.СhatNumber);
            //чистим массив сообщений и заполняем его null
            messages.Clear();
            messages = new List<Message>() { null, null, null, null, null };
            //в цикле заполняем массив сообщений последними 5ю сообщениями
            for (int i = ChN - 5, j = 0; i < ChN; i++, j++)
            {
                //id сообщения (каждое следующее имеет +1 к предыдущему)
                int messageID = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgId(ChS, i));
                //уникальный wid человека, оправившего сообщение
                int wid = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgWid(ChS, i));
                //тип сообщения: 1 - мирчат, 2 - группчат, 3 - кланчат, 4 - пм
                byte messageType = CalcMethods.CalcByteValue(oph, Offsets.OffsetsMsgType(ChS, i));
                //адрес и текст первой формы сообщения
                int message_1_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm1(ChS, i));
                string message_1 = CalcMethods.CalcStringValue(oph, message_1_address);
                //адрес и текст второй формы сообщения
                int message_2_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm2(ChS, i));
                string message_2 = CalcMethods.CalcStringValue(oph, message_2_address);
                //заполняем j-й элемент массива сообщений
                messages[j] = new Message(i, messageType, messageID, message_1, message_2, (uint)wid);
                //если сообщение новой и ранее не обрабатывалось, то проверяем его на наличие служебной команды
                if (messageID > ChNold)
                {
                    #region За целью
                    try
                    {
                        //реакция на команду "следовать за целью"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.To_him) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && s && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл
                            if (messages[j].Wid == ListClients.work_collection[0].Wid)
                            {
                                //проверим, находится ли пл рядом
                                bool near_player_state = CalcMethods.SearchPlayerNearby(oph, (int)messages[j].Wid);
                                if (near_player_state)
                                {
                                    //возьмем пла в ассист
                                    packet.selectNpc((int)messages[j].Wid);
                                    Thread.Sleep(300);
                                    //узнаем вид в таргете
                                    int target_wid = CalcMethods.TargetPlayerWid(oph, messages[j].Wid);
                                    //узнаем наш wid
                                    int my_wid = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWid);
                                    //проверим, игрок ли это и находится ли он рядом
                                    bool near_player_target_state = CalcMethods.SearchPlayerNearby(oph, target_wid);
                                    if (near_player_state && target_wid != my_wid)
                                    {
                                        //возьмем таргет пла в ассист
                                        packet.selectNpc(target_wid);
                                        Thread.Sleep(300);
                                        //запишем в структуру окна wid пла
                                        int[] result = CalcMethods.CalcControlAddress(oph, "Win_QuickAction", "Follow", 1);
                                        CalcMethods.WriteProcessBytes(oph, target_wid, result[0] + Offsets.OffsetToWidWin_QuickAction);
                                        SimonSayMethods.FollowMe(result, oph);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Стоп
                    try
                    {
                        //реакция на команду "следовать за целью"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Stop) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && s && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл
                            if (messages[j].Wid == ListClients.work_collection[0].Wid)
                            {
                                SimonSayMethods.Stop(oph);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }

                    #endregion

                    #region Ребаф
                    try
                    {
                        //реакция на команду "бафнуть"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Rebaf) != -1
                            && (messages[j].Type == 2 || messages[j].Type == 4)
                            && b && temp)
                        {
                            mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 2),
                                        new FormatText(message_1, Brushes.Black, 13, 2));
                            //проверим, является ли  отправителем сообщения пл или Щирое ))
                            if (messages[j].Wid == ListClients.work_collection[0].Wid ||
                                messages[j].Msg_2.IndexOf("&Щирое&") != -1)
                            {
                                //проверяем, находится ли в образе персонаж и выходим из образа, если это так
                                int buf_count = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsBufsCount);
                                for (int b = 0; b < buf_count; b++)
                                {
                                    int buf_id = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsIdBuf(b));
                                    if (buf_id == 75 || buf_id == 47 || buf_id == 225)
                                    {
                                        int cd_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToCdSkill(mw.ChangeForm.Number));
                                        while (cd_skill > 0)
                                        {
                                            Thread.Sleep(cd_skill);
                                            cd_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToCdSkill(mw.ChangeForm.Number));
                                        }
                                        Injects.Skill_Inject(mw.ChangeForm.Id, oph);
                                        Thread.Sleep(1000);
                                        continue;
                                    }
                                }
                                //юзаем скиллы из списка разрешенных бафов
                                SimonSayMethods.UseSkillMassive(My_skills_for_buf, oph);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }
                    
                    #endregion

                    //временная переменная, чтобы не обрабатывать 1 сообщение более 1 раза
                    ChNold = messageID;
                }
            }
            //делаем true после первой итерации
            temp = true;
        }

        /// <summary>
        /// Метод для чтения сообщений в чате плом
        /// </summary>
        public void ReadChat_3()
        {
            //узнаем начало чата
            int ChS = CalcMethods.CalcInt32Value(oph, Offsets.СhatStart);
            //узнаем количество сообщений в памяти клиента
            int ChN = CalcMethods.CalcInt32Value(oph, Offsets.СhatNumber);
            //чистим массив сообщений и заполняем его null
            messages.Clear();
            messages = new List<Message>() { null, null, null, null, null };
            //в цикле заполняем массив сообщений последними 5ю сообщениями
            for (int i = ChN - 5, j = 0; i < ChN; i++, j++)
            {
                //id сообщения (каждое следующее имеет +1 к предыдущему)
                int messageID = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgId(ChS, i));
                //уникальный wid человека, оправившего сообщение
                int wid = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgWid(ChS, i));
                //тип сообщения: 1 - мирчат, 2 - группчат, 3 - кланчат, 4 - пм
                byte messageType = CalcMethods.CalcByteValue(oph, Offsets.OffsetsMsgType(ChS, i));
                //адрес и текст первой формы сообщения
                int message_1_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm1(ChS, i));
                string message_1 = CalcMethods.CalcStringValue(oph, message_1_address);
                //адрес и текст второй формы сообщения
                int message_2_address = CalcMethods.CalcInt32Value(oph, Offsets.OffsetsMsgForm2(ChS, i));
                string message_2 = CalcMethods.CalcStringValue(oph, message_2_address);
                //заполняем j-й элемент массива сообщений
                messages[j] = new Message(i, messageType, messageID, message_1, message_2, (uint)wid);
                //если сообщение новой и ранее не обрабатывалось, то проверяем его на наличие служебной команды
                if (messageID > ChNold)
                {
                    #region Пересбор
                    try
                    {
                        //реакция на команду "пересбор пати"
                        if (messages[j].Msg_2.IndexOf(HeadViewModel.CheckBoxSettings.Peresbor) != -1
                            && messages[j].Type == 2
                            && messages[j].Wid == 0xFFFFFFFF
                            && temp)
                        {
                            //считаем количество людей в пати и либо собираем новую, либо пересобираем старую пати
                            int partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);
                            if (partyCount > 0)
                            {
                                SimonSayMethods.Peresbor();
                            }
                            else
                            {
                                SimonSayMethods.QuitFromPartyFirstPl();
                                SimonSayMethods.InvitetoPartyFromPl();
                                SimonSayMethods.AcceptInviteToPartyFromPl();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mw.Logging(new FormatText(ex.Message, Brushes.Red, 20, 1));
                    }
                    
                    #endregion
                    //временная переменная, чтобы не обрабатывать 1 сообщение более 1 раза
                    ChNold = messageID;
                }
            }
            //делаем true после первой итерации
            temp = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
