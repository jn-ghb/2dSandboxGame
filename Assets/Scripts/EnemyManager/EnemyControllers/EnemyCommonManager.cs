using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyCommonManager : MonoBehaviour
{
    string _tagName;
    string _tagNameCategory = "Enemy_";

    string _name;
    ReactiveProperty<int> _Hp = new ReactiveProperty<int>();
    int _Atk;

    public ReactiveProperty<bool> _isDamaged = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _isDie = new ReactiveProperty<bool>();

    SpriteRenderer _spriteRenderer;

    public Dictionary<string, int> _Hps;
    public Dictionary<string, int> _Atks;
    Enemy.EnemysStates _enemysStates;

    Common.SpriteRenderFadeAnimation _spriteRenderFadeAnimation;
    LayerManager _layerManager;

    private void Awake()
    {
        _enemysStates = new Enemy.EnemysStates();
        _enemysStates.Init();
        _Hps = _enemysStates._hp;
        _Atks = _enemysStates._Atk;
        _tagName = this.GetComponent<Transform>().tag;
        _name = _tagName.Replace(_tagNameCategory, "");
        _Hp.Value = _Hps[_name];
        _Atk = _Atks[_name];

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderFadeAnimation = new Common.SpriteRenderFadeAnimation(_spriteRenderer);
        _layerManager = new LayerManager();
        _layerManager.Init();

        _Hp.Subscribe(hp =>
        {
            if (hp <= 0)
            {
                Die();
            }
        }).AddTo(this);
    }


    public void OnDamage(int damage)
    {
        _isDamaged.Value = true;
        _isDamaged.Value = false;
        _Hp.Value -= damage;
        if (_Hp.Value <= 0) _Hp.Value = 0;
    }
    void Die()
    {
        _isDie.Value = true;
        _layerManager.SetLayer(this.gameObject, "EnemyDie");
        this.UpdateAsObservable()
                .Do(_ => _spriteRenderFadeAnimation.FadeOut())
                .Where(_ => _spriteRenderFadeAnimation._completed)
                .Subscribe(_ => Destroy(this.gameObject))
                .AddTo(this);
    }
    public int GetAtk()
    {
        return _Atk;
    }

    void Update()
    {

    }
    private void OnDestroy()
    {
        _isDamaged.Dispose();
        _isDie.Dispose();
    }
}
