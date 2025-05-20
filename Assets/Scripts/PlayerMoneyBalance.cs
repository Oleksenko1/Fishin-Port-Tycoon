using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyBalance
{
    public event Action OnMoneyDiscarded;
    public event Action OnMoneyAdded;
    private int currentBalance = 0;

    public void AddMoney(int x)
    {
        if (x <= 0) return;

        currentBalance += x;

        OnMoneyAdded?.Invoke();
    }
    public void DiscardMoney(int x)
    {
        if (x <= 0) return;

        currentBalance -= x;

        if (currentBalance < 0) currentBalance = 0;

        OnMoneyDiscarded?.Invoke();
    }
    public bool IsAvailable(int x) => currentBalance >= x;
    public int GetCurrentBalance() => currentBalance;
}
