using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace InputManagerExtends
{
    public class DoubleInput
    {
        Common.Timer _timer;
        bool _lockedSignal;
        float _lockedTime;
        IDisposable _lockedSignalDelay;
        ReactiveProperty<bool> _isSignal = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _defaultAttackSignal = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _secondAttackSignal = new ReactiveProperty<bool>();
        //0,1の順番に繰り返す。時間計測中に送られる。
        int _actionTypeCount = 0;

        public DoubleInput(float timeLimit, float lockedTime)
        {
            _timer = new Common.Timer();
            _timer._timeLimit = timeLimit;
            _lockedTime = lockedTime;
            _lockedSignal = false;
            Main();
        }

        void Main()
        {
            //信号のロックを解除
            _lockedSignal = false;
            //タイマーが稼働している時
            _timer.TimerPerformedAction = () =>
            {
                //2回目の信号が送られた時
                if (_actionTypeCount == 2)
                {
                    //信号をロック
                    _lockedSignal = true;
                    //2回目の攻撃アクションを実行
                    _secondAttackSignal.Value = true;
                    _secondAttackSignal.Value = false;
                    //タイマー停止
                    _timer.Init();
                    //信号を指定秒数の間停止
                    _lockedSignalDelay = Observable.Return(Unit.Default)
                        .Delay(TimeSpan.FromSeconds(_lockedTime))
                        .Subscribe(_ =>
                        {
                            _lockedSignal = false;
                        });
                }
            };

            //タイマーが完了した時、アクションをデフォルトに戻す
            _timer.TimerCompletedAction = () =>
            {
                _actionTypeCount = 0;
            };

            //true,falseで1回づつ呼ばれる
            _isSignal.Subscribe(value =>
            {
                if (value)
                {
                    if (_actionTypeCount > 1)
                    {
                        _actionTypeCount = 0;
                    }
                    if (_actionTypeCount == 0)
                    {
                        _defaultAttackSignal.Value = true;
                        _defaultAttackSignal.Value = false;
                    }
                    _timer.Start();
                    _actionTypeCount++;
                }
            });
        }

        //true,falseで1回づつ呼ばれる、全ての信号源
        //これを外部から呼び出しで起動する
        public void FireSignal()
        {
            if (_lockedSignal) return;
            _isSignal.Value = true;
            _isSignal.Value = false;
        }
        //updateも忘れずに呼び出し、配置する
        public void Update()
        {
            _timer.Update();
        }

        public void onDestroy()
        {
            if (_lockedSignalDelay != null) _lockedSignalDelay.Dispose();
            if (_isSignal != null) _isSignal.Dispose();
            if (_defaultAttackSignal != null) _defaultAttackSignal.Dispose();
            if (_secondAttackSignal != null) _secondAttackSignal.Dispose();
        }
    }
}

