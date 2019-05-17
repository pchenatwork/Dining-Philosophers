using Philosopher;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner
{
    class ConsoleOutputter : AbstractOutputter
    {
        protected override void _WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
