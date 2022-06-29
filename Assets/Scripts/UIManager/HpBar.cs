using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBar
{
    Slider _slider;
    Image _fillImage;

    int _maximumHp;
    int _currentHp;
    float _worningPercentage;
    float _dangerousPercentage;
    int _barLength = 0;

    public HpBar(Slider slider, Image fillImage, float worningPercentage, float dangerousPercentage, int maximumHp, int currentHp)
    {
        _slider = slider;
        _fillImage = fillImage;
        _maximumHp = maximumHp;
        _currentHp = currentHp;
        _barLength = _currentHp;
        _worningPercentage = worningPercentage;
        _dangerousPercentage = dangerousPercentage;

        SetBerLength();
        SetBarColor();
    }

    public void Update(int maximumHp, int currentHp)
    {
        _maximumHp = maximumHp;
        _currentHp = currentHp;
        _barLength = _currentHp;

        SetBerLength();
        SetBarColor();
    }
    void SetBerLength()
    {
        _slider.value = _barLength;
    }
    void SetBarColor()
    {

        if (_slider.value < Mathf.Floor(_maximumHp * _worningPercentage) && _slider.value >= Mathf.Floor(_maximumHp * _dangerousPercentage))
        {
            SetBarColorWorning();
        }
        else if (_slider.value < Mathf.Floor(_maximumHp * _dangerousPercentage))
        {

            SetBarColorDangerous();
        }
    }
    void SetBarColorWorning()
    {
        _fillImage.color = new Color32(255, 255, 86, 255);
    }
    void SetBarColorDangerous()
    {
        _fillImage.color = new Color32(255, 0, 0, 255);
    }
}

