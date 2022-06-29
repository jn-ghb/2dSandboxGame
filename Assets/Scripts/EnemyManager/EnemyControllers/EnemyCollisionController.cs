using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyCollisionController
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
        Vector3 startVec = _transform.position - _transform.right * 0.5f * _transform.localScale.x;
        Vector3 endVec = startVec - _transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);
        return Physics2D.Linecast(startVec, endVec, groundLayer);
    }

    public Collider2D ObjectsHittingAttackFromLayer(Transform attackPoint, float attackRadius, LayerMask layer)
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRadius, layer);
    }

    public void Update()
    {

    }
}
