using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UpgradeButtons;
using Color = UnityEngine.Color;

public class Shop : MonoBehaviour
{
    [SerializeField] private Collider2D triggerZone;
    [SerializeField] private GameObject visualShop;
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private Button thirdButton;
    [SerializeField] private List<ShopItem> items;

    private CharacterInventory _inv;
    private Money _money;
    
    private void Awake()
    {
        firstButton.onClick.AddListener(firstItem);
        secondButton.onClick.AddListener(secondItem);
        thirdButton.onClick.AddListener(thirdItem);
        
        _inv = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterInventory>();
        _money = GameObject.FindGameObjectWithTag("Money")?.GetComponent<Money>();
        
        if(!_inv)
            Debug.LogError($"No object: {nameof(_inv)}");
        if(!_money)
            Debug.LogError($"No object: {nameof(_money)}");
    }
    
    private void firstItem()
    {
        if (_money.amountOfMoney >= items[0].price)
        {
            _money.ReduceMoney(items[0].price);
            _inv.maxInventoryItems += items[0].invSize;
            firstButton.GetComponent<Image>().color = Color.white;
            firstButton.onClick.RemoveAllListeners();;
        }
    }
    
    private void secondItem()
    {
        if (_money.amountOfMoney >= items[1].price)
        {
            _money.ReduceMoney(items[1].price);
            _inv.maxInventoryItems += items[1].invSize;
            secondButton.GetComponent<Image>().color = Color.white;
            secondButton.onClick.RemoveAllListeners();;
        }        
    }
    
    private void thirdItem()
    {
        if (_money.amountOfMoney >= items[2].price)
        {
            _money.ReduceMoney(items[2].price);
            _inv.maxInventoryItems += items[2].invSize;
            thirdButton.GetComponent<Image>().color = Color.white;
            thirdButton.onClick.RemoveAllListeners();;
        }        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(1);
        if (!other.CompareTag("Player"))
            return;
        
        visualShop.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        visualShop.SetActive(false);
    }

    
    [System.Serializable]
    private class ShopItem
    {
        public int price;
        public int invSize;
    }
}
