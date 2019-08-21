using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class BattleResult
{
    public Troop AttackerTroop;

    private Base DefendingBase;

    public BattleResult(Troop attackingTroop, Base defendingBase)
    {
        AttackerTroop = attackingTroop;
        DefendingBase = defendingBase;
    }

    public void CalculateBattleResult()
    {
        var troopPower = Mathf.Max(0, AttackerTroop.TroopHealth +
                        AttackerTroop.TroopDefence +
                        AttackerTroop.TroopSpeed / 2 -
                        DefendingBase.BaseAttack);

        var basePower = Mathf.Max(0, DefendingBase.BaseHealth +
                        DefendingBase.BaseDefence +
                        DefendingBase.BaseSpeed / 2 -
                        AttackerTroop.TroopAttack);

        var result = Mathf.Abs(troopPower - basePower);

        if (troopPower > basePower)
        {
            int aliveUnitsCount = (int)(result / troopPower) * AttackerTroop.UnitsCount;
            int deadUnitsCount = AttackerTroop.UnitsCount - aliveUnitsCount;
            List<Unit> troopUnitList = AttackerTroop.UnitList;

            System.Random random = new System.Random();
            for (int i = 0; i < deadUnitsCount; i++)
            {
                var randomUnit = troopUnitList[random.Next(0, troopUnitList.Count)];
                troopUnitList.Remove(randomUnit);
            }
            Troop troop = new Troop(troopUnitList);

            if (aliveUnitsCount == 0)
            {
                AttackerTroop = null;
            }

            AttackerTroop = troop;
        }
        if (troopPower < basePower)
        {
            int aliveUnitsCount = (int)((result / basePower) * DefendingBase.UnitsCount);
            int deadUnitsCount = DefendingBase.UnitsCount - aliveUnitsCount;

            for (int i = 0; i < deadUnitsCount; i++)
            {
                DefendingBase.AcquireRandomUnit();
            }

            AttackerTroop = null;
        }

        for (int i = 0; i < DefendingBase.UnitsCount; i++)
        {
            DefendingBase.AcquireRandomUnit();
        }

        AttackerTroop = null;
    }
}
