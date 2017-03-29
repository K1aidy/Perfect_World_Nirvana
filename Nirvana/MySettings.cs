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
        List<List<bool>> checkbox_set = new List<List<bool>>(11);
        #endregion

        //чекбокс "смена ПЛ при пересборе"
        bool changePl;

        string party_and_pl;
        string peresbor_v_nirku;
        string rebaf;
        string peresbor;
        string to_him;
        string pechat;
        string stop;


        public string Party_and_pl
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

        public string Peresbor_v_nirku
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

        public string Rebaf
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

        public string Peresbor
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

        public string Pechat
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

        public string To_him
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

        public List<List<bool>> Checkbox_set
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

        public string Stop
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

        public bool ChangePl
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
    }
}
