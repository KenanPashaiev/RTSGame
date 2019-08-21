using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RTS
{
    public class Barracks : Facility, IBarracks
    {
        public List<Unit> UnitList;

        public int UnitsCount  => AttackUnitsCount + DefenceUnitsCount + SpeedUnitsCount;
        public int AttackUnitsCount
        {
            get
            {
                int count = 0;
                for(int i = 0; i < UnitList.Count; i++)
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

        public readonly static int UnitTrainingCreditsCost = 10;
        public readonly static int UnitTrainingProductsCost = 10;
        public readonly static int UnitTrainingResidentsCost = 1;
        
        public int UnitLimit { get; private set; }
        public float UnitLevelBonus { get; private set; }

        public float UnitsHealth => UnitList.Sum(unit => unit.Health);
        public float UnitsAttack => UnitList.Sum(unit => unit.Attack) + UnitsCount * UnitLevelBonus;
        public float UnitsDefence => UnitList.Sum(unit => unit.Defence) + UnitsCount * UnitLevelBonus;
        public float UnitsSpeed => UnitList.Sum(unit => unit.Speed);

        public Barracks(Base parentBase) : base(parentBase)
        {
            UnitList = new List<Unit>();

            UnitLimit = 200;
            UnitLevelBonus = 0;
        }

        public bool CanTrainUnit
        {
            get
            {
                var isEnoughResidents = _base.ResidentsCount >= UnitTrainingResidentsCost;
                var isEnoughProducts = _base.ProductsCount >= UnitTrainingProductsCost;
                var isEnoughCredits = _base.CreditsCount >= UnitTrainingCreditsCost;
                var isEnoughPlace = UnitsCount < UnitLimit;

                var canTrainUnit = isEnoughCredits && isEnoughProducts
                                   && isEnoughResidents && isEnoughPlace;

                return canTrainUnit;
            }
        }

        public bool TrainUnit(UnitType unitType)
        {
            if (!CanTrainUnit)
            {
                return false;
            }

            _base.AcquireResidents(UnitTrainingResidentsCost);
            _base.AcquireProducts(UnitTrainingProductsCost);
            _base.AcquireCredits(UnitTrainingCreditsCost);

            Unit unit = null;

            switch (unitType)
            {
                case UnitType.attackUnit:
                    {
                        unit = new AttackUnit(this);
                        break;
                    }
                case UnitType.defenceUnit:
                    {
                        unit = new DefenceUnit(this);
                        break;
                    }
                case UnitType.speedUnit:
                    {
                        unit = new SpeedUnit(this);
                        break;
                    }
            }
            UnitList.Add(unit);

            return true;
        }

        public Unit AcquireUnit(UnitType unitType)
        {
            Unit unit = null;

            for(int i = 0; i < UnitList.Count; i++)
            {
                if(UnitList[i].UnitType == unitType)
                {
                    unit = UnitList[i];
                    UnitList.Remove(unit);
                    break;
                }
            }
            return unit;
        }

        public bool AddUnit(Unit unit)
        {
            var isEnoughSpace = UnitsCount < UnitLimit;

            if (!isEnoughSpace)
            {
                return false;
            }

            UnitList.Add(unit);
            return true;
        }

        public Unit AcquireRandomUnit()
        {
            System.Random random = new System.Random();

            var randomUnit = UnitList[random.Next(0, UnitList.Count)];
            UnitList.Remove(randomUnit);
            return randomUnit;
        }

        public override bool LevelUp()
        {
            var isEnoughProducts = LevelUpPrice < _base.ProductsCount;
            var isEnoughCredits = LevelUpPrice < _base.CreditsCount;

            var canLevelUp = isEnoughProducts && isEnoughCredits;

            if (!canLevelUp)
            {
                return false;
            }

            _base.AcquireProducts(LevelUpPrice);
            _base.AcquireCredits(LevelUpPrice);

            UnitLimit += 100;
            UnitLevelBonus += 0.1f;

            Level++;

            return true;
        }
    }
}
