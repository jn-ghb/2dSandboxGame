using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemController
{
    public List<string> _carryingItems;
    public Dictionary<string, int> _carryingItemsNum;

    public void Init()
    {
        _carryingItems = new List<string>();
        _carryingItemsNum = new Dictionary<string, int>();
    }

    public void AddCarryingItems(string tagName)
    {
        // リストに要素が無ければ追加
        if (!_carryingItems.Contains(tagName))
        {
            _carryingItems.Add(tagName);
        }

        SetCarryingItemsNum(tagName);
    }

    void SetCarryingItemsNum(string tagName)
    {
        if (_carryingItemsNum.ContainsKey(tagName))
        {
            _carryingItemsNum[tagName] += 1;
        }
        else
        {
            _carryingItemsNum.Add(tagName, 1);
        }
    }
}
