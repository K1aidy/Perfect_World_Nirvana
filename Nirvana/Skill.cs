using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nirvana
{
    public class Skill
    {
        
        int id;
        int number;

        public Skill(int id, int number)
        {
            this.id = id;
            this.number = number;
        }
        /// <summary>
        /// ID скилла
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// номер скилла в массиве скиллов персонажа
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
        }
    }
}
