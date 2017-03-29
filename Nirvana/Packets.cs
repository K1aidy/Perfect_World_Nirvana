using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana
{
    /// <summary>
    /// Класс для работы с пакетами
    /// </summary>
    class Packets
    {

        const UInt32 INFINITE = 0xFFFFFFFF;
        const UInt32 WAIT_OBJECT_0 = 0x00000000;
        IntPtr oph;

        #region Покинуть отряд
        //отправка пакета "покинуть отряд"
        int leavePartyAddress;
        byte[] leavePartyAddressRev;
        byte[] leavePartyPkt = new byte[] { 0x1E, 0x00 };

        public void leaveParty()
        {
            //Вычисляем размер пакета
            int packetSize = leavePartyPkt.Length;

            if (leavePartyAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(leavePartyPkt, ref leavePartyAddress, ref leavePartyAddressRev);
            }

            sendPacket(leavePartyAddressRev, packetSize);
        }
        #endregion

        #region Пригласить в отряд
        //отправка пакета "пригласить в отряд"
        private int invitePartyAddress;
        private byte[] invitePartyAddressRev;
        private byte[] invitePartyPkt = new byte[]
        {
            0x1B, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00     //playerId
        };

        public void inviteParty(int playerId)
        {
            //Узнаем размер пакета
            int packetSize = invitePartyPkt.Length;

            if (invitePartyAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(invitePartyPkt, ref invitePartyAddress, ref invitePartyAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, invitePartyAddress + 2, playerIdRev);

            sendPacket(invitePartyAddressRev, packetSize);
        }
        #endregion

        #region Кикнуть из пати
        //отправка пакета "выгнать из отряда"
        private int kickFromAddress;
        private byte[] kickFromPartyAddressRev;
        private byte[] kickFromPartyPkt = new byte[]
        {
            0x1F, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00      //playerId
        };

        public void kickFromParty(int playerId)
        {
            //Узнаем размер пакета
            int packetSize = kickFromPartyPkt.Length;

            if (kickFromAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(kickFromPartyPkt, ref kickFromAddress, ref kickFromPartyAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, kickFromAddress + 2, playerIdRev);

            sendPacket(kickFromPartyAddressRev, packetSize);
        }
        #endregion

        #region Сменить ПЛ
        //отправка пакета "сменить ПЛ"
        private int changePlAddress;
        private byte[] changePlAddressRev;
        private byte[] changePlPkt = new byte[]
        {
            0x48, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00     //playerId
        };

        public void ChangePl(int playerId)
        {
            //Get size of the packet
            int packetSize = changePlPkt.Length;

            if (changePlAddress == 0)
            {
                //load packet in memory
                loadPacket(changePlPkt, ref changePlAddress, ref changePlAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, changePlAddress + 2, playerIdRev);

            sendPacket(changePlAddressRev, packetSize);
        }
        #endregion

        #region Принять пати
        //Отправка пакета "принять пати"
        private int acceptPartyInviteAddress;
        private byte[] acceptPartyInviteAddressRev;

        private byte[] acceptPartyInvitePkt = new byte[]
        {
            0x1C, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00,     //playerId
            0x00, 0x00, 0x00, 0x00      //partyInviteCounter
        };

        public void acceptPartyInvite(int playerId, int partyInviteCounter)
        {
            //Узнаем размер пакета
            int packetSize = acceptPartyInvitePkt.Length;

            if (acceptPartyInviteAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(acceptPartyInvitePkt, ref acceptPartyInviteAddress, ref acceptPartyInviteAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, acceptPartyInviteAddress + 2, playerIdRev);

            byte[] partyInviteCounterRev = BitConverter.GetBytes(partyInviteCounter);
            partyInviteCounterRev.Reverse();
            MemWriteBytes(oph, acceptPartyInviteAddress + 6, partyInviteCounterRev);

            sendPacket(acceptPartyInviteAddressRev, packetSize);
        }
        #endregion

        #region Призыв шамана
        //отправка пакета "призыв шамана"
        private int callShamanAddress;
        private byte[] callShamanAddressRev;
        private byte[] callShamanPkt = new byte[]
        {
            0x29, 0x00, 0x20, 0x07, 0x00, 0x00, 0x00, 0x01,                 //Header
            0x00, 0x00, 0x00, 0x00     //playerId
        };

        public void callShamanParty(int playerId)
        {
            //Узнаем размер пакета
            int packetSize = callShamanPkt.Length;

            if (callShamanAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(callShamanPkt, ref callShamanAddress, ref callShamanAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, callShamanAddress + 8, playerIdRev);

            sendPacket(callShamanAddressRev, packetSize);
        }
        #endregion

        #region Принять призыв шамана
        //отправка пакета "принять призыв шамана"
        private int acceptСallShamanAddress;
        private byte[] acceptСallShamanAddressRev;
        private byte[] acceptСallShamanPkt = new byte[]
        {
            0x82, 0x00, 0x02, 0x01,                 //Header
            0x00, 0x00, 0x00, 0x00     //playerId
        };

        public void acceptCallShamanParty(int playerId)
        {
            //Узнаем размер пакета
            int packetSize = acceptСallShamanPkt.Length;

            if (acceptСallShamanAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(acceptСallShamanPkt, ref acceptСallShamanAddress, ref acceptСallShamanAddressRev);
            }

            byte[] playerIdRev = BitConverter.GetBytes(playerId);
            playerIdRev.Reverse();
            MemWriteBytes(oph, acceptСallShamanAddress + 4, playerIdRev);

            sendPacket(acceptСallShamanAddressRev, packetSize);
        }
        #endregion

        #region Выделить нпса
        //отправка пакета "выделить нпса"
        private int selectNpcAddress;
        private byte[] selectNpcAddressRev;
        private byte[] selectNpcPkt = new byte[]
        {
            0x02, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00     //npcWid
        };

        public void selectNpc(int npcWid)
        {
            //Узнаем размер пакета
            int packetSize = selectNpcPkt.Length;

            if (selectNpcAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(selectNpcPkt, ref selectNpcAddress, ref selectNpcAddressRev);
            }

            byte[] npcWidRev = BitConverter.GetBytes(npcWid);
            npcWidRev.Reverse();
            MemWriteBytes(oph, selectNpcAddress + 2, npcWidRev);

            sendPacket(selectNpcAddressRev, packetSize);
        }
        #endregion

        #region Поговорить с нпсом
        //отправка пакета "поговорить с нпсом"
        private int talkToNpcAddress;
        private byte[] talkToNpcAddressRev;
        private byte[] talkToNpcPkt = new byte[]
        {
            0x23, 0x00,                 //Header
            0x00, 0x00, 0x00, 0x00     //npcWid
        };

        public void talkToNpc(int npcWid)
        {
            //Узнаем размер пакета
            int packetSize = talkToNpcPkt.Length;

            if (talkToNpcAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(talkToNpcPkt, ref talkToNpcAddress, ref talkToNpcAddressRev);
            }

            byte[] npcWidRev = BitConverter.GetBytes(npcWid);
            npcWidRev.Reverse();
            MemWriteBytes(oph, talkToNpcAddress + 2, npcWidRev);

            sendPacket(talkToNpcAddressRev, packetSize);
        }
        #endregion

        #region Взять квест
        //отправка пакета "взять квест"
        private int takeQuestAddress;
        private byte[] takeQuestAddressRev;
        private byte[] takeQuestPkt = new byte[]
        {
            0x25, 0x00, 0x07, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00,         //begin
            0x00, 0x00,                                                         //questWid
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00          //end
        };

        public void takeQuest(int questWid)
        {
            //Узнаем размер пакета
            int packetSize = takeQuestPkt.Length;

            if (takeQuestAddress == 0)
            {
                //Загружаем пакет в память
                loadPacket(takeQuestPkt, ref takeQuestAddress, ref takeQuestAddressRev);
            }

            byte[] questWidRev = BitConverter.GetBytes(questWid);
            questWidRev.Reverse();
            MemWriteBytes(oph, takeQuestAddress + 10, questWidRev);

            sendPacket(takeQuestAddressRev, packetSize);
        }
        #endregion

        /// <summary>
        /// Метод отправки пакета
        /// </summary>
        /// <param name="packetLocation"></param>
        /// <param name="packetSize"></param>
        public void sendPacket(byte[] packetLocation, int packetSize)
        {
            if (sendPacketOpcodeAddress == 0)
            {
                loadSendPacketOpcode();
            }

            MemWriteBytes(oph, packetAddressLocation, packetLocation);
            MemWriteByte(oph, packetSizeAddress, (byte)packetSize);

            //Запускаем опткод
            IntPtr threadHandle = CreateRemoteThread(oph, sendPacketOpcodeAddress);

            //Ждем завершения
            WaitForSingleObject(threadHandle);

            //Закрываем поток
            WinApi.CloseHandle(threadHandle);
        }

        /// <summary>
        /// Метод загрузки пакета
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="packetAddress"></param>
        /// <param name="packetAddressRev"></param>
        private void loadPacket(byte[] packet, ref int packetAddress, ref byte[] packetAddressRev)
        {
            //По заданному дескриптору резервируем память для нашего пакета
            packetAddress = (int)WinApi.VirtualAllocEx(oph, (IntPtr)0, packet.Length, WinApi.AllocationType.Commit, WinApi.MemoryProtection.ExecuteReadWrite);

            //Записываем пакет в зарезервированную память
            MemWriteBytes(oph, packetAddress, packet);

            //Реверсируем пакет, так как в памяти пакеты читаются с конца
            packetAddressRev = BitConverter.GetBytes(packetAddress);

            packetAddressRev.Reverse();
        }

        /// <summary>
        /// Метод записи в память, написан для читабельности кода
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public static void MemWriteBytes(IntPtr processHandle, int address, byte[] value)
        {
            bool success;
            int nBytesRead = 0;
            success = WinApi.WriteProcessMemory(processHandle, address, value, value.Length, out nBytesRead);
        }

        /// <summary>
        /// Ожидаем завершения функции, при проблемах завершения выдаст ошибку
        /// </summary>
        /// <param name="threadHandle"></param>
        public static void WaitForSingleObject(IntPtr threadHandle)
        {
            if (WinApi.WaitForSingleObject(threadHandle, INFINITE) != WAIT_OBJECT_0)
            {
                CalcMethods.Logging("Failed waiting for single object");
            }
        }

        /// <summary>
        /// Метод запуска функции в памяти
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IntPtr CreateRemoteThread(IntPtr processHandle, int address)
        {
            IntPtr x = IntPtr.Zero;
            return WinApi.CreateRemoteThread(processHandle, (IntPtr)0, 0, (IntPtr)address, (IntPtr)0, 0, out x);
        }

        /// <summary>
        /// Метод записи одного байта в память
        /// </summary>
        /// <param name="processHandle"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public static void MemWriteByte(IntPtr processHandle, int address, byte value)
        {
            bool success;
            byte[] buffer = BitConverter.GetBytes(value);
            int nBytesRead = 0;
            success = WinApi.WriteProcessMemory(processHandle, address, buffer, 1, out nBytesRead);
        }

        //Рабочие переменные - адреса
        private int packetAddressLocation;
        private int packetSizeAddress;
        private int sendPacketOpcodeAddress;

        //Конструктор
        public Packets(IntPtr oph)
        {
            this.oph = oph;
        }

        //Опткод для отправки
        private byte[] sendPacketOpcode = new byte[]
        {
            0x60,                                   //PUSHAD
            0xB8, 0x00, 0x00, 0x00, 0x00,           //MOV EAX, SendPacketAddress
            0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00,     //MOV ECX, DWORD PTR [realBaseAddress]
            0x8B, 0x49, 0x20,                       //MOV ECX, DWORD PTR [ECX+20]
            0xBF, 0x00, 0x00, 0x00, 0x00,           //MOV EDI, packetAddress
            0x6A, 0x00,                             //PUSH packetSize
            0x57,                                   //PUSH EDI
            0xFF, 0xD0,                             //CALL EAX
            0x61,                                   //POPAD
            0xC3                                    //RET
        };

        /// <summary>
        /// Метод для загрузки опткода
        /// </summary>
        private void loadSendPacketOpcode()
        {
            //Резервирование памяти
            sendPacketOpcodeAddress = (int)WinApi.VirtualAllocEx(oph, (IntPtr)0, sendPacketOpcode.Length, WinApi.AllocationType.Commit, WinApi.MemoryProtection.ExecuteReadWrite);

            //Запись в память
            MemWriteBytes(oph, sendPacketOpcodeAddress, sendPacketOpcode);

            //Загружаем в записанный в память опткод инвертированные BaseAddress и SendPacket
            byte[] functionAddress = BitConverter.GetBytes(Offsets.SendPacket);
            functionAddress.Reverse();
            byte[] realBaseAddress = BitConverter.GetBytes(Offsets.BaseAdress);
            realBaseAddress.Reverse();
            MemWriteBytes(oph, sendPacketOpcodeAddress + 2, functionAddress);
            MemWriteBytes(oph, sendPacketOpcodeAddress + 8, realBaseAddress);
            packetAddressLocation = sendPacketOpcodeAddress + 16;
            packetSizeAddress = sendPacketOpcodeAddress + 21;
        }

    }
}
