using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3rd_Assigment
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<string> stringList = new GenericList<string>(5) {"aaaaa", "bbbb", "cc", "dddddd"};
            foreach (string value in stringList)
            {
                Console.WriteLine(value);
            }

            Console.Read();
        }
    }
}
