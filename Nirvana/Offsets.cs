using System;
using System.Collections.Generic;
using System.Text;

namespace Nirvana
{
    /// <summary>
    /// Класс, содержащий необходимые оффсеты
    /// </summary>
    public static class Offsets
    {
        //ID иконки в трее
        const uint trayID = 101;

        #region Адреса и офсеты

        //базовый адрес
        static int baseAdress;
        //гейм адрес
        static int gameAdress;
        //GUI адрес
        static int guiAdress;
        //адрес для инжекта скиллов
        static int useSkill;
        //адрес для отправки пакетов
        static int sendPacket;
        //адреса для инжекта движения
        static int action_1;
        static int action_2;
        static int action_3;
        // Счетчик уведомлений
        static int inviteCount;
        // Указатель на структуру уведомлений
        static int inviteStruct;
        //Указатель на начало чата
        static int сhatStart;
        //Указатель на число сообщений
        static int сhatNumber;
        //смещение к ID сообщения
        static int msgId;
        //смещение к типу сообщения
        static int msgType;
        //смещение к msg-form1
        static int msg_form1;
        //смещение к msg-form2
        static int msg_form2;
        //смещение к WID сообщения
        static int msgWid;
        //смещение к ID игрока, отправившего первое приглашение 
        static int inviteWidPlayer;
        //смещение к ID пати, в которую приглашают
        static int inviteWidParty;
        //смещение до gameadress
        static int offsetToGameAdress;
        //смещение до структуры перса
        static int offsetToPersStruct;
        //смещение до счетчика бафов
        static int offsetToCountBufs;
        //смещение до структуры бафов
        static int offsetToBufsArray;
        //смещение до счетчика умений
        static int offsetToSkillsCount;
        //смещение до массива умений
        static int offsetToSkillsArray;
        //смещение до юзающегося скилла
        static int offsetToCurrentSkill;
        //смещение до ID скилла
        static int offsetToIdSkill;
        //смещение до кулдауна скилла
        static int offsetToCdSkill;
        //смещение до структуры пати
        static int offsetToParty;
        //смещение до начала структуры пати
        static int offsetToStructParty;
        //смещение до счетчика пати
        static int offsetToCountParty;
        //смещение до имени персонажа
        static int offsetToName;
        //смщение до идентификатора класса
        static int offsetToClassID;
        //смещение до состояния копания шахты
        static int offsetToMiningState;
        //cмещение до wid для работы окна Win_QuickAction
        static int offsetToWidWin_QuickAction;
        //смещения к координатам
        static int offsetToX;
        static int offsetToY;
        static int offsetToZ;
        //смещение к walk_mode
        static int offsetToWalkMode;
        //смещение к wid персонажа
        static int offsetToWid;
        //смещение к wid таргета
        static int offsetToTargetWid;
        //смещение до хэштаблиц
        static int offsetToHashTables;
        //смещение до структуры игроков
        static int offsetToPlayersStruct;
        //смещение до начала массива игроков
        static int offsetToBeginPlayersStruct;
        //смещение до счетчика игроков
        static int offsetToPlayersCount;
        //смещение до структуры мобов
        static int offsetToMobsStruct;
        //смещение до начала массива мобов
        static int offsetToBeginMobsStruct;
        //смещение до счетчика мобов
        static int offsetToMobsCount;
        //смещение до имени моба 
        static int offsetToMobName;
        //смещение до wid моба
        static int offsetToMobWid;

        #endregion

