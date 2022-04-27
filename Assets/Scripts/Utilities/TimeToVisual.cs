using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class TimeToVisual : MonoBehaviour
{
    public float delay;
    public List<GameObject> _objects;

    public bool isOnlyChild = false;
    public bool isStartFromLevel = false;

    private void OnEnable()
    {
        if (isOnlyChild)
        {
            _objects = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
                _objects.Add(transform.GetChild(i).gameObject);
        }

        if (isStartFromLevel)
        {
            var c = PlayerPrefs.GetInt("CompletedLvls");
            _objects.RemoveRange(0,c);
        }
        StartCoroutine(nameof(Show));
    }
    
    private void OnDisable()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
        StopAllCoroutines();
    }

    private IEnumerator Show()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}
