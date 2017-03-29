using System;
using System.Collections.Generic;
using System.Text;

namespace Nirvana
{
    [Serializable]
    /// <summary>
    /// Класс для сериализации оффсетов
    /// </summary>
    public class Offset
    {
        //базовый адрес
        public int baseAdress;// = 0x00EFF604;
        //гейм адрес
        public int gameAdress;// = 0x00EFFDAC;
        //GUI адрес
        public int guiAdress;// = 0x00AE71C0;
        //адрес для отправки пакетов
        public int sendPacket;// = 0x0087A600;
        //адрес для инжекта скиллов
        public int useSkill;// = 0x4BC760;
        //адреса для инжекта движения
        public int action_1;// = 0x04CF3D0;
        public int action_2;// = 0x04D5950;
        public int action_3;// = 0x04CF9E0;
        // Счетчик уведомлений
        public int inviteCount;// = 0x00F0DF94;
        // Указатель на структуру уведомлений
        public int inviteStruct;// = 0x00F0DF88;
        //Указатель на начало чата
        public int сhatStart;// = 0x00F06768;
        //Указатель на число сообщений
        public int сhatNumber;// = 0x00F06774;
        //смещение к ID игрока, отправившего первое приглашение 
        public int inviteWidPlayer;// = 0x10;
        //смещение к ID пати, в которую приглашают
        public int inviteWidParty;// = 0x14;
        //смещение до gameadress
        public int offsetToGameAdress;// = 0x1C;
        //смещение до структуры перса
        public int offsetToPersStruct;// = 0x34;
        //смещение до структуры пати
        public int offsetToParty;// = 0x7D0;
        //смещение до счетчика пати
        public int offsetToCountParty;// = 0x18;
        //смещение до имени персонажа
        public int offsetToName;// = 0x700;
        //смщение до идентификатора класса
        public int offsetToClassID;// = 0x0704;
        //смещение до состояния копания шахты
        public int offsetToMiningState;// = 0x288;
        //cмещение до wid для работы окна Win_QuickAction
        public int offsetToWidWin_QuickAction;// = 0xf4;
        //смещения к координатам
        public int offsetToX;// = 0x3c;
        public int offsetToY;// = 0x44;
        public int offsetToZ;// = 0x40;
        //смещение к walk_mode
        public int offsetToWalkMode;// = 0x710;
        //смещение к wid персонажа
        public int offsetToWid;// = 0x4B8;
        //смещение к wid таргета
        public int offsetToTargetWid;// = 0x5A4; 
        public int offsetToStructParty;// = 0x14;
        //цепочка офсетов до имени локации
        public int[] offsetsLocationName;// = new int[] { 0x1c, 0x1c, 0x60, 0x4, 0x0 };
        //смещение до счетчика умений
        public int offsetToSkillsCount;// = 0x157C;
        //смещение до массива умений
        public int offsetToSkillsArray;// = 0x1578;
        //смещение до юзающегося скилла
        public int offsetToCurrentSkill;// = 0x7ec;
        //смещение до ID скилла
        public int offsetToIdSkill;// = 0x8;
        //смещение до кулдауна скилла
        public int offsetToCdSkill;// = 0x10;
        //смещение до счетчика бафов
        public int offsetToCountBufs;// = 0x398;
        //смещение до структуры бафов
        public int offsetToBufsArray;// = 0x390;
        //смещение до хэштаблиц
        public int offsetToHashTables;// = 0x1c;
        //смещение до структуры мобов
        public int offsetToMobsStruct;// = 0x20;
        //смещение до начала массива мобов
        public int offsetToBeginMobsStruct;// = 0x5C;
        //смещение до счетчика мобов
        public int offsetToMobsCount;// = 0x18;
        //смещение до имени моба 
        public int offsetToMobName;// = 0x260;
        //смещение до wid моба
        public int offsetToMobWid;// = 0x114;
        //смещение до структуры игроков
        public int offsetToPlayersStruct;// = 0x1c;
        //смещение до начала массива игроков
        public int offsetToBeginPlayersStruct;// = 0x98;
        //смещение до счетчика игроков
        public int offsetToPlayersCount;// = 0x18;
        //смещение к ID сообщения
        public int msgId;// = 0x14;
        //смещение к типу сообщения
        public int msgType;// = 0x4;
        //смещение к msg-form1
        public int msg_form1;// = 0xC;
        //смещение к msg-form2
        public int msg_form2;// = 0x8;
        //смещение к WID сообщения
        public int msgWid;// = 0x20;
    }
}
