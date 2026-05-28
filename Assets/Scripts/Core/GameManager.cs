using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _spawnPos;

    private InputHandler _inputHandler = new();

    [Header("Time")]
    [SerializeField] private float _startTime_BASE = 150f;
    [SerializeField] private float _currentTime;

    #region Debug
    [SerializeField] private bool _isPlaying = false;
    #endregion

    private void Awake()
    {
        _inputHandler.Init();

        if ( _isPlaying )
        {
            StartGame();
        }
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
        _playerController = Instantiate(_playerController, _spawnPos.position, _spawnPos.rotation);

        _currentTime = _startTime_BASE;

        _inputHandler.InitInputs(_playerController);
        _inputHandler.Inputs.Enable();
    }

    private void FinishGame()
    {

    }
}
