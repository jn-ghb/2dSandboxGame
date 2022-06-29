using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameManager;

    [SerializeField]
    GameObject _HpBarManager;
    HpBarManager _hpBarManager;

    [SerializeField]
    GameObject _ItemListUsablePlayerManager;
    ItemListUsablePlayerManager _itemListUsablePlayerManager;


    public void Init(int maximumHp, int currentHp)
    {
        _hpBarManager.Init(maximumHp, currentHp);
    }

    void Awake()
    {
        _hpBarManager = _HpBarManager.GetComponent<HpBarManager>();
        _itemListUsablePlayerManager = _ItemListUsablePlayerManager.GetComponent<ItemListUsablePlayerManager>();
        _itemListUsablePlayerManager.Init();

    }

    // Update is called once per frame
    void Update()
    {
        //_hpBarManager.UpdateManaged(maximumHp, currentHp);
    }

    public void HpBarUpdateInit(int maximumHp, int currentHp)
    {
        _hpBarManager.Init(maximumHp, currentHp);
    }

    public void HpBarUpdate(int maximumHp, int currentHp)
    {
        _hpBarManager.UpdateManaged(maximumHp, currentHp);
    }


    public void SetForcusStartButton()
    {
        // EventSystem.current.SetSelectedGameObject(StartButton);
    }

    public void ClickEvents()
    {
        if (EventSystem.current.currentSelectedGameObject == null) return;


        //if (EventSystem.current.currentSelectedGameObject.tag == "Button_Start")
        //{
        //    SceneManager.LoadScene("Main");
        //}
        //else if (EventSystem.current.currentSelectedGameObject.tag == "Button_End")
        //{
        //    _gameManager.Quit();
        //}
    }

    public void SetCarryingItemCursor(int index)
    {
        _itemListUsablePlayerManager._activateItemImageOverlapFrameIndex.Value = index;
    }

    public void SetCarryingItemCursorFromRightArrow()
    {
        _itemListUsablePlayerManager.SetCarryingItemCursorFromRightArrow();
    }
    public void SetCarryingItemCursorFromLeftArrow()
    {
        _itemListUsablePlayerManager.SetCarryingItemCursorFromLeftArrow();
    }
    public void SetCarryingItemImage(
        List<Dictionary<string, int>> playerHaveItems,
        Dictionary<string, Sprite> sprites
       )
    {
        _itemListUsablePlayerManager.SetCarryingItemImage(playerHaveItems, sprites);
    }

    //ユーザー所持アイテムUIから、フォーカスされているIndexを取得する
    public ReactiveProperty<int> GetCarryingItemIndexFocusedCursor()
    {
        return _itemListUsablePlayerManager._activateItemIndex;
    }

    private void OnDestroy()
    {
        _itemListUsablePlayerManager.onDestroyManaged();
    }
}
