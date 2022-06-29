using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemListUsablePlayerIconPrefabManager : MonoBehaviour
{
    [SerializeField]
    GameObject ItemImage;
    [SerializeField]
    GameObject ItemImageOverlapFrame;
    [SerializeField]
    GameObject CarryingItemCount;

    public ReactiveProperty<int> _carryingItemCount = new ReactiveProperty<int>();
    public ReactiveProperty<bool> _activateItemImageOverlapFrame = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> _activateEmptyImage = new ReactiveProperty<bool>();

    public void Init()
    {
        _activateEmptyImage.Subscribe(_ =>
        {
            if (_) ItemImage.GetComponent<Image>().enabled = true;
            else ItemImage.GetComponent<Image>().enabled = false;
        });

        _activateItemImageOverlapFrame.Subscribe(_ =>
        {
            if (_) ItemImageOverlapFrame.GetComponent<Image>().enabled = true;
            else ItemImageOverlapFrame.GetComponent<Image>().enabled = false;
        });

        _carryingItemCount.Subscribe(_ =>
        {
            if (_ <= 0) CarryingItemCount.GetComponent<Text>().enabled = false;
            else
            {
                CarryingItemCount.GetComponent<Text>().enabled = true;
                CarryingItemCount.GetComponent<Text>().text = _.ToString();
            }
        });
    }


    public Sprite GetSprite()
    {
        return ItemImage.GetComponent<Image>().sprite;
    }

    public void SetImage(Sprite sprite)
    {
        ItemImage.GetComponent<Image>().sprite = sprite;
        _activateEmptyImage.Value = true;
    }

    public void OnDestroy()
    {
        _carryingItemCount.Dispose();
        _activateItemImageOverlapFrame.Dispose();
        _activateEmptyImage.Dispose();
    }
}
