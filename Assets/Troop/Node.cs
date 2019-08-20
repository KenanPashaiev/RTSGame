﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> neighbours;
    public int x;
    public int y;

    public Node()
    {
        neighbours = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        if (n == null)
        {
            Debug.LogError("undefined path");
        }

        return Vector2.Distance(
            new Vector2(x, y),
            new Vector2(n.x, n.y)
            );
    }
}
