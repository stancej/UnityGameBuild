using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private Text visualCount;
    
    [SerializeField] private int initialFreePoints;
    private int _u_freePoint { get; set; }
    public int u_freePoint { get => _u_freePoint;
        set 
        {
            _u_freePoint = value;
            visualCount.text = $"{_u_freePoint}";
        }
    }

    private void Awake()
    {
        u_freePoint = initialFreePoints;
    }
}
