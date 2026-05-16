using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f; 
    [SerializeField] private Transform _target; 
    
    private void FixedUpdate() 
    {
        Vector3 position = _target.position;
        position.z = -5;
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.fixedDeltaTime); 
    }
}
