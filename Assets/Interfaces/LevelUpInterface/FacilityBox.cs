using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class FacilityBox : MonoBehaviour
{
    public Text FacilityTitle;
    public Text FacilityLevel;
    public Text FirstUpgrade;
    public Text SecondUpgrade;
    public Text UpgradeCost;
    public Button LevelUpButton;
    public Facility facility;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetInfo()
    {
        if (facility is Barracks)
        {
            var barrack = facility as Barracks;
            FacilityTitle.text = "Barrack";
            FacilityLevel.text = "Level: " + barrack.Level + " (+1)";
            FirstUpgrade.text = "Unit limit: " + barrack.UnitLimit + " (+100)";
            SecondUpgrade.text = "Unit bonus: " + barrack.UnitLevelBonus + " (+0.1)";

        }
        if (facility is Walls)
        {
            var walls = facility as Walls;
            FacilityTitle.text = "Walls";
            FacilityLevel.text = "Level: " + walls.Level + " (+1)";
            FirstUpgrade.text = "Defence: " + walls.Defence + " (+0.05)";
            SecondUpgrade.text = "";
            //UpgradeCost.text = "Cost: " + facility.LevelUpPrice;
        }
        if (facility is Portal)
        {
            var portal = facility as Portal;
            FacilityTitle.text = "Portal";
            FacilityLevel.text = "Level: " + portal.Level + " (+1)";
            FirstUpgrade.text = "Product buy/sale price difference: " + (portal.ProductBuyPrice - portal.ProductSalePrice) + " (-1%)";
            SecondUpgrade.text = "Portal productivity bonus: " + portal.PortalProductGrowthBonus + " (+0.25%)";
        }
        if (facility is ResidentModule)
        {
            var residentModule = facility as ResidentModule;
            FacilityTitle.text = "Resident module";
            FacilityLevel.text = "Level: " + residentModule.Level + " (+1)";
            FirstUpgrade.text = "Resident limit: " + residentModule.ResidentLimit + " (+200)";
            SecondUpgrade.text = "Resident grow speed: " + residentModule.ResidentGrowth + " (+5%)";
        }
        if (facility is Factory)
        {
            var factory = facility as Factory;
            FacilityTitle.text = "Factory";
            FacilityLevel.text = "Level: " + factory.Level + " (+1)";
            FirstUpgrade.text = "Resident productivity: " + factory.ProductGrowth + " (+1.75%)";
            SecondUpgrade.text = "";
        }
        UpgradeCost.text = "Cost: " + facility.LevelUpPrice;

    }

    public void LevelUp()
    {
        if(facility is Barracks)
        {
            (facility as Barracks).LevelUp();
        }
        if(facility is Walls)
        {
            (facility as Walls).LevelUp();
        }
        if (facility is Portal)
        {
            (facility as Portal).LevelUp();
        }
        if (facility is Factory)
        {
            (facility as Factory).LevelUp();
        }
        if (facility is ResidentModule)
        {
            (facility as ResidentModule).LevelUp();
        }

        SetInfo();
    }
}
