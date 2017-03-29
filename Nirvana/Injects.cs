using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana
{
    /// <summary>
    /// Класс для работы с инжектами
    /// </summary>
    class Injects
    {
        /// <summary>
        /// Инжект для GUI элементов
        /// </summary>
        /// <param name="win_struct"></param>
        /// <param name="command_text"></param>
        /// <param name="processID"></param>
        public static void GUI_Inject(int win_struct, int command_text, IntPtr oph)
        {
            try
            {
                // ---- Создаем скелет пакета для инжектирования
                byte[] gui_packet =
                {
                0x60,                           //Pushad
                0xB9, 0x0, 0x0, 0x0, 0x0,       //Mov_ECX + win_struct_address
                0x68, 0x0, 0x0, 0x0, 0x0,       //Push68 + command_text_address
                0xB8, 0x0, 0x0, 0x0, 0x0,       //Mov_EAX + call_address
                0xFF, 0xD0,                     //Call_EAX
                0x61,                           //Popad
                0xC3                            //Ret
            };

                // ---- заменяем указанные эелементы пакета адресом для GUI инжектирования
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.GuiAdress), 0, gui_packet, 12, 4);
                // ---- заменяем указанные эелементы пакета адресом структуры необходимого окна
                Buffer.BlockCopy(BitConverter.GetBytes(win_struct), 0, gui_packet, 2, 4);
                // ---- заменяем указанные эелементы пакета адресом функции необходимого контрола
                Buffer.BlockCopy(BitConverter.GetBytes(command_text), 0, gui_packet, 7, 4);
                // ---- временные переменные
                int lpNumberOfBytesWritten = 0;
                IntPtr lpThreadId;
                // ---- выделяем место в памяти
                IntPtr gui_address = WinApi.VirtualAllocEx(oph, IntPtr.Zero, 20, WinApi.AllocationType.Commit, WinApi.MemoryProtection.ReadWrite);
                // ---- записываем в выделенную память наш пакет
                WinApi.WriteProcessMemory(oph, (int)gui_address, gui_packet, 20, out lpNumberOfBytesWritten);
                // ---- запускаем записанную в память функцию
                IntPtr hProcThread = WinApi.CreateRemoteThread(oph, IntPtr.Zero, 0, gui_address, IntPtr.Zero, 0, out lpThreadId);
                // ---- Ожидаем завершения функции
                WinApi.WaitForSingleObject(hProcThread, WinApi.INFINITE);
                // ---- подчищаем за собой
                WinApi.VirtualFreeEx(oph, gui_address, 20, WinApi.FreeType.Release);
                WinApi.VirtualFreeEx(oph, hProcThread, 20, WinApi.FreeType.Release);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Инжект движения
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="fly_mode"></param>
        public static void WalkTo(IntPtr oph, float X, float Y, float Z, int walk)
        {
            try
            {
                //так как при walk_mode=2 надо инжектить значение 1
                int walk_mode = 1;
                if (walk == 0) walk_mode = 0;

                // ---- Создаем скелет пакета для инжектирования
                #region my_inject   
                byte[] walk_packet =
                {
                0x60,                                       //pushad
                0xB8, /*2*/0x00, 0x00, 0x00, 0x00,          //mov eax, BA
                0x8B, 0x00,                                 //mox eax, dword ptr [eax]
                0x8B, 0x40, 0x1c,                           //mov eax, dword ptr[eax + 1C]
                0x8B, 0x78, 0x34,                           //mov edi, dword ptr[eax + 0x34]
                0x8B, 0x8F, 0x4C, 0x15, 0x00, 0x00,         //mov ecx, dword ptr[edi + 0x154C]
                0x6A, 0x01,                                 //push 1
                0xB8, /*23*/0x00, 0x00, 0x00, 0x00,         //mov eax, action_1
                0xFF, 0xD0,                                 //call eax
                0x8D, 0x54, 0x24, 0x1C,                     //lea edx, dword ptr[esp + 0x1C]
                0x8B, 0xD8,                                 //mov ebx, eax
                0x52,                                       //push edx
                0x68, /*37*/0x00, 0x00, 0x00, 0x00,         //push walk_mode
                0x8B, 0xCB,                                 //mov ecx, ebx
                0xB8, /*44*/0x00, 0x00, 0x00, 0x00,         //mov eax, action_2
                0xFF, 0xD0,                                 //call eax
                0x8B, 0x8F, 0x4C, 0x15, 0x00, 0x00,         //mov ecx, dword ptr [edi + 0x154C]
                0xB8, /*57*/0x00, 0x00, 0x00, 0x00,         //mov eax, x
                0x89, 0x43, 0x20,                           //mov dword ptr[ebx + 0x20], eax
                0xB8, /*65*/0x00, 0x00, 0x00, 0x00,         //mov eax, z
                0x89, 0x43, 0x24,                           //mov dword ptr[ebx + 0x24], eax
                0xB8, /*73*/0x00, 0x00, 0x00, 0x00,         //mov eax, y
                0x89, 0x43, 0x28,                           //mov dword ptr[ebx + 0x28], eax
                0x6A, 0x00,                                 //push 0
                0x53,                                       //push ebx
                0x6A, 0x01,                                 //push 1
                0xB8, /*86*/0x00, 0x00, 0x00, 0x00,         //mov eax, action_2
                0xFF, 0xD0,                                 //call eax
                0x61,                                       //popad
                0xC3                                        //ret
            };
                #endregion

                // ---- пишем BA
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.BaseAdress), 0, walk_packet, 2, 4);

                // ---- пишем Action_1, Action_2, Action_3
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.Action_1), 0, walk_packet, 23, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.Action_2), 0, walk_packet, 44, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.Action_3), 0, walk_packet, 86, 4);

                // ---- пишем walk_mode
                Buffer.BlockCopy(BitConverter.GetBytes(walk_mode), 0, walk_packet, 37, 4);
                // ---- пишем X, Y, Z
                Buffer.BlockCopy(BitConverter.GetBytes(X), 0, walk_packet, 57, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Z * 2), 0, walk_packet, 65, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Y), 0, walk_packet, 73, 4);
                // ---- временные переменные
                int lpNumberOfBytesWritten = 0;
                IntPtr lpThreadId;
                // ---- выделяем место в памяти
                IntPtr walk_address = WinApi.VirtualAllocEx(oph, IntPtr.Zero, walk_packet.Length, WinApi.AllocationType.Commit, WinApi.MemoryProtection.ReadWrite);
                // ---- записываем в выделенную память наш пакет
                WinApi.WriteProcessMemory(oph, (int)walk_address, walk_packet, walk_packet.Length, out lpNumberOfBytesWritten);
                // ---- запускаем записанную в память функцию
                IntPtr hProcThread = WinApi.CreateRemoteThread(oph, IntPtr.Zero, 0, walk_address, IntPtr.Zero, 0, out lpThreadId);
                // ---- Ожидаем завершения функции
                WinApi.WaitForSingleObject(hProcThread, WinApi.INFINITE);
                // ---- подчищаем за собой
                WinApi.VirtualFreeEx(oph, walk_address, walk_packet.Length, WinApi.FreeType.Release);
                WinApi.VirtualFreeEx(oph, hProcThread, walk_packet.Length, WinApi.FreeType.Release);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        /// <summary>
        /// Инжекст скилла
        /// </summary>
        /// <param name="skill_id"></param>
        /// <param name="oph"></param>
        public static void Skill_Inject(int skill_id, IntPtr oph)
        {
            try
            {
                // ---- Создаем скелет пакета для инжектирования
                byte[] skill_packet =
                {
                 0x60,                                 //pushad
                 0x6A, 0xFF,                           //push -1
                 0x6A, 0x00,                           //push 0
                 0x6A, 0x00,                           //push 0
                 0x68, 0x00, 0x00, 0x00, 0x00,         //pusk skill_id
                 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00,   //mov ecx, dword ptr [BA]
                 0x8B, 0x89, 0x00, 0x00, 0x00, 0x00,   //mov ecx, dword ptr [ecx + 0x1C]
                 0x8B, 0x89, 0x00, 0x00, 0x00, 0x00,   //mov ecx, dword ptr [ecx + 0x34]
                 0xB8, 0x00, 0x00, 0x00, 0x00,         //mov edx, P1
                 0xFF, 0xD0,                           //call P1
                 0x61,                                 //popad
                 0xC3                                  //ret
            };

                // ---- заменяем указанные эелементы пакета на ID скилла
                Buffer.BlockCopy(BitConverter.GetBytes(skill_id), 0, skill_packet, 8, 4);
                // ---- заменяем указанные эелементы пакета на BA
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.BaseAdress), 0, skill_packet, 14, 4);
                // ---- заменяем указанные эелементы пакета на офсет к GA
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.OffsetToGameAdress), 0, skill_packet, 20, 4);
                // ---- заменяем указанные эелементы пакета на офсет к структуре персонажа
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.OffsetToPersStruct), 0, skill_packet, 26, 4);
                // ---- заменяем указанные эелементы пакета на ID скилла
                Buffer.BlockCopy(BitConverter.GetBytes(Offsets.UseSkill), 0, skill_packet, 31, 4);

                // ---- временные переменные
                int lpNumberOfBytesWritten = 0;
                IntPtr lpThreadId;
                // ---- выделяем место в памяти
                IntPtr skill_address = WinApi.VirtualAllocEx(oph, IntPtr.Zero, skill_packet.Length, WinApi.AllocationType.Commit, WinApi.MemoryProtection.ReadWrite);
                // ---- записываем в выделенную память наш пакет
                WinApi.WriteProcessMemory(oph, (int)skill_address, skill_packet, skill_packet.Length, out lpNumberOfBytesWritten);
                // ---- запускаем записанную в память функцию
                IntPtr hProcThread = WinApi.CreateRemoteThread(oph, IntPtr.Zero, 0, skill_address, IntPtr.Zero, 0, out lpThreadId);
                // ---- Ожидаем завершения функции
                WinApi.WaitForSingleObject(hProcThread, WinApi.INFINITE);
                // ---- подчищаем за собой
                WinApi.VirtualFreeEx(oph, skill_address, skill_packet.Length, WinApi.FreeType.Release);
                WinApi.VirtualFreeEx(oph, hProcThread, skill_packet.Length, WinApi.FreeType.Release);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
