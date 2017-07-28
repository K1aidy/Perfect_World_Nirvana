using Nirvana.Models.TaskBar;
using Nirvana.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media;

namespace Nirvana.Models.BotModels
{
    /// <summary>
    /// Данный класс описывает методы для ботов
    /// </summary>
    class SimonSayMethods
    {
        /// <summary>
        /// Выход из пати всех ботов, первым выходит пересборщик
        /// </summary>
        public static void QuitFromParty()
        {
            try
            {
                for (int i = ListClients.work_collection.Count() - 1; i > -1; i--)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        //узнаем указатель на процесс
                        IntPtr oph = ListClients.work_collection[i].Oph;

                        int partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);

                        if (partyCount > 0)
                        {
                            //если счетчик людей в пати >0, то выходим из пати
                            ListClients.work_collection[i].PacketSend.leaveParty();
                            Thread.Sleep(300);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Выход из пати всех ботов, первым выходит патилидер
        /// </summary>
        public static void QuitFromPartyFirstPl()
        {
            try
            {
                for (int i = 0; i < ListClients.work_collection.Count(); i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        //узнаем указатель на процесс
                        IntPtr oph = ListClients.work_collection[i].Oph;

                        int partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);
                        //закрываем дескриптор

                        if (partyCount > 0)
                        {
                            //если счетчик людей в пати > 0, то выходим из пати
                            ListClients.work_collection[i].PacketSend.leaveParty();
                            Thread.Sleep(300);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Отправление приглашения в пати пересборщиков всем ботам
        /// </summary>
        public static void InvitetoParty()
        {
            try
            {
                for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                        {
                            //если в соответствующем комбобоксе указан бот, то кидаем ему пати
                            ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.inviteParty((int)ListClients.work_collection[i].Wid);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Отправление приглашения в пати плом всем ботам
        /// </summary>
        public static void InvitetoPartyFromPl()
        {
            try
            {
                for (int i = 1; i < ListClients.work_collection.Count(); i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                        {
                            //если в соответствующем комбобоксе указан бот, то кидаем ему пати
                            ListClients.work_collection[0].PacketSend.inviteParty((int)ListClients.work_collection[i].Wid);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }

        /// <summary>
        /// Все боты принимают приглашения в пати
        /// </summary>
        public static void AcceptInviteToParty()
        {
            try
            {
                for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        //узнаем указатель на процесс
                        IntPtr oph = ListClients.work_collection[i].Oph;

                        int inviteCount = CalcMethods.CalcInt32Value(oph, Offsets.InviteCount);

                        if (inviteCount > 0)
                        {
                            //узнаем ID персонажа, отправившего пати
                            int[] inviteIdPlayer = { Offsets.InviteWidPlayer };
                            int playerId = CalcMethods.ReadInt(oph, Offsets.InviteStruct, inviteIdPlayer);
                            //узнаем ID пати, в которую нас приглашают
                            int[] inviteIdParty = { Offsets.InviteWidParty };
                            int partyID = CalcMethods.ReadInt(oph, Offsets.InviteStruct, inviteIdParty);
                            //принмаем пати
                            ListClients.work_collection[i].PacketSend.acceptPartyInvite(playerId, partyID);
                            Thread.Sleep(300);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Все боты принимают приглашения в пати от патилидера
        /// </summary>
        public static void AcceptInviteToPartyFromPl()
        {
            try
            {
                for (int i = 1; i < ListClients.work_collection.Count(); i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        //узнаем указатель на процесс
                        IntPtr oph = ListClients.work_collection[i].Oph;

                        int inviteCount = CalcMethods.CalcInt32Value(oph, Offsets.InviteCount);

                        if (inviteCount > 0)
                        {
                            //узнаем ID персонажа, отправившего пати
                            int[] inviteIdPlayer = { Offsets.InviteWidPlayer };
                            int playerId = CalcMethods.ReadInt(oph, Offsets.InviteStruct, inviteIdPlayer);
                            //узнаем ID пати, в которую нас приглашают
                            int[] inviteIdParty = { Offsets.InviteWidParty };
                            int partyID = CalcMethods.ReadInt(oph, Offsets.InviteStruct, inviteIdParty);
                            //принмаем пати
                            ListClients.work_collection[i].PacketSend.acceptPartyInvite(playerId, partyID);
                            Thread.Sleep(300);
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Пересбор пати плом
        /// </summary>
        public static void Peresbor()
        {
            try
            {
                //узнаем указатель на процесс
                IntPtr oph = ListClients.work_collection[0].Oph;

                int partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);

                //узнаем wid всех членов пати
                int[] party_struct = new int[partyCount - 1];
                for (int k = 1; k < partyCount; k++)
                {
                    party_struct[k - 1] = CalcMethods.ReadInt(oph, Offsets.BaseAdress,
                                                new int[] { Offsets.OffsetToGameAdress,
                                                        Offsets.OffsetToPersStruct,
                                                        Offsets.OffsetToParty,
                                                        0x14, k * 0x4, 0xC });
                }
                //проверка на пээльство хД
                int pl_wid = CalcMethods.ReadInt(oph, Offsets.BaseAdress,
                                                new int[] { Offsets.OffsetToGameAdress,
                                                        Offsets.OffsetToPersStruct,
                                                        Offsets.OffsetToParty,
                                                        0x14, 0, 0xC});
                if (ListClients.work_collection[0].Wid != pl_wid)
                {
                    bool temp = true;
                    for (int k = 1; k < ListClients.work_collection.Count(); k++)
                    {
                        if (ListClients.work_collection[k] != null)
                            if (ListClients.work_collection[k].Wid == pl_wid)
                            {
                                ListClients.work_collection[k].PacketSend.ChangePl(ListClients.work_collection[0].Wid);
                                temp = false;
                                Thread.Sleep(1000);
                                for (int l = 1; l < partyCount; l++)
                                {
                                    party_struct[l - 1] = CalcMethods.ReadInt(oph, Offsets.BaseAdress,
                                                                new int[] { Offsets.OffsetToGameAdress,
                                                        Offsets.OffsetToPersStruct,
                                                        Offsets.OffsetToParty,
                                                        0x14, l * 0x4, 0xC });
                                }
                                break;
                            }
                    }
                    if (temp) return;

                    Thread.Sleep(1500);
                }
                //кикаем всех кроме одного
                for (int k = 0; k < party_struct.Count() - 1; k++)
                {
                    ListClients.work_collection[0].PacketSend.kickFromParty(party_struct[k]);
                    Thread.Sleep(300);
                }
                //передаем пл последнему (если отмечен чекбокс в настройках)
                if (HeadViewModel.CheckBoxSettings.ChangePl)
                {
                    ListClients.work_collection[0].PacketSend.ChangePl(party_struct[party_struct.Count() - 1]);
                    Thread.Sleep(500);
                }

                //выходим из пати
                ListClients.work_collection[0].PacketSend.leaveParty();
                Thread.Sleep(1500);

                //кидаем всем пати
                for (int k = 0; k < party_struct.Count(); k++)
                {
                    ListClients.work_collection[0].PacketSend.inviteParty(party_struct[k]);
                    Thread.Sleep(300);
                }
                Thread.Sleep(1500);

                //все боты принимают пати, если они получили приглашение
                AcceptInviteToPartyFromPl();
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        /// <summary>
        /// Пересборщик передает ПЛ первому боту
        /// </summary>
        public static void ChangePl()
        {
            try
            {
                if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                    ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.ChangePl(ListClients.work_collection[0].Wid);
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Пересборщик (шаман) кидает всем ботам призывы по очереди, боты принимают призыв
        /// </summary>
        public static void CallShaman(Skill call_skill)
        {
            try
            {
                for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
                {
                    if (ListClients.work_collection[i] != null)
                    {
                        if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                        {
                            string location = CalcMethods.ReadString(ListClients.work_collection[i].Oph,
                                Offsets.BaseAdress, Offsets.OffsetsLocationName);
                            while (location.IndexOf("Телепорт в Зал Перерождения") == -1)
                            {
                                //каст призыва
                                ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.callShamanParty(ListClients.work_collection[i].Wid);
                                Thread.Sleep(3000);
                                //принятие призыва
                                ListClients.work_collection[i].PacketSend.acceptCallShamanParty(ListClients.work_collection[ListClients.work_collection.Count() - 1].Wid);
                                //ждем, пока персонаж не сменит локацию после тп
                                Thread.Sleep(10000);
                                location = CalcMethods.ReadString(ListClients.work_collection[i].Oph,
                                    Offsets.BaseAdress, Offsets.OffsetsLocationName);
                            }
                            Thread.Sleep(1000);
                            //бот берет квест на заход в нирвану (id взято отсюда http://www.pwdatabase.com/ru/quest/20790 )
                            TalkToNPC(ListClients.work_collection[i], "Страж Зала Перерождения", 20790);

                            #region старый кусок кода
                            //Int32 mob_wid = CalcMethods.MobSearch(ListClients.work_collection[i].Oph, "Страж Зала Перерождения");
                            //if (mob_wid != 0)
                            //{
                            //    //Выделяем НПСа
                            //    ListClients.work_collection[i].PacketSend.selectNpc(mob_wid);
                            //    Thread.Sleep(300);
                            //    //Открываем диалоговое окно с НПСом
                            //    ListClients.work_collection[i].PacketSend.talkToNpc(mob_wid);
                            //    Thread.Sleep(1000);
                            //    //Узнаем необходимые данные об окне, так как после взятия кв оно перестанет быть активным
                            //    int[] adress_window = CalcMethods.CalcControlAddress(ListClients.work_collection[i].Oph, "Win_NPC", "Btn_Back", 1);
                            //    Thread.Sleep(300);
                            //    //Берем кв на заход в нирвану (id взято отсюда http://www.pwdatabase.com/ru/quest/20790 )
                            //    ListClients.work_collection[i].PacketSend.takeQuest(20790);
                            //    //если адреса считались нормально, то закрываем диалоговое окно с НПСом
                            //    if (adress_window[0] != 0 && adress_window[1] != 0)
                            //        Injects.GUI_Inject(adress_window[0], adress_window[1], ListClients.work_collection[i].Oph);
                            //    else
                            //    {
                            //        ListClients.work_collection[i].Logging(new FormatText(ListClients.work_collection[i].Name, Brushes.Red, 14, 1),
                            //                                               new FormatText("не смог закрыть окно", Brushes.Red, 14, 1));
                            //    } 
                            //}
                            #endregion
                        }
                        //ждем отката призыва
                        //Thread.Sleep(3000);
                        int cd_call = CalcMethods.ReadInt(ListClients.work_collection[0].Oph,
                                                            Offsets.BaseAdress, Offsets.OffsetsToCdSkill(call_skill.Number));
                        while (cd_call > 0)
                        {
                            Thread.Sleep(1000);
                            cd_call = CalcMethods.ReadInt(ListClients.work_collection[0].Oph,
                                                            Offsets.BaseAdress, Offsets.OffsetsToCdSkill(call_skill.Number));
                        }
                    }
                }
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        /// <summary>
        /// Пл, проверяет, есть ли у ботов самоцветы и открывает физнирку
        /// </summary>
        public static void OpenFizNirvana()
        {
            Boolean temp = false;
            for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
            {
                //Thread.Sleep(200);
                //проверяем, есть ли в инвентаре необходимый предмет
                if (ListClients.work_collection[i] != null)
                    temp = ReadInvent(ListClients.work_collection[i].Oph, ListClients.work_collection[i].Handle);
            }
            Thread.Sleep(1500);
            //если у всех ботов есть самоцвет, то берем квест
            if (temp)
            {
                Say(ListClients.work_collection[0], "!!Внимание, беру квест!");
                TalkToNPC(ListClients.work_collection[0], "Загадочный старец", 20714);
            }
        }

        /// <summary>
        /// бот открывает диалог с НПС
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="npcName"></param>
        /// <param name="questId"></param>
        public static void TalkToNPC(My_Windows mw, String npcName, Int32 questId)
        {
            //находим wid НПСа по имени
            Int32 mob_wid = CalcMethods.MobSearch(mw.Oph, npcName);
            //если НПС нашелся, то берем у него квест и закрываем диалоговое окно
            if (mob_wid != 0)
            {
                //Выделяем НПСа
                mw.PacketSend.selectNpc(mob_wid);
                Thread.Sleep(300);

                //Открываем диалоговое окно с НПСом
                mw.PacketSend.talkToNpc(mob_wid);
                Thread.Sleep(1000);

                //Узнаем необходимые данные об окне, так как после взятия кв оно перестанет быть активным
                int[] adress_window = CalcMethods.CalcControlAddress(mw.Oph, "Win_NPC", "Btn_Back", 1);
                Thread.Sleep(300);

                //Берем кв
                mw.PacketSend.takeQuest(questId);

                //если адреса считались нормально, то закрываем диалоговое окно с НПСом
                if (adress_window[0] != 0 && adress_window[1] != 0)
                    Injects.GUI_Inject(adress_window[0], adress_window[1], mw.Oph);
                else
                {
                    mw.Logging(new FormatText(mw.Name, Brushes.Red, 14, 1), new FormatText("не смог закрыть окно", Brushes.Red, 14, 1));
                }
            }
        }

        /// <summary>
        /// Бот просматривает инвентарь на предмет нахождения "Самоцвет зала перерождения" и пишет в чат об этом
        /// </summary>
        /// <param name="oph"></param>
        /// <param name="hwnd"></param>
        public static Boolean ReadInvent(IntPtr oph, IntPtr hwnd)
        {
            Int32 count_cells = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsInventCellsCount);
            for (Int32 iter = 0; iter < count_cells; iter++)
            {
                if (CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsItemInCellID(iter)) == 27422) // id самоцвета зала перерождения
                {
                    //отправим сообщение в чат
                    Int32[] address_window_1 = CalcMethods.CalcControlAddress(oph, "Win_Chat", "DEFAULT_Txt_Speech", 2);
                    Injects.SetText(oph, "!!Самоцвет найден!", address_window_1[2]);
                    if (address_window_1[0] != 0 && address_window_1[1] != 0)
                        Injects.GUI_Inject(address_window_1[0], address_window_1[1], oph);
                    return true;
                }
            }
            //отправим сообщение в чат
            Int32[] address_window = CalcMethods.CalcControlAddress(oph, "Win_Chat", "DEFAULT_Txt_Speech", 2);
            Injects.SetText(oph, "!!Самоцвет не найден!", address_window[2]);
            if (address_window[0] != 0 && address_window[1] != 0)
                Injects.GUI_Inject(address_window[0], address_window[1], oph);
            return false;
        }

        /// <summary>
        /// Шаман кидает пати, отдает пл, призывает и выходит из пати
        /// </summary>
        /// <param name="wid"></param>
        public static void PartyAndPl(int wid)
        {
            try
            {
                if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                {
                    //узнаем указатель на процесс
                    IntPtr oph = ListClients.work_collection[ListClients.work_collection.Count() - 1].Oph;

                    int partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);

                    if (partyCount > 0)
                    {
                        //если счетчик людей в пати >0, то выходим из пати
                        ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.leaveParty();
                    }
                    Thread.Sleep(1000);
                    ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.inviteParty(wid);
                    int j = 0;
                    while (j < 40)
                    {
                        Thread.Sleep(500);
                        partyCount = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCountParty);
                        if (partyCount > 0)
                        {
                            //если счетчик людей в пати >0, то передаем пл, кидаем призыв и выходим из пати
                            int[] temp_mas = { 0x1c, 0x34, 0x820, 0x14, 0x0, 0xc };
                            //если пересборщик - пл, то передаем пл 
                            Int32 temp_int = CalcMethods.ReadInt(oph, Offsets.BaseAdress, temp_mas);
                            if (CalcMethods.ReadInt(oph, Offsets.BaseAdress, temp_mas)
                                == ListClients.work_collection[ListClients.work_collection.Count() - 1].Wid)
                            {
                                ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.ChangePl(wid);
                                Thread.Sleep(1000);
                            }
                            ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.callShamanParty(wid);
                            Thread.Sleep(5000);
                            ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.leaveParty();
                            Thread.Sleep(500);
                            return;
                        }
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Пл ребафает пати и пишет в чат остальным ботам "ребаф"
        /// </summary>
        public static void Rebuf(String text)
        {
            UseSkillMassive(ListClients.work_collection[0].My_skills_for_buf, ListClients.work_collection[0].Oph);
            Say(ListClients.work_collection[0], "!!" + text);
        }

        /// <summary>
        /// Бот пишет в чат заданный текст
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="text"></param>
        public static void Say(My_Windows mw, String text)
        {
            //запишем сообщение в чат
            Int32[] address_window = CalcMethods.CalcControlAddress(mw.Oph, "Win_Chat", "DEFAULT_Txt_Speech", 2);
            Injects.SetText(mw.Oph, text, address_window[2]);
            //нажимаем кнопку отправить
            if (address_window[0] != 0 && address_window[1] != 0)
                Injects.GUI_Inject(address_window[0], address_window[1], mw.Oph);
        }

        /// <summary>
        /// Вызов GUI инжекта
        /// </summary>
        /// <param name="result"></param>
        /// <param name="processID"></param>
        public static void FollowMe(int[] result, IntPtr oph)
        {
            try
            {
                if (result[0] != 0 && result[1] != 0)
                {
                    Injects.GUI_Inject(result[0], result[1], oph);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Остановиться
        /// </summary>
        /// <param name="processID"></param>
        public static void Stop(IntPtr oph)
        {
            try
            {
                //Расчитываем нынешние коорды персонажа
                float x = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsX);
                float y = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsY);
                float z = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsZ);
                //узнаем состояние: 0 - земля, 1 - вода, 2 - воздух
                int walk_mode = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWalkMode);
                //юзаем инжект
                Injects.WalkTo(oph, x, y, z, walk_mode);
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        /// <summary>
        /// По указанному дескриптору бот юзает указанный список скиллов
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="oph"></param>
        public static void UseSkillMassive(List<Skill> skill, IntPtr oph)
        {
            try
            {
                foreach (Skill s in skill)
                {
                    int cd_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToCdSkill(s.Number));
                    while (cd_skill > 0)
                    {
                        Thread.Sleep(cd_skill);
                        cd_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsToCdSkill(s.Number));
                    }
                    Injects.Skill_Inject(s.Id, oph);
                    Thread.Sleep(1000);
                    int lock_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCurrentSkill);
                    while (lock_skill > 0)
                    {
                        Thread.Sleep(100);
                        lock_skill = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsCurrentSkill);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Бот движется по указанному адресу, но не более 60 секунд.
        /// </summary>
        /// <param name="oph"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void MoveTo(IntPtr oph, float x, float y, float z)
        {
            try
            {
                //узнаем состояние: 0 - земля, 1 - вода, 2 - воздух
                Int32 walk_mode = CalcMethods.ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWalkMode);
                //юзаем инжект движения
                Injects.WalkTo(oph, x, y, z, walk_mode);
                //Расчитываем нынешние коорды персонажа
                float x_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsX);
                float y_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsY);
                float z_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsZ);
                //в цикле проверяем, достиг ли персонаж цели и не застрял ли он
                Int32 iter = 0;
                while (!CalcMethods.GetCoord(oph, x, y, z))
                {
                    Thread.Sleep(1000);
                    //если персонаж встал на месте, то подтолкнем его еще раз
                    if (CalcMethods.GetCoord(oph, x_temp, y_temp, z_temp))
                        Injects.WalkTo(oph, x, y, z, walk_mode);
                    //если персонаж двигается, то перезаписываем его координаты
                    else
                    {
                        x_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsX);
                        y_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsY);
                        z_temp = CalcMethods.ReadFloat(oph, Offsets.BaseAdress, Offsets.OffsetsZ);
                    }
                    iter++;
                    //если бот бежит дольше минуты, то прекращаем бежать
                    if (iter > 60)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
