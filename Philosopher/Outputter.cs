using System;
using System.Collections.Generic;
using System.Text;

namespace Philosopher
{
    public interface IOutputter
    {
        void WriteLine(string s);
    }

    public class ConsoleOutputter : IOutputter
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
