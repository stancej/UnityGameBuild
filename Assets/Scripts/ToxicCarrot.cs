using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicCarrot : Collectable
{
    private ProductsScript pr;
    [SerializeField] private int penaltySize;
    [SerializeField] private float timeToDestroy = 20;

    private void Awake()
    {
        pr = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>() as ProductsScript;
    }

    private void Start()
    {
        DestroyTheSoil(timeToDestroy);
    }


    public void DestroyTheSoil(float time = 20.0f)
    {
        if (pr != null)
        {
            StartCoroutine(Destroing(time));
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    private IEnumerator Destroing(float time)
    {
        yield return new WaitForSeconds(time);

        var money = GameObject.FindGameObjectWithTag("Money");
        if (money)
        {
            var x = money.GetComponent<Money>();
            x.ReduceMoney( Mathf.CeilToInt(penaltySize * GameDificultyManager.diffuclty));
        }
        

        var cell = pr.GetCell(transform.position);
        pr.ToxinTheSoil(cell);
         
        GameObject.Destroy(gameObject);
    }
}
