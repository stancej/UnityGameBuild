using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public event Action<int> onMoneyChanged; 

    [SerializeField] private Text moneyText;
    [SerializeField] private int _AmountOfMoney;

    private int _amountOfMoney;
    public int amountOfMoney
    {
        get
        {
            return _amountOfMoney;
        }
        private set
        {
            _amountOfMoney = value;
            if(onMoneyChanged != null)
                onMoneyChanged(_amountOfMoney);
            SetText();
        }
    }

    private void Awake()
    {
        amountOfMoney = _AmountOfMoney;
    }

    public void AddMoney(int count)
    {
        amountOfMoney += count;
    }

    public void ReduceMoney(int count)
    {
        amountOfMoney -= count;
        if (amountOfMoney < 0)
        {
            amountOfMoney = 0;
        }
    }

    private void SetText()
    {
        moneyText.text = $"{amountOfMoney}";
    }
}
