using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Common
{
    public class Coroutine
    {
        public IEnumerator IEDelay(float time,Action Action)
        {
            yield return new WaitForSeconds(time);
            Action();
        }
    }

}
