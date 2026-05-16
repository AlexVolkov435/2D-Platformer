using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _verticalOffset;
    
    [SerializeField] private float _speed = 3f; 
    [SerializeField] private Transform _target; 
    
    private float _depthOffset = -5f;
    
    private void FixedUpdate() 
    {
        Vector3 position = _target.position;
        
        Vector3 desiredPosition = new Vector3(
            position.x + _horizontalOffset,
            position.y + _verticalOffset,
            position.z + _depthOffset
        );
     
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.fixedDeltaTime); 
    }
}
