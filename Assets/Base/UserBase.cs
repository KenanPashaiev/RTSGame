using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserBase : MonoBehaviour
{
    public Map map;

    public Base Base = new Base();

    public GameObject TroopPrefab;
    protected TroopController Troop;
}
