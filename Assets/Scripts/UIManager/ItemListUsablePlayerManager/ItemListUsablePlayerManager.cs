using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class ItemListUsablePlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _setPlaneItemImages;

    public ReactiveProperty<int> _activateItemImageOverlapFrameIndex = new ReactiveProperty<int>(0);
    public ReactiveProperty<int> _activateItemIndex = new ReactiveProperty<int>();

    //アイテム必要情報
    //画像リスト
    //個数

    public void Init()
    {
        InitItemList();
        _activateItemImageOverlapFrameIndex.Subscribe(_ =>
        {
            SetActiveAllCarryingItemCursor(false);
            SetActiveCarryingItemCursor(_, true);
        });
    }

    public void SetCarryingItemCursorFromRightArrow()
    {
        if (_activateItemImageOverlapFrameIndex.Value == 8)
        {
            _activateItemImageOverlapFrameIndex.Value = 0;
        }
        else
        {
            _activateItemImageOverlapFrameIndex.Value += 1;
        }
    }
    public void SetCarryingItemCursorFromLeftArrow()
    {
        if (_activateItemImageOverlapFrameIndex.Value == 0)
        {
            _activateItemImageOverlapFrameIndex.Value = 8;
        }
        else
        {
            _activateItemImageOverlapFrameIndex.Value -= 1;
        }
    }

    public void SetCarryingItemImage(
        List<Dictionary<string, int>> playerHaveItems,
        Dictionary<string, Sprite> sprites
        )
    {
        //リストの表示個数をUIに合わせる
        int length = playerHaveItems.Count;
        if (playerHaveItems.Count > _setPlaneItemImages.Length)
        {
            length = _setPlaneItemImages.Length;
        };

        //アイコン画像を設定
        for (int i = 0; i < length; i++)
        {
            string tagName = "";
            foreach (string key in playerHaveItems[i].Keys)
            {
                tagName = key;
            }
            _setPlaneItemImages[i].GetComponent<ItemListUsablePlayerIconPrefabManager>()
            .SetImage(sprites[tagName]);
        }

        //アイテム個数を設定
        for (int i = 0; i < length; i++)
        {
            int num = 0;
            foreach (int value in playerHaveItems[i].Values)
            {
                num = value;
                //１個の時は数字を表示しない
                if (num == 1) num = 0;
            }
            _setPlaneItemImages[i].GetComponent<ItemListUsablePlayerIconPrefabManager>()
            ._carryingItemCount.Value = num;
        }
    }

    void InitItemList()
    {
        for (int i = 0; i < _setPlaneItemImages.Length; i++)
        {
            _setPlaneItemImages[i].GetComponent<ItemListUsablePlayerIconPrefabManager>().Init();
        }
    }


    void SetActiveAllCarryingItemCursor(bool isActive)
    {
        for (int i = 0; i < _setPlaneItemImages.Length; i++)
        {
            _setPlaneItemImages[i].GetComponent<ItemListUsablePlayerIconPrefabManager>()
            ._activateItemImageOverlapFrame.Value = isActive;
        }
    }

    void SetActiveCarryingItemCursor(int index, bool isActive)
    {
        int NO_ITEM = -1;

        _setPlaneItemImages[index].GetComponent<ItemListUsablePlayerIconPrefabManager>()
            ._activateItemImageOverlapFrame.Value = isActive;
        if (_setPlaneItemImages[index].GetComponent<ItemListUsablePlayerIconPrefabManager>().GetSprite() == null)
        {
            _activateItemIndex.SetValueAndForceNotify(NO_ITEM);
        }
        else
        {
            _activateItemIndex.SetValueAndForceNotify(index);
        }

    }



    // -- destroy --

    void ItemListReactivePropertyDispose()
    {
        for (int i = 0; i < _setPlaneItemImages.Length; i++)
        {
            _setPlaneItemImages[i].GetComponent<ItemListUsablePlayerIconPrefabManager>().OnDestroy();
        }
    }

    public void onDestroyManaged()
    {
        ItemListReactivePropertyDispose();
        _activateItemIndex.Dispose();
        _activateItemImageOverlapFrameIndex.Dispose();
    }

}
