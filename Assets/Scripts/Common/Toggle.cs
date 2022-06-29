using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common{
    public class Toggle
    {
        bool _toggleFlug = false;
        public bool Fire()
        {
            _toggleFlug = !_toggleFlug;
            return _toggleFlug;
        }
    }
}

