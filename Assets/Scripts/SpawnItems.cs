using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    
    [SerializeField] Coin _coinPrefab;
    [SerializeField] private TextMeshProUGUI _text;

    private int _spawnDelay = 3;
    private int _numberOfCoins;
    
    private PoolMono<Coin> _pool;
    
    private void Awake()
    {
        _pool = new PoolMono<Coin>(_coinPrefab, _poolCount, transform);
        _pool.SetAutoExpand(_autoExpand);
    }

    private void Start()
    {
       StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        GameEvents.PayerReceivedCoin += OnReceiveCoin;
    }

    private void OnDisable()
    {
        GameEvents.PayerReceivedCoin -= OnReceiveCoin;
    }

    private void OnReceiveCoin(int number)
    {
        _numberOfCoins += number;
        _text.text = _numberOfCoins.ToString();
    }
    
    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
        
        while (enabled)
        {
            yield return wait;
            CreateItem();
        }
    }

    private void CreateItem()
    {
        Coin coin = _pool.GetFreeElement();
        coin.transform.position = transform.position;
    }
}
