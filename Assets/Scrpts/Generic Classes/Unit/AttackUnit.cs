using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class AttackUnit : Unit
    {
        public static float DefaultAttack = 5;
        public static float DefaultDefence = 1;
        public static float DefaultSpeed = 2;

        public AttackUnit(Barracks barrack) : base(barrack)
        {
            UnitType = UnitType.attackUnit;
            Attack = DefaultAttack + _barrack.UnitLevelBonus;
            Defence = DefaultDefence + _barrack.UnitLevelBonus;
            Speed = DefaultSpeed;
        }
    }
}
