using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public new string name;

    public int quantity { get; set; } = 0;

    private void Start()
    {
        StartCoroutine("Created");
    }

    public void LateUpdate()
    {
        if (quantity <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        var inventory = collision.gameObject.GetComponent<CharacterInventory>();
        if (inventory == null)
            return;

        int c = inventory.Set(this);
        quantity -= c;
    }

    public IEnumerator Created()
    {
        var c = GetComponent<Collider2D>();
        if (c == null)
        {
            GameObject.Destroy(gameObject);
            yield break;
        }
        c.enabled = false;
        yield return new WaitForSeconds(1);
        c.enabled = true;
    }
}
