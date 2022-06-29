using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Tilemaps;

public class PlayerPickPointManager
{
    public ReactiveProperty<bool> _show = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitUpPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitDownPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitLeftUpPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitLeftDownPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitRightUpPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _permitRightDownPick = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _destroyBlockSignal = new ReactiveProperty<bool>();
    public ReactiveProperty<string> _destroyBlockSpriteName = new ReactiveProperty<string>();
    public ReactiveProperty<Vector3> _destroyBlockSpritePosition = new ReactiveProperty<Vector3>();

    float _hitPointRadius = 0.3f;
    public GameObject _hitPoint;
    Collider2D _hittingObject;
    Action<Collider2D> _HittingAction;
    LayerMask _blockLayer;

    public void Init(GameObject hitPoint, float hitPointRadius, LayerMask blockLayer)
    {
        _hitPoint = hitPoint;
        _hitPointRadius = hitPointRadius;
        _blockLayer = blockLayer;

        _show.Subscribe(_ =>
        {
            if (_) hitPoint.SetActive(true);
            else hitPoint.SetActive(false);
        });

        _permitRightUpPick.Subscribe(_ =>
        {
            if (!_show.Value) return;
            if (_) PickPointDiagonallyUpMove();
            else PickPositionRecet();

        });

        _permitLeftUpPick.Subscribe(_ =>
        {
            if (!_show.Value) return;
            if (_) PickPointDiagonallyUpMove();
            else PickPositionRecet();

        });
        _permitDownPick.Subscribe(_ =>
        {
            if (!_show.Value) return;
            if (_) PickPointDownMove();
            else PickPositionRecet();

        });
        _permitUpPick.Subscribe(_ =>
        {
            if (!_show.Value) return;
            if (_) PickPointUpMove();
            else PickPositionRecet();

        });
    }

    public void Update(float x, float y)
    {
        if (x > 0.6 && x < 0.9 && y > 0.4 && y < 0.8) _permitRightUpPick.Value = true;
        else _permitRightUpPick.Value = false;

        if (x < -0.4 && x > -0.9 && y > 0.4 && y < 0.8) _permitLeftUpPick.Value = true;
        else _permitLeftUpPick.Value = false;

        if (y > 0.98f) _permitUpPick.Value = true;
        else _permitUpPick.Value = false;

        if (y < -0.98f) _permitDownPick.Value = true;
        else _permitDownPick.Value = false;
    }

    public void onDestroy()
    {
        _permitUpPick.Dispose();
        _permitDownPick.Dispose();
        _permitLeftUpPick.Dispose();
        _permitLeftDownPick.Dispose();
        _permitRightUpPick.Dispose();
        _permitRightDownPick.Dispose();
        _destroyBlockSignal.Dispose();
    }


    public Collider2D GetObjectsHittingAttackFromLayer(LayerMask layer)
    {
        return Physics2D.OverlapCircle(_hitPoint.transform.position, _hitPointRadius, layer);
    }
    public void SetActionToHittingObjectFromLayer(Action<Collider2D> HittingAction, LayerMask layer)
    {
        _HittingAction = HittingAction;
        _hittingObject = GetObjectsHittingAttackFromLayer(layer);
        if (_hittingObject == null) return;
        HittingAction(_hittingObject);
    }
    public void Pick()
    {
        SetActionToHittingObjectFromLayer((hittingObject) =>
        {
            if (!_show.Value) return;
            int x = (int)Math.Floor(_hitPoint.transform.position.x);
            int y = (int)Math.Floor(_hitPoint.transform.position.y);
            if (hittingObject.GetComponent<Tilemap>().GetSprite(new Vector3Int(x, y, 0)) == null) return;
            _destroyBlockSpriteName.Value = hittingObject.GetComponent<Tilemap>().GetSprite(new Vector3Int(x, y, 0)).name.ToString();
            _destroyBlockSpritePosition.Value = new Vector3(x, y, 0);
            _destroyBlockSignal.Value = true;
            _destroyBlockSignal.Value = false;
            hittingObject.GetComponent<Tilemap>().SetTile(
                new Vector3Int(x, y, 0)
                , null
            );

        }, _blockLayer);
    }
    public void PickPositionRecet()
    {
        _hitPoint.transform.localPosition = new Vector3(0.68f, 0.5f, 0);
    }
    public void PickPointUpMove()
    {
        _hitPoint.transform.localPosition = new Vector3(0, 2.5f, 0);
    }
    public void PickPointDownMove()
    {
        _hitPoint.transform.localPosition = new Vector3(0, -0.5f, 0);
    }
    public void PickPointDiagonallyUpMove()
    {
        _hitPoint.transform.localPosition = new Vector3(0.68f, 1.5f, 0);
    }
}
