using System;

namespace Philosopher
{
    /// <summary>
    /// Philosopher Interface
    /// </summary>
    public interface IPhilosopher
    {
        int Id { get; }
        void Eat(IUtencil leftChopstick, IUtencil rightChopstick, int EatingMinutes);
        void EatAndThink(IUtencil leftChopstick, IUtencil rightChopstick, int eatSec, int thinkSec, int times);
    }
    /// <summary>
    /// Defines the Outputter(aka looger/displayer) interface
    /// </summary>
    public interface IOutputter
    {
        void WriteLine(string s);
    }

    /// <summary>
    /// OutputterBase implemented lock to make it threadsafe
    /// </summary>
    public abstract class AbstractOutputter : IOutputter
    {
        private readonly object _locker = new object();
        public void WriteLine(string s)
        {  ///** make sure one thread user "WriteLine" at a time**
            lock (_locker)
            {
                _WriteLine(s);
            }
        }
        protected abstract void _WriteLine(string s);
    }

    /// <summary>
    /// Utencil interface
    /// </summary>
    public interface IUtencil
    {
        int Id { get; }
    }


    /// <summary>
    /// Generic Singlton Factory base to any Factory class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FactoryBase<T> where T : class
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);
        public static T Instance => instance.Value;
    }
}
