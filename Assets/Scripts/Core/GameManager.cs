using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private InputHandler _inputHandler = new();

    private void Awake()
    {
        _inputHandler.Init();
        _inputHandler.InitInputs(_playerController);
        _inputHandler.Inputs.Enable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        // ???
        _inputHandler.Inputs?.Disable();
        _inputHandler.RemoveInputs(_playerController);
        _inputHandler.Inputs?.Dispose();
        // ???
    }

    private void StartGame()
    {

    }

    private void FinishGame()
    {

    }
}
