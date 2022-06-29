using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;


public class InputManager : MonoBehaviour
{
    float _axisXHorizontal;
    float _axisYVertical;
    float _mouseScrollWeelAxis;

    InputActions _inputActions;


    //攻撃ボタンのロック時間
    float _lockedAttackSeconds = 0.2f;

    //通常攻撃、2回攻撃
    InputManagerExtends.DoubleInput _doubleInputFromAttack;
    public float _timeLimitFromSecondAttackFire = 0.3f;
    public Action DefaultAttackAction = null;
    public Action DefaultSecondAttackAction = null;

    //ジャンプ
    public Action JumpAction = null;

    //掘る
    public Action PickUp = null;
    public Action PickDown = null;
    public Action PickLeft = null;
    public Action PickRight = null;
    public Action PickLeftUp = null;
    public Action PickRightUp = null;
    //UI
    public Action UIClickAction = null;
    public Action UISubmitAction = null;
    public Action UICancelAction = null;
    public Action LeftArrowAction = null;
    public Action RightArrowAction = null;
    public Action F1Action = null;
    public Action F2Action = null;
    public Action F3Action = null;
    public Action F4Action = null;
    public Action F5Action = null;
    public Action F6Action = null;
    public Action F7Action = null;
    public Action F8Action = null;
    public Action F9Action = null;

    // UIとPlayerActionの入力を切り替える
    public bool _activeUiInput = false;

    void Awake()
    {

        _inputActions = new InputActions();
        _inputActions.Enable();

        //attack
        _doubleInputFromAttack = new InputManagerExtends.DoubleInput(
            _timeLimitFromSecondAttackFire,
            _lockedAttackSeconds
        );

        _doubleInputFromAttack._defaultAttackSignal.Subscribe(_ =>
        {
            if (_)
            {
                DefaultAttackAction();
            }
        }).AddTo(this);

        _doubleInputFromAttack._secondAttackSignal.Subscribe(_ =>
        {
            if (_)
            {
                DefaultSecondAttackAction();
            }
        }).AddTo(this);
    }
    void Start()
    {

        // --- PlayerAction ---

        _inputActions.Player.Attack.performed += context =>
        {
            if (_activeUiInput) return;
            _doubleInputFromAttack.FireSignal();
        };

        //jump
        _inputActions.Player.Jump.performed += context =>
        {
            if (_activeUiInput) return;
            JumpAction();
        };

        //Pick
        _inputActions.Player.Top.performed += context =>
        {
            if (_activeUiInput) return;
            PickUp();
        };
        _inputActions.Player.Down.performed += context =>
        {
            if (_activeUiInput) return;
            PickDown();
        };
        _inputActions.Player.Left.performed += context =>
        {
            if (_activeUiInput) return;
            PickLeft();
        };
        _inputActions.Player.Right.performed += context =>
        {
            if (_activeUiInput) return;
            PickRight();
        };

        _inputActions.Player.LeftUp.performed += context =>
        {
            if (_activeUiInput) return;
            PickLeftUp();
        };
        _inputActions.Player.RightUp.performed += context =>
        {
            if (_activeUiInput) return;
            PickRightUp();
        };


        // --- UI ---
        _inputActions.UI.Click.canceled += context =>
        {
            if (!_activeUiInput) return;
            UIClickAction();
        };
        _inputActions.UI.Cancel.canceled += context =>
        {
            if (!_activeUiInput) return;
            UICancelAction();
        };
        _inputActions.UI.Submit.canceled += context =>
        {
            if (!_activeUiInput) return;
            UISubmitAction();
        };

        _inputActions.UI.LeftArrow.performed += context =>
        {
            LeftArrowAction();
        };
        _inputActions.UI.RightArrow.performed += context =>
        {
            RightArrowAction();
        };

        _inputActions.UI.F1.performed += context =>
        {
            F1Action();
        };
        _inputActions.UI.F2.performed += context =>
        {
            F2Action();
        };
        _inputActions.UI.F3.performed += context =>
        {
            F3Action();
        };
        _inputActions.UI.F4.performed += context =>
        {
            F4Action();
        };
        _inputActions.UI.F5.performed += context =>
        {
            F5Action();
        };
        _inputActions.UI.F6.performed += context =>
        {
            F6Action();
        };
        _inputActions.UI.F7.performed += context =>
        {
            F7Action();
        };
        _inputActions.UI.F8.performed += context =>
        {
            F8Action();
        };
        _inputActions.UI.F9.performed += context =>
        {
            F9Action();
        };
    }



    void Update()
    {
        if (_activeUiInput) return;
        _doubleInputFromAttack.Update();
        _axisXHorizontal = _inputActions.Player.Move.ReadValue<Vector2>().x;
        _axisYVertical = _inputActions.Player.Move.ReadValue<Vector2>().y;
        _mouseScrollWeelAxis = Input.GetAxis("Mouse ScrollWheel");
    }

    // -- get set
    public float GetAxisHorizontal()
    {
        return _axisXHorizontal;
    }
    public float GetAxisVertical()
    {
        return _axisYVertical;
    }
    public float GetMouseScrollWeelAxis()
    {
        return _mouseScrollWeelAxis;
    }

    private void OnDestroy()
    {
        _doubleInputFromAttack.onDestroy();
        _inputActions.Dispose();
    }
}
