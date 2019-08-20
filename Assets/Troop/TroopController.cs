using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;

public class TroopController : MonoBehaviour
{
    public SpriteRenderer troop_Txt;

    public Map map;
    public Base Base;
    public Troop troop;

    public int raidedBaseIndex;

    public float gameStep;

    public ClickableTile nextPosition;

    public ClickableTile origin;

    private List<ClickableTile> path;

    public Node[,] graph;

    private void GeneratePathfindingGraph()
    {
        int mapSizeX = map.MapSizeX;
        int mapSizeY = map.MapSizeY;

        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (x < mapSizeX - 1)
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
                
            }
        }
    }

    public void GeneratePathToBase(int baseIndex)
    {
        path = new List<ClickableTile>();

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[origin.positionX, origin.positionY];

        Node target = new Node();

        dist[source] = 0;
        prev[source] = null;


        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }


        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (map.tileMap[u.x, u.y].BaseIndex == baseIndex && map.tileMap[u.x, u.y].IsBase)
            {
                target = u;
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {

                float alt = dist[u] + 1;
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;
        
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        for(int i = 0; i < currentPath.Count; i++)
        {
            path.Add(map.tileMap[currentPath[i].x, currentPath[i].y]);
        }
    }

    private void Update()
    {
        if (map.BaseList[Base.baseIndex] == null)
        {
            if(map.BaseList[raidedBaseIndex] != null)
                map.BaseList[raidedBaseIndex].isRaided = false;
            Destroy(gameObject);
        }

        if (nextPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition.transform.position, Time.deltaTime * troop.TroopSpeed / gameStep);

            if (Vector3.Distance(transform.position, nextPosition.transform.position) < 0.001f)
            {
                AdvancePathing();
            }
        }
    }

    public void MoveToDestination(int baseIndex)
    {
        GeneratePathfindingGraph();

        GeneratePathToBase(baseIndex);

        nextPosition = path[0];
        
    }

    public void MoveToOrigin()
    {
        origin = map.tileMap[nextPosition.positionX, nextPosition.positionY];

        GeneratePathfindingGraph();

        GeneratePathToBase(Base.baseIndex);

        nextPosition = path[0];
    }

    void AdvancePathing()
    {
        transform.position = nextPosition.transform.position;
        path.RemoveAt(0);

        if (path.Count == 0)
        {

            if (map.tileMap[nextPosition.positionX, nextPosition.positionY].BaseIndex == raidedBaseIndex)
            {
                RaidBase();
                MoveToOrigin();
                
                return;
            }
            ArriveHome();
            nextPosition = null;
            return;
        }

        nextPosition = path[0];
    }

    void RaidBase()
    {
        Base raidedBase = map.BaseList[raidedBaseIndex];

        Troop newTroop = raidedBase.Battle(troop);
        
        raidedBase.isRaided = false;

        if(newTroop == null)
        {
            Destroy(gameObject);
            return;
        }

        newTroop = troop;

        map.BaseList[raidedBaseIndex].DestroyBase();
    }

    void ArriveHome()
    {
        Base.ReceiveTroop(troop);
        Destroy(gameObject);
    }

    private void Start()
    {
        troop_Txt.color = Base.baseColor;

        origin = Base.TileList[0];

        MoveToDestination(raidedBaseIndex);
    }
}
