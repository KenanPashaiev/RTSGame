using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class DefenceUnit : Unit
    {
        public static float DefaultAttack = 2;
        public static float DefaultDefence = 4;
        public static float DefaultSpeed = 2;

        public DefenceUnit(Barracks barrack) : base(barrack)
        {
            Attack = DefaultAttack + _barrack.UnitLevelBonus;
            Defence = DefaultDefence + _barrack.UnitLevelBonus;
            Speed = DefaultSpeed;
        }
    }
}
