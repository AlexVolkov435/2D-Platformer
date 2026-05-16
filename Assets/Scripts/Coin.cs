using UnityEngine;

public class Coin :MonoBehaviour
{
    [SerializeField] private int _numberOfCoins;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        { 
            GameEvents.EnemyKilled(_numberOfCoins);
        }
    }
}
