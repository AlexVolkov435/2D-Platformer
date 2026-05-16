using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
   [SerializeField] private Transform[] _waypoints;
   [SerializeField] private float _moveSpeed = 2f;
   [SerializeField] private float _reachDistance  = 0.1f;
   
   private Rigidbody2D _rigidbody2D;
   private Animator _animator;
   
   private int _currentPointIndex;
   private bool _isFacingRight = true;
   
   private string _moveX = "moveX";

   private void Awake()
   {
      Initialization();
   }

   private void Initialization()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _animator = GetComponent<Animator>();
   }

   private void FixedUpdate()
   {
      Patrol();
      Flip();
   }
   
   private void Patrol()
   {
      if (_waypoints == null || _waypoints.Length == 0)
         return;

      Transform currentWaypoint = _waypoints[_currentPointIndex];

      if (currentWaypoint == null)
         return;
      
      float distanceX = Mathf.Abs(transform.position.x - currentWaypoint.position.x);
        
      if (distanceX < _reachDistance)
      {
         _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
         currentWaypoint = _waypoints[_currentPointIndex];
            
         if (currentWaypoint == null)
            return;
      }
      
      float directionX = Mathf.Sign(currentWaypoint.position.x - transform.position.x);
        
      _animator.SetFloat(_moveX, Mathf.Abs(directionX));
      
      _rigidbody2D.linearVelocity = new Vector2(directionX * _moveSpeed, _rigidbody2D.linearVelocity.y);
        
      if (directionX > 0 && transform.localScale.x < 0)
      {
         transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
      }
      else if (directionX < 0 && transform.localScale.x > 0)
      {
         transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
      }
   }
   
   private void Flip()
   {
      _isFacingRight = !_isFacingRight;
      transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
   }
}