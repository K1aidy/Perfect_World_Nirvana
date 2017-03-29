using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;

namespace Nirvana
{
    /// <summary>
    /// Класс, описывающий отельное сообщение в чате
    /// </summary>
    class Message
    {
        int number;
        int type;
        int id;
        string msg_1;
        string msg_2;
        uint wid;

        /// <summary>
        /// Номер собщения в данном сеансе
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        public int Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Номер сообщения в клиенте
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Первая форма сообщения
        /// </summary>
        public string Msg_1
        {
            get
            {
                return msg_1;
            }
        }

        /// <summary>
        /// Вторая форма сообщения
        /// </summary>
        public string Msg_2
        {
            get
            {
                return msg_2;
            }
        }

        /// <summary>
        /// Уникальный WID человека, отправившего сообщение
        /// </summary>
        public uint Wid
        {
            get
            {
                return wid;
            }
        }

        /// <summary>
        /// Конструктор сообщения
        /// </summary>
        /// <param name="number"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="msg_1"></param>
        /// <param name="msg_2"></param>
        /// <param name="wid"></param>
        public Message(int number, int type, int id, string msg_1, string msg_2, uint wid)
        {
            this.wid = wid;
            this.number = number;
            this.type = type;
            this.id = id;
            this.msg_1 = msg_1;
            this.msg_2 = msg_2;
        }

        /// <summary>
        /// Метод для вывода лога о сообщении
        /// </summary>
        public void InfoMessage()
        {
            CalcMethods.Logging("++++++++++++++");
            CalcMethods.Logging(type.ToString());
            CalcMethods.Logging(id.ToString());
            CalcMethods.Logging(wid.ToString());
            CalcMethods.Logging(msg_1);
            CalcMethods.Logging(msg_2);
            /*string puth = Environment.CurrentDirectory + "\\log.txt";
            using (StreamWriter sm = new StreamWriter(puth, true, Encoding.Default))
            //StreamWriter fstream = new StreamWriter(Environment.CurrentDirectory + "\\log.txt", false, FileMode.Append))
            {
                // запись в файл
                sm.WriteLine(id.ToString());
                // запись в файл
                sm.WriteLine(type.ToString());
                // запись в файл
                sm.WriteLine(wid.ToString());
                // запись в файл
                sm.WriteLine(msg_1);
                // запись в файл
                sm.WriteLine(msg_2);
                sm.WriteLine("");
            }*/
        }
    }
}
