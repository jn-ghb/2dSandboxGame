using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class HpBarManager : MonoBehaviour
{


    [SerializeField]
    Slider _slider;
    [SerializeField]
    Image _fillImage;

    float worningPercentage = 0.7f;
    float dangerousPercentage = 0.3f;

    HpBar _hpBar;

    public void Init(int maximumHp, int currentHp)
    {
        _hpBar = new HpBar(_slider, _fillImage, worningPercentage, dangerousPercentage, maximumHp, currentHp);
    }

    public void UpdateManaged(int maximumHp, int currentHp)
    {
        _hpBar.Update(maximumHp, currentHp);
    }
}
