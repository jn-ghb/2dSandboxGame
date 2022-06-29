using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabManager : MonoBehaviour
{
    public enum ItemTypes
    {
        Wepon,
        Consumption,
        Event,
        Tool,
        Light
    }
    public ItemTypes _type;
    public Sprite _iconSprite;
    public Sprite _eqipSprite;
    public float _updateHp;
    public float _updateAtk;
    public float _updateDiffence;
    public float _updateAtkTime;
    public float _updateDiffenceTime;
    public int maxLength;
    public string[] _states;
}
