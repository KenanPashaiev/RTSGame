using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class AIBase : UserBase
{
    public AIType AIType;
    public float GameStepLength;

    private void Start()
    {
        Base.Parent = this;

        GameStepLength = map.GameStepLength;

        InvokeRepeating("GameStep", 0, GameStepLength);
    }

    public void DestroySelf()
    {
        for (int i = 0; i < Base.TileList.Count; i++)
        {
            Base.TileList[i].RemoveBase();
        }
        map.BaseList[Base.baseIndex] = null;
        Destroy(gameObject);
    }

    private void GameStep()
    {
        Action agresiveMove = AgresiveMove;
        Action passiveMove = PassiveMove;

        ActionRandomizer actionRandomizer = new ActionRandomizer();

        switch (AIType)
        {
            case AIType.agressor:
                {
                    actionRandomizer.Add(agresiveMove, 75);
                    actionRandomizer.Add(passiveMove, 25);
                    break;
                }

            case AIType.builder:
                {
                    actionRandomizer.Add(agresiveMove, 25);
                    actionRandomizer.Add(passiveMove, 75);
                    break;
                }

            case AIType.merchant:
                {
                    actionRandomizer.Add(passiveMove, 100);
                    break;
                }
        }

        Action move = actionRandomizer.GetRandomAction();
        move();
    }


    private void AgresiveMove()
    {
        Action levelUpFacility = LevelUpFacility;
        Action trainUnits = TrainUnits;
        Action raid = Raid;

        ActionRandomizer actionRandomizer = new ActionRandomizer();

        actionRandomizer.Add(levelUpFacility, 50);
        actionRandomizer.Add(trainUnits, 45);
        actionRandomizer.Add(raid, 5);

        Action action = actionRandomizer.GetRandomAction();
        action();
    }

    private void PassiveMove()
    {
        Action levelUpFacility = LevelUpFacility;
        Action trainUnits = TrainUnits;
        Action expand = Expand;

        ActionRandomizer actionRandomizer = new ActionRandomizer();

        actionRandomizer.Add(levelUpFacility, 35);
        actionRandomizer.Add(trainUnits, 5);
        actionRandomizer.Add(expand, 60);

        Action action = actionRandomizer.GetRandomAction();
        action();
    }
    
    private void LevelUpFacility()
    {
        var facilityList = new List<Facility>();
        facilityList.AddRange(Base.facilityList);

        System.Random random = new System.Random();
        while(facilityList.Count > 0)
        {
            var randomFacility = facilityList[random.Next(0, facilityList.Count)];
            bool done = randomFacility.LevelUp();
            if(done)
            {
                return;
            }
            facilityList.Remove(randomFacility);
        }

        ExchangeResources();

        return;
    }

    private void TrainUnits()
    {
        System.Random random = new System.Random();
        var randomUnitType = random.Next(0, 3);

        var minimalResource = Math.Min(Base.CreditsCount, Base.ResidentsCount);
        var trainCount = (int)(minimalResource / 40);
        var randomTrainedUnitCount = random.Next(trainCount - 2, trainCount + 2);

        while(randomTrainedUnitCount > 0)
        {
            bool done = Base.TrainUnit(GetRandomUnitType());
            if(!done)
            {
                return;
            }
            randomTrainedUnitCount--;
        }
    }

    private UnitType GetRandomUnitType()
    {
        Array unitTypes = Enum.GetValues(typeof(UnitType));
        int unitTypesCount = unitTypes.Length;

        System.Random random = new System.Random();
        int randomUnitTypeIndex = random.Next(unitTypesCount);
        UnitType randomUnitType = (UnitType)unitTypes.GetValue(randomUnitTypeIndex);

        return randomUnitType;
    }

    private void ExchangeResources()
    {
        float creditsCount = Base.CreditsCount;
        float productsCount = Base.ProductsCount;

        float exchangeCount = Mathf.Abs(creditsCount - productsCount) / 2;
        if(exchangeCount < 500)
        {
            return;
        }

        if(creditsCount > productsCount)
        {
            int boughtProductsCount = (int)(exchangeCount / Base.ProductBuyPrice);
            Base.BuyProducts(boughtProductsCount);
            return;
        }
        else if(productsCount > creditsCount)
        {
            int soldProductsCount = (int)exchangeCount;
            Base.SellProducts(soldProductsCount);
            return;
        }
    }

    private void Raid()
    {
        var unitCountIsLow = Base.UnitsCount < 10;
        if (unitCountIsLow)
        {
            return;
        }

        System.Random random = new System.Random();
        int attackUnitsCount = random.Next((int)Base.AttackUnitsCount / 2, Base.AttackUnitsCount);
        int defenceUnitsCount = random.Next((int)Base.DefenceUnitsCount / 2, Base.DefenceUnitsCount);
        int speedUnitsCount = random.Next((int)Base.SpeedUnitsCount / 2, Base.SpeedUnitsCount);

        var raidedBase = GetRandomRaidBase();

        var raidedBaseIsNull = raidedBase == null;
        var baseIsRaided = raidedBase.isRaided == true;
        if (raidedBaseIsNull && baseIsRaided)
        {
            return;
        }

        Troop troop = Base.CreateTroop(attackUnitsCount, defenceUnitsCount, speedUnitsCount);

        if (troop != null && raidedBase != null && raidedBase.isRaided == false)
        {
            raidedBase.isRaided = true;
            GameObject go = Instantiate(TroopPrefab, Base.TileList[0].transform);
            Troop = go.GetComponent<TroopController>();
            Troop.origin = Base.TileList[0];
            Troop.map = map;
            Troop.Base = Base;
            Troop.raidedBaseIndex = raidedBase.baseIndex;
            Troop.troop = troop;
        }
    }

    private Base GetRandomRaidBase()
    {
        System.Random random = new System.Random();
        int raidedBaseIndex = random.Next(0, map.BaseList.Count);

        while (raidedBaseIndex == Base.baseIndex)
        {
            raidedBaseIndex = random.Next(0, map.BaseList.Count);
        }
        var raidedBase = map.BaseList[raidedBaseIndex];

        return raidedBase;
    }

    private void Expand()
    {

        List<ClickableTile> tileList = map.GetFreeNeighbourTiles(Base.baseIndex);
        UnMarkTiles();
        System.Random random = new System.Random();

        var randomTile = tileList[random.Next(0, tileList.Count)];
        if(Base.AddTile(randomTile))
        {
            return;
        }

        LevelUpFacility();
    }

    private void UnMarkTiles()
    {
        List<ClickableTile> tileList = map.GetFreeNeighbourTiles(Base.baseIndex);

        for (int i = 0; i < tileList.Count; i++)
            tileList[i].SetIsNotExpandable();
    }
}
