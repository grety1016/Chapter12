using System;
using System.Linq;
using static System.Console;

namespace LinqWithObject
{
    class Program
    {
        static bool NameLongerThanFour(string name)
        {
            return name.Length > 4;
        }
        static void LinqWithArrayOfString()
        {
            var names = new string[]{"Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed" };
            var query = names.Where(name => name.Length > 4);
                foreach (string item in query)
                {
                    WriteLine(item) ;
                }
        }
        static void Main(string[] args)
        {
             LinqWithArrayOfString();
        }
    }
}
