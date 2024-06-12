using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using лаба10;

namespace ClassLibrary1
{
    public class IdNumber : ICloneable
    {
        public int number;
        public IdNumber(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Error");
            }

            this.number = number;
        }

        public override string ToString()
        {
            return number.ToString();
        }
        public object Clone()
        {
            return new IdNumber(this.number); // Создаем новый объект IdNumber с тем же значением
        }
        public int ToInt()
        {
            return number;
        }
        public override bool Equals(object obj)
        {
            if (obj is IdNumber n)
            {
                return this.number == n.number;
            }
            return false;
        }
        
    }
}

