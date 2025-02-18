using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isGrounded;
    private InputManager _inputManager;

    [SerializeField]
    private float _jumpForce = 8f; // Lực nhảy
    [SerializeField]
    private float _dashSpeed = 10f; // Tốc độ lướt
    
    [SerializeField]
    private float MovementSpeed = 5f; // Tốc độ di chuyển
    [SerializeField]
    private float RotationSpeed = 360f; // Tốc độ xoay
    [SerializeField]
    private Transform _cameraTransform; // Tham chiếu đến Cinemachine Virtual Camera

    [SerializeField]
    private LayerMask _groundLayer; // Layer của mặt đất
    [SerializeField]
    private float _groundCheckDistance = 0.2f; // Khoảng cách raycast

    private bool _canDash = true; // Biến kiểm tra cooldown cho Dash
    private Coroutine _dashCooldownCoroutine;
    [SerializeField]
    private Animator _animator;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        if (_inputManager == null)
        {
            Debug.LogError("InputManager is missing!");
        }

        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody is missing!");
        }

        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
            if (_cameraTransform == null)
            {
                Debug.LogError("Main camera is missing! Please assign a camera.");
            }
        }

        _rb.freezeRotation = true; // Ngăn nhân vật tự xoay
        _rb.linearDamping = 0f; // Loại bỏ lực cản
    }

  

    private void Update()
    {
        //DebugInputs();

        OnDash();
        OnJump();

        var targetVector = new Vector3(_inputManager.InputVector.x, 0, _inputManager.InputVector.y);
        MoveTowardTarget(targetVector);
        float speed = targetVector.magnitude;
        UpdateAnimator(speed);
        RotateTowardMovementVector(targetVector);
        
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
       // Debug.Log($"Is Grounded: {_isGrounded}");
    }

    private void DebugInputs()
    {
        if (_inputManager.IsJumpPressed())
        {
            Debug.Log("Jump Pressed");
        }

        if (_inputManager.IsDashPressed())
        {
            Debug.Log("Dash Pressed");
        }
    }

    private void UpdateAnimator(float speed)
    {
        if (_animator != null )
        {
            bool isMoving = speed > 0.1f; // Nếu tốc độ > 0.1, coi như nhân vật đang di chuyển
            _animator.SetBool("isMoving", isMoving);
        }
    }
    private void MoveTowardTarget(Vector3 targetVector)
    {
        targetVector = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * targetVector;
        _rb.linearVelocity = new Vector3(targetVector.x * MovementSpeed, _rb.linearVelocity.y, targetVector.z * MovementSpeed);
      
    }

    // private void RotateFromMouseVector()
    // {
    //     Ray ray = _cameraTransform.GetComponent<Camera>().ScreenPointToRay(_inputManager.MousePosition);
    //     if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
    //     {
    //         var target = hitInfo.point;
    //         target.y = transform.position.y;
    //         transform.LookAt(target);
    //     }
    // }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0) return;
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    private void OnJump()
    {
        if (_inputManager.IsJumpPressed() && _isGrounded)
        {
            Debug.Log("Jump Activated");
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpForce, _rb.linearVelocity.z);
        }
    }

    public void OnDash()
    {
        if (!_canDash || _rb == null || _inputManager == null) return;

        if (_inputManager.IsDashPressed()&& _isGrounded)
        {
            Debug.Log("Dash Activated");
            _rb.linearVelocity = transform.TransformDirection(Vector3.forward) * _dashSpeed;

            _canDash = false;

            if (_dashCooldownCoroutine != null)
            {
                StopCoroutine(_dashCooldownCoroutine);
            }
            _dashCooldownCoroutine = StartCoroutine(DashCooldown(5f));
        }
        
    }

    private IEnumerator DashCooldown(float cooldownTime)
    {
        Debug.Log("Dash Cooldown Started");
        yield return new WaitForSeconds(cooldownTime);
        _canDash = true;
        Debug.Log("Dash Cooldown Ended");
    }
}