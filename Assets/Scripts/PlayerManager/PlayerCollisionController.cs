using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Player
{
    public class PlayerCollisionController
    {
        Transform _transform;
        LayerMask _blockLayer;
        Transform attackPoint;
        float attackPointRadius;
        public ReactiveProperty<bool> _crashedToTrap = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> _crashedToItem = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> _crashedToEnemy = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> _crashedToFinish = new ReactiveProperty<bool>(false);
        public bool _isGround = false;

        //public Enemy.EnemyManager _crashedToEnemyObject;


        public void Init(Transform transform)
        {
            _transform = transform;
        }

        public bool IsGround(LayerMask groundLayer)
        {
            _blockLayer = groundLayer;
            //始点と終点を定義
            //プレイヤーの中心位置から始点を作成(0.2ずらしている)
            Vector3 leftStartPosition = _transform.position - Vector3.right * 0.2f;
            Vector3 rightStartPosition = _transform.position + Vector3.right * 0.2f;
            //終点を定義
            Vector3 endPoint = _transform.position - Vector3.up * 0.05f;

            //シーンviewで当たり判定ベクトルを確認用
            Debug.DrawLine(leftStartPosition, endPoint, Color.red);
            Debug.DrawLine(rightStartPosition, endPoint, Color.red);

            //始点、終点、当たっているものを引数に渡すと、当たり判定をboolで返却する
            return Physics2D.Linecast(leftStartPosition, endPoint, _blockLayer) || Physics2D.Linecast(rightStartPosition, endPoint, _blockLayer);
        }
        public Collider2D[] ObjectsHittingAttackFromLayer(Transform attackPoint, float attackRadius, LayerMask layer)
        {
            return Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, layer);
        }

        public void Update()
        {

        }

        public void onDestroy()
        {
            _crashedToTrap.Dispose();
            _crashedToItem.Dispose();
            _crashedToEnemy.Dispose();
            _crashedToFinish.Dispose();
        }
    }
}

