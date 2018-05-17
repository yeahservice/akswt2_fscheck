using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckCarAlarm
{
    class Program
    {
        static void Main(string[] args)
        {
            new CarAlarmSpec()
                .ToProperty()
                .QuickCheck();

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
