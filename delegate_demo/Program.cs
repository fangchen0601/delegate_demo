using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using System.Diagnostics;

namespace delegate_demo
{
    public delegate int MyDelegate3(int x);

    public delegate void MyDelegate4();

    public enum SortOrder { FirstName, LastName, BirthDate }

    class Program
    {
        

        static void Main(string[] args)
        {
            MyDelegate d = new MyDelegate((x) => Console.WriteLine(x));
            d("Hello World!");

            var d2 = new MyDelegate2((x) => x + 0.1);
            Console.WriteLine(d2(1));

            var d3 = new MyDelegate3(calculate_square);
            Console.WriteLine(d3.Invoke(3));

            var d4 = new MyDelegate4(Method1);
            var d5 = new MyDelegate4(Method2);
            MyDelegate4 d6 = d4 + d5;
            d6.Invoke();

            Func<int, int> get_square = calculate_square;
            Console.WriteLine(get_square(5));

            Action myDelegateMethod1 = Method1;
            myDelegateMethod1.Invoke();

            Action myDelegateDemo = () =>
            {
                Console.WriteLine("hi");
            };
            myDelegateDemo.Invoke();

            List<int> myIntList = new List<int> { 1, 3, 5, 99, 100, 200, 500, 999 };
            var myBigNubmers = myIntList.Where(IsBigNumber);
            Console.WriteLine(String.Join(",", myBigNubmers.Select(x => x.ToString())));

            List<int> excludeList = Exclude(myIntList, IsBigNumber);
            Console.WriteLine(String.Join(",", excludeList.Select(x => x.ToString())));

            IEnumerable<Person> people = new List<Person>() { 
                new Person("fang", "chen", new DateTime(1982,7,6)),
                new Person("fang2", "chen2", new DateTime(1982,7,5)),
                new Person("fang3", "chen3", new DateTime(1982,7,4))
            };
            var newOrderedPeople = people.OrderBy(GetSortFunction(SortOrder.BirthDate));
            Console.WriteLine(String.Join("; ", newOrderedPeople.Select(p =>p.ToString())));
        }

        public delegate void MyDelegate(string text);

        public delegate Double MyDelegate2(int val);

        public static int calculate_square(int x)
        {
            return x * x;
        }
        
        public static void Method1()
        {
            Console.WriteLine("Inside Method1");
        }

        public static void Method2()
        {
            Console.WriteLine("Inside Method2");
        }

        public static bool IsBigNumber(int x)
        {
            return x > 100;
        }

        public static List<T> Exclude<T>(List<T> values, Func<T, bool> condition)
        {
            return values.Where(v => !condition(v)).ToList();
        }

        public static Func<Person, IComparable> GetSortFunction(SortOrder sortOrder)
        {
            switch(sortOrder)
            {
                case SortOrder.FirstName:
                    return person => person.FirstName;
                case SortOrder.LastName:
                    return person => person.LastName;
                default:
                    return person => person.BirthDate;
            }
        }
    }
}
