using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RTS;

public class Base
{
    public object Parent;

    public List<ClickableTile> TileList;
    public int baseIndex;
    
    private Barracks _barrack;
    private Walls _walls;
    private Portal _portal;
    private List<ResidentModule> _residentModuleList;
    private List<Factory> _factoryList;

    public List<Facility> facilityList
    {
        get
        {
            var list = new List<Facility>();
            list.Add(_barrack);
            list.Add(_walls);
            list.Add(_portal);
            list.AddRange(_residentModuleList);
            list.AddRange(_factoryList);
            return list;
        }
    }

    public float CreditsCount { get; private set; }
    public float ProductsCount { get; private set; }
    public int ResidentsCount
    {
        get
        {
            var allResidents = _residentModuleList.Sum(residentModule =>
                                                       residentModule.AmountOfResidents);
            return allResidents;
        }
    }

    public int AttackUnitsCount => _barrack.AttackUnitsCount;
    public int DefenceUnitsCount => _barrack.DefenceUnitsCount;
    public int SpeedUnitsCount => _barrack.SpeedUnitsCount;
    public int UnitsCount { get { return _barrack.UnitsCount; } }

    public int ResidentLimit
    {
        get
        {
            var allResidents = _residentModuleList.Sum(residentModule =>
                                                       residentModule.ResidentLimit);
            return allResidents;
        }
    }
    
    public int ResidentGrowth => _residentModuleList.Sum(a => a.ResidentGrowth);
    public float ProductGrowth => (int)(_factoryList.Sum(a => a.ProductGrowth) * _portal.PortalProductGrowthBonus);
    public float CreditGrowth => ResidentsCount * 0.1f;

    public float ProductBuyPrice { get => _portal.ProductBuyPrice; }
    public float ProductSalePrice { get => _portal.ProductSalePrice; }

    public float UnitLevelBonus { get => _barrack.UnitLevelBonus; }

    public bool AcquireResidents(int amountOfAcquiredResidents)
    {
        if (amountOfAcquiredResidents > ResidentsCount)
        {
            return false;
        }

        for (int i = 0; i < _residentModuleList.Count; i++)
        {
            var residentModule = _residentModuleList[i];
            if (amountOfAcquiredResidents > residentModule.AmountOfResidents)
            {
                amountOfAcquiredResidents -= residentModule.AmountOfResidents;
                residentModule.AcquireResidents(residentModule.AmountOfResidents);
                continue;
            }

            residentModule.AcquireResidents(amountOfAcquiredResidents);
            break;
        }
        return true;
    }

    public bool AcquireProducts(float amountOfAcquiredProducts)
    {
        if (amountOfAcquiredProducts > ProductsCount)
        {
            return false;
        }

        ProductsCount -= amountOfAcquiredProducts;
        return true;
    }

    public bool AcquireCredits(float amountOfAcquiredCredits)
    {
        if (amountOfAcquiredCredits > CreditsCount)
        {
            return false;
        }

        CreditsCount -= amountOfAcquiredCredits;
        return true;
    }

    public bool AddResidents(int amountOfAddedResidents)
    {
        if (ResidentsCount + amountOfAddedResidents > ResidentLimit)
        {
            return false;
        }

        for (int i = 0; i < _residentModuleList.Count; i++)
        {
            var residentModule = _residentModuleList[i];
            if (residentModule.AmountOfResidents + amountOfAddedResidents > residentModule.ResidentLimit)
            {
                amountOfAddedResidents -= residentModule.ResidentLimit - residentModule.AmountOfResidents;
                residentModule.AddResidents(residentModule.ResidentLimit - residentModule.AmountOfResidents);
                continue;
            }

            residentModule.AddResidents(amountOfAddedResidents);
            break;
        }
        return true;
    }

    public bool AddProducts(float amountOfAddedProducts)
    {
        ProductsCount += amountOfAddedProducts;
        return true;
    }

    public bool AddCredits(float amountOfAddedCredits)
    {
        CreditsCount += amountOfAddedCredits;
        return true;
    }

    public bool BuyProducts(int boughtProductsCount) => _portal.BuyProducts(boughtProductsCount);

    public bool SellProducts(int soldProductsCount) => _portal.SellProducts(soldProductsCount);

    public bool TrainUnit(int unitType) => _barrack.TrainUnit(unitType);

    public bool CanTrainUnit() => _barrack.CanTrainUnit();

    public Troop CreateTroop(int attackUnitsCount, int defenceUnitsCount, int speedUnitsCount)
    {
        bool isEnoughAttackUnits = _barrack.AttackUnitsCount - attackUnitsCount >= 0;
        bool isEnoughDefenceUnits = _barrack.DefenceUnitsCount - defenceUnitsCount >= 0;
        bool isEnoughSpeedUnits = _barrack.SpeedUnitsCount - speedUnitsCount >= 0;
        bool isEnoughUnits = isEnoughAttackUnits && isEnoughDefenceUnits && isEnoughSpeedUnits;

        if (!isEnoughUnits)
        {
            return null;
        }

        List<Unit> unitList = new List<Unit>();

        for (int i = 0; i < attackUnitsCount; i++)
        {
            unitList.Add(_barrack.AqcuireUnit(0) as AttackUnit);
        }

        for (int i = 0; i < defenceUnitsCount; i++)
        {
            unitList.Add(_barrack.AqcuireUnit(1) as DefenceUnit);
        }

        for (int i = 0; i < speedUnitsCount; i++)
        {
            unitList.Add(_barrack.AqcuireUnit(2) as SpeedUnit);
        }

        Troop troop = new Troop(unitList);
        return troop;
    }

