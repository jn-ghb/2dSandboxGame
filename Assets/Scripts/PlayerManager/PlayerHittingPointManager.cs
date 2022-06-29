using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Player
{
    public class PlayerHittingPointManager
    {

        float _hitPointRadius = 0.3f;
        public Transform _hitPoint;
        Collider2D _hittingObject;
        Action<Collider2D> _HittingAction;

        public void Init(Transform hitPoint, float hitRadius)
        {
            _hitPoint = hitPoint;
            _hitPointRadius = hitRadius;
        }
        public void Update()
        {
            OnDrawGizmos();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hitPoint.position, _hitPointRadius);
        }

        public Collider2D GetObjectsHittingAttackFromLayer(LayerMask layer)
        {
            return Physics2D.OverlapCircle(_hitPoint.position, _hitPointRadius, layer);
        }

        public void SetActionToHittingObjectFromLayer(Action<Collider2D> HittingAction, LayerMask layer)
        {
            _HittingAction = HittingAction;
            _hittingObject = GetObjectsHittingAttackFromLayer(layer);
            if (_hittingObject == null) return;
            HittingAction(_hittingObject);
        }
    }
}