        /// <summary>
        /// Метод обновления цепочек смещений
        /// </summary>
        public static void RefreshOffsets()
        {
            offsetsCountParty = new int[] { offsetToGameAdress,offsetToPersStruct, offsetToParty, offsetToCountParty };
            offsetsStructParty = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToParty, offsetToStructParty };
            offsetsClassId = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToClassID };
            offsetsMiningState = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToMiningState };
            offsetsX = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToX };
            offsetsY = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToY };
            offsetsZ = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToZ };
            offsetsWalkMode = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToWalkMode };
            offsetsWid = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToWid };
            offsetsName = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToName, 0x0 };
            OffsetsSkillsCount = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToSkillsCount };
            offsetsCurrentSkill = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToCurrentSkill };
            offsetsBufsCount = new int[] { offsetToGameAdress, offsetToPersStruct, offsetToCountBufs };
            offsetsMobsCount = new int[] { offsetToGameAdress, offsetToHashTables, offsetToMobsStruct, offsetToMobsCount };
            offsetsPlayersCount = new int[] { offsetToGameAdress, offsetToHashTables, offsetToPlayersStruct, offsetToPlayersCount };
        }

        #region Цепочки смещений
        //цепочка оффсетов до названия локации
        static int[] offsetsLocationName;
        //цепочка оффсетов до счетчика пати
        static int[] offsetsCountParty;
        //цепочка оффсетов до структуры пати
        static int[] offsetsStructParty;
        //цепочка оффсетов до ID класс
        static int[] offsetsClassId;
        //цепочка оффсетов до состояния копки
        static int[] offsetsMiningState;
        //цепочка оффсетов до координаты X
        static int[] offsetsX;
        //цепочка оффсетов до координаты Y
        static int[] offsetsY;
        //цепочка оффсетов до координаты Z
        static int[] offsetsZ;
        //цепочка оффсетов до walk_mode
        static int[] offsetsWalkMode;
        //цепочка оффсетов до wid персонажа
        static int[] offsetsWid;
        //цепочка оффсетов до имени персонажа
        static int[] offsetsName;
        //цепочка оффсетов до счетчика скиллов
        static int[] offsetsSkillsCount;
        //цепочка оффсетов до счетчика бафов
        static int[] offsetsBufsCount;
        //цепочка оффсетов до структуры юзающегося скилла
        static int[] offsetsCurrentSkill;
        //цепочка смещение до счетчика мобов
        static int[] offsetsMobsCount;
        //цепочка смещение до счетчика игроков
        static int[] offsetsPlayersCount;
        //цепочка оффсетов до ID скилла
        public static int[] OffsetsToIdSkill(int iter)
        {
            return new int[] {offsetToGameAdress, offsetToPersStruct, offsetToSkillsArray, 0x4*iter, offsetToIdSkill };
        }
        //цепочка оффсетов до кулдауна скилла
        public static int[] OffsetsToCdSkill(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToPersStruct, offsetToSkillsArray, 0x4 * iter, offsetToCdSkill };
        }
        //цепочка оффсетов до ID бафа
        public static int[] OffsetsIdBuf(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToPersStruct, offsetToBufsArray, 0x12 * iter};
        }
        //цепочка оффсетов до имени моба
        public static int[] OffsetsNameMob(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToHashTables, offsetToMobsStruct,
                                offsetToBeginMobsStruct, 0x4 * iter, offsetToMobName, 0x0};
        }
        //цепочка оффсетов до WID моба
        public static int[] OffsetsWidMob(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToHashTables, offsetToMobsStruct,
                                offsetToBeginMobsStruct, 0x4 * iter, offsetToMobWid };
        }
        //цепочка оффсетов до WID игрока
        public static int[] OffsetsWidPlayer(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToHashTables, offsetToPlayersStruct,
                                offsetToBeginPlayersStruct, 0x4 * iter, offsetToWid };
        }
        //цепочка оффсетов до WID таргета игрока
        public static int[] OffsetsTargetWidPlayer(int iter)
        {
            return new int[] { offsetToGameAdress, offsetToHashTables, offsetToPlayersStruct,
                                offsetToBeginPlayersStruct, 0x4 * iter, offsetToTargetWid};
        }
        //цепочка оффсетов до ID сообщения
        public static int OffsetsMsgId(int chatStartAddress ,int iter)
        {
            return (chatStartAddress + (iter * 0x24)) + msgId;
        }
        //цепочка оффсетов до типа сообщения
        public static int OffsetsMsgType(int chatStartAddress, int iter)
        {
            return (chatStartAddress + (iter * 0x24)) + msgType;
        }
        //цепочка оффсетов до WID сообщения
        public static int OffsetsMsgWid(int chatStartAddress, int iter)
        {
            return (chatStartAddress + (iter * 0x24)) + msgWid;
        }
        //цепочка оффсетов до 1й формы сообщения
        public static int OffsetsMsgForm1(int chatStartAddress, int iter)
        {
            return (chatStartAddress + (iter * 0x24)) + msg_form1;
        }
        //цепочка оффсетов до 2й формы сообщения
        public static int OffsetsMsgForm2(int chatStartAddress, int iter)
        {
            return (chatStartAddress + (iter * 0x24)) + msg_form2;
        }

        #endregion

        #region Getters/Setters

        public static uint TrayID
        {
            get
            {
                return trayID;
            }
        }

        public static int[] OffsetsClassId
        {
            get
            {
                return offsetsClassId;
            }
        }

        public static int[] OffsetsName
        {
            get
            {
                return offsetsName;
            }
        }

        public static int[] OffsetsX
        {
            get
            {
                return offsetsX;
            }
        }

        public static int[] OffsetsY
        {
            get
            {
                return offsetsY;
            }
        }

        public static int[] OffsetsZ
        {
            get
            {
                return offsetsZ;
            }
        }

        public static int[] OffsetsMiningState
        {
            get
            {
                return offsetsMiningState;
            }
        }

        public static int[] OffsetsCountParty
        {
            get
            {
                return offsetsCountParty;
            }
        }

        public static int[] OffsetsWid
        {
            get
            {
                return offsetsWid;
            }
        }

        public static int[] OffsetsWalkMode
        {
            get
            {
                return offsetsWalkMode;
            }
        }

        public static int[] OffsetsStructParty
        {
            get
            {
                return offsetsStructParty;
            }
        }

        public static int BaseAdress
        {
            get
            {
                return baseAdress;
            }

            set
            {
                baseAdress = value;
            }
        }

        public static int GameAdress
        {
            get
            {
                return gameAdress;
            }

            set
            {
                gameAdress = value;
            }
        }

        public static int GuiAdress
        {
            get
            {
                return guiAdress;
            }

            set
            {
                guiAdress = value;
            }
        }

        public static int SendPacket
        {
            get
            {
                return sendPacket;
            }

            set
            {
                sendPacket = value;
            }
        }

        public static int Action_1
        {
            get
            {
                return action_1;
            }

            set
            {
                action_1 = value;
            }
        }

        public static int Action_2
        {
            get
            {
                return action_2;
            }

            set
            {
                action_2 = value;
            }
        }

        public static int Action_3
        {
            get
            {
                return action_3;
            }

            set
            {
                action_3 = value;
            }
        }

        public static int InviteCount
        {
            get
            {
                return inviteCount;
            }

            set
            {
                inviteCount = value;
            }
        }

        public static int InviteStruct
        {
            get
            {
                return inviteStruct;
            }

            set
            {
                inviteStruct = value;
            }
        }

        public static int СhatStart
        {
            get
            {
                return сhatStart;
            }

            set
            {
                сhatStart = value;
            }
        }

        public static int СhatNumber
        {
            get
            {
                return сhatNumber;
            }

            set
            {
                сhatNumber = value;
            }
        }

        public static int InviteWidPlayer
        {
            get
            {
                return inviteWidPlayer;
            }

            set
            {
                inviteWidPlayer = value;
            }
        }

        public static int InviteWidParty
        {
            get
            {
                return inviteWidParty;
            }

            set
            {
                inviteWidParty = value;
            }
        }

        public static int OffsetToGameAdress
        {
            get
            {
                return offsetToGameAdress;
            }

            set
            {
                offsetToGameAdress = value;
            }
        }

        public static int OffsetToPersStruct
        {
            get
            {
                return offsetToPersStruct;
            }

            set
            {
                offsetToPersStruct = value;
            }
        }

        public static int OffsetToParty
        {
            get
            {
                return offsetToParty;
            }

            set
            {
                offsetToParty = value;
            }
        }

        public static int OffsetToCountParty
        {
            get
            {
                return offsetToCountParty;
            }

            set
            {
                offsetToCountParty = value;
            }
        }

        public static int OffsetToName
        {
            get
            {
                return offsetToName;
            }

            set
            {
                offsetToName = value;
            }
        }

        public static int OffsetToClassID
        {
            get
            {
                return offsetToClassID;
            }

            set
            {
                offsetToClassID = value;
            }
        }

        public static int OffsetToMiningState
        {
            get
            {
                return offsetToMiningState;
            }

            set
            {
                offsetToMiningState = value;
            }
        }

        public static int OffsetToWidWin_QuickAction
        {
            get
            {
                return offsetToWidWin_QuickAction;
            }

            set
            {
                offsetToWidWin_QuickAction = value;
            }
        }

        public static int OffsetToX
        {
            get
            {
                return offsetToX;
            }

            set
            {
                offsetToX = value;
            }
        }

        public static int OffsetToY
        {
            get
            {
                return offsetToY;
            }

            set
            {
                offsetToY = value;
            }
        }

        public static int OffsetToZ
        {
            get
            {
                return offsetToZ;
            }

            set
            {
                offsetToZ = value;
            }
        }

        public static int OffsetToWalkMode
        {
            get
            {
                return offsetToWalkMode;
            }

            set
            {
                offsetToWalkMode = value;
            }
        }

        public static int OffsetToWid
        {
            get
            {
                return offsetToWid;
            }

            set
            {
                offsetToWid = value;
            }
        }

        public static int OffsetToTargetWid
        {
            get
            {
                return offsetToTargetWid;
            }

            set
            {
                offsetToTargetWid = value;
            }
        }

        public static int OffsetToStructParty
        {
            get
            {
                return offsetToStructParty;
            }

            set
            {
                offsetToStructParty = value;
            }
        }

        public static int UseSkill
        {
            get
            {
                return useSkill;
            }

            set
            {
                useSkill = value;
            }
        }

        public static int[] OffsetsSkillsCount
        {
            get
            {
                return offsetsSkillsCount;
            }

            set
            {
                offsetsSkillsCount = value;
            }
        }

        public static int[] OffsetsCurrentSkill
        {
            get
            {
                return offsetsCurrentSkill;
            }

            set
            {
                offsetsCurrentSkill = value;
            }
        }

        public static int[] OffsetsLocationName
        {
            get
            {
                return offsetsLocationName;
            }

            set
            {
                offsetsLocationName = value;
            }
        }

        public static int OffsetToSkillsArray
        {
            get
            {
                return offsetToSkillsArray;
            }

            set
            {
                offsetToSkillsArray = value;
            }
        }

        public static int OffsetToIdSkill
        {
            get
            {
                return offsetToIdSkill;
            }

            set
            {
                offsetToIdSkill = value;
            }
        }

        public static int OffsetToCdSkill
        {
            get
            {
                return offsetToCdSkill;
            }

            set
            {
                offsetToCdSkill = value;
            }
        }

        public static int OffsetToSkillsCount
        {
            get
            {
                return offsetToSkillsCount;
            }

            set
            {
                offsetToSkillsCount = value;
            }
        }

        public static int OffsetToCurrentSkill
        {
            get
            {
                return offsetToCurrentSkill;
            }

            set
            {
                offsetToCurrentSkill = value;
            }
        }

        public static int OffsetToCountBufs
        {
            get
            {
                return offsetToCountBufs;
            }

            set
            {
                offsetToCountBufs = value;
            }
        }

        public static int OffsetToBufsArray
        {
            get
            {
                return offsetToBufsArray;
            }

            set
            {
                offsetToBufsArray = value;
            }
        }

        public static int[] OffsetsBufsCount
        {
            get
            {
                return offsetsBufsCount;
            }

            set
            {
                offsetsBufsCount = value;
            }
        }

        public static int OffsetToHashTables
        {
            get
            {
                return offsetToHashTables;
            }

            set
            {
                offsetToHashTables = value;
            }
        }

        public static int OffsetToMobsStruct
        {
            get
            {
                return offsetToMobsStruct;
            }

            set
            {
                offsetToMobsStruct = value;
            }
        }

        public static int OffsetToMobsCount
        {
            get
            {
                return offsetToMobsCount;
            }

            set
            {
                offsetToMobsCount = value;
            }
        }

        public static int OffsetToMobName
        {
            get
            {
                return offsetToMobName;
            }

            set
            {
                offsetToMobName = value;
            }
        }

        public static int OffsetToMobWid
        {
            get
            {
                return offsetToMobWid;
            }

            set
            {
                offsetToMobWid = value;
            }
        }

        public static int[] OffsetsMobsCount
        {
            get
            {
                return offsetsMobsCount;
            }

            set
            {
                offsetsMobsCount = value;
            }
        }

        public static int OffsetToBeginMobsStruct
        {
            get
            {
                return offsetToBeginMobsStruct;
            }

            set
            {
                offsetToBeginMobsStruct = value;
            }
        }

        public static int OffsetToPlayersStruct
        {
            get
            {
                return offsetToPlayersStruct;
            }

            set
            {
                offsetToPlayersStruct = value;
            }
        }

        public static int OffsetToBeginPlayersStruct
        {
            get
            {
                return offsetToBeginPlayersStruct;
            }

            set
            {
                offsetToBeginPlayersStruct = value;
            }
        }

        public static int OffsetToPlayersCount
        {
            get
            {
                return offsetToPlayersCount;
            }

            set
            {
                offsetToPlayersCount = value;
            }
        }

        public static int[] OffsetsPlayersCount
        {
            get
            {
                return offsetsPlayersCount;
            }

            set
            {
                offsetsPlayersCount = value;
            }
        }

        public static int MsgId
        {
            get
            {
                return msgId;
            }

            set
            {
                msgId = value;
            }
        }

        public static int MsgType
        {
            get
            {
                return msgType;
            }

            set
            {
                msgType = value;
            }
        }

        public static int Msg_form1
        {
            get
            {
                return msg_form1;
            }

            set
            {
                msg_form1 = value;
            }
        }

        public static int Msg_form2
        {
            get
            {
                return msg_form2;
            }

            set
            {
                msg_form2 = value;
            }
        }

        public static int MsgWid
        {
            get
            {
                return msgWid;
            }

            set
            {
                msgWid = value;
            }
        }

        #endregion
    }
}
