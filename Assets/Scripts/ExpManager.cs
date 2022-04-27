using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private Text expQuantity;
    [SerializeField] private Text expAddedQuantity;
    [SerializeField] private GameObject expAddedGO;

    [SerializeField] private float timeToAddExp;

    [SerializeField] private int baseExpFromOneCarrot = 5;
    
    private int _expAdded = 0;
    private int expAdded
    {
        get => _expAdded;
        set
        {
            _expAdded = value;
            expAddedQuantity.text = $"+{_expAdded}";
        }
    }

    private int _exp = 0;
    public int exp 
    {
        get => _exp;
        set
        {
            _exp = value;
            expQuantity.text = $"{_exp}";
        }
    }


    private void Awake()
    {
        expQuantity.text = $"{exp}";
        expAddedQuantity.text = $"+{expAdded}";
        expAddedGO.SetActive(false);
    }

    public void AddExp(int quantity)
    {
        StopAllCoroutines();

        expAdded += quantity*baseExpFromOneCarrot;
        
        StartCoroutine(nameof(AddExpEnum));
    }

    private IEnumerator AddExpEnum()
    {
        expAddedGO.SetActive(true);
        
        yield return new WaitForSeconds(timeToAddExp);

        exp += expAdded;
        
        expAddedGO.SetActive(false);
        expAdded = 0;
    }
    
    
    
}
