using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerEquipToachLightController
{
    public GameObject _playerEquipToachLight;
    public ReactiveProperty<bool> _show = new ReactiveProperty<bool>();

    public void Init(GameObject playerEquipToachLight)
    {
        _playerEquipToachLight = playerEquipToachLight;
        _show.Subscribe(_ =>
        {
            if (_) _playerEquipToachLight.SetActive(true);
            else _playerEquipToachLight.SetActive(false);
        });
    }
}
