using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] GameObject _DefaultAttackEffect;
    [SerializeField] GameObject _SecondAttackEffect;

    public ReactiveProperty<bool> _activeAttackEffect = new ReactiveProperty<bool>();

    public void SetActiveDefaultAttackEffect(bool isActive)
    {
        if (!_activeAttackEffect.Value) return;
        _DefaultAttackEffect.SetActive(isActive);
    }
    public void SetActiveSecondAttackEffect(bool isActive)
    {
        if (!_activeAttackEffect.Value) return;
        _SecondAttackEffect.SetActive(isActive);
    }

    private void OnDestroy()
    {
        _activeAttackEffect.Dispose();
    }
}
