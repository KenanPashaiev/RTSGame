using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float time { get; private set; }

    public string PlayerName;

    public int MapSizeX;
    public int MapSizeY;

    public float GameStepLength = 0.1f;

    public ClickableTile[,] tileMap;

    public GameObject tilePrefab;
    public GameObject playerBasePrefab;
    public GameObject aiBasePrefab;

    public List<Base> BaseList;
    
    public void FinishGame()
    {
        return;
    }

    public List<ClickableTile> GetFreeNeighbourTiles(int baseIndex)
    {
        var result = new List<ClickableTile>();

        var tileList = BaseList[baseIndex].TileList;
        for (int i = 0; i < tileList.Count; i++)
        {
            var neighbourList = tileList[i].neighbourList;
            for (int j = 0; j < neighbourList.Count; j++)
            {
                if(!neighbourList[j].IsBase && !neighbourList[j].IsExpandable)
                {
                    result.Add(neighbourList[j]);
                }
            }
        }

        return result;
    }

    private void Start()
    {
        GameStepLength = 0.1f;
        tileMap = new ClickableTile[MapSizeX, MapSizeY];
        BaseList = new List<Base>();

        GenerateMap();
        GenerateBases();
        
        InvokeRepeating("GameStep", GameStepLength, GameStepLength);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void GameStep()
    {
        for (int i = 0; i < BaseList.Count; i++)
        {
            if (BaseList[i] != null)
            {
                BaseList[i].GameStepBase();
            }
        }
    }

    private void GenerateMap()
    {
        for(int i = 0; i < MapSizeX; i++)
        {
            for(int j = 0; j < MapSizeY; j++)
            {
                
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), new Quaternion());
                ClickableTile clickableTile = tile.GetComponent < ClickableTile>();

                clickableTile.positionX = i;
                clickableTile.positionY = j;
                clickableTile.map = this;
                tileMap[i, j] = clickableTile;
            }
        }
    }

    private void SetNeigbours()
    {
        for (int i = 0; i < MapSizeX; i++)
        {
            for (int j = 0; j < MapSizeY; j++)
            {
                int x = tileMap[i, j].positionX;
                int y = tileMap[i, j].positionY;

                if (x - 1 >= 0)
                    tileMap[i, j].neighbourList.Add(tileMap[x - 1, y]);
                if (x + 1 < MapSizeX)
                    tileMap[i, j].neighbourList.Add(tileMap[x + 1, y]);
                if (y - 1 >= 0)
                    tileMap[i, j].neighbourList.Add(tileMap[x, y - 1]);
                if (y + 1 < MapSizeY)
                    tileMap[i, j].neighbourList.Add(tileMap[x, y + 1]);
            }
        }
    }

    private void GenerateBases()
    {
        GeneratePlayerBase();

        GenerateAIBase(5);
    }

    private void GeneratePlayerBase()
    {
        GameObject gameObj = (GameObject)Instantiate(playerBasePrefab, new Vector3(), new Quaternion());
        PlayerBase player = gameObj.GetComponent<PlayerBase>();

        player.map = this;
        BaseList.Add(player.Base);
        player.Base.baseIndex = BaseList.IndexOf(player.Base);

        var randomTile = GetFreeTile();

        player.Base.baseColor = Color.blue;
        player.Base.AddTile(randomTile);

        SetNeigbours();
    }

    private void GenerateAIBase(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObj = (GameObject)Instantiate(aiBasePrefab, new Vector3(), new Quaternion());
            AIBase aiBase = gameObj.GetComponent<AIBase>();
            BaseList.Add(aiBase.Base);

            aiBase.AIType = UnityEngine.Random.Range(0, 2);
            aiBase.GameStepLength = GameStepLength;

            aiBase.map = this;
            aiBase.Base.baseIndex = BaseList.IndexOf(aiBase.Base);

            var randomTile = GetFreeTile();

            aiBase.Base.baseColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f);
            aiBase.Base.AddTile(randomTile);

            SetNeigbours();
        }
    }

    private ClickableTile GetFreeTile()
    {
        bool isEmpty = false;
        System.Random random = new System.Random();
        int randomPositionX = random.Next(0, MapSizeX); ;
        int randomPositionY = random.Next(0, MapSizeY); ;
        var randomTile = tileMap[randomPositionX, randomPositionY];

        while (!isEmpty)
        {
            randomPositionX = random.Next(0, MapSizeX);
            randomPositionY = random.Next(0, MapSizeY);
            randomTile = tileMap[randomPositionX, randomPositionY];
            isEmpty = randomTile.IsBase == false;
        }

        return tileMap[randomPositionX, randomPositionY];
    }
}
