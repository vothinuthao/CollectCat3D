using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _dashAction;

    public Vector2 InputVector { get; private set; } // Lưu trữ đầu vào di chuyển
    public Vector2 MousePosition { get; private set; } // Lưu trữ vị trí chuột

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing!");
            return;
        }

        // Lấy tham chiếu đến các hành động
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _dashAction = _playerInput.actions["Dash"];

        if (_moveAction == null || _jumpAction == null || _dashAction == null)
        {
            Debug.LogError("One or more actions are missing in the Input Action Asset!");
            return;
        }
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện cho hành động di chuyển
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;

        // Đăng ký sự kiện cho hành động nhảy và lướt
        _jumpAction.performed += OnJumpPerformed;
        _dashAction.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;

        // Hủy đăng ký sự kiện cho hành động nhảy và lướt
        _jumpAction.performed -= OnJumpPerformed;
        _dashAction.performed -= OnDashPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        InputVector = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Jump action performed");
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Dash action performed");
    }

    public bool IsJumpPressed()
    {
        return _jumpAction?.triggered ?? false;
    }

    public bool IsDashPressed()
    {
        return _dashAction?.triggered ?? false;
    }

    private void Update()
    {
        // Cập nhật vị trí chuột
        MousePosition = Mouse.current.position.ReadValue();
    }
}