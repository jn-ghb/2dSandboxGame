using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item
    {
        public string _tagName;
        public string _type;
        public Sprite _iconSprite;
        public Sprite _eqipSprite;
        public float _updateHp;
        public float _updateAtk;
        public float _updateDiffence;
        public float _updateAtkTime;
        public float _updateDiffenceTime;

        public string[] _states;

        //public void Init(
        //    string tagName,
        //    string type,
        //    Sprite iconSprite,
        //    Sprite eqipSprite,
        //    float updateAtk,
        //    float updateDiffence,
        //    string[] states
        //)
        //{
        //    _tagName = tagName;
        //    _type = type;
        //    _iconSprite = iconSprite;
        //    _eqipSprite = eqipSprite;
        //    _updateAtk = updateAtk;
        //    _updateDiffence = updateDiffence;
        //    _states = states;
        //}
    }
}

