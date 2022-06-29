using UnityEngine;
using System.Collections;
using UniRx;
using System;

namespace Player
{
    public class PlayerMovementController
    {
        Rigidbody2D _rigidbody2D;
        Transform _transform;
        float _x;
        float _moveSpeed;
        float _jumpForce;
        string _direction;
        public IDisposable _jumpMoveRecet;

        float _knockBackUpForce = 300;
        float _knockBackHorizontalForce = 200;

        public void Init(Rigidbody2D rigidbody2D, Transform transform)
        {
            _rigidbody2D = rigidbody2D;
            _transform = transform;
        }

        //停止
        public void Stop()
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
        }

        //移動
        public void Move(float x, float moveSpeed)
        {
            _x = x;
            _moveSpeed = moveSpeed;
            _rigidbody2D.velocity = new Vector2(_x * _moveSpeed, _rigidbody2D.velocity.y);
        }
        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        //反転
        public void Frip(string direction)
        {

            _direction = direction;
            if (direction == "RIGHT")
            {
                _transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        //ジャンプ
        public void Jump(float jumpForce)
        {
            _jumpForce = jumpForce;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            //0.25f後に速度をリセット
            _jumpMoveRecet = Observable.Return(Unit.Default)
                .Delay(TimeSpan.FromSeconds(0.25f))
                .Subscribe(_ => _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0));
        }
        //攻撃
        public void Attack(string direction)
        {
            _direction = direction;
            _rigidbody2D.velocity = new Vector2(0, 0);

            if (_direction == "LEFT")
            {
                _rigidbody2D.AddForce(Vector2.left * 100);
            }
            else
            {
                _rigidbody2D.AddForce(Vector2.right * 100);
            }
        }
        //ノックバック
        public void KnockBack(string direction)
        {
            _direction = direction;
            _rigidbody2D.velocity = new Vector2(0, 0);
            _rigidbody2D.AddForce(Vector2.up * _knockBackUpForce);
            if (_direction == "RIGHT")
            {
                _rigidbody2D.AddForce(Vector2.left * _knockBackHorizontalForce);
            }
            else
            {
                _rigidbody2D.AddForce(Vector2.right * _knockBackHorizontalForce);
            }
        }

        public void onDestroy()
        {
            if (_jumpMoveRecet != null) _jumpMoveRecet.Dispose();
        }
    }
}
