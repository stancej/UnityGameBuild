using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    private void Start()
    {
        var holder = GameObject.FindGameObjectWithTag("DropHolder");
        if (holder is GameObject)
            transform.parent = holder.transform;
    }



    public void CreateObjects(List<Collectable> itemsToCreate, int quantity = 1, float dropRange = 0.3f)
    {
        int l = itemsToCreate.Count;
        float indent = Random.Range(0, 2 * Mathf.PI);
        for (int i = 0; i < l; i++)
        {
            float angl = ((2 * Mathf.PI) / (i + 1)) * (Random.Range(0.9f, 1.1f)) + indent;
            float x = dropRange * Mathf.Sin(angl);
            float y = dropRange * Mathf.Cos(angl);
            try
            {
                var t = GameObject.Instantiate(itemsToCreate[i].gameObject, this.transform) as GameObject;
                t.transform.localPosition = new Vector3(x, y);
                t.GetComponent<Collectable>().quantity = quantity;
            }
            catch
            {
                Debug.LogError("Что то не так с системой спавна!");
            }
        }
    }

    public void CreateObject(Collectable itemToCreate, int quantity = 1, float dropRange = 0.3f)
    {
        float angl = Random.Range(0, 2 * Mathf.PI);
        float x = dropRange * Mathf.Sin(angl);
        float y = dropRange * Mathf.Cos(angl);
        try
        {
            var t = GameObject.Instantiate(itemToCreate.gameObject, this.transform) as GameObject;
            t.transform.localPosition = new Vector3(x, y);
            t.GetComponent<Collectable>().quantity = quantity;
        }
        catch
        {
            Debug.LogError("Что то не так с системой спавна!");
        }
    }

    public void DropObject(Collectable itemToDrop, float dropRange = 0.3f)
    {
        float angl = Random.Range(0, 2 * Mathf.PI);
        float x = dropRange * Mathf.Sin(angl);
        float y = dropRange * Mathf.Cos(angl);
        try
        {
            if (itemToDrop?.transform is null)
                Debug.LogError("Задан неправильный объект");
    
            itemToDrop.transform.parent = transform;
            itemToDrop.transform.localPosition = new Vector3(x, y);
            itemToDrop.StartCoroutine("Created");
        }
        catch
        {
            Debug.LogError("Что то не так с системой спавна!");
        }
    }

    public void DropObjects(List<Collectable> itemsToDrop, float dropRange = 0.3f)
    {
        int l = itemsToDrop.Count;
        float indent = Random.Range(0, 2 * Mathf.PI);
        for (int i = 0; i < l; i++)
        {
            float angl = ((2 * Mathf.PI) / (i + 1)) * (Random.Range(0.9f, 1.1f)) + indent;
            float x = dropRange * Mathf.Sin(angl);
            float y = dropRange * Mathf.Cos(angl);
            try
            {
                if (itemsToDrop[i]?.transform is null)
                    Debug.LogError("Задан неправильный объект");

                itemsToDrop[i].transform.localPosition = new Vector3(x, y);
                itemsToDrop[i].StartCoroutine("Created");
            }
            catch
            {
                Debug.LogError("Что то не так с системой спавна!");
            }
        }
    }
}
