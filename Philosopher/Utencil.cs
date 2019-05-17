using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Philosopher
{
    public class Chopstick : IUtencil
    {
        private int _id;
        public Chopstick(int Id)
        {
            _id = Id;
        }
        public int Id { get => _id; }
    }
}
