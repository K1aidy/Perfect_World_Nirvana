using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Threading;

namespace Nirvana
{
    /// <summary>
    /// Класс, выполняющий сложные расчеты
    /// </summary>
    public static class CalcMethods
    {
        /// <summary>
        /// считываем Int32 по одному оффсету
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static Int32 CalcInt32Value(IntPtr handle, Int32 address)
        {
            IntPtr read;
            byte[] buffer = new byte[4];
            WinApi.ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out read);
            Int32 x = BitConverter.ToInt32(buffer, 0);
            return x;
        }

        /// <summary>
        /// считываем значение Int32 по нашей цепочке оффсетов
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Int32 ReadInt(IntPtr handle, int address, int[] offset)
        {
            byte[] buffer = new byte[4];
            Int32 value = address;
            value = CalcInt32Value(handle, value);
            if (offset.Length > 0)
            {
                for (int i = 0; i < offset.Length - 1; i++)
                {
                    value = CalcInt32Value(handle, value + offset[i]);
                }
                value = CalcInt32Value(handle, value + offset[offset.Length - 1]);
            }
            return value;
        }

        /// <summary>
        /// считываем Float по одному оффсету
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static float CalcFloatValue(IntPtr handle, Int32 address)
        {
            IntPtr read;
            byte[] buffer = new byte[50];
            WinApi.ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out read);
            float x = BitConverter.ToSingle(buffer, 0);
            return x;
        }

        /// <summary>
        /// считываем значение Float по нашей цепочке оффсетов
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static float ReadFloat(IntPtr handle, int address, int[] offset)
        {
            byte[] buffer = new byte[50];
            var value = address;
            value = CalcInt32Value(handle, value);
            if (offset.Length > 0)
            {
                for (int i = 0; i < offset.Length - 1; i++)
                {
                    value = CalcInt32Value(handle, value + offset[i]);
                }
            }
            return CalcFloatValue(handle, value + offset[offset.Length - 1]); ;
        }

        /// <summary>
        /// считываем значение String по нашей цепочке оффсетов
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadString(IntPtr handle, int address, int[] offset)
        {
            byte[] buffer = new byte[4];
            var value = address;
            value = CalcInt32Value(handle, value);
            if (offset.Length > 0)
            {
                for (int i = 0; i < offset.Length - 1; i++)
                {
                    value = CalcInt32Value(handle, value + offset[i]);
                }
            }
            return CalcStringValue(handle, value + offset[offset.Length - 1]); ;
        }

        /// <summary>
        /// считываем String по одному оффсету
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string CalcStringValue(IntPtr handle, Int32 address)
        {
            IntPtr read;
            byte[] buffer = new byte[300];
            WinApi.ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out read);
            string x = Encoding.Unicode.GetString(buffer);
            return (x.IndexOf('\0') != -1) ? x.Substring(0, x.IndexOf('\0')) : x;
        }

        /// <summary>
        /// считываем Byte по одному оффсету
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static byte CalcByteValue(IntPtr handle, Int32 address)
        {
            IntPtr read;
            byte[] buffer = new byte[1];
            WinApi.ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out read);
            return buffer[0];
        }

        /// <summary>
        /// считываем значение Int32 по нашей цепочке оффсетов
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte ReadByte(IntPtr handle, int address, int[] offset)
        {
            byte[] buffer = new byte[50];
            var value = address;
            value = CalcInt32Value(handle, value);
            if (offset.Length > 0)
            {
                for (int i = 0; i < offset.Length - 1; i++)
                {
                    value = CalcInt32Value(handle, value + offset[i]);
                }
            }
            return CalcByteValue(handle, value + offset[offset.Length - 1]); ;
        }

        /// <summary>
        /// Поиск wid по названию NPC/моба/пета,
        /// возвращает первый встретившийся,
        /// если ничего не найдено, то возвращает 0.
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int MobSearch(IntPtr oph, string name)
        {
            int mobs_count = ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsMobsCount);
            for (int i = 0; i < mobs_count; i++)
            {
                string mob_name = ReadString(oph, Offsets.BaseAdress, Offsets.OffsetsNameMob(i));
                if (mob_name.Length > 0)
                {
                    //если имя моба/NPC/пета совпадает с заданным, возвращает его wid
                    if (mob_name.IndexOf(name) != -1)
                        return ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWidMob(i));
                }             
            }
            return 0;
        }

        /// <summary>
        /// считываем значение String по нашей цепочке оффсетов с кодировкой ASCII
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadString_ASCII(IntPtr handle, int address, int[] offset)
        {
            byte[] buffer = new byte[4];
            var value = address;
            value = CalcInt32Value(handle, value);
            if (offset.Length > 0)
            {
                for (int i = 0; i < offset.Length - 1; i++)
                {
                    value = CalcInt32Value(handle, value + offset[i]);
                }
            }
            return CalcStringValue_ASCII(handle, value + offset[offset.Length - 1]); ;
        }

        /// <summary>
        /// считываем String по одному оффсету с кодировкой ASCII
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string CalcStringValue_ASCII(IntPtr handle, Int32 address)
        {
            IntPtr read;
            byte[] buffer = new byte[300];
            WinApi.ReadProcessMemory(handle, (IntPtr)address, buffer, buffer.Length, out read);
            string x = Encoding.ASCII.GetString(buffer);
            return (x.IndexOf('\0') != -1) ? x.Substring(0, x.IndexOf('\0')) : x;
        }

        /// <summary>
        /// Вывод записи в лог, принимает 1 параметр string.
        /// </summary>
        /// <param name="log"></param>
        public static void Logging(string log)
        {
            MainWindow.loging_box.Dispatcher.Invoke
                        (DispatcherPriority.Background, new
                             Action(() =>
                             {
                                 //MainWindow.loging_box += log + "\n";
                                 MainWindow.loging_box.ScrollToEnd();
                             }
                             )
                        );
        }

        /// <summary>
        /// Узнаем адрес контрола активного окна указанного по ID процесса клиента
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public static int[] CalcControlAddress(IntPtr oph)
        {
            int[] result = { 0, 0 };
            string name_control = "";
            int[] offset_win_struct = { 0x1c, 0x18, 0x8, 0x74 };
            int address_win_struct = ReadInt(oph, Offsets.BaseAdress, offset_win_struct);

            int temp_address = CalcInt32Value(oph, address_win_struct + 0x1cc);
            for (int k = 0; k < 50; k++)
            {
                int window_address = CalcInt32Value(oph, temp_address + 0x4);
                for (int j = 0; j < k; j++)
                {
                    window_address = CalcInt32Value(oph, window_address + 0x4);
                }
                int controlstruct_address = CalcInt32Value(oph, window_address + 0x8);
                window_address = CalcInt32Value(oph, controlstruct_address + 0x18);
                name_control = CalcStringValue_ASCII(oph, window_address + 0x0);
                if (name_control.IndexOf("Btn_Back") != -1)
                {
                    int address_to_command_control = CalcInt32Value(oph, controlstruct_address + 0x1c);
                    result[0] = address_win_struct;
                    result[1] = address_to_command_control;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// Узнаем адрес активного окна
        /// </summary>
        /// <param name="oph"></param>
        /// <returns></returns>
        public static int CalcAddressActiveWindow(IntPtr oph)
        {
            int[] offset_win_struct = { 0x1c, 0x18, 0x8, 0x74 };
            return ReadInt(oph, Offsets.BaseAdress, offset_win_struct);
        }

        /// <summary>
        /// Узнаем параметры для GUI инжекта по названию неактивного окна и названию контрола
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="window_name"></param>
        /// <param name="control_name"></param>
        /// <returns></returns>
        public static int[] CalcWindowAddress(IntPtr oph, string window_name, string control_name)
        {
            int[] result = { 0, 0};
            string name_control = "";
            string name_window = "";
            int[] offset_win_struct = { 0x1c, 0x18, 0x8, 0x8C};
            int address_win_struct = ReadInt(oph, Offsets.BaseAdress, offset_win_struct);
            int windowstruct_adress = 0;

            for (int k = 0; k<1000; k++)
            {
                int window_address = CalcInt32Value(oph, address_win_struct + 0x0);
                for (int j = 0; j < k; j++)
                {
                    window_address = CalcInt32Value(oph, window_address + 0x0);
                }
                windowstruct_adress = CalcInt32Value(oph, window_address + 0x8);
                window_address = CalcInt32Value(oph, windowstruct_adress + 0x4c);
                name_window = CalcStringValue_ASCII(oph, window_address + 0x0);
                if (name_window.IndexOf(window_name) != -1)
                {
                    result[0] = windowstruct_adress;
                    break;
                }
            }

            int temp_address = CalcInt32Value(oph, windowstruct_adress + 0x1cc);
            for (int k = 0; k < 50; k++)
            {
                int window_address = CalcInt32Value(oph, temp_address + 0x4);
                for (int j = 0; j < k; j++)
                {
                    window_address = CalcInt32Value(oph, window_address + 0x4);
                }
                int controlstruct_address = CalcInt32Value(oph, window_address + 0x8);
                window_address = CalcInt32Value(oph, controlstruct_address + 0x18);
                name_control = CalcStringValue_ASCII(oph, window_address + 0x0);
                if (name_control.IndexOf(control_name) != -1)
                {
                    int address_to_command_control = CalcInt32Value(oph, controlstruct_address + 0x1c);
                    result[1] = address_to_command_control;
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// Запись значения по указанной области памяти указанного процесса
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="value"></param>
        /// <param name="adress"></param>
        public static void WriteProcessBytes(IntPtr oph, int value, int adress)
        {
            byte[] new_value = BitConverter.GetBytes(value);
            int number_of_bytes_written;
            WinApi.WriteProcessMemory(oph, adress, new_value, new_value.Length, out number_of_bytes_written);
        }

        /// <summary>
        /// Поиск среди ближайших игроков по wid
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="wid"></param>
        /// <returns></returns>
        public static bool SearchPlayerNearby(IntPtr oph, int wid)
        {
            //считаем количество народа вокруг
            int player_count = ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsPlayersCount);
            for (int i = 0; i < player_count; i++)
            {
                //считываем wid каждого
                int player_wid = ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWidPlayer(i));
                //если найден нужный wid, выходим и возвращаем true
                if (player_wid == wid)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ищем wid таргета по wid персонажа, с которого берем ассист
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="wid"></param>
        /// <returns></returns>
        public static int TargetPlayerWid(IntPtr oph, uint wid)
        {
            //считаем количество народа вокруг
            int player_count = ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsPlayersCount);
            for (int i = 0; i < player_count; i++)
            {
                //считываем wid каждого
                int player_wid = ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsWidPlayer(i));
                //если найден нужный wid, выходим и возвращаем true
                if (player_wid == wid)
                {
                    return ReadInt(oph, Offsets.BaseAdress, Offsets.OffsetsTargetWidPlayer(i));
                }
            }
            return -1;
        }
    }
}
