using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Nirvana.Models.BotModels
{
    /// <summary>
    /// Класс для обновления комбобокса со списком запущенных персонажей по таймеру
    /// </summary>
    public class ListClients
    {
        /// <summary>
        /// Коллекция коллекций, к которым привязаны комбобоксы, 
        /// обновляется по таймеру только при изменении количества запущенных персонажей
        /// </summary>
        public static ObservableCollection<ObservableCollection<My_Windows>> my_windows_clients = new ObservableCollection<ObservableCollection<My_Windows>>
        {
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>(),
            new ObservableCollection<My_Windows>()

        };
        /// <summary>
        /// Коллекция, к которой привязн комбобокс, 
        /// обновляется по таймеру только при изменении количества запущенных персонажей
        /// </summary>
        public static ObservableCollection<My_Windows> my_windows = new ObservableCollection<My_Windows>();

        /// <summary>
        /// Коллекция запущенных персонажей, 
        /// обновляется каждый тик таймера
        /// </summary>
        private static ObservableCollection<My_Windows> my_windows_temp = new ObservableCollection<My_Windows>();

        /// <summary>
        /// Массив для работы
        /// </summary>
        public static My_Windows[] work_collection = new My_Windows[11];

        /// <summary>
        /// Коллекция исключений для комбобоксов
        /// </summary>
        public static ObservableCollection<string> exList = new ObservableCollection<string> { null, null, null, null, null, null, null, null, null, null, null };

        /// <summary>
        /// Метод обновления списка клиентов
        /// </summary>
        public static void Count_Clients()
        {
            Refresh();
        }

        /// <summary>
        /// Обновление коллекции клиентов
        /// </summary>
        private static void Refresh()
        {
            // Задаем начало отсчета
            IntPtr hwnd = IntPtr.Zero;
            my_windows.Clear();
            //В бесконечном цикле перебираем все запущенные окна с классом ElementClient Window
            while (true)
            {
                //очищаем коллекцию клиентов и начинаем заполнять заново
                //получаем следующее окно с классом ElementClient Window. 
                hwnd = WinApi.FindWindowEx(IntPtr.Zero, hwnd, "ElementClient Window", null);
                //Если наткнулись на ноль - значит выходим 
                if (hwnd == IntPtr.Zero) break;
                
                //добавляем элемент в нашу коллекцию
                My_Windows my_wind = new My_Windows(hwnd);
                if (my_wind.Name.Length > 0)
                {
                    my_windows.Add(my_wind);
                }
            }
            RefreshAllCombobox();
        }

        /// <summary>
        /// Обновление коллекций, привязанных к комбобоксам
        /// </summary>
        public static void RefreshAllCombobox()
        {
            foreach (ObservableCollection<My_Windows> mw_coll in my_windows_clients)
                mw_coll.Clear();
            //чистим коллекции
            foreach (My_Windows mw in my_windows)
                foreach (ObservableCollection<My_Windows> mw_coll in my_windows_clients)
                    mw_coll.Add(mw);
        }

        /// <summary>
        /// Проверяет, указали ли мы пла и пересборщика пати
        /// </summary>
        /// <returns></returns>
        public bool StateWorkCollection()
        {
            if (work_collection[0] != null && work_collection[10] != null)
                return true;
            else
                return false;
        }

    }
}
