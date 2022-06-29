using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Player
{
    public class PlayerStateController
    {
        public ReactiveProperty<int> _MaxixmuHp = new ReactiveProperty<int>();
        public ReactiveProperty<int> _Hp = new ReactiveProperty<int>();
        public ReactiveProperty<int> _Atk = new ReactiveProperty<int>();
        public ReactiveProperty<string> _EquipWepon = new ReactiveProperty<string>("");
        public ReactiveProperty<string> _EquipWeponType = new ReactiveProperty<string>("");

        public ReactiveProperty<bool> _isIdle = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isWalk = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isRun = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isMove = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isJumping = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isAttacked = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isDamaged = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isDie = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isPicking = new ReactiveProperty<bool>();
        public ReactiveProperty<string> _isGetItem = new ReactiveProperty<string>();


        public void Init()
        {
            _MaxixmuHp.Value = 10;
            _Atk.Value = 1;
            _Hp.Value = _MaxixmuHp.Value;
            _isMove.Value = true;
            _isAttacked.Value = true;
        }

        public void onDestroy()
        {
            _MaxixmuHp.Dispose();
            _Hp.Dispose();
            _Atk.Dispose();
            _EquipWepon.Dispose();
            _isIdle.Dispose();
            _isWalk.Dispose();
            _isRun.Dispose();
            _isMove.Dispose();
            _isJumping.Dispose();
            _isAttacked.Dispose();
            _isDamaged.Dispose();
            _isDie.Dispose();
            _isGetItem.Dispose();
            _EquipWeponType.Dispose();
        }
    }
}
