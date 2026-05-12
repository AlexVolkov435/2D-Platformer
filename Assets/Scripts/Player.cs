using UnityEngine;

public class Player : MonoBehaviour
{
      [Header("Movement Settings")]
      [SerializeField] private float _moveSpeed = 5f;
      [SerializeField] private float _jumpForce = 7f;
  
      [Header("Ground Detection")]
      [SerializeField] private Transform _groundCheck;
      [SerializeField] private float _groundDistance = 0.4f;
      [SerializeField] private LayerMask _groundMask;
  
      private bool _jumpPressed;
      private bool _isFacingRight = true;
      
      private Vector2 _moveInput;
      private Rigidbody2D _rigidbody2D;
      private InputSystem_Actions _inputSystem;
    
      private void Awake()
      {
          Initialization();
      }

      private void Initialization()
      {
          _rigidbody2D = GetComponent<Rigidbody2D>();
          _inputSystem = new InputSystem_Actions();
          
          _inputSystem.Player.Move.performed += context => _moveInput = context.ReadValue<Vector2>();
          _inputSystem.Player.Move.canceled += context => _moveInput = context.ReadValue<Vector2>();
          _inputSystem.Player.Jump.performed += contextJump => _jumpPressed = true;
      }
      
      private void OnEnable()
      {
          _inputSystem.Enable();
      }
  
      private void OnDisable()
      {
          _inputSystem.Disable();
      }
  
      private void FixedUpdate()
      {
          Move();
          Jump();
      }
  
      private void Move()
      {
          float moveX = _moveInput.x;
          Vector2 movement = new Vector2(moveX * _moveSpeed, _rigidbody2D.linearVelocity.y);
          _rigidbody2D.linearVelocity = movement;

          ChooseTurn(moveX);
      }

      private void ChooseTurn(float moveX)
      {
          if (moveX > 0 && !_isFacingRight)
              Flip();
          else if (moveX < 0 && _isFacingRight)
              Flip();
      }
  
      private void Jump()
      {
          if (_jumpPressed && IsGrounded())
          {
              _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
              _jumpPressed = false;
          }
      }
  
      private bool IsGrounded()
      {
          return Physics2D.OverlapCircle(_groundCheck.position, _groundDistance, _groundMask);
      }
  
      private void Flip()
      {
          _isFacingRight = !_isFacingRight;
          transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
      }
  }
