using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{ 
    public class PlayerDirectionController
    {
        string _direction;
        public bool _locked = false;

        public void Init(string direction = "RIGHT")
        {
            _direction = direction;
        }

        public void Update(float x)
        {
            if (_locked) return;

            if (x > 0)
            {
                _direction = "RIGHT";
            }
            else if (x < 0)
            {
                _direction = "LEFT";
            }
        }

        public string GetDirection()
        {
            return _direction;
        }
        public void SetInitDirection(string direction)
        {
            _direction = direction;
        }
    }
}
