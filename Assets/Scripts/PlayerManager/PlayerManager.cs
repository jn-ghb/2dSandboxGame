using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        float _currentMoveSpeed = 4f;
        float _walkSpeed = 4f;
        float _runSpeed = 10f;
        float _jumpForce = 16.5f;

        Animator _animator;
        Rigidbody2D _rigidbody2D;
        SpriteRenderer _spriteRenderer;

        public LayerMask _blockLayer;
        public LayerMask _enemyLayer;
        public Transform _attackPoint;
        float _attackRadius = 0.3f;

        public GameObject _pickPoint;
        float _pickRadius = 0.5f;

        public GameObject _playerEquip;

        public GameObject PlayerEquipToachLight;

        PlayerAnimationController _playerAnimationController;
        PlayerMovementController _playerMovementController;
        PlayerDirectionController _playerDirectionController;
        PlayerCollisionController _playerCollisionController;
        PlayerStateController _playerStateController;
        PlayerHittingPointManager _playerAttackPointManager;
        PlayerPickPointManager _playerPickPointManager;
        PlayerEquipController _playerEquipController;
        PlayerEquipToachLightController _playerEquipToachLightController;
        PlayerItemController _playerItemController;

        [SerializeField]
        GameObject PlayerEffects;
        PlayerEffectController _playerEffectController;

        LayerManager _layerManager;
        Common.Locked _locked;

        //初期化
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _playerEffectController = PlayerEffects.GetComponent<PlayerEffectController>();

            _playerAnimationController = new PlayerAnimationController();
            _playerAnimationController.Init(_animator);

            _playerMovementController = new PlayerMovementController();
            _playerMovementController.Init(_rigidbody2D, transform);

            _playerDirectionController = new PlayerDirectionController();
            _playerDirectionController.Init();

            _playerCollisionController = new PlayerCollisionController();
            _playerCollisionController.Init(transform);

            _playerStateController = new PlayerStateController();
            _playerStateController.Init();

            _playerAttackPointManager = new PlayerHittingPointManager();
            _playerAttackPointManager.Init(_attackPoint, _attackRadius);

            _playerPickPointManager = new PlayerPickPointManager();
            _playerPickPointManager.Init(_pickPoint, _pickRadius, _blockLayer);

            _playerEquipController = new PlayerEquipController();
            _playerEquipController.Init(_spriteRenderer, _playerEquip);

            _playerEquipToachLightController = new PlayerEquipToachLightController();
            _playerEquipToachLightController.Init(PlayerEquipToachLight);

            _playerItemController = new PlayerItemController();
            _playerItemController.Init();



            //エフェクト
            _playerEquipController._isActiveDefaultAttackEffect.Subscribe(_ =>
            {
                _playerEffectController.SetActiveDefaultAttackEffect(_);
            });
            _playerEquipController._isActiveSecondAttackEffect.Subscribe(_ =>
            {
                _playerEffectController.SetActiveSecondAttackEffect(_);
            });

            //装備のタイプ
            //PickPointを表示する
            _playerStateController._EquipWeponType.Subscribe(_ =>
            {
                if (_ == "Tool") _playerPickPointManager._show.Value = true;
                else _playerPickPointManager._show.Value = false;

                if (_ == "Light") _playerEquipToachLightController._show.Value = true;
                else _playerEquipToachLightController._show.Value = false;

            });


            _layerManager = new LayerManager();
            _layerManager.Init();
            _locked = new Common.Locked();

            _playerStateController._Hp.Subscribe(_ =>
            {
                if (_ <= 0)
                {
                    Die();
                }
            }).AddTo(this);
        }

        //移動
        public void Move(float x, float moveSpeed)
        {
            if (!_playerStateController._isMove.Value)
            {
                _playerMovementController.Move(0, 0);
                _playerAnimationController.Move(0);
                return;
            };

            _playerMovementController.Move(x, moveSpeed);
            _playerAnimationController.Move(Mathf.Abs(x));

            //スティック傾きが0.1fを超えた時
            if (Mathf.Abs(x) > 0.1f)
            {
                _playerMovementController.Frip(_playerDirectionController.GetDirection());
            }
        }
        public void ChangeMoveSpeedToWalk()
        {
            _currentMoveSpeed = _walkSpeed;
        }
        public void ChangeMoveSpeedToRun()
        {
            _currentMoveSpeed = _runSpeed;
        }
        public void SetMoveSpeed(float moveSpeed)
        {
            _currentMoveSpeed = moveSpeed;
        }

        //ジャンプ
        bool _rockedJump = false;
        public void Jump()
        {
            if (_rockedJump) return;
            _playerStateController._isJumping.Value = true;
        }

        bool _jumping = false;
        public void JumpUpdate()
        {
            if (_playerCollisionController.IsGround(_blockLayer))
            {
                _playerAnimationController.Jump(false);
                _jumping = false;
                if (_playerStateController._isJumping.Value)
                {
                    _jumping = true;
                    _playerAnimationController.Move(0);
                    _playerStateController._isJumping.Value = false;
                    _playerAnimationController.Jump(true);
                    _playerMovementController.Jump(_jumpForce);
                }

            }
        }

        //掘る
        public void Pick()
        {
            if (!_playerStateController._isAttacked.Value) return;

            _playerPickPointManager.Pick();
        }
        public void PickUp()
        {
            _playerPickPointManager.PickPointUpMove();

        }
        public void PickDown()
        {
            _playerPickPointManager.PickPointDownMove();

        }
        public void PickLeft()
        {
            _playerMovementController.Frip("LEFT");
            _playerPickPointManager.PickPositionRecet();

        }
        public void PickRight()
        {
            _playerMovementController.Frip("RIGHT");
            _playerPickPointManager.PickPositionRecet();

        }
        public void PickLeftUp()
        {
            _playerMovementController.Frip("LEFT");
            _playerPickPointManager.PickPointDiagonallyUpMove();

        }
        public void PickRightUp()
        {
            _playerMovementController.Frip("RIGHT");
            _playerPickPointManager.PickPointDiagonallyUpMove();

        }

        public ReactiveProperty<bool> GetDestroyBlockSignalFromPick()
        {
            return _playerPickPointManager._destroyBlockSignal;
        }
        public ReactiveProperty<string> GetDestroyBlockSpriteNameFromPick()
        {
            return _playerPickPointManager._destroyBlockSpriteName;
        }

        //攻撃
        public void DefaultAttack()
        {
            if (!_playerStateController._isAttacked.Value) return;

            if (!_jumping)
            {
                _locked.LockedFromTimeTwoAction(0.3f, () =>
                {
                    _playerStateController._isMove.Value = false;
                }, () =>
                {
                    _playerStateController._isMove.Value = true;
                });
            }
            //pickの場合はモーションを変更する
            if (_playerStateController._isPicking.Value) _playerAnimationController.Pick();
            else _playerAnimationController.Attack();

            _playerAttackPointManager.SetActionToHittingObjectFromLayer((hittingObject) =>
            {

                hittingObject.GetComponent<Enemys.IOnDamage>().OnDamage(_playerStateController._Atk.Value);

            }, _enemyLayer);
        }
        public void DefaultSecondAttack()
        {
            //pickの場合は停止
            if (_playerStateController._isPicking.Value) return;

            if (!_playerStateController._isAttacked.Value) return;

            _playerAnimationController.DefaultSecondAttack();
            _playerAttackPointManager.SetActionToHittingObjectFromLayer((hittingObject) =>
            {
                hittingObject.GetComponent<Enemys.IOnDamage>().OnDamage(_playerStateController._Atk.Value);

            }, _enemyLayer);
        }


        //ダメージ
        public void OnDamage(int damage)
        {
            _playerStateController._Hp.Value -= damage;
        }

        //死亡
        public void Die()
        {
            _layerManager.SetLayer(this.gameObject, "PlayerDie");
            _updateLocked = true;
            _playerAnimationController.Die();
            _playerMovementController.Stop();
            _playerStateController._isMove.Value = false;
            _playerStateController._isAttacked.Value = false;
            _rockedJump = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //攻撃エフェクトのオンオフ
        public void ActiveAttackEffect(bool active)
        {
            _playerEffectController._activeAttackEffect.Value = active;
        }


        //Update
        bool _updateLocked = false;
        public void UpdateManaged(float x, float y)
        {
            _playerEquipController.Update();

            if (_updateLocked) return;
            //pick
            _playerPickPointManager.Update(x, y);

            //スティック傾きが0.94fを超えた時
            if (Mathf.Abs(x) > 0.94f) _currentMoveSpeed = _runSpeed;

            //攻撃している時は方角変更、移動を停止する
            if (_playerAnimationController._playingDefaultAttack.Value)
            {
                x = 0;
                _currentMoveSpeed = 0;
                //_playerMovementController.Attack(_playerDirectionController.GetDirection());
            }
            Move(x, _currentMoveSpeed);
            _playerDirectionController.Update(x);

            JumpUpdate();
            _playerAnimationController.Update();



        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_pickPoint.transform.position, _pickRadius);
        }

        //衝突監視
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.layer == _layerManager._layers["Enemy"])
            {
                _playerStateController._isMove.Value = false;
                _playerStateController._isAttacked.Value = false;
                _rockedJump = true;
                _locked.LockedFromTimeTwoAction(0.3f, () =>
                {
                    OnDamage(collision.gameObject.GetComponent<Enemys.IGetBodyHittingAtk>().GetBodyHittingAtk());
                    _playerAnimationController.Hurt(true);
                    _playerMovementController.KnockBack(_playerDirectionController.GetDirection());
                }, () =>
                 {
                     _playerAnimationController.Hurt(false);
                     _rockedJump = false;
                     _playerStateController._isMove.Value = true;
                     _playerStateController._isAttacked.Value = true;
                 });
            }

            //アイテムと衝突
            if (collision.gameObject.layer == _layerManager._layers["Item"])
            {
                collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
                _playerStateController._isGetItem.SetValueAndForceNotify(collision.gameObject.tag);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Scene_FirstForest")
            {
                SceneManager.LoadScene("FirstForest");
            }
            if (collision.gameObject.tag == "OutZone")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void SetEquipSprite(Sprite sprite)
        {
            _playerEquipController.SetSprite(sprite);
        }

        public string GetDirection()
        {
            return _playerDirectionController.GetDirection();
        }


        public PlayerStateController GetPlayerStateController()
        {
            return _playerStateController;
        }

        private void OnDestroy()
        {
            _playerStateController.onDestroy();
            _playerMovementController.onDestroy();
            _playerCollisionController.onDestroy();
            _playerAnimationController.onDestroy();
            _playerPickPointManager.onDestroy();
            _playerEquipController.onDestroy();
        }
    }

}

