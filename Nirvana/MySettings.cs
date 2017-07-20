using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana
{
    [Serializable]
    public class MySettings
    {
        #region Состояние чекбоксов "следовать", "бафать" и "дебафать"
        List<List<Boolean>> checkbox_set = new List<List<Boolean>>(11);
        #endregion

        //чекбокс "смена ПЛ при пересборе"
        Boolean changePl;

        String party_and_pl;
        String peresbor_v_nirku;
        String rebaf;
        String peresbor;
        String to_him;
        String pechat;
        String stop;
        String downloader;
        String userId_1;
        String userId_2;


        public String Party_and_pl
        {
            get
            {
                return party_and_pl;
            }

            set
            {
                party_and_pl = value;
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
            }
        }

        public String Pechat
        {
            get
            {
                return pechat;
            }

            set
            {
                pechat = value;
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
            }
        }

        public List<List<Boolean>> Checkbox_set
        {
            get
            {
                return checkbox_set;
            }

            set
            {
                checkbox_set = value;
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
            }
        }

        public String Downloader
        {
            get
            {
                return downloader;
            }

            set
            {
                downloader = value;
            }
        }

        public String UserId_1
        {
            get
            {
                return userId_1;
            }

            set
            {
                userId_1 = value;
            }
        }

        public String UserId_2
        {
            get
            {
                return userId_2;
            }

            set
            {
                userId_2 = value;
            }
        }
    }
}
