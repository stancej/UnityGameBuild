using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private int initialMaxInventoryItems;
    private int _maxInventoryItems;
    public int maxInventoryItems { get => _maxInventoryItems;
        set { _maxInventoryItems = value; Visualise(); }
    }

    [SerializeField] private Text t_carrotQuantity;
    [SerializeField] private Image invSprite;
    [SerializeField] private Canvas canvas;

    
    public int itemsInventoryQuantity { get; private set; } = 0;

    private Collectable _invItem;
    public Collectable invItem
    {
        get
        {
            return _invItem;
        }
        set
        {
            _invItem = value;
            Visualise();
        }
    }

    public Drop drop;

    private void Awake()
    {
        maxInventoryItems = initialMaxInventoryItems;
    }

    /// <summary>
    /// Set character Item and quantity
    /// </summary>
    /// <param name="quantity">if not declareted set quantity from item script</param>
    /// <param name="rt">If true returns quanity of added items. If false returns quantity of remaining items</param>
    /// <returns></returns>
    public int Set(Collectable item, int quantity = 0, bool rt = true)
    {
        if (quantity == 0)
            quantity = item.quantity;

        if (invItem == null || item.name == invItem.name)
        {
            if (item.name != invItem?.name)
            {
                DropItem();
                var newItem = GameObject.Instantiate(item.gameObject, transform);
                newItem.SetActive(false);
                newItem.transform.localPosition = Vector3.zero;
                invItem = newItem.GetComponent<Collectable>();
            }
            if (quantity + invItem.quantity > maxInventoryItems)
            {
                int q = invItem.quantity;
                invItem.quantity = maxInventoryItems;
                Visualise();
                if (rt)
                    return maxInventoryItems - q;
                else
                    return quantity - (maxInventoryItems - q);
            }
            else
            {
                invItem.quantity += quantity;
                Visualise();
                if (rt)
                    return quantity;
                else
                    return 0;
            }
        }
        Visualise();
        if (rt)
            return 0;
        else
            return quantity;
    }

    public void DropItem()
    {
        if (drop == null || invItem == null)
            return;

        invItem.gameObject.SetActive(true);

        var d = GameObject.Instantiate(drop, transform.position, Quaternion.identity) as Drop;
        d.DropObject(invItem);
        invItem = null;
        itemsInventoryQuantity = 0;
    }


    // returns true is whole fine
    public bool CheckItem()
    {
        if (invItem?.gameObject is null)
        {
            Debug.LogWarning("Game object in inv item is maybe equals null");
            return false;
        }
        if (invItem.quantity == 0)
        {
            if (invItem.gameObject != null)
                Destroy(invItem.gameObject);
            invItem = null;
            return false;
        }
        return true;
    }


    public void Visualise()
    {
        if (invItem != null)
        {
            canvas.gameObject.SetActive(true);
            invSprite.sprite = invItem.GetComponent<SpriteRenderer>().sprite;
            t_carrotQuantity.text = (invItem.quantity == 0) ? "" : $"{invItem.quantity}";
            if (invItem.quantity == maxInventoryItems)
                t_carrotQuantity.color = Color.red;
            else
                t_carrotQuantity.color = Color.black;
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
