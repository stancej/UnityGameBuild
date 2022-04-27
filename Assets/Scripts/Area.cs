using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Area : MonoBehaviour
{
    public Vector2 size;
    [SerializeField] private Color gizmosColor = Color.cyan;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position,size);
    }
}
