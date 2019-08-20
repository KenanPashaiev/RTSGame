using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class BattleResult
{
    private Troop _attackingTroop;

    private Base _defendingBase;

    public object winner;

    public BattleResult(Troop attackingTroop, Base defendingBase)
    {
        _attackingTroop = attackingTroop;
        _defendingBase = defendingBase;
    }

    public void CalculateBattleResult()
    {
        var troopPower = _attackingTroop.TroopHealth +  
                        _attackingTroop.TroopDefence + 
                        _attackingTroop.TroopSpeed / 2 - 
                        _defendingBase.BaseAttack;

        var basePower = _defendingBase.BaseHealth +
                        _defendingBase.BaseDefence +
                        _defendingBase.BaseSpeed / 2 -
                        _attackingTroop.TroopAttack;

        var result = Mathf.Abs(troopPower - basePower);

        if(troopPower > basePower)
        {
            int aliveUnitsCount = (int)(result / troopPower) * _attackingTroop.UnitsCount;
            int deadUnitsCount = _attackingTroop.UnitsCount - aliveUnitsCount;
            List<Unit> troopUnitList = _attackingTroop.UnitList;

            System.Random random = new System.Random();
            for(int i = 0; i < deadUnitsCount; i++)
            {
                var randomUnit = troopUnitList[random.Next(0, troopUnitList.Count)];
                troopUnitList.Remove(randomUnit);
            }

            //_defendingBase = 

        }
    }
}
