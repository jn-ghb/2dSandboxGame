using UnityEngine;

public class StartSceanManager : MonoBehaviour
{
    [SerializeField]
    GameObject InputManager;

    [SerializeField]
    GameObject UIManager;

    InputManager _inputManager;
    StartUIManager _uIManager;

    void Awake()
    {
        _uIManager = UIManager.GetComponent<StartUIManager>();

        _inputManager = InputManager.GetComponent<InputManager>();
        _inputManager._activeUiInput = true;

        _uIManager.SetForcusStartButton();

        _inputManager.UIClickAction = () =>
        {
            _uIManager.ClickEvents();
        };
        _inputManager.UISubmitAction = () =>
        {
            _uIManager.ClickEvents();
        };
    }

    void Update()
    {

    }
}
