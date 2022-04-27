using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBarrel : MonoBehaviour
{
    [SerializeField] private Collectable carrotType;
    [SerializeField] private DestroingScript sellAnimation;
    [SerializeField] private int initialMaxCarrots = 30;
    private int _maxCarrots;
    public int maxCarrots { get => _maxCarrots;
        set
        {
            _maxCarrots = value;
            Visualise();
        }
    }
    public UnityEngine.UI.Text t_visual;

    private int _currentCarrots;
    public int currentCarrots { get => _currentCarrots; private set {  _currentCarrots = value; Visualise(); } }


    private void Awake()
    {
        maxCarrots = initialMaxCarrots;
    }

    private void Start()
    {
        Visualise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var inventory = collision.gameObject.GetComponent<CharacterInventory>();
        if (inventory == null)
            return;

        if (inventory.invItem is Collectable)
        {
            if (!IsCorrectCarrotType(inventory.invItem))
                return;

            var added = Add(inventory.invItem.quantity);
            inventory.invItem.quantity -= added;
            inventory.CheckItem();
            inventory.Visualise();
        }
        else if (currentCarrots > 0)
        {
            int x = (inventory.invItem?.quantity is null) ? 0 : inventory.invItem.quantity;
            int q = Subtract(inventory.maxInventoryItems - x);
            inventory.Set(carrotType, q);
        }
    }

    public bool IsCorrectCarrotType(Collectable carrot)
    {
        if (carrot?.name == carrotType.name)
            return true;
        return false;
    }

    // returns quantity of added carrots
    public int Add(int count)
    {
        if (count + currentCarrots > maxCarrots)
        {
            int q = currentCarrots;
            currentCarrots = maxCarrots;
            return maxCarrots - q;
        }
        currentCarrots += count;
        return count;
    }

    // returns how many carrots you can get
    public int Subtract(int count)
    {
        if (currentCarrots < count)
        {
            var x = currentCarrots;
            currentCarrots = 0;
            return x;
        }
        currentCarrots -= count;
        return count;
    }

    public void Visualise()
    {
        t_visual.text = $"{currentCarrots}/{maxCarrots}";
        if (currentCarrots == maxCarrots)
            t_visual.color = Color.red;
        else
            t_visual.color = Color.black;
    }

    public int Sell()
    {
        if (currentCarrots == 0)
            return 0;

        var c = currentCarrots;
        currentCarrots = 0;
        Instantiate(sellAnimation, transform);
        return c;
    }

}
