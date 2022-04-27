using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAddBuff : Buff
{
    [SerializeField] private int pointsToAdd;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        
        AddPoint(pointsToAdd);
        GameObject.Destroy(gameObject);
    }

    private void AddPoint(int count)
    {
        var points = GameObject.FindGameObjectWithTag("Skills")?.GetComponent<PointsManager>();

        if (points)
        {
            points.u_freePoint += count;
        }
    }
}
