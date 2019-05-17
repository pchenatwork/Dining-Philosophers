using System;
using System.Collections.Generic;
using System.Text;

namespace Philosopher
{
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
}
