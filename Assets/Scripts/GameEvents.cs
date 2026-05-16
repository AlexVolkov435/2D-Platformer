using System;

public static class GameEvents
{
    public static Action<int> PayerReceivedCoin;
    
    public static void OnPayerReceivedCoin(int number)
        => PayerReceivedCoin?.Invoke(number);
}
