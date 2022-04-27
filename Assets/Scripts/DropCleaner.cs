using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCleaner : MonoBehaviour
{
    [SerializeField] private float time = 10;

    private void Start()
    {
        StartCoroutine("DeleteEmptyDrops");
    }


    IEnumerator DeleteEmptyDrops()
    {
        while (true)
        {
            foreach (Transform child in transform)
            {
                if (child.childCount == 0)
                {
                    Destroy(child.gameObject);
                }
            }
            yield return new WaitForSeconds(time);
        }
    } 
}
