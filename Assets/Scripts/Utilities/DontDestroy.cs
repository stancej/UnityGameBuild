using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
