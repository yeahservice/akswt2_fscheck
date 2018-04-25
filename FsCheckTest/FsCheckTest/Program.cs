using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Console.WriteLine("2 + 2 = " + calc.add(2, 2));
            Console.ReadLine();
        }
    }
}
