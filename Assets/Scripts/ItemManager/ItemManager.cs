using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] _itemPrefabs;
    [SerializeField] GameObject _loadDedicatedPrefabs;
    public ReactiveProperty<string> _updatePlayerHaveItemTags = new ReactiveProperty<string>();
    List<Dictionary<string, int>> _playerHaveItems = new List<Dictionary<string, int>>();
    Dictionary<string, Sprite> _itemIconSprites;
    Dictionary<string, Sprite> _itemEquipSprites;

    public enum ItemTypes
    {
        Consumption,
        Wepon,
        Tool,
        Stand,
        Light
    }

    /*_playerHaveItems
     *  [
     *     { "cherry":1},
     *     { "cherry":1},
     *     { "cherry":1}
     *  ]
     */


    public void Init()
    {
        _itemIconSprites = GenerateItemIconSprites();
        _itemEquipSprites = GenerateItemEquipSprites();
    }

    public void AddPlayerHaveItemTags(string tagName)
    {
        //所持アイテムが空ではない
        if (_playerHaveItems.Count != 0)
        {
            //同じアイテムを所持していない
            if (!PlayerHaveItemsTagNameIncludes(tagName))
            {
                //新規アイテムを追加
                AddNewlyItemFromTagName(tagName);
            }
            //同じアイテムを所持している
            else
            {
                if (ItemAllLimitCountIf(tagName))
                {
                    //新規アイテムを追加
                    AddNewlyItemFromTagName(tagName);
                }
                else
                {
                    //数を追加
                    SetNumToPlayerItemFromTagName(tagName);
                }
            }
        }
        //所持しているアイテムが空
        else
        {
            //新規アイテムを追加
            AddNewlyItemFromTagName(tagName);
        }

    }

    //アイテムが全て限界個数であるか
    bool ItemAllLimitCountIf(string tagName)
    {
        bool flug = false;
        for (int i = 0; i < _playerHaveItems.Count; i++)
        {
            if (_playerHaveItems[i].ContainsKey(tagName))
            {
                ////アイテムが限界個数の場合
                if (_playerHaveItems[i][tagName] == GetItemLimitLengthFromTagName(tagName))
                {
                    flug = true;
                }
                else
                {
                    flug = false;
                }
            }
        }
        return flug;
    }

    //タグ名が含まれているか
    bool PlayerHaveItemsTagNameIncludes(string tagName)
    {
        bool flug = false;
        for (int i = 0; i < _playerHaveItems.Count; i++)
        {
            if (_playerHaveItems[i].ContainsKey(tagName))
            {
                flug = true;
            }
        }
        return flug;
    }

    //新規アイテムを追加
    void AddNewlyItemFromTagName(string tagName)
    {
        Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        _dictionary.Add(tagName, 1);
        _playerHaveItems.Add(_dictionary);
    }

    //数を追加
    void SetNumToPlayerItemFromTagName(string tagName)
    {
        for (int i = 0; i < _playerHaveItems.Count; i++)
        {
            ////限界個数ではないアイテムの場合
            if (_playerHaveItems[i].ContainsKey(tagName))
            {
                if (_playerHaveItems[i][tagName] != GetItemLimitLengthFromTagName(tagName))
                {
                    //数を追加
                    _playerHaveItems[i][tagName] += 1;
                }
            }
        }
    }

    //アイテムの最大保持数を取得
    int GetItemLimitLengthFromTagName(string tagName)
    {
        int maximum = 0;
        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            if (_itemPrefabs[i].tag == tagName)
            {
                maximum = _itemPrefabs[i].GetComponent<ItemPrefabManager>().maxLength;
            }
        }

        return maximum;
    }

    //アイテムアイコンのスプライトシートを生成
    Dictionary<string, Sprite> GenerateItemIconSprites()
    {
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            sprites.Add(_itemPrefabs[i].tag, _itemPrefabs[i].GetComponent<ItemPrefabManager>()._iconSprite);
        }

        return sprites;
    }

    //アイテム装備時のスプライトシートを生成
    Dictionary<string, Sprite> GenerateItemEquipSprites()
    {
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            sprites.Add(_itemPrefabs[i].tag, _itemPrefabs[i].GetComponent<ItemPrefabManager>()._eqipSprite);
        }

        return sprites;
    }

    //プレイヤーの所持アイテムリストを取得
    public List<Dictionary<string, int>> GetPlayerHaveItems()
    {
        return _playerHaveItems;
    }

    //アイテムアイコンのスプライトリストを取得
    public Dictionary<string, Sprite> GetItemIconSprites()
    {
        return _itemIconSprites;
    }

    //アイテム装備時のスプライトリストを取得
    public Dictionary<string, Sprite> GetItemEquipSprites()
    {
        return _itemEquipSprites;
    }

    //プレハブからタイプを取得する
    public string GetTypeFromTagName(string tagName)
    {
        string _type = null;
        foreach (var type in Enum.GetValues(typeof(ItemTypes)))
        {
            if (tagName.Contains(type.ToString())) _type = type.ToString();
        }
        return _type;
    }

    //アイテムのプレハブ生成
    public void InstantiatePrefab(int index, Vector2 vector2)
    {
        Instantiate(_itemPrefabs[index], vector2, Quaternion.identity, this.transform).SetActive(true);
    }

    //アイテムのプレハブをランダムに生成
    public void InstantiateRandomPrefabFromPositionsAndRate(List<Vector2> positions, int[] rate)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Instantiate(_itemPrefabs[Common.GetRandomInt.GetRandomIndex(rate)], positions[i], Quaternion.identity, this.transform).SetActive(true);
        }
    }
    private void OnDestroy()
    {
        _updatePlayerHaveItemTags.Dispose();
    }
}