    public float TrainUnitCreditsCost { get => Barracks.UnitTrainingCreditsCost; }
    public float TrainUnitProductsCost { get => Barracks.UnitTrainingProductsCost; }
    public float TrainUnitResidentsCost { get => Barracks.UnitTrainingResidentsCost; }

    public bool LevelUp(Facility facility)
    {
        facilityList.Find(x => x == facility).LevelUp();
        return true;
    }

    public void GameStepBase()
    {
        AddCredits(CreditGrowth);
        AddProducts(ProductGrowth);
        AddResidents(ResidentGrowth);
    }

    public Color baseColor;

    public readonly static int ExpandCreditCost = 1000;
    public readonly static int ExpandProductCost = 1000;
    public readonly static int ExpandResidentCost = 500;
    public readonly static int ExpandLevelCost = 5;

    public bool AddTile(ClickableTile clickableTile)
    {
        var isEnoughCredits = CreditsCount >= ExpandCreditCost;
        var isEnoughProducts = ProductsCount >= ExpandProductCost;
        var isEnoughResidents = CreditsCount >= ExpandResidentCost;
        var isEnoughLevels = _walls.Level - 5 >= 1;

        var canExpand = isEnoughCredits && isEnoughProducts && isEnoughResidents && isEnoughLevels;

        if (canExpand && TileList.Count > 0)
        {
            TileList.Add(clickableTile);
            clickableTile.SetBase(baseIndex);
            clickableTile.SetColor(baseColor);
            ExpandBase();
            _walls.ExpandWalls(ExpandLevelCost);
            return true;
        }

        if(TileList.Count == 0)
        {
            TileList.Add(clickableTile);
            clickableTile.SetBase(baseIndex);
            clickableTile.SetColor(baseColor);
            return true;
        }

        return false;
    }

    public void ExpandBase()
    {
        AcquireCredits(ExpandCreditCost);
        AcquireProducts(ExpandProductCost);
        AcquireResidents(ExpandResidentCost);
        _residentModuleList.Add(new ResidentModule(this));
        _factoryList.Add(new Factory(this));
    }
    
    public void DestroyBase()
    {
        if (Parent is AIBase)
        {
            (Parent as AIBase).DestroySelf();
        }
        if (Parent is PlayerBase)
        {
            (Parent as PlayerBase).DestroySelf();
        }
    }

    public float BaseHealth => _barrack.UnitsHealth;
    public float BaseAttack => _barrack.UnitsAttack;
    public float BaseDefence => _barrack.UnitsDefence + _walls.Defence;
    public float BaseSpeed => _barrack.UnitsSpeed;

    public bool isRaiding = false;
    public bool isRaided = false;

    public Troop Battle(Troop attackerTroop)
    {
        var troopPower = Mathf.Max(0, attackerTroop.TroopHealth +
                        attackerTroop.TroopDefence +
                        attackerTroop.TroopSpeed / 2 -
                        BaseAttack);

        var basePower = Mathf.Max(0, BaseHealth +
                        BaseDefence +
                        BaseSpeed / 2 -
                        attackerTroop.TroopAttack);

        var result = Mathf.Abs(troopPower - basePower);

        //

        if (troopPower > basePower)
        {
            int aliveUnitsCount = (int)(result / troopPower) * attackerTroop.UnitsCount;
            int deadUnitsCount = attackerTroop.UnitsCount - aliveUnitsCount;
            List<Unit> troopUnitList = attackerTroop.UnitList;

            Debug.Log("Troop: " + aliveUnitsCount.ToString());

            System.Random random = new System.Random();
            for (int i = 0; i < deadUnitsCount; i++)
            {
                var randomUnit = troopUnitList[random.Next(0, troopUnitList.Count)];
                troopUnitList.Remove(randomUnit);
            }
            Troop troop = new Troop(troopUnitList);

            if (aliveUnitsCount == 0)
                return null;

            return troop;
        }
        if(troopPower < basePower)
        {
            int aliveUnitsCount = (int)((result / basePower) * UnitsCount);
            int deadUnitsCount = UnitsCount - aliveUnitsCount;

            Debug.Log("Base: " + aliveUnitsCount.ToString());

            for (int i = 0; i < deadUnitsCount; i++)
            {
                _barrack.AqcuireRandomUnit();
            }

            return null;
        }

        for (int i = 0; i < UnitsCount; i++)
        {
            _barrack.AqcuireRandomUnit();
        }

        return null;
    }
    public void ReceiveTroop(Troop troop)
    {
        for(int i = 0; i < troop.UnitsCount; i++)
        {
            var isDone =_barrack.AddUnit(troop.UnitList[i]);
            if(!isDone)
            {
                break;
            }
        }
        AddCredits(1000);
        AddProducts(1000);

        isRaiding = false;
    }

    public Base()
    {
        TileList = new List<ClickableTile>();
        baseIndex = 0;

        _barrack = new Barracks(this);
        _walls = new Walls(this);
        _portal = new Portal(this);

        _residentModuleList = new List<ResidentModule>();
        _residentModuleList.Add(new ResidentModule(this));

        _factoryList = new List<Factory>();
        _factoryList.Add(new Factory(this));

        ProductsCount = 300;
        CreditsCount = 500;
    }
}
