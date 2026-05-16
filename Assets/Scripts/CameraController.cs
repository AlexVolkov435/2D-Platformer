using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 3f; 
    [SerializeField] private Transform target; 
    
    private void FixedUpdate() 
    {
        Vector3 position = target.position;
        position.z = -5;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.fixedDeltaTime); 
    }
}
