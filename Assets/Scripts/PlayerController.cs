using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Links")]
    public Camera Camera => _camera;

    [Header("CORE")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Camera _camera;

    [Header("Look")]
    [SerializeField] private float _mouseSensivity = 1f;
    [SerializeField] [Range(30, 140)] private float _fieldOfView = 80f;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 0.6f, 0f);
    private Vector2 _cameraInput;
    private float _cameraVerticalAngle = 0f;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 3f;
    private Vector2 _moveInput;
    // maybe add _moveSpeed that need to be filled for max speed by time

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _gravityMultiplier = 1f;
    private float _velocity_Y = 0;
    private bool _isJumping;
    public bool IsGrounded => _controller.isGrounded;

    //[Header("Crouch")]
    //[SerializeField] private float 

    [Header("Dash")]
    [SerializeField] private float _dashDistance = 3f;
    [SerializeField] private float _dashTime = 0.1f;
    private Vector2 _dashVelocity;

    [Header("Weapon")]
    [SerializeField] private A_Weapon _currentWeapon;

    private void Update()
    {
        HandleRotation();
        SetGravity();

        HandleJump();
        HandleMovement();

#if UNITY_EDITOR
        //GetMouse();
        _camera.fieldOfView = _fieldOfView;
        _camera.transform.localPosition = _cameraOffset;
#endif
    }

    #region Input
    public void OnLookInput(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumping = context.ReadValueAsButton();
        // maybe invoke action/unitask for double jump etc
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {

    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
            _currentWeapon.Shoot();
    }
    #endregion

    #region Handle
    private void SetGravity()
    {
        if (IsGrounded && !_isJumping)
        {
            _velocity_Y = -1f;
        }
        else
        {
            _velocity_Y += GameVariables.Gravity * _gravityMultiplier * Time.deltaTime; // negative
        }
    }

    private void HandleRotation()
    {
        float mouseX = _cameraInput.x * _mouseSensivity;
        float mouseY = _cameraInput.y * _mouseSensivity;

        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0, 0);
        this.transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        if (_isJumping && IsGrounded)
        {
            _velocity_Y = _jumpPower;
        }
    }

    private void HandleMovement()
    {
        Vector3 move = (
            this.transform.right * _moveInput.x * _walkSpeed
            + this.transform.up * _velocity_Y
            + this.transform.forward * _moveInput.y * _walkSpeed
            );

        _controller.Move(move * Time.deltaTime);
    }

    private void HandleCrouch()
    {

    }

    private void HandleDash()
    {
        float iHateYellow = _dashDistance + _dashTime;
    }

    #endregion

    #region Debub_funny
    public void GetMouse()
    {
        Debug.Log(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
    }

    [Header("DEBUG_PUSH")]
    [SerializeField] private float _maxPushMass = 15;
    [SerializeField] private float _minPushMass = 1;
    [SerializeField] private float _pushPower = 5;
    public bool IsPushEnabled = true;

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!IsPushEnabled) return;
        
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic || rb.mass <= _minPushMass || rb.mass >= _maxPushMass)
            return;

        if (hit.moveDirection.y < -0.3f) 
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        float adjustedForce = _pushPower / rb.mass;
        rb.AddForce(pushDirection * adjustedForce, ForceMode.Impulse);
    }
    #endregion
}