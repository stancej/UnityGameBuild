using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPool : MonoBehaviour
{
    [SerializeField] private Collectable watterBucket;

    [SerializeField] private float slowDebuff;
    public int quantity = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var input = collision.gameObject.GetComponent<CharacterInput>();

        if (input)
        {
            input.ch_speed /= slowDebuff;
        }
        
        var inventory = collision.gameObject.GetComponent<CharacterInventory>();

        if (inventory == null)
            return;

        if (inventory.invItem != null)
            return;
        
        inventory.Set(watterBucket,inventory.maxInventoryItems);
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        
        var input = collision.gameObject.GetComponent<CharacterInput>();

        if (input)
        {
            input.ch_speed *= slowDebuff;
        }

    }
}
