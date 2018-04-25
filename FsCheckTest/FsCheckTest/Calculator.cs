using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsCheckTest
{
    public class Calculator
    {
        public int add(int a, int b)
        {
            if (a > 10) return 42;
            return a + b;
        }
    }
}
