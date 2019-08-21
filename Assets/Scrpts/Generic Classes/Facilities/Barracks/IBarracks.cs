using System.Collections;
using System.Collections.Generic;
using RTS;
using UnityEngine;

public interface IBarracks
{
    Unit AcquireUnit(UnitType unitType);
    Unit AcquireRandomUnit();

    bool TrainUnit(UnitType unitType);
    bool AddUnit(Unit unit);
}
