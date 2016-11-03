using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_life
{
    class amoebe
    {
        public Int32 name { get; set; }
        public Int32 age { get; set; }
        public Int32 xPos { get; set; }
        public Int32 yPos { get; set; }
        public Boolean furtile = true;

        public amoebe.gender sex { get; set; }
        public enum gender
        {
            m = "male";
            f = "Female";
        }

    }
}
