using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedConverter : MonoBehaviour
{

    [Range(0f, 1f)] [SerializeField] private float initialChanceToDrop = 1;
    public float currentChanceToDrop { get; set; }

    [SerializeField] private float spawnTime = 0.4f;
    [SerializeField] private float reloadingTime = 2f;
    [SerializeField] private int carrotsNeedToCreate = 2;
    [SerializeField] private Transform positionToSpawn;

    [SerializeField] private Collectable carrot;
    [SerializeField] private Collectable carrotSeed;
    [SerializeField] private UnityEngine.UI.Text t_visual;
    [SerializeField] private Drop drop;


    private bool generating = false;
    private Animator anim;

    private int _currentCarrots;
    private int currentCarrots 
    {
        get 
        {
            return _currentCarrots;
        }
        set
        {
            _currentCarrots = value;
            if(currentCarrots == carrotsNeedToCreate)
            {
                StartCoroutine("Generate");
            }
            Visualise();
        }
    }

    private void Awake()
    {
        currentChanceToDrop = initialChanceToDrop;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        Visualise();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (generating)
            return;

        if (collision.gameObject.tag != "Player")
            return;

        var inventory = collision.gameObject.GetComponent<CharacterInventory>();

        if (inventory?.invItem?.name != carrot.name)
            return;

        int x = Add(inventory.invItem.quantity);
        inventory.invItem.quantity -= x;
        inventory.CheckItem();
        inventory.Visualise();
    }


    private IEnumerator Generate()
    {
        if (generating)
            yield break;
        generating = true;

        anim.SetBool("Creating", true);

        yield return new WaitForSeconds(spawnTime);

        //Calculate create chance
        float chance = Random.Range(0f, 1f);
        if (chance <= currentChanceToDrop)
        {
            var d = GameObject.Instantiate(drop, positionToSpawn) as Drop;
            d.CreateObject(carrotSeed);
        }
        yield return new WaitForSeconds(reloadingTime-spawnTime);
        
        anim.SetBool("Creating", false);
        currentCarrots = 0;
        generating = false;
    }

    // returns quantity of added carrots
    public int Add(int count)
    {
        if (count + currentCarrots > carrotsNeedToCreate)
        {
            int q = currentCarrots;
            currentCarrots = carrotsNeedToCreate;
            return carrotsNeedToCreate - q;
        }
        currentCarrots += count;
        return count;
    }

    private void Visualise()
    {
        t_visual.text = $"{currentCarrots}/{carrotsNeedToCreate}";
        if (currentCarrots == carrotsNeedToCreate)
            t_visual.color = Color.red;
        else
            t_visual.color = Color.black;
    }

}
