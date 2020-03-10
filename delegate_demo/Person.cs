using System;
using System.Collections.Generic;
using System.Text;

namespace delegate_demo
{
    public class Person
    {
        public String FirstName;
        public DateTime BirthDate;
        public String LastName;

        public Person(String f, String l, DateTime b)
        {
            FirstName = f;
            LastName = l;
            BirthDate = b;
        }

        public override string ToString()
        {
            return FirstName + ", " + LastName + ", " + BirthDate.ToString();
        }
    }
}
