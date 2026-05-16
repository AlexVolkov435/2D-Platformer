using UnityEngine;

public class Coin :MonoBehaviour
{
    [SerializeField] private int _numberCoins;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        { 
            GameEvents.OnPayerReceivedCoin(_numberCoins);
        }
    }
}
