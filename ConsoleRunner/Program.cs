using Philosopher;
using System;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            IOutputter outputter = new ConsoleOutputter();
            ///DiningPhilosopher.Start(5, outputter);
            ///Console.ReadKey(false);
            DiningPhilosopher.Start(5, 3, outputter);
            Console.WriteLine("\nPress any key to exit.");
        }
    }
}
