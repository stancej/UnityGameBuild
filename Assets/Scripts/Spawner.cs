using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    [SerializeField] private List<Area> spawnAreas;

    [SerializeField] private List<SpawnBuff> buffs;

    public float spawnRateMultiplier { get; set; } = 1;

    private void Start()
    {
        foreach (var b in buffs)
        {
            StartCoroutine(SpawnTimer(b.buff, b.timeToSpawn));
        }
    }


    private IEnumerator SpawnTimer(Buff obj, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time/spawnRateMultiplier);
            Transform parent;
            var pos = GetRandomSpawnPosition(out parent);

            Spawn(obj,pos,parent);
        }
    }
    
    public void Spawn(Buff obj, Vector2 pos,Transform parent)
    {
        var o = GameObject.Instantiate(obj.gameObject,parent);
        o.transform.Translate(pos);
    }

    private Vector2 GetRandomSpawnPosition(out Transform parent)
    {
        if (spawnAreas.Count > 0)
        {
            var c = UnityEngine.Random.Range(0,spawnAreas.Count);

            var area = spawnAreas[c];
            parent = area.transform;

            var x = Random.Range(-area.size.x / 2, area.size.x / 2);
            var y = Random.Range(-area.size.y / 2, area.size.y / 2);
            return new Vector2(x, y);
        }
        else
        {
            Debug.LogError("spawn areas wasn't set");
            parent = null;
            return Vector2.zero;
        }
        
    }
    
    [System.Serializable]
    public class SpawnBuff
    {
        public Buff buff;
        public float timeToSpawn;
    }
}
