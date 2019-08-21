using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBase
{
    void Raid(int attackUnitsCount, int defenceUnitsCount, int speedUnitsCount, int raidedBaseIndex);

    void Expand();

    void DestroySelf();
}
