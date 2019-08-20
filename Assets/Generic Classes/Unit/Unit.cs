using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public abstract class Unit
    {
        protected readonly Barracks _barrack;

        public float Health { get; protected set; }
        public float Attack { get; protected set; }
        public float Defence { get; protected set; }
        public float Speed { get; protected set; }

        protected Unit(Barracks barrack)
        {
            _barrack = barrack;
            Health = 5;
            Attack = 0;
            Defence = 0;
            Speed = 0;
        }
    }
}
