using ClassLibrary1;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace лаба10
{
    public class MusicalInstrument : IInit, IComparable<MusicalInstrument>, ICloneable, IStr, IComparable
    {
        Random rnd = new Random();
        
        protected string name;
        public IdNumber num;
        //public int Num = Convert.ToInt32(num.ToString());
        static string[] Names = { "ROCKDALE STARS BLACK", "IBANEZ GRX70QA-TRB", "YAMAHA F310", "YAMAHA C40", "ROLAND FP-30X-BK" };
        public string Name
        {
            get { return name; }
            set
            {

                
                if (value == null)
                {
                    throw new Exception("пустая строка");
                }
                name = value;
            }
        }
        public string Name1 { get => name;set { name = value; } }
        public bool HasCharacters
        {
            get { return !string.IsNullOrEmpty(name); }
        }

        public MusicalInstrument(string name, int number)
        {
            Name = name;
            num = new IdNumber(number);
        }
        public MusicalInstrument(string name, IdNumber num)
        {
            Name = name;
           
        }

        public MusicalInstrument()
        {
            Name = "";
            num = new IdNumber(1);
        }

        public virtual void Show()
        {
            Console.WriteLine($"Название: {Name} ");
        }

        public override bool Equals(object obj)
        {
            if (obj is MusicalInstrument m)
            {
                return this.Name == m.Name;
            }
            return false;
        }

        public override string ToString()
        {
            return name;
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public virtual string ToString1()
        {
            return name;
        }

        public virtual void Init()
        {

            Console.WriteLine("Введите название");
            Name = Console.ReadLine();
        }
        public virtual void Istr(string n)
        {


            Name = n;
        }
        public virtual void RandomInit()
        {

            Name = Names[rnd.Next(Names.Length)];

        }

        public virtual object Clone()
        {
            var instrument = (MusicalInstrument)MemberwiseClone();
            instrument.name = (string)name.Clone();
            instrument.num = (IdNumber)num.Clone(); // Если IdNumber также требует глубокого клонирования

            return instrument;
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
        public string GetName(MusicalInstrument m)
        {
            return m.Name;
        }
        
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MusicalInstrument otherInstrument = obj as MusicalInstrument;
            if (otherInstrument != null)
                return this.Name.CompareTo(otherInstrument.Name);
            else
                throw new ArgumentException("Объект не музыкальный элемент");
        }
        //public virtual object Clone()
        //{
        //    var instrument = (MusicalInstrument)MemberwiseClone();
        //    instrument.Name = (string)Name.Clone();
           
        //    return instrument;
        //}
        public static void ViewInstruments(MusicalInstrument[] instruments)
        {
            Console.WriteLine("Обычный просмотр элементов массива:");
            foreach (var instrument in instruments)
            {
                Console.WriteLine(instrument.ToString());
            }
        }
        public static void ViewInstrumentsVirtual(MusicalInstrument[] instruments)
        {
            Console.WriteLine("Виртуальный просмотр элементов массива:");
            foreach (var instrument in instruments)
            {
                instrument.Show();
            }
        }
        public int CompareTo(MusicalInstrument other)
        {
            if (other == null) return 1;
            return this.Name.CompareTo(other.Name);
        }



    }
}
