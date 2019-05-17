using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosopher
{

    public class Philosopher : IPhilosopher
    {
        private int _id;
        private IOutputter _writer;
        public int Id { get => _id; }
        private Philosopher(int ID, IOutputter writer)
        {
            _id = ID; _writer = writer;
        }

        public void Eat(IUtencil leftChopstick, IUtencil rightChopstick, int secs)
        {
            lock (leftChopstick)                // Grab utencil on the left
            {
                lock (rightChopstick)           // Grab utencil on the right
                {
                    // Eat here
                    _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] + ({leftChopstick.Id})({rightChopstick.Id}) chopstick and start to eat for { secs} secounds...");
                    Thread.Sleep(secs * 1000);
                    _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] end eating.");
                }
            }
            _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] - ({leftChopstick.Id})({rightChopstick.Id}) chopstick.");
        }

        private void Think(int secs)
        {
            _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] start thinking for {secs} secounds...");
            ///Task.Delay(secs * 1000);
            Thread.Sleep(secs * 1000);
            _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] end thinking.");
        }

        public void EatAndThink(IUtencil leftChopstick, IUtencil rightChopstick, int eatSec, int thinkSec, int times)
        {
            _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] will do Eat-n-Think {times} times. Think for {thinkSec} secs and eat for {eatSec} secs ...");
            for (int i = 0; i<times; i++)
            {
                Eat(leftChopstick, rightChopstick, eatSec);
                Think(thinkSec);
            }
            _writer.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.FFFF").PadRight(13, '0')} : P[{_id}] finish eat and think!!!");
        }

    }
    public class PhilosopherFactory : FactoryBase<PhilosopherFactory>
    {
        public IPhilosopher GetPhilosopher(int id, IOutputter outputWriter)
        {
            object[] args = { id, outputWriter };
            return Activator.CreateInstance(typeof(Philosopher),
                BindingFlags.NonPublic | BindingFlags.Instance, null,
                    args, null) as IPhilosopher;
        }
    }

    public class DiningPhilosopher
    {
        private static readonly Random rnd = new Random();
        public static void Start(int numPhilosophers, IOutputter outputter)
        {
            var chopsticks = new Dictionary<int, IUtencil>(numPhilosophers);
            for (int i = 0; i<numPhilosophers; i++)
            {
                chopsticks.Add(i, new Chopstick(i));
            }

            Task[] philosophers = new Task[numPhilosophers];
            for (int i = 0; i < numPhilosophers; ++i)
            {
                // First philosopher [0] take utencil [-1] + [0] where [-1] == NumofPhilosopher -1
                int left = i - 1 < 0 ? numPhilosophers - 1 : i - 1;
                int right = i;
                int sec = rnd.Next(1, 10);
                IPhilosopher P = PhilosopherFactory.Instance.GetPhilosopher(i, outputter);
                philosophers[i] = new Task(() => P.Eat(chopsticks[left], chopsticks[right], sec));
            }

            // May eat!
            Parallel.ForEach(philosophers, t =>
            {
                t.Start();
            });

            // Wait for all philosophers to finish their dining
            Task.WaitAll(philosophers);
        }

        public static void Start(int numPhilosophers, int timesOfEating, IOutputter outputter)
        {
            var chopsticks = new Dictionary<int, IUtencil>(numPhilosophers);
            for (int i = 0; i < numPhilosophers; i++)
            {
                chopsticks.Add(i, new Chopstick(i));
            }

            Task[] philosophers = new Task[numPhilosophers];
            for (int i = 0; i < numPhilosophers; ++i)
            {
                // First philosopher [0] take utencil [-1] + [0] where [-1] == NumofPhilosopher -1
                int left = i - 1 < 0 ? numPhilosophers - 1 : i - 1;
                int right = i;
                int secEat = rnd.Next(1, 10);
                int secThink = rnd.Next(1, 10);
                Philosopher P = (Philosopher)PhilosopherFactory.Instance.GetPhilosopher(i, outputter);
                philosophers[i] = new Task(() => P.EatAndThink(chopsticks[left], chopsticks[right], secEat, secThink, timesOfEating));
            }

            // May eat!
            Parallel.ForEach(philosophers, t =>
            {
                t.Start();
            });

            // Wait for all philosophers to finish their dining
            Task.WaitAll(philosophers);

        }
    }


}
