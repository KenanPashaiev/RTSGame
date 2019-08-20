using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public abstract class Facility
    {
        protected readonly Base _base;

        public int Level { get; protected set; }

        public int LevelUpPrice { get { return (int)Math.Round(Level * 100 / 0.985f); } }

        public Facility(Base parentBase)
        {
            _base = parentBase;
            Level = 1;
        }

        public virtual bool LevelUp()
        {
            return true;
        }
    }
}