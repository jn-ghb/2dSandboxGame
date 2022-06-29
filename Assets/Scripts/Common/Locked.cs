using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Common
{
    public class Locked
    {
        bool _locked = false;
        public void LockedFromTime(float lockSeconds, Action Action)
        {
            if (!_locked)
            {
                _locked = true;
                Action();

                Observable.Return(Unit.Default)
                    .Delay(TimeSpan.FromSeconds(lockSeconds))
                    .Subscribe(_ => _locked = false);
            }
        }
        public void LockedFromTimeTwoAction(float lockSeconds, Action FirstAction, Action ActionFromCompleted)
        {
            if (!_locked)
            {
                _locked = true;
                FirstAction();

                Observable.Return(Unit.Default)
                    .Delay(TimeSpan.FromSeconds(lockSeconds))
                    .Subscribe(_ =>
                    {
                        ActionFromCompleted();
                        _locked = false;
                    });
            }
        }
    }
}

