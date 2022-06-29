using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Enemys
{
    public class SlimeManager : MonoBehaviour, IOnDamage, IDie, IGetBodyHittingAtk
    {
        //tag
        string _tagName;
        string _tagNameCategory = "Enemy_";
        string _name;

        //states
        public ReactiveProperty<int> _Hp = new ReactiveProperty<int>();
        public ReactiveProperty<int> _Atk = new ReactiveProperty<int>();
        public ReactiveProperty<int> _BodyHittingAtk = new ReactiveProperty<int>();

        public ReactiveProperty<bool> _isDamaged = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> _isDie = new ReactiveProperty<bool>();

        public Dictionary<string, int> _Hps;
        public Dictionary<string, int> _Atks;
        public Dictionary<string, int> _BodyHittingAtks;
        Enemy.EnemysStates _enemysStates;

        //move
        float x = -1;
        float _moveSpeed = 2f;

        //get_base_component
        Animator _animator;
        Rigidbody2D _rigidbody2D;
        public LayerMask _block;
        public LayerMask _playerLayer;

        //common
        Common.Locked _locked;
        SpriteRenderer _spriteRenderer;
        Common.SpriteRenderFadeAnimation _spriteRenderFadeAnimation;
        LayerManager _layerManager;

        //当たり判定
        public Transform _changeMoveSpeedCollider;
        Vector3 _changeMoveSpeedColliderRect = new Vector3(3f, 1f, 0);


        EnemyMovementController _enemyMovementController;
        EnemyDirectionController _enemyDirectionController;
        EnemyCollisionController _enemyCollisionController;
        EnemyAnimationController _enemyAnimationController;

        bool _lockedUpdate = false;
        bool _moveLocked = true;
        ReactiveProperty<bool> _changeMoveSpeed = new ReactiveProperty<bool>();

        void StatesInit()
        {
            _enemysStates = new Enemy.EnemysStates();
            _enemysStates.Init();
            _Hps = _enemysStates._hp;
            _Atks = _enemysStates._Atk;
            _BodyHittingAtks = _enemysStates._BodyHittingAtk;
            _tagName = this.GetComponent<Transform>().tag;
            _name = _tagName.Replace(_tagNameCategory, "");
            _Hp.Value = _Hps[_name];
            _Atk.Value = _Atks[_name];
            _BodyHittingAtk.Value = _BodyHittingAtks[_name];
        }

        void LayerInit()
        {
            _layerManager = new LayerManager();
            _layerManager.Init();

        }

        void SpriteRendererInit()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderFadeAnimation = new Common.SpriteRenderFadeAnimation(_spriteRenderer);
        }

        public void OnDamage(int damage)
        {
            _isDamaged.Value = true;
            _isDamaged.Value = false;
            _Hp.Value -= damage;
            if (_Hp.Value <= 0) _Hp.Value = 0;
        }

        public void Die()
        {
            _isDie.Value = true;
            _layerManager.SetLayer(this.gameObject, "EnemyDie");
            this.UpdateAsObservable()
                    .Do(_ => _spriteRenderFadeAnimation.FadeOut())
                    .Where(_ => _spriteRenderFadeAnimation._completed)
                    .Subscribe(_ => Destroy(this.gameObject))
                    .AddTo(this);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_changeMoveSpeedCollider.position, new Vector2(3f, 1));
        }

        public int GetBodyHittingAtk()
        {
            return _BodyHittingAtk.Value;
        }

        // --- LifeCycle ----
        private void Awake()
        {
            StatesInit();
            LayerInit();
            SpriteRendererInit();

            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _enemyAnimationController = new EnemyAnimationController();
            _enemyAnimationController.Init(_animator);

            _enemyMovementController = new EnemyMovementController();
            _enemyMovementController.Init(_rigidbody2D, transform);

            _enemyDirectionController = new EnemyDirectionController();
            _enemyDirectionController.Init();

            _enemyCollisionController = new EnemyCollisionController();
            _enemyCollisionController.Init(transform);

            _locked = new Common.Locked();


            //subscribes
            _Hp.Subscribe(hp =>
            {
                if (hp <= 0)
                {
                    Die();
                }
            }).AddTo(this);

            //damage
            _isDamaged.Subscribe(_ =>
            {

                if (_)
                {
                    _lockedUpdate = true;
                    _enemyAnimationController.Hurt(true);
                    //_enemyMovementController.KnockBack(_enemyDirectionController.GetDirection(), 100, 0);
                    Observable.Return(Unit.Default)
                   .Delay(TimeSpan.FromMilliseconds(500))
                   .Subscribe(_ =>
                   {
                       _enemyAnimationController.Hurt(false);
                       _lockedUpdate = false;
                   })
                   .AddTo(this);
                }

            });

            //die
            _isDie.Subscribe(_ =>
            {
                if (_) _enemyAnimationController.Die();
            }).AddTo(this);


            //move
            this.UpdateAsObservable()
                .FirstOrDefault()
                .Delay(TimeSpan.FromMilliseconds(1500))
                .Do(_ => _moveLocked = false)
                .Delay(TimeSpan.FromMilliseconds(2000))
                .Do(_ => _moveLocked = true)
                .Repeat()
                .Subscribe()
                .AddTo(this);

            _changeMoveSpeed
                .Subscribe(_ =>
                {
                    if (_) _moveSpeed = 5f;
                    else _moveSpeed = 2f;
                })
                .AddTo(this);


        }


        private void Update()
        {
            if (!_enemyCollisionController.IsGround(_block))
            {
                x *= -1;
            }
            if (_lockedUpdate) return;
            _enemyDirectionController.Update(x);
            _enemyMovementController.Frip(_enemyDirectionController.GetDirection());
            if (_moveLocked) return;
            _enemyMovementController.Move(x, _moveSpeed);


            //当たり判定
            if (Physics2D.OverlapBox(_changeMoveSpeedCollider.position, _changeMoveSpeedColliderRect, 0, _playerLayer) != null)
            {
                _locked.LockedFromTimeTwoAction(0.3f, () =>
                {
                    _changeMoveSpeed.Value = true;
                }, () =>
                {
                    _changeMoveSpeed.Value = false;
                });
            }
        }

        private void OnDestroy()
        {
            _changeMoveSpeed.Dispose();
            _isDamaged.Dispose();
            _isDie.Dispose();
            _Hp.Dispose();
            _Atk.Dispose();
            _BodyHittingAtk.Dispose();
        }
    }
}

