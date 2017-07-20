using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nirvana
{
    /// <summary>
    /// Класс, содержащий методы для убийства боссов
    /// </summary>
    public static class KillBoss
    {
        #region координаты боссов
        //маловероятно, что эти координаты когда-нибудь поменяются
        static readonly float[] point_1 = { 318.760162f, -72.09003f, 21.873148f };
        static readonly float[] point_2 = { 0, 2, 4 };
        static readonly float[] point_3 = { 0, 2, 4 };
        static readonly float[] point_4 = { 0, 2, 4 };
        static readonly float[] point_5 = { 0, 2, 4 };
        static readonly float[] point_6 = { 0, 2, 4 };
        static readonly float[] point_7 = { 0, 2, 4 };
        #endregion

        delegate void MyFunc(My_Windows mw);

        //заполняем словарь методами для каждого босса, ключом будет являться номер босса
        static Dictionary<Int32, MyFunc> dict = new Dictionary<Int32, MyFunc>()
        {
            { 1, KillBoss_1 },
            { 2, KillBoss_2 },
            { 3, KillBoss_3 },
            { 4, KillBoss_4 },
            { 5, KillBoss_5 },
            { 6, KillBoss_6 },
            { 7, KillBoss_7 }
        };

        /// <summary>
        /// Метод для запуска методов KillBoss всеми ботами
        /// </summary>
        public static void KillBossForAll(Int32 numberBoss)
        {
            //создаем коллекцию для тасков
            List<Task> tasks = new List<Task>();
            //для каждого бота, отмеченного галочкой, создаем таск
            for (Int32 iter = 0; iter < ListClients.work_collection.Count() - 1; iter++)
            {
                //времення переменная, помогающая избежать замыкания переменной iter
                Int32 tempIter = iter;
                //у пла нет галочек с настройками, он участвует по умолчанию
                if (iter == 0) tasks.Add(new Task(() => {dict[numberBoss](ListClients.work_collection[tempIter]);}));
                else
                {
                    if (ListClients.work_collection[iter] != null)
                        if (ListClients.work_collection[iter].ChatRead.R)
                            tasks.Add(new Task(() => {dict[numberBoss](ListClients.work_collection[tempIter]);}));
                }
            }
            //запускаем таски для всех ботов
            foreach (Task task in tasks)
                task.Start();
            //ждем, пока все боты завершат свои таски
            Task.WaitAll(tasks.ToArray());
        }


        static void KillBoss_1(My_Windows mw)
        {
            try
            {
                //следуем к первой точке
                SimonSayMethods.MoveTo(mw.Oph, point_1[0], point_1[1], point_1[2]);

                SimonSayMethods.Say(mw, "!!начинаю бить босса");
                //ищем босса поблизости
                Int32 bossWid = CalcMethods.MobSearch(mw.Oph, "Повелитель кругов ада");
                while (bossWid > 0)
                {
                    if (mw.ClassID == 0)
                    {
                        //здесь метод для вара
                    }
                    else
                    {
                        //здесь метод для остальных классов
                    }
                }
                Thread.Sleep(5000);
                SimonSayMethods.Say(mw, "!!закончил бить босса");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void KillBoss_2(My_Windows mw)
        {

        }
        public static void KillBoss_3(My_Windows mw)
        {

        }
        public static void KillBoss_4(My_Windows mw)
        {

        }
        public static void KillBoss_5(My_Windows mw)
        {

        }
        public static void KillBoss_6(My_Windows mw)
        {

        }
        public static void KillBoss_7(My_Windows mw)
        {

        }
    }
}
