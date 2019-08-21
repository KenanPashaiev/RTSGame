using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResidentModule
{
    bool AcquireResidents(int amountOfAcquiredResidents);

    bool AddResidents(int amountOfAddedResidents);
}
