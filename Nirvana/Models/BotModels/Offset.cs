using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Nirvana.Models.BotModels
{
    public class Offset : INotifyPropertyChanged, ICloneable
    {
        Int32 id;
        public Int32 Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        String version;
        public String Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
                OnPropertyChanged("Version");
            }
        }

        String ba;
        public String BA
        {
            get
            {
                return ba;
            }

            set
            {
                ba = value;
                OnPropertyChanged("BA");
            }
        }

        String ga;
        public String GA
        {
            get
            {
                return ga;
            }

            set
            {
                ga = value;
                OnPropertyChanged("GA");
            }
        }

        String guiAdd;
        public String GuiAdd
        {
            get
            {
                return guiAdd;
            }

            set
            {
                guiAdd = value;
                OnPropertyChanged("GuiAdd");
            }
        }

        String sendPacket;
        public String SendPacket
        {
            get
            {
                return sendPacket;
            }

            set
            {
                sendPacket = value;
                OnPropertyChanged("SendPacket");
            }
        }

        String autoAttack;
        public String AutoAttack
        {
            get
            {
                return autoAttack;
            }

            set
            {
                autoAttack = value;
                OnPropertyChanged("AutoAttack");
            }
        }

        String useSkill;
        public String UseSkill
        {
            get
            {
                return useSkill;
            }

            set
            {
                useSkill = value;
                OnPropertyChanged("UseSkill");
            }
        }

        String action_1;
        public String Action_1
        {
            get
            {
                return action_1;
            }

            set
            {
                action_1 = value;
                OnPropertyChanged("Action_1");
            }
        }

        String action_2;
        public String Action_2
        {
            get
            {
                return action_2;
            }

            set
            {
                action_2 = value;
                OnPropertyChanged("Action_2");
            }
        }

        String action_3;
        public String Action_3
        {
            get
            {
                return action_3;
            }

            set
            {
                action_3 = value;
                OnPropertyChanged("Action_3");
            }
        }

        String inviteCount;
        public String InviteCount
        {
            get
            {
                return inviteCount;
            }

            set
            {
                inviteCount = value;
                OnPropertyChanged("InviteCount");
            }
        }

        String inviteStruct;
        public String InviteStruct
        {
            get
            {
                return inviteStruct;
            }

            set
            {
                inviteStruct = value;
                OnPropertyChanged("InviteStruct");
            }
        }

        String chatStart;
        public String ChatStart
        {
            get
            {
                return chatStart;
            }

            set
            {
                chatStart = value;
                OnPropertyChanged("ChatStart");
            }
        }

        String chatNumber;
        public String ChatNumber
        {
            get
            {
                return chatNumber;
            }

            set
            {
                chatNumber = value;
                OnPropertyChanged("ChatNumber");
            }
        }

        String inviteWidPlayer;
        public String InviteWidPlayer
        {
            get
            {
                return inviteWidPlayer;
            }

            set
            {
                inviteWidPlayer = value;
                OnPropertyChanged("InviteWidPlayer");
            }
        }

        String inviteWidParty;
        public String InviteWidParty
        {
            get
            {
                return inviteWidParty;
            }

            set
            {
                inviteWidParty = value;
                OnPropertyChanged("InviteWidParty");
            }
        }

        String offsetToGameAdress;
        public String OffsetToGameAdress
        {
            get
            {
                return offsetToGameAdress;
            }

            set
            {
                offsetToGameAdress = value;
                OnPropertyChanged("OffsetToGameAdress");
            }
        }

        String offsetToPersStruct;
        public String OffsetToPersStruct
        {
            get
            {
                return offsetToPersStruct;
            }

            set
            {
                offsetToPersStruct = value;
                OnPropertyChanged("OffsetToPersStruct");
            }
        }

        String offsetToParty;
        public String OffsetToParty
        {
            get
            {
                return offsetToParty;
            }

            set
            {
                offsetToParty = value;
                OnPropertyChanged("OffsetToParty");
            }
        }

        String offsetToCountParty;
        public String OffsetToCountParty
        {
            get
            {
                return offsetToCountParty;
            }

            set
            {
                offsetToCountParty = value;
                OnPropertyChanged("OffsetToCountParty");
            }
        }

        String offsetToName;
        public String OffsetToName
        {
            get
            {
                return offsetToName;
            }

            set
            {
                offsetToName = value;
                OnPropertyChanged("OffsetToName");
            }
        }

        String offsetToClassID;
        public String OffsetToClassID
        {
            get
            {
                return offsetToClassID;
            }

            set
            {
                offsetToClassID = value;
                OnPropertyChanged("OffsetToClassID");
            }
        }

        String offsetToMiningState;
        public String OffsetToMiningState
        {
            get
            {
                return offsetToMiningState;
            }

            set
            {
                offsetToMiningState = value;
                OnPropertyChanged("OffsetToMiningState");
            }
        }

        String offsetToWidWin_QuickAction;
        public String OffsetToWidWin_QuickAction
        {
            get
            {
                return offsetToWidWin_QuickAction;
            }

            set
            {
                offsetToWidWin_QuickAction = value;
                OnPropertyChanged("OffsetToWidWin_QuickAction");
            }
        }

        String offsetToX;
        public String OffsetToX
        {
            get
            {
                return offsetToX;
            }

            set
            {
                offsetToX = value;
                OnPropertyChanged("OffsetToX");
            }
        }

        String offsetToY;
        public String OffsetToY
        {
            get
            {
                return offsetToY;
            }

            set
            {
                offsetToY = value;
                OnPropertyChanged("OffsetToY");
            }
        }

        String offsetToZ;
        public String OffsetToZ
        {
            get
            {
                return offsetToZ;
            }

            set
            {
                offsetToZ = value;
                OnPropertyChanged("OffsetToZ");
            }
        }

        String offsetToWalkMode;
        public String OffsetToWalkMode
        {
            get
            {
                return offsetToWalkMode;
            }

            set
            {
                offsetToWalkMode = value;
                OnPropertyChanged("OffsetToWalkMode");
            }
        }

        String offsetToWid;
        public String OffsetToWid
        {
            get
            {
                return offsetToWid;
            }

            set
            {
                offsetToWid = value;
                OnPropertyChanged("OffsetToWid");
            }
        }

        String offsetToTargetWid;
        public String OffsetToTargetWid
        {
            get
            {
                return offsetToTargetWid;
            }

            set
            {
                offsetToTargetWid = value;
                OnPropertyChanged("OffsetToTargetWid");
            }
        }

        String offsetToStructParty;
        public String OffsetToStructParty
        {
            get
            {
                return offsetToStructParty;
            }

            set
            {
                offsetToStructParty = value;
                OnPropertyChanged("OffsetToStructParty");
            }
        }

        String offsetsLocationName_0;
        public String OffsetsLocationName_0
        {
            get
            {
                return offsetsLocationName_0;
            }

            set
            {
                offsetsLocationName_0 = value;
                OnPropertyChanged("OffsetsLocationName_0");
            }
        }

        String offsetsLocationName_1;
        public String OffsetsLocationName_1
        {
            get
            {
                return offsetsLocationName_1;
            }

            set
            {
                offsetsLocationName_1 = value;
                OnPropertyChanged("OffsetsLocationName_1");
            }
        }

        String offsetsLocationName_2;
        public String OffsetsLocationName_2
        {
            get
            {
                return offsetsLocationName_2;
            }

            set
            {
                offsetsLocationName_2 = value;
                OnPropertyChanged("OffsetsLocationName_2");
            }
        }

        String offsetsLocationName_3;
        public String OffsetsLocationName_3
        {
            get
            {
                return offsetsLocationName_3;
            }

            set
            {
                offsetsLocationName_3 = value;
                OnPropertyChanged("OffsetsLocationName_3");
            }
        }

        String offsetsLocationName_4;
        public String OffsetsLocationName_4
        {
            get
            {
                return offsetsLocationName_4;
            }

            set
            {
                offsetsLocationName_4 = value;
                OnPropertyChanged("OffsetsLocationName_4");
            }
        }

        String offsetToSkillsCount;
        public String OffsetToSkillsCount
        {
            get
            {
                return offsetToSkillsCount;
            }

            set
            {
                offsetToSkillsCount = value;
                OnPropertyChanged("OffsetToSkillsCount");
            }
        }

        String offsetToSkillsArray;
        public String OffsetToSkillsArray
        {
            get
            {
                return offsetToSkillsArray;
            }

            set
            {
                offsetToSkillsArray = value;
                OnPropertyChanged("OffsetToSkillsArray");
            }
        }

        String offsetToCurrentSkill;
        public String OffsetToCurrentSkill
        {
            get
            {
                return offsetToCurrentSkill;
            }

            set
            {
                offsetToCurrentSkill = value;
                OnPropertyChanged("OffsetToCurrentSkill");
            }
        }

        String offsetToIdSkill;
        public String OffsetToIdSkill
        {
            get
            {
                return offsetToIdSkill;
            }

            set
            {
                offsetToIdSkill = value;
                OnPropertyChanged("OffsetToIdSkill");
            }
        }

        String offsetToCdSkill;
        public String OffsetToCdSkill
        {
            get
            {
                return offsetToCdSkill;
            }

            set
            {
                offsetToCdSkill = value;
                OnPropertyChanged("OffsetToCdSkill");
            }
        }

        String offsetToCountBufs;
        public String OffsetToCountBufs
        {
            get
            {
                return offsetToCountBufs;
            }

            set
            {
                offsetToCountBufs = value;
                OnPropertyChanged("OffsetToCountBufs");
            }
        }

        String offsetToBufsArray;
        public String OffsetToBufsArray
        {
            get
            {
                return offsetToBufsArray;
            }

            set
            {
                offsetToBufsArray = value;
                OnPropertyChanged("OffsetToBufsArray");
            }
        }

        String offsetToHashTables;
        public String OffsetToHashTables
        {
            get
            {
                return offsetToHashTables;
            }

            set
            {
                offsetToHashTables = value;
                OnPropertyChanged("OffsetToHashTables");
            }
        }

        String offsetToMobsStruct;
        public String OffsetToMobsStruct
        {
            get
            {
                return offsetToMobsStruct;
            }

            set
            {
                offsetToMobsStruct = value;
                OnPropertyChanged("OffsetToMobsStruct");
            }
        }

        String offsetToBeginMobsStruct;
        public String OffsetToBeginMobsStruct
        {
            get
            {
                return offsetToBeginMobsStruct;
            }

            set
            {
                offsetToBeginMobsStruct = value;
                OnPropertyChanged("OffsetToBeginMobsStruct");
            }
        }

        String offsetToMobsCount;
        public String OffsetToMobsCount
        {
            get
            {
                return offsetToMobsCount;
            }

            set
            {
                offsetToMobsCount = value;
                OnPropertyChanged("OffsetToMobsCount");
            }
        }

        String offsetToMobName;
        public String OffsetToMobName
        {
            get
            {
                return offsetToMobName;
            }

            set
            {
                offsetToMobName = value;
                OnPropertyChanged("OffsetToMobName");
            }
        }

        String offsetToMobWid;
        public String OffsetToMobWid
        {
            get
            {
                return offsetToMobWid;
            }

            set
            {
                offsetToMobWid = value;
                OnPropertyChanged("OffsetToMobWid");
            }
        }

        String offsetToPlayersStruct;
        public String OffsetToPlayersStruct
        {
            get
            {
                return offsetToPlayersStruct;
            }

            set
            {
                offsetToPlayersStruct = value;
                OnPropertyChanged("OffsetToPlayersStruct");
            }
        }

        String offsetToBeginPlayersStruct;
        public String OffsetToBeginPlayersStruct
        {
            get
            {
                return offsetToBeginPlayersStruct;
            }

            set
            {
                offsetToBeginPlayersStruct = value;
                OnPropertyChanged("OffsetToBeginPlayersStruct");
            }
        }

        String offsetToPlayersCount;
        public String OffsetToPlayersCount
        {
            get
            {
                return offsetToPlayersCount;
            }

            set
            {
                offsetToPlayersCount = value;
                OnPropertyChanged("OffsetToPlayersCount");
            }
        }

        String msgId;
        public String MsgId
        {
            get
            {
                return msgId;
            }

            set
            {
                msgId = value;
                OnPropertyChanged("MsgId");
            }
        }

        String msgType;
        public String MsgType
        {
            get
            {
                return msgType;
            }

            set
            {
                msgType = value;
                OnPropertyChanged("MsgType");
            }
        }

        String msg_form1;
        public String Msg_form1
        {
            get
            {
                return msg_form1;
            }

            set
            {
                msg_form1 = value;
                OnPropertyChanged("Msg_form1");
            }
        }

        String msg_form2;
        public String Msg_form2
        {
            get
            {
                return msg_form2;
            }

            set
            {
                msg_form2 = value;
                OnPropertyChanged("Msg_form2");
            }
        }

        String msgWid;
        public String MsgWid
        {
            get
            {
                return msgWid;
            }

            set
            {
                msgWid = value;
                OnPropertyChanged("MsgWid");
            }
        }

        String invent_struct;
        public String Invent_struct
        {
            get
            {
                return invent_struct;
            }

            set
            {
                invent_struct = value;
                OnPropertyChanged("Invent_struct");
            }
        }

        String invent_struct_2;
        public String Invent_struct_2
        {
            get
            {
                return invent_struct_2;
            }

            set
            {
                invent_struct_2 = value;
                OnPropertyChanged("Invent_struct_2");
            }
        }

        String cellsCount;
        public String CellsCount
        {
            get
            {
                return cellsCount;
            }

            set
            {
                cellsCount = value;
                OnPropertyChanged("CellsCount");
            }
        }

        String itemInCellType;
        public String ItemInCellType
        {
            get
            {
                return itemInCellType;
            }

            set
            {
                itemInCellType = value;
                OnPropertyChanged("ItemInCellType");
            }
        }

        String itemInCellID;
        public String ItemInCellID
        {
            get
            {
                return itemInCellID;
            }

            set
            {
                itemInCellID = value;
                OnPropertyChanged("ItemInCellID");
            }
        }

        String itemInCellCount;
        public String ItemInCellCount
        {
            get
            {
                return itemInCellCount;
            }

            set
            {
                itemInCellCount = value;
                OnPropertyChanged("ItemInCellCount");
            }
        }

        String itemInCellPrice;
        public String ItemInCellPrice
        {
            get
            {
                return itemInCellPrice;
            }

            set
            {
                itemInCellPrice = value;
                OnPropertyChanged("ItemInCellPrice");
            }
        }

        String itemInCellName;
        public String ItemInCellName
        {
            get
            {
                return itemInCellName;
            }

            set
            {
                itemInCellName = value;
                OnPropertyChanged("ItemInCellName");
            }
        }

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //реализация интерфейса IClonable
        public object Clone()
        {
            return new Offset
            {
                Id = this.Id,
                BA = this.BA,
                GA = this.GA,
                Version = this.Version,
                GuiAdd = this.GuiAdd,
                SendPacket = this.SendPacket,
                AutoAttack = this.AutoAttack,
                UseSkill = this.UseSkill,
                Action_1 = this.Action_1,
                Action_2 = this.Action_2,
                Action_3 = this.Action_3,
                InviteCount = this.InviteCount,
                InviteStruct = this.InviteStruct,
                ChatStart = this.ChatStart,
                ChatNumber = this.ChatNumber,
                InviteWidPlayer = this.InviteWidPlayer,
                InviteWidParty = this.InviteWidParty,
                OffsetToGameAdress = this.OffsetToGameAdress,
                OffsetToPersStruct = this.OffsetToPersStruct,
                OffsetToParty = this.OffsetToParty,
                OffsetToCountParty = this.OffsetToCountParty,
                OffsetToName = this.OffsetToName,
                OffsetToClassID = this.OffsetToClassID,
                OffsetToMiningState = this.OffsetToMiningState,
                OffsetToWidWin_QuickAction = this.OffsetToWidWin_QuickAction,
                OffsetToX = this.OffsetToX,
                OffsetToY = this.OffsetToY,
                OffsetToZ = this.OffsetToZ,
                OffsetToWalkMode = this.OffsetToWalkMode,
                OffsetToWid = this.OffsetToWid,
                OffsetToTargetWid = this.OffsetToTargetWid,
                OffsetToStructParty = this.OffsetToStructParty,
                OffsetsLocationName_0 = this.OffsetsLocationName_0,
                OffsetsLocationName_1 = this.OffsetsLocationName_1,
                OffsetsLocationName_2 = this.OffsetsLocationName_2,
                OffsetsLocationName_3 = this.OffsetsLocationName_3,
                OffsetsLocationName_4 = this.OffsetsLocationName_4,
                OffsetToSkillsCount = this.OffsetToSkillsCount,
                OffsetToSkillsArray = this.OffsetToSkillsArray,
                OffsetToCurrentSkill = this.OffsetToCurrentSkill,
                OffsetToIdSkill = this.OffsetToIdSkill,
                OffsetToCdSkill = this.OffsetToCdSkill,
                OffsetToCountBufs = this.OffsetToCountBufs,
                OffsetToBufsArray = this.OffsetToBufsArray,
                OffsetToHashTables = this.OffsetToHashTables,
                OffsetToMobsStruct = this.OffsetToMobsStruct,
                OffsetToBeginMobsStruct = this.OffsetToBeginMobsStruct,
                OffsetToMobsCount = this.OffsetToMobsCount,
                OffsetToMobName = this.OffsetToMobName,
                OffsetToMobWid = this.OffsetToMobWid,
                OffsetToPlayersStruct = this.OffsetToPlayersStruct,
                OffsetToBeginPlayersStruct = this.OffsetToBeginPlayersStruct,
                OffsetToPlayersCount = this.OffsetToPlayersCount,
                MsgId = this.MsgId,
                MsgType = this.MsgType,
                Msg_form1 = this.Msg_form1,
                Msg_form2 = this.Msg_form2,
                MsgWid = this.MsgWid,
                Invent_struct = this.Invent_struct,
                Invent_struct_2 = this.Invent_struct_2,
                CellsCount = this.CellsCount,
                ItemInCellType = this.ItemInCellType,
                ItemInCellID = this.ItemInCellID,
                ItemInCellCount = this.ItemInCellCount,
                ItemInCellPrice = this.ItemInCellPrice,
                ItemInCellName = this.ItemInCellName
            };
        }
    }
}
