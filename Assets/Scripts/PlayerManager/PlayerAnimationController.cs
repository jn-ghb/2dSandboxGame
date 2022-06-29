using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Player
{
    public class PlayerAnimationController
    {
        Animator _animator;
        float _AxisX;
        public ReactiveProperty<bool> _playingDefaultAttack = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _playingDefaultSecondAttack = new ReactiveProperty<bool>();
        public ReactiveProperty<int> _playingIdleSpriteNum = new ReactiveProperty<int>();

        public void Init(Animator animator)
        {
            _animator = animator;
        }
        public void Move(float x)
        {
            _AxisX = x;
            _animator.SetFloat("moveSpeed", _AxisX);
        }
        public void Attack()
        {
            _animator.SetTrigger("isDefaultAttack");
        }
        public void Pick()
        {
            _animator.SetTrigger("isPick");
        }
        public void DefaultAttack(Animator animator)
        {
            _animator = animator;
            _animator.SetTrigger("isDefaultAttack");
        }
        public void DefaultSecondAttack()
        {
            _animator.SetTrigger("isDefaultSecondAttack");
        }
        public void Hurt(bool isHurt)
        {
            _animator.SetBool("isHurt", isHurt);
        }
        public void Jump(bool isJumping)
        {
            _animator.SetBool("isJumping", isJumping);
        }
        public void Die()
        {
            _animator.SetTrigger("isDie");
        }

        public void Update()
        {
            _playingDefaultAttack.Value = _animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDefaultAttackAnimation");
            _playingDefaultSecondAttack.Value = _animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDefaultSecondAttackAnimation");
        }
        public void onDestroy()
        {
            _playingDefaultAttack.Dispose();
            _playingDefaultSecondAttack.Dispose();
        }
    }
}

