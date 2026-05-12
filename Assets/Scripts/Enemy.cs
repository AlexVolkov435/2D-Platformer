using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
   [SerializeField] private Transform[] _waypoints;
   [SerializeField] private float _moveSpeed = 2f;
   [SerializeField] private float _reachDistance  = 0.1f;
   
   private int _currentPointIndex;
   private bool _isFacingRight = true;
   
   private void FixedUpdate()
   {
      Patrol();
      Flip();
   }

   private void Patrol()
   {
      if (_waypoints.Length == 0) return;

      Transform targetWaypoint = _waypoints[_currentPointIndex];
        
      transform.position = Vector3.MoveTowards(
         transform.position,
         targetWaypoint.position,
         _moveSpeed * Time.fixedDeltaTime
      );
        
      if (Vector3.Distance(transform.position, targetWaypoint.position) <= _reachDistance)
      {
         _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
      }
   }
   
   private void Flip()
   {
      _isFacingRight = !_isFacingRight;
      transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
   }
}