using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class SpeedUnit : Unit
    {
        public static float DefaultAttack = 3;
        public static float DefaultDefence = 1;
        public static float DefaultSpeed = 4;

        public SpeedUnit(Barracks barrack) : base(barrack)
        {
            Attack = DefaultAttack + _barrack.UnitLevelBonus;
            Defence = DefaultDefence + _barrack.UnitLevelBonus;
            Speed = DefaultSpeed;
        }
    }
}
