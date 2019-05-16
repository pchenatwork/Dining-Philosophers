using Philosopher;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner
{
    class ConsoleOutputter : IOutputter
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
