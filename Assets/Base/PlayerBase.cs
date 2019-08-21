using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RTS;

public class PlayerBase : MonoBehaviour
{
    public Map map;

    public Base Base = new Base();

    public GameObject PlayerInterfacePrefab;
    private Interface playerInterface;

    public GameObject TroopPrefab;
    private TroopController Troop;

    public void Raid(int attackUnitsCount, int defenceUnitsCount, int speedUnitsCount, int raidedBaseIndex)
    {
        var raidedBase = map.BaseList[raidedBaseIndex];

        var raidedBaseIsNull = raidedBase == null;
        var baseIsRaided = raidedBase.isRaided == true;
        if (raidedBaseIsNull && baseIsRaided)
        {
            return;
        }

        Troop troop = Base.CreateTroop(attackUnitsCount, defenceUnitsCount, speedUnitsCount);

        if (troop != null)
        {
            raidedBase.isRaided = true;
            GameObject go = Instantiate(TroopPrefab, Base.TileList[0].transform);
            Troop = go.GetComponent<TroopController>();
            Troop.map = map;
            Troop.Base = Base;
            Troop.raidedBaseIndex = raidedBaseIndex;
            Troop.troop = troop;
            Base.isRaiding = true;
        }
    }

    public void Expand()
    {
        List<ClickableTile> tileList = map.GetFreeNeighbourTiles(Base.baseIndex);

        for(int i = 0; i < tileList.Count; i++)
        {
            tileList[i].SetIsExpandable(Base.baseIndex);
        }
    }

    public void UnMarkTiles()
    {
        List<ClickableTile> tileList = map.GetFreeNeighbourTiles(Base.baseIndex);

        for (int i = 0; i < tileList.Count; i++)
            tileList[i].SetIsNotExpandable();
    }

    public void DestroySelf()
    {
        for (int i = 0; i < Base.TileList.Count; i++)
        {
            Base.TileList[i].RemoveBase();
        }
        map.BaseList[Base.baseIndex] = null;
        playerInterface.gameObject.SetActive(false);
        Destroy(playerInterface);
        Destroy(gameObject);
        map.FinishGame(MatchResult.defeat);
    }

    private void Start()
    {
        Base.Parent = this;
        GameObject gameObject = Instantiate(PlayerInterfacePrefab, new Vector3(), new Quaternion());
        playerInterface = gameObject.GetComponent<Interface>();
        playerInterface.PlayerBase = this;
    }

    private void Update()
    {
        if (playerInterface.PlayerBase.Base != null)
        {
            playerInterface.UpdateValues();
        }
    }
}
