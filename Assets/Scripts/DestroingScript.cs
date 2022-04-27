using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroingScript : MonoBehaviour
{
    [SerializeField] private float time = 1.0f;

    private void Start()
    {
        GameObject.Destroy(gameObject, time);
    }
}
