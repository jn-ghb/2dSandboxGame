using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager
{
    public Dictionary<string, int> _layers;
    GameObject _game0bject;
    public void Init()
    {

        _layers = new Dictionary<string, int>()
        {
            { "Enemy",7 },
            { "Item",9 },
            { "PlayerDie",31 },
            { "EnemyDie",30 }
        };
    }

    public void SetLayer(GameObject gameObject, string setLayerName)
    {
        _game0bject = gameObject;
        _game0bject.layer = _layers[setLayerName];
    }
}
