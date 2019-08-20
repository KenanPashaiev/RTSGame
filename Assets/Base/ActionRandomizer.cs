using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WeightedAction
{
    public readonly Action Action;
    public int Probabilty { get; set; }

    public WeightedAction(Action action, int probabilty)
    {
        Action = action;
        Probabilty = probabilty;
    }
}

public class ActionRandomizer
{
    private List<WeightedAction> _probList;
    private int weight;

    

    public ActionRandomizer()
    {
        _probList = new List<WeightedAction>();
        weight = 0;
    }

    public void Add(Action action, int probability)
    {
        _probList.Add(new WeightedAction(action, probability));
        weight += probability;
    }

    public Action GetRandomAction()
    {
        System.Random random = new System.Random();
        int randomNumber = UnityEngine.Random.Range(0, 99);

        for(int i = 0; i < _probList.Count; i++)
        {
            _probList[i].Probabilty /= weight / 100;
        }

        int summ = 0;
        for(int i = 0; i < _probList.Count; i++)
        {
            if(randomNumber < _probList[i].Probabilty + summ && randomNumber >= summ)
            {
                return _probList[i].Action;
            }

            summ += _probList[i].Probabilty;
        }

        return null;
    }
}
