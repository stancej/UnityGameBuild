using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private Text carsText;

    [SerializeField]private int initialSellingCars;
    private int _maxSellingCars;
    public int maxSellingCars { get => _maxSellingCars; set { _maxSellingCars = value; Visualise(); } }

    [SerializeField] private int initialCarrotPrice;
    private int _carrotPrice;
    public int carrotPrice { get => _carrotPrice; set { _carrotPrice = value; Visualise(); } }

    [SerializeField] private float initialSellingTime;
    private float _sellingTime;
    public float sellingTime { get => _sellingTime; set { _sellingTime = value; Visualise(); } }

    private int _cFreeSellingCars;
    public int cFreeSellingCars { get => _cFreeSellingCars; set { _cFreeSellingCars = value;Visualise(); } }

    public CarrotBarrel[] barrels;

    [SerializeField] private Button sellButton;
    [SerializeField] private AudioSource moneyAddingSound;

    private Money money;
    private ExpManager expManager;
    private void Awake()
    {
        maxSellingCars = initialSellingCars;
        carrotPrice = initialCarrotPrice;
        sellingTime = initialSellingTime;
        
        cFreeSellingCars = maxSellingCars;

        money = GameObject.FindGameObjectWithTag("Money")?.GetComponent<Money>();

        if (money == null)
            Debug.LogError($"Отсутствует обьект {typeof(Money)}");  
        
        expManager = GameObject.FindGameObjectWithTag("ExpManager")?.GetComponent<ExpManager>();
        
        sellButton.onClick.AddListener(Sell);
        
    }


    public void Sell()
    {
        foreach (var b in barrels)
        {
            SellBarrel(b);
        }
    }

    public void SellBarrel(CarrotBarrel barrel)
    {
        if (cFreeSellingCars < 1 || barrel == null)
            return;

        StartCoroutine(SellingBarrel(barrel));
    }

    private IEnumerator SellingBarrel(CarrotBarrel barrel)
    {
        var selledCarrotsCount = barrel.Sell();
        var c = selledCarrotsCount * carrotPrice;
        if (c <= 0)
            yield break;

        cFreeSellingCars -= 1;
        yield return new WaitForSeconds(sellingTime);

        money.AddMoney(c);
        if(expManager)
            expManager.AddExp(selledCarrotsCount);
        
        cFreeSellingCars += 1;

        if(moneyAddingSound != null)
            moneyAddingSound.Play();
    }

    private void Visualise()
    {
        carsText.text = $"{cFreeSellingCars}/{maxSellingCars}";
    }

}
