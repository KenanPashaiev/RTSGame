using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using RTS;

namespace RTS
{
    public class Troop
    {
        public int UnitsCount => UnitList.Count;
        public int AttackUnitsCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < UnitList.Count; i++)
                {
                    if (UnitList[i] is AttackUnit)
                        count++;
                }
                return count;
            }
        }
        public int DefenceUnitsCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < UnitList.Count; i++)
                {
                    if (UnitList[i] is DefenceUnit)
                        count++;
                }
                return count;
            }
        }
        public int SpeedUnitsCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < UnitList.Count; i++)
                {
                    if (UnitList[i] is SpeedUnit)
                        count++;
                }
                return count;
            }
        }

        public float TroopHealth => UnitList.Sum(unit => unit.Health);
        public float TroopAttack => UnitList.Sum(unit => unit.Attack);
        public float TroopDefence => UnitList.Sum(unit => unit.Defence);
        public float TroopSpeed => UnitList.Min(unit => unit.Speed);

        public List<Unit> UnitList { get; }

        public Troop(List<Unit> unitList)
        {
            UnitList = unitList;
        }
    }
}
