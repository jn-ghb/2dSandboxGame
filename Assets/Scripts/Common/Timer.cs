using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Timer
    {
        public float _nowTime = 0f;
        public float _timeLimit;
        //タイマー計測中のアクション
        public System.Action TimerPerformedAction;
        //タイマーが終了した時のアクション
        public System.Action TimerCompletedAction;
        public bool _instrumentationed;
        public void Instrumentation()
        {
            if (_instrumentationed)
            {
                _nowTime += Time.deltaTime;
                if (_nowTime <= _timeLimit)
                {
                    TimerPerformedAction();
                }
                else
                {
                    TimerCompletedAction();
                    _nowTime = 0;
                    _instrumentationed = false;
                }
            }

        }
        public void Init()
        {
            _instrumentationed = false;
            _nowTime = 0;
        }
        public void Start()
        {
            _instrumentationed = true;
        }
        public void Stop()
        {
            _instrumentationed = false;
        }
        public void Update()
        {
            Instrumentation();
        }
    }
}

