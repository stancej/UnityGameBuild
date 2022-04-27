using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 size;

    [SerializeField] private List<Sprite> leafs;
    [SerializeField] private GameObject prefab;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    
    [SerializeField] private float spawnTime;

    private void Start()
    {
        StartCoroutine(nameof(Spawn));
    }


    private IEnumerator Spawn()
    {
        
        while (true)
        {
            var leaf = leafs[Random.Range(0, leafs.Count)];
            var x = Random.Range(-size.x, size.x + 1);
            var y = Random.Range(-size.y, size.y + 1);
            var o = Instantiate(prefab, transform).GetComponent<Leaf>();
            o.transform.Translate(new Vector3(x,y));
            o.ChangeImage(leaf);
            float speed = Random.Range(minSpeed, maxSpeed);
            float animMultipler = speed / maxSpeed;
            o.SetValues(speed,animMultipler);

            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,size);

    }
}
