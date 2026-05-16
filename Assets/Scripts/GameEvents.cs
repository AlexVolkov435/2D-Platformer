using System;

public static class GameEvents
{
    public static Action<int> PayerReceivedCoin;
    
    public static void EnemyKilled(int number)
        => PayerReceivedCoin?.Invoke(number);
}
