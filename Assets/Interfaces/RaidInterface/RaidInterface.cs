using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidInterface : MonoBehaviour
{
    public Interface Interface;

    public Base PlayerBase;
    
    public GameObject UnitChooseManager;
    public LayerMask clickableLayer;
    public bool isChoosingBaseToRaid = false;
    private int raidedBaseIndex;

    public UnitRaidInterface Attack;
    public UnitRaidInterface Defence;
    public UnitRaidInterface Speed;

    private void Start()
    {
        isChoosingBaseToRaid = true;
        UnitChooseManager.SetActive(false);

        Attack.PlayerBase = PlayerBase;
        Attack.SetInfo(0);
        Defence.PlayerBase = PlayerBase;
        Defence.SetInfo(1);
        Speed.PlayerBase = PlayerBase;
        Speed.SetInfo(2);
    }

    public void CreateTroop()
    {
        var attackUnitCount = Attack.UnitCount;
        var defenceUnitCount = Defence.UnitCount;
        var speedUnitCount = Speed.UnitCount;

        var unitCount = attackUnitCount + defenceUnitCount + speedUnitCount;
        if (unitCount > 0)
        {
            Interface.PlayerBase.Raid(attackUnitCount, defenceUnitCount, speedUnitCount, raidedBaseIndex);
            CloseInterface();
            return;
        }
        MessageBox.Show("You should choose at least one unit");
    }

    public void CloseInterface()
    {
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isChoosingBaseToRaid)
        {
            RaycastHit raycastHit;
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, clickableLayer);

            ClickableTile clickableTile = raycastHit.collider.GetComponent<ClickableTile>();
            bool isTile = raycastHit.collider.GetComponent<ClickableTile>() is ClickableTile;

            bool isBaseTile = isTile && clickableTile.IsBase && clickableTile.BaseIndex != Interface.PlayerBase.Base.baseIndex;

            if (isHit && isBaseTile)
            {
                isChoosingBaseToRaid = false;
                UnitChooseManager.SetActive(true);
                raidedBaseIndex = clickableTile.BaseIndex;
            }
        }
    }
}
