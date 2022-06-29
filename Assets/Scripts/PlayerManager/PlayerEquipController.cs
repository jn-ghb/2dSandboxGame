using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerEquipController
{
    SpriteRenderer _spriteRenderer;
    SpriteRenderer _playerEquipSpriteRenderer;
    GameObject _playerEquipGameObject;
    Vector2 _initLocalScale;
    Transform _transform;

    public ReactiveProperty<bool> _isActiveDefaultAttackEffect = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _isActiveSecondAttackEffect = new ReactiveProperty<bool>();

    public void Init(SpriteRenderer spriteRenderer, GameObject playerEquipGameObject)
    {
        _spriteRenderer = spriteRenderer;
        _playerEquipGameObject = playerEquipGameObject;
        _playerEquipSpriteRenderer = _playerEquipGameObject.GetComponent<SpriteRenderer>();
        _initLocalScale = _playerEquipGameObject.GetComponent<Transform>().localPosition;
        _transform = _playerEquipGameObject.GetComponent<Transform>();
    }

    void Idle()
    {
        if (_spriteRenderer.sprite.name == "PlayerIdleAnimation1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.528f, 0.878f);
            _transform.localRotation = Quaternion.Euler(0, 0, -34.209f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerIdleAnimation2")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.528f, 0.896f);
            _transform.localRotation = Quaternion.Euler(0, 0, -34.209f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }

        if (_spriteRenderer.sprite.name == "PlayerIdleAnimation3")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.528f, 0.878f);
            _transform.localRotation = Quaternion.Euler(0, 0, -34.209f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerIdleAnimation4")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.528f, 0.896f);
            _transform.localRotation = Quaternion.Euler(0, 0, -34.209f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }

    void Run()
    {
        if (_spriteRenderer.sprite.name == "PlayerRunAnimation1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.829f, 1.132f);
            _transform.localRotation = Quaternion.Euler(0, 0, -24.063f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerRunAnimation2")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.774f, 1.149f);
            _transform.localRotation = Quaternion.Euler(0, 0, -24.063f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerRunAnimation3")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.829f, 1.191f);
            _transform.localRotation = Quaternion.Euler(0, 0, -24.063f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerRunAnimation4")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.829f, 1.132f);
            _transform.localRotation = Quaternion.Euler(0, 0, -24.063f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }
    void Jump()
    {
        if (_spriteRenderer.sprite.name == "PlayerJumpAnimation1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(-0.682f, 1.393f);
            _transform.localRotation = Quaternion.Euler(0, 0, 25.144f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerJumpAnimation2")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(-0.676f, 1.416f);
            _transform.localRotation = Quaternion.Euler(0, 0, 25.144f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }

    void DefaultAttack()
    {
        if (_spriteRenderer.sprite.name == "PlayerDefaultAttack_1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(-0.827f, 0.958f);
            _transform.localRotation = Quaternion.Euler(0, 0, 82.501f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerDefaultAttack_2")
        {
            _isActiveDefaultAttackEffect.Value = true;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipGameObject.SetActive(false);
        }
        if (_spriteRenderer.sprite.name == "PlayerDefaultAttack_3")
        {
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipGameObject.SetActive(true);
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipBack";
            _transform.localPosition = new Vector2(0.388f, 1.327f);
            _transform.localRotation = Quaternion.Euler(0, 0, -40.857f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }

    void SecondAttack()
    {
        if (_spriteRenderer.sprite.name == "PlayerSecondAttack1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipBack";
            _transform.localPosition = new Vector2(0.336f, 1.465f);
            _transform.localRotation = Quaternion.Euler(0, 0, 12.233f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerSecondAttack2")
        {
            _isActiveSecondAttackEffect.Value = true;
            _playerEquipGameObject.SetActive(false);
        }
        if (_spriteRenderer.sprite.name == "PlayerSecondAttack3")
        {
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipGameObject.SetActive(true);
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipBack";
            _transform.localPosition = new Vector2(0.388f, 1.327f);
            _transform.localRotation = Quaternion.Euler(0, 0, -40.857f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerSecondAttack4")
        {
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipGameObject.SetActive(true);
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipBack";
            _transform.localPosition = new Vector2(0.388f, 1.327f);
            _transform.localRotation = Quaternion.Euler(0, 0, -40.857f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }

    void Pick()
    {
        if (_spriteRenderer.sprite.name == "PlayerDig01")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.159f, 1.367f);
            _transform.localRotation = Quaternion.Euler(0, 0, 52.759f);
            _transform.localScale = new Vector3(0.7673693f, 0.783848f, 0.6726f);
        }
        if (_spriteRenderer.sprite.name == "PlayerDig02")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.047f, 1.266f);
            _transform.localRotation = Quaternion.Euler(0, 0, 78.829f);
            _transform.localScale = new Vector3(0.7673693f, 0.783848f, 0.6726f);
        }
        if (_spriteRenderer.sprite.name == "PlayerDig03")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _isActiveSecondAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.621f, 0.464f);
            _transform.localRotation = Quaternion.Euler(0, 0, -104.413f);
            _transform.localScale = new Vector3(0.7673693f, 0.783848f, 0.6726f);
        }
    }

    void Hurt()
    {
        if (_spriteRenderer.sprite.name == "PlayerDamageAnimation1")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.629f, 1.064f);
            _transform.localRotation = Quaternion.Euler(0, 0, -13.708f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
        if (_spriteRenderer.sprite.name == "PlayerDamageAnimation2")
        {
            _playerEquipGameObject.SetActive(true);
            _isActiveDefaultAttackEffect.Value = false;
            _playerEquipSpriteRenderer.sortingLayerName = "PlayerEquipFront";
            _transform.localPosition = new Vector2(0.629f, 1.064f);
            _transform.localRotation = Quaternion.Euler(0, 0, -13.708f);
            _transform.localScale = new Vector3(0.8941613f, 0.9133629f, 0.7837334f);
        }
    }

    void Die()
    {
        if (_spriteRenderer.sprite.name == "PlayerDieAnimation1")
        {
            _playerEquipGameObject.SetActive(false);
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _playerEquipSpriteRenderer.sprite = sprite;
    }

    public void Update()
    {
        Idle();
        Jump();
        Run();
        Hurt();
        DefaultAttack();
        SecondAttack();
        Pick();
        Die();
    }

    public void onDestroy()
    {
        _isActiveDefaultAttackEffect.Dispose();
        _isActiveSecondAttackEffect.Dispose();
    }
}
