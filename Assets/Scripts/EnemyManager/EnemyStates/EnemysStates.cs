using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemysStates
    {
        public Dictionary<string, int> _hp;
        public Dictionary<string, int> _Atk;
        public Dictionary<string, int> _BodyHittingAtk;
        public void Init()
        {
            _hp = new Dictionary<string, int>()
            {
                { "Rat",3 },
                { "Slime",3 }
            };

            _Atk = new Dictionary<string, int>()
            {
                  { "Rat",1 },
                  { "Slime",1 }
            };
            _BodyHittingAtk = new Dictionary<string, int>()
            {
                  { "Rat",1 },
                  { "Slime",1 }
            };
        }
    }
}

