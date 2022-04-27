using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuff : Buff
{
    [SerializeField] private int moneyToAdd = 2000;
    public float multiplier { get; set; } = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Buff();
    }


    private void Buff()
    {
        var m = GameObject.FindGameObjectWithTag("Money")?.GetComponent<Money>();
        var p = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>();

        if (m != null)
            m.AddMoney(Mathf.FloorToInt(moneyToAdd * multiplier));

        if (p != null)
        {
            foreach (var c in p.deletedTiles)
            {
                p.SetToUnwateredGarden(c);
            }
            p.deletedTiles.Clear();
            
            p.PlantLoop(100);
        }
        GameObject.Destroy(gameObject);
    }
    

}
