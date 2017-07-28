using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana
{
    public class MySettings : INotifyPropertyChanged
    {
        //чекбокс "смена ПЛ при пересборе"
        Boolean changePl;

        String party_and_pl;
        String peresbor_v_nirku;
        String rebaf;
        String peresbor;
        String to_him;
        String fiznirka;
        String stop;

        #region Состояние чекбоксов "следовать", "бафать" и "дебафать"
        Boolean check_1_1;
        Boolean check_1_2;
        Boolean check_1_3;
        Boolean check_2_1;
        Boolean check_2_2;
        Boolean check_2_3;
        Boolean check_3_1;
        Boolean check_3_2;
        Boolean check_3_3;
        Boolean check_4_1;
        Boolean check_4_2;
        Boolean check_4_3;
        Boolean check_5_1;
        Boolean check_5_2;
        Boolean check_5_3;
        Boolean check_6_1;
        Boolean check_6_2;
        Boolean check_6_3;
        Boolean check_7_1;
        Boolean check_7_2;
        Boolean check_7_3;
        Boolean check_8_1;
        Boolean check_8_2;
        Boolean check_8_3;
        Boolean check_9_1;
        Boolean check_9_2;
        Boolean check_9_3;
        Boolean check_10_1;
        Boolean check_10_2;
        Boolean check_10_3;
        #endregion

        public String Party_and_pl
        {
            get
            {
                return party_and_pl;
            }

            set
            {
                party_and_pl = value;
                OnPropertyChanged("Party_and_pl");
            }
        }
        public String Peresbor_v_nirku
        {
            get
            {
                return peresbor_v_nirku;
            }

            set
            {
                peresbor_v_nirku = value;
                OnPropertyChanged("Peresbor_v_nirku");
            }
        }
        public String Rebaf
        {
            get
            {
                return rebaf;
            }

            set
            {
                rebaf = value;
                OnPropertyChanged("Rebaf");
            }
        }
        public String Peresbor
        {
            get
            {
                return peresbor;
            }

            set
            {
                peresbor = value;
                OnPropertyChanged("Peresbor");
            }
        }
        public String Fiznirka
        {
            get
            {
                return fiznirka;
            }

            set
            {
                fiznirka = value;
                OnPropertyChanged("Fiznirka");
            }
        }
        public String To_him
        {
            get
            {
                return to_him;
            }

            set
            {
                to_him = value;
                OnPropertyChanged("To_him");
            }
        }
        public String Stop
        {
            get
            {
                return stop;
            }

            set
            {
                stop = value;
                OnPropertyChanged("Stop");
            }
        }
        public Boolean ChangePl
        {
            get
            {
                return changePl;
            }

            set
            {
                changePl = value;
                OnPropertyChanged("ChangePl");
            }
        }

        #region Чекбоксы
        public Boolean Check_1_1
        {
            get
            {
                return check_1_1;
            }

            set
            {
                check_1_1 = value;
                OnPropertyChanged("check_1_1");
            }
        }
        public Boolean Check_1_2
        {
            get
            {
                return check_1_2;
            }

            set
            {
                check_1_2 = value;
                OnPropertyChanged("check_1_2");
            }
        }
        public Boolean Check_1_3
        {
            get
            {
                return check_1_3;
            }

            set
            {
                check_1_3 = value;
                OnPropertyChanged("check_1_3");
            }
        }

        public Boolean Check_2_1
        {
            get
            {
                return check_2_1;
            }

            set
            {
                check_2_1 = value;
                OnPropertyChanged("check_2_1");
            }
        }
        public Boolean Check_2_2
        {
            get
            {
                return check_2_2;
            }

            set
            {
                check_2_2 = value;
                OnPropertyChanged("check_2_2");
            }
        }
        public Boolean Check_2_3
        {
            get
            {
                return check_2_3;
            }

            set
            {
                check_2_3 = value;
                OnPropertyChanged("check_2_3");
            }
        }

        public Boolean Check_3_1
        {
            get
            {
                return check_3_1;
            }

            set
            {
                check_3_1 = value;
                OnPropertyChanged("check_3_1");
            }
        }
        public Boolean Check_3_2
        {
            get
            {
                return check_3_2;
            }

            set
            {
                check_3_2 = value;
                OnPropertyChanged("check_3_2");
            }
        }
        public Boolean Check_3_3
        {
            get
            {
                return check_3_3;
            }

            set
            {
                check_3_3 = value;
                OnPropertyChanged("check_3_3");
            }
        }

        public Boolean Check_4_1
        {
            get
            {
                return check_4_1;
            }

            set
            {
                check_4_1 = value;
                OnPropertyChanged("check_4_1");
            }
        }
        public Boolean Check_4_2
        {
            get
            {
                return check_4_2;
            }

            set
            {
                check_4_2 = value;
                OnPropertyChanged("check_4_2");
            }
        }
        public Boolean Check_4_3
        {
            get
            {
                return check_4_3;
            }

            set
            {
                check_4_3 = value;
                OnPropertyChanged("check_4_3");
            }
        }

        public Boolean Check_5_1
        {
            get
            {
                return check_5_1;
            }

            set
            {
                check_5_1 = value;
                OnPropertyChanged("check_5_1");
            }
        }
        public Boolean Check_5_2
        {
            get
            {
                return check_5_2;
            }

            set
            {
                check_5_2 = value;
                OnPropertyChanged("check_5_2");
            }
        }
        public Boolean Check_5_3
        {
            get
            {
                return check_5_3;
            }

            set
            {
                check_5_3 = value;
                OnPropertyChanged("check_5_3");
            }
        }

        public Boolean Check_6_1
        {
            get
            {
                return check_6_1;
            }

            set
            {
                check_6_1 = value;
                OnPropertyChanged("check_6_1");
            }
        }
        public Boolean Check_6_2
        {
            get
            {
                return check_6_2;
            }

            set
            {
                check_6_2 = value;
                OnPropertyChanged("check_6_2");
            }
        }
        public Boolean Check_6_3
        {
            get
            {
                return check_6_3;
            }

            set
            {
                check_6_3 = value;
                OnPropertyChanged("check_6_3");
            }
        }

        public Boolean Check_7_1
        {
            get
            {
                return check_7_1;
            }

            set
            {
                check_7_1 = value;
                OnPropertyChanged("check_7_1");
            }
        }
        public Boolean Check_7_2
        {
            get
            {
                return check_7_2;
            }

            set
            {
                check_7_2 = value;
                OnPropertyChanged("check_7_2");
            }
        }
        public Boolean Check_7_3
        {
            get
            {
                return check_7_3;
            }

            set
            {
                check_7_3 = value;
                OnPropertyChanged("check_7_3");
            }
        }

        public Boolean Check_8_1
        {
            get
            {
                return check_8_1;
            }

            set
            {
                check_8_1 = value;
                OnPropertyChanged("check_8_1");
            }
        }
        public Boolean Check_8_2
        {
            get
            {
                return check_8_2;
            }

            set
            {
                check_8_2 = value;
                OnPropertyChanged("check_8_2");
            }
        }
        public Boolean Check_8_3
        {
            get
            {
                return check_8_3;
            }

            set
            {
                check_8_3 = value;
                OnPropertyChanged("check_8_3");
            }
        }

        public Boolean Check_9_1
        {
            get
            {
                return check_9_1;
            }

            set
            {
                check_9_1 = value;
                OnPropertyChanged("check_9_1");
            }
        }
        public Boolean Check_9_2
        {
            get
            {
                return check_9_2;
            }

            set
            {
                check_9_2 = value;
                OnPropertyChanged("check_9_2");
            }
        }
        public Boolean Check_9_3
        {
            get
            {
                return check_9_3;
            }

            set
            {
                check_9_3 = value;
                OnPropertyChanged("check_9_3");
            }
        }

        public Boolean Check_10_1
        {
            get
            {
                return check_10_1;
            }

            set
            {
                check_10_1 = value;
                OnPropertyChanged("check_10_1");
            }
        }
        public Boolean Check_10_2
        {
            get
            {
                return check_10_2;
            }

            set
            {
                check_10_2 = value;
                OnPropertyChanged("check_10_2");
            }
        }
        public Boolean Check_10_3
        {
            get
            {
                return check_10_3;
            }

            set
            {
                check_10_3 = value;
                OnPropertyChanged("check_10_3");
            }
        }
        #endregion

        //временные трудности с архитектурой приложения, в будущем будет исправлено
        public Boolean GetValueCheckBox(Int32 firstIndex, Int32 secondIndex)
        {
            switch (firstIndex)
            {
                case 1:
                    switch (secondIndex)
                    {
                        case 1: return Check_1_1;
                        case 2: return Check_2_1;
                        case 3: return Check_3_1;
                        case 4: return Check_4_1;
                        case 5: return Check_5_1;
                        case 6: return Check_6_1;
                        case 7: return Check_7_1;
                        case 8: return Check_8_1;
                        case 9: return Check_9_1;
                        case 10: return Check_10_1;
                    }
                    return false;
                case 2:
                    switch (secondIndex)
                    {
                        case 1: return Check_1_2;
                        case 2: return Check_2_2;
                        case 3: return Check_3_2;
                        case 4: return Check_4_2;
                        case 5: return Check_5_2;
                        case 6: return Check_6_2;
                        case 7: return Check_7_2;
                        case 8: return Check_8_2;
                        case 9: return Check_9_2;
                        case 10: return Check_10_2;
                    }
                    return false;
                case 3:
                    switch (secondIndex)
                    {
                        case 1: return Check_1_3;
                        case 2: return Check_2_3;
                        case 3: return Check_3_3;
                        case 4: return Check_4_3;
                        case 5: return Check_5_3;
                        case 6: return Check_6_3;
                        case 7: return Check_7_3;
                        case 8: return Check_8_3;
                        case 9: return Check_9_3;
                        case 10: return Check_10_3;
                    }
                    return false;
                default:
                    return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
