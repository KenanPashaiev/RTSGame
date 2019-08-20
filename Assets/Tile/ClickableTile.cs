using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public Map map;

    public int distance;

    public int positionX;
    public int positionY;

    public bool IsBase = false;
    public bool IsExpandable = false;

    public List<ClickableTile> neighbourList = new List<ClickableTile>();

    public int BaseIndex;
    private Color BaseColor = Color.white;

    private void OnMouseUp()
    {
        if(IsExpandable)
        {
            if (map.BaseList[BaseIndex].AddTile(this))
            {
                List<ClickableTile> tileList = map.GetFreeNeighbourTiles(BaseIndex);

                for (int i = 0; i < tileList.Count; i++)
                    tileList[i].SetIsNotExpandable();
            }
        }
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetIsExpandable(int baseIndex)
    {
        if (!IsBase && !IsExpandable)
        {
            IsExpandable = true;
            BaseIndex = baseIndex;
            BaseColor = gameObject.GetComponent<SpriteRenderer>().color;
            SetColor(new Color32(167, 167, 241, 255));
        }
    }

    public void SetIsNotExpandable()
    {
        if (!IsBase && IsExpandable)
        {
            IsExpandable = false;
            BaseIndex = 0;
            SetColor(BaseColor);
        }
    }

    public void SetBase(int baseIndex)
    {
        IsBase = true;
        IsExpandable = false;
        BaseIndex = baseIndex;
    }

    public void RemoveBase()
    {
        IsBase = false;
        IsExpandable = false;
        BaseIndex = 0;
        SetColor(Color.white);
    }
}
