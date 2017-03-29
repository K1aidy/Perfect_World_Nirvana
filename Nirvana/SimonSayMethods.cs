using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using work = Nirvana.ListClients;

namespace Nirvana
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
            for (int i= ListClients.work_collection.Count()-1; i > -1; i--)
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

        /// <summary>
        /// Выход из пати всех ботов, первым выходит патилидер
        /// </summary>
        public static void QuitFromPartyFirstPl()
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

        /// <summary>
        /// Отправление приглашения в пати пересборщиков всем ботам
        /// </summary>
        public static void InvitetoParty()
        {
            for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
            {
                if (ListClients.work_collection[i] != null)
                {
                    if (ListClients.work_collection[ListClients.work_collection.Count()-1] != null)
                    {
                        //если в соответствующем комбобоксе указан бот, то кидаем ему пати
                        ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.inviteParty((int)ListClients.work_collection[i].Wid);
                    }
                }
            }
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Отправление приглашения в пати плом всем ботам
        /// </summary>
        public static void InvitetoPartyFromPl()
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

        /// <summary>
        /// Все боты принимают приглашения в пати
        /// </summary>
        public static void AcceptInviteToParty()
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

        /// <summary>
        /// Все боты принимают приглашения в пати от патилидера
        /// </summary>
        public static void AcceptInviteToPartyFromPl()
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

        /// <summary>
        /// Пересбор пати плом
        /// </summary>
        public static void Peresbor()
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
            for (int k = 0; k< party_struct.Count()-1; k++)
            {
                ListClients.work_collection[0].PacketSend.kickFromParty(party_struct[k]);
                Thread.Sleep(300);
            }
            //передаем пл последнему (если отмечен чекбокс в настройках)
            if (MainWindow.settings.ChangePl)
            {
                ListClients.work_collection[0].PacketSend.ChangePl(party_struct[party_struct.Count() - 1]);
                Thread.Sleep(500);
            }      
             
            //выходим из пати
            ListClients.work_collection[0].PacketSend.leaveParty();
            Thread.Sleep(1500);

            //кидаем всем пати
            for(int k = 0; k < party_struct.Count(); k++)
            {
                ListClients.work_collection[0].PacketSend.inviteParty(party_struct[k]);
                Thread.Sleep(300);
            }
            Thread.Sleep(1500);

            //все боты принимают пати, если они получили приглашение
            AcceptInviteToPartyFromPl();
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Пересборщик передает ПЛ первому боту
        /// </summary>
        public static void ChangePl()
        {
            if (ListClients.work_collection[ListClients.work_collection.Count() - 1] != null)
                ListClients.work_collection[ListClients.work_collection.Count() - 1].PacketSend.ChangePl(ListClients.work_collection[0].Wid);
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Пересборщик (шаман) кидает всем ботам призывы по очереди, боты принимают призыв
        /// </summary>
        public static void CallShaman(Skill call_skill)
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
                        int mob_wid = CalcMethods.MobSearch(ListClients.work_collection[i].Oph, "Страж Зала Перерождения");
                        if (mob_wid != 0)
                        {
                            //Выделяем НПСа
                            ListClients.work_collection[i].PacketSend.selectNpc(mob_wid);
                            Thread.Sleep(300);
                            //Открываем диалоговое окно с НПСом
                            ListClients.work_collection[i].PacketSend.talkToNpc(mob_wid);
                            Thread.Sleep(1000);
                            //Узнаем необходимые данные об окне, так как после взятия кв оно перестанет быть активным
                            int[] adress_window = CalcMethods.CalcControlAddress(ListClients.work_collection[i].Oph);
                            Thread.Sleep(300);
                            //Берем кв на заход в нирвану (id взято отсюда http://www.pwdatabase.com/ru/quest/20790 )
                            ListClients.work_collection[i].PacketSend.takeQuest(20790);
                            //если адреса считались нормально, то закрываем диалоговое окно с НПСом
                            int adressActiveWindow = CalcMethods.CalcAddressActiveWindow(ListClients.work_collection[i].Oph);
                            if (adress_window[0] != 0 && adress_window[1] != 0 &&
                                adress_window[0] == adressActiveWindow)
                                Injects.GUI_Inject(adress_window[0], adress_window[1], ListClients.work_collection[i].Oph);
                            else
                            {
                                ListClients.work_collection[i].Logging(new FormatText(ListClients.work_collection[i].Name, Brushes.Red, 14, 1),
                                                                       new FormatText("не смог закрыть окно", Brushes.Red, 14, 1));
                            }
                                
                            
                        }
                    }
                    //ждем отката призыва
                    Thread.Sleep(3000);
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

        /// <summary>
        /// Боты входят в нирвану, все кроме пересборщика
        /// </summary>
        public static void EnterToNirvana()
        {
            for (int i = 0; i < ListClients.work_collection.Count() - 1; i++)
            {
                if (ListClients.work_collection[i] != null)
                {
                    int mob_wid = CalcMethods.MobSearch(ListClients.work_collection[i].Oph, "Страж Зала Перерождения");
                    if (mob_wid != 0)
                    {
                        //Выделяем НПСа
                        ListClients.work_collection[i].PacketSend.selectNpc(mob_wid);
                        Thread.Sleep(300);
                        //Открываем диалоговое окно с НПСом
                        ListClients.work_collection[i].PacketSend.talkToNpc(mob_wid);
                        Thread.Sleep(1000);
                        //Узнаем необходимые данные об окне, так как после взятия кв оно перестанет быть активным
                        int[] adress_window = CalcMethods.CalcControlAddress(ListClients.work_collection[i].Oph);
                        Thread.Sleep(300);
                        //Берем кв на заход в нирвану (id взято отсюда http://www.pwdatabase.com/ru/quest/20790 )
                        ListClients.work_collection[i].PacketSend.takeQuest(20790);
                        Thread.Sleep(5000);
                        //если адреса считались нормально, то закрываем диалоговое окно с НПСом
                        if (adress_window[0] != 0 && adress_window[1] != 0)
                            Injects.GUI_Inject(adress_window[0], adress_window[1], ListClients.work_collection[i].Oph);
                        Thread.Sleep(300);
                    }
                }
            }
        }
        
        /// <summary>
        /// Шаман кидает пати, отдает пл, призывает и выходит из пати
        /// </summary>
        /// <param name="wid"></param>
        public static void PartyAndPl(int wid)
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
                        int[] temp_mas = { 0x1c, 0x34, 0x7d0, 0x14, 0x0, 0xc };
                        //если пересборщик - пл, то передаем пл 
                        if (CalcMethods.ReadInt(oph, Offsets.BaseAdress, temp_mas) 
                            == ListClients.work_collection[ListClients.work_collection.Count()-1].Wid)
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

        /// <summary>
        /// Вызов GUI инжекта
        /// </summary>
        /// <param name="result"></param>
        /// <param name="processID"></param>
        public static void FollowMe(int[] result, IntPtr oph)
        {
                if (result[0] != 0 && result[1] != 0)
                {
                    Injects.GUI_Inject(result[0], result[1], oph);
                }
        }

        /// <summary>
        /// Остановиться
        /// </summary>
        /// <param name="processID"></param>
        public static void Stop(IntPtr oph)
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

        public static void UseSkillMassive(List<Skill> skill, IntPtr oph)
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
    }
}
