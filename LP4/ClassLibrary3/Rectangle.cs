using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class Rectangle
    {
        public Rectangle()
        {
        }

        public Rectangle(int length)
        {
            this.Length = length;
        }

        public int Length
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Convert.ToString(this.Length);
        }
    }
}
