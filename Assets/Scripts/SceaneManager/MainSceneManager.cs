using UnityEngine;
using UniRx;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerManager;
    [SerializeField]
    private GameObject UIManager;
    [SerializeField]
    private GameObject InputManager;
    [SerializeField]
    private GameObject TileMapMamnager;
    [SerializeField]
    private GameObject CameraManager;

    [SerializeField]
    private GameObject ItemMamnager;

    InputManager _inputManager;
    Player.PlayerManager _playerManager;
    Player.PlayerStateController _playerStateController;
    CameraManager _cameraManager;
    ItemManager _itemMamnager;
    UIManager _uIManager;
    TileMapManager _tileMapManager;
    public int _seed = 50;


    private void Awake()
    {

        //managers
        _inputManager = InputManager.GetComponent<InputManager>();
        _cameraManager = CameraManager.GetComponent<CameraManager>();
        _playerManager = PlayerManager.GetComponent<Player.PlayerManager>();
        _playerStateController = _playerManager.GetPlayerStateController();
        _uIManager = UIManager.GetComponent<UIManager>();
        _tileMapManager = TileMapMamnager.GetComponent<TileMapManager>();
        _itemMamnager = ItemMamnager.GetComponent<ItemManager>();
        _inputManager._activeUiInput = false;

        //リソースロード
        _itemMamnager.Init();

        //シード値初期化
        Random.InitState(_seed);

        //タイルマップ生成
        _tileMapManager.Init();

        //アイテムを生成
        _itemMamnager.InstantiateRandomPrefabFromPositionsAndRate(
             _tileMapManager.GetAirBlockPositionsFromMaximumXAndYAndRate(200, 1, new int[] { 3, 7 }),
             new int[] { 2, 10, 4, 5, 3, 2, 8 }
             );

        //攻撃ボタンのアクション
        _inputManager.DefaultAttackAction = () =>
        {
            _playerManager.DefaultAttack();
            _playerManager.Pick();
        };
        _inputManager.DefaultSecondAttackAction = () => _playerManager.DefaultSecondAttack();

        //ジャンプ
        _inputManager.JumpAction = () => _playerManager.Jump();

        //掘る
        _inputManager.PickUp = () => _playerManager.PickUp(); ;
        _inputManager.PickDown = () => _playerManager.PickDown();
        _inputManager.PickLeft = () => _playerManager.PickLeft();
        _inputManager.PickRight = () => _playerManager.PickRight();
        _inputManager.PickLeftUp = () => _playerManager.PickLeftUp();
        _inputManager.PickRightUp = () => _playerManager.PickRightUp();

        //ユーザーアイテム所持リストのカーソル更新
        _inputManager.RightArrowAction = () => _uIManager.SetCarryingItemCursorFromRightArrow();
        _inputManager.LeftArrowAction = () => _uIManager.SetCarryingItemCursorFromLeftArrow();
        _inputManager.F1Action = () => _uIManager.SetCarryingItemCursor(1);
        _inputManager.F2Action = () => _uIManager.SetCarryingItemCursor(2);
        _inputManager.F3Action = () => _uIManager.SetCarryingItemCursor(3);
        _inputManager.F4Action = () => _uIManager.SetCarryingItemCursor(4);
        _inputManager.F5Action = () => _uIManager.SetCarryingItemCursor(5);
        _inputManager.F6Action = () => _uIManager.SetCarryingItemCursor(6);
        _inputManager.F7Action = () => _uIManager.SetCarryingItemCursor(7);
        _inputManager.F8Action = () => _uIManager.SetCarryingItemCursor(8);
        _inputManager.F9Action = () => _uIManager.SetCarryingItemCursor(9);


        //プレイヤーがpickし、ブロックを破壊した
        _playerManager.GetDestroyBlockSignalFromPick().Subscribe(_ =>
        {
            if (_)
            {
                Debug.Log("Item_block_" + _playerManager.GetDestroyBlockSpriteNameFromPick());
            }
        });

        //HPバーの更新
        _uIManager.HpBarUpdateInit(
            _playerStateController._MaxixmuHp.Value,
                _playerStateController._Hp.Value
            );

        _playerStateController._Hp.Subscribe(_ =>
        {
            _uIManager.HpBarUpdate(
                _playerStateController._MaxixmuHp.Value,
                _playerStateController._Hp.Value
                );
        });

        //プレイヤーの装備タイプ
        _playerStateController._EquipWeponType.Subscribe(_ =>
        {
            //消費アイテムの場合
            if (_ == ItemManager.ItemTypes.Consumption.ToString() || _ == "")
            {
                _playerManager.ActiveAttackEffect(false);
                _playerStateController._isPicking.Value = false;
            }
            else if (_ == ItemManager.ItemTypes.Wepon.ToString())
            {
                _playerManager.ActiveAttackEffect(true);
                _playerStateController._isPicking.Value = false;
            }
            else if (_ == ItemManager.ItemTypes.Tool.ToString())
            {
                _playerManager.ActiveAttackEffect(false);
                _playerStateController._isPicking.Value = true;
            }
        });

        //プレイヤーがアイテムを取得した
        _playerStateController._isGetItem.Skip(1).Subscribe(_ =>
        {
            //プレイヤーの所持アイテムにタグを追加
            _itemMamnager._updatePlayerHaveItemTags.SetValueAndForceNotify(_);
        });

        //プレイヤーの所持アイテムが変更された
        _itemMamnager._updatePlayerHaveItemTags.Skip(1).Subscribe(_ =>
        {
            _itemMamnager.AddPlayerHaveItemTags(_);
            if (_playerStateController._EquipWepon.Value == "")
            {
                _playerManager.SetEquipSprite(_itemMamnager.GetItemEquipSprites()[_]);
                _playerStateController._EquipWepon.Value = _;
                _playerStateController._EquipWeponType.Value = _itemMamnager.GetTypeFromTagName(_);
            }

            _uIManager.SetCarryingItemImage(
                _itemMamnager.GetPlayerHaveItems(),
                _itemMamnager.GetItemIconSprites()
                );
        });

        //プレイヤーの所持アイテムUI_カーソルがフォーカスしているアイテムが変更された
        _uIManager.GetCarryingItemIndexFocusedCursor().Skip(1).Subscribe(_ =>
        {
            int NO_ITEM = -1;
            int NOT_HAVE_ITEM = 0;

            if (_ == NO_ITEM)
            {
                _playerManager.SetEquipSprite(null);
                _playerStateController._EquipWepon.Value = "";
                _playerStateController._EquipWeponType.Value = "";
                return;
            }

            if (_itemMamnager.GetPlayerHaveItems().Count == NOT_HAVE_ITEM)
            {
                _playerManager.SetEquipSprite(null);
                _playerStateController._EquipWepon.Value = "";
                _playerStateController._EquipWeponType.Value = "";
            }
            string tagName = "";
            foreach (var key in _itemMamnager.GetPlayerHaveItems()[_].Keys)
            {
                tagName = key;
            }
            _playerManager.SetEquipSprite(_itemMamnager.GetItemEquipSprites()[tagName]);
            _playerStateController._EquipWepon.Value = tagName;
            _playerStateController._EquipWeponType.Value = _itemMamnager.GetTypeFromTagName(tagName);
        });

    }

    private void Update()
    {
        _playerManager.UpdateManaged(_inputManager.GetAxisHorizontal(), _inputManager.GetAxisVertical());
        _cameraManager.UpdateManaged(_inputManager.GetMouseScrollWeelAxis());
    }

}
