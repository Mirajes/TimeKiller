public class InputHandler
{
    public InputSystem_Actions Inputs => _inputs;
    private InputSystem_Actions _inputs;

    public void Init()
    {
        _inputs = new InputSystem_Actions();
    }

    public void InitInputs(PlayerController playerController)
    {
        _inputs.Player.Move.performed += playerController.OnMoveInput;
        _inputs.Player.Move.canceled += playerController.OnMoveInput;

        _inputs.Player.Look.performed += playerController.OnLookInput;
        _inputs.Player.Look.canceled += playerController.OnLookInput;

        _inputs.Player.Jump.performed += playerController.OnJumpInput;
        _inputs.Player.Jump.canceled += playerController.OnJumpInput;
    }

    public void RemoveInputs(PlayerController playerController)
    {
        _inputs.Player.Move.performed -= playerController.OnMoveInput;
        _inputs.Player.Move.canceled -= playerController.OnMoveInput;

        _inputs.Player.Look.performed -= playerController.OnLookInput;
        _inputs.Player.Look.canceled -= playerController.OnLookInput;

        _inputs.Player.Jump.performed -= playerController.OnJumpInput;
        _inputs.Player.Jump.canceled -= playerController.OnJumpInput;
    }
}