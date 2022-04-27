using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UpgradeButtons;

[RequireComponent(typeof(Animator))]
public class CharacterFunctionality : MonoBehaviour
{
    [SerializeField] private Collectable seedItem;
    [SerializeField] private Collectable waterItem;
    [SerializeField] private Image progressBar;
    [SerializeField] private Canvas progressBarCanvas;

    [SerializeField] private float _wateringTime;
    [SerializeField] private float _plantTime;
    [SerializeField] private float _harvestTime;
    [SerializeField] private float threshold;

    [SerializeField] private AudioSource as_water;
    [SerializeField] private AudioSource as_harvest;
    [SerializeField] private AudioSource as_plant;

    public float wateringTime { get; set; }
    public float plantTime { get; set; }
    public float harvestTime { get; set; }

    private ProductsScript products;
    private CharacterInventory inventory;
    private Animator anim;

    public bool animating { get; private set; } = false;

    public bool need2Animate  = true;

    private void Awake()
    {
        products = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>();
        inventory = GetComponent<CharacterInventory>();
        anim = GetComponent<Animator>();

        wateringTime = _wateringTime;
        plantTime = _plantTime;
        harvestTime = _harvestTime;

    }

    public void Do()
    {
        if (inventory.invItem?.name == seedItem.name)
            Plant();
        else if (inventory.invItem?.name == waterItem.name)
            Watering();
        else Harvest();
    }

    public IEnumerator StartChangeProgressBar(float time)
    {
        progressBarCanvas.gameObject.SetActive(true);
        float remainingTime = time;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            float p = 1 - (remainingTime / time);
            progressBar.fillAmount = p;
            yield return null;
        }
        progressBarCanvas.gameObject.SetActive(false);
    }
    public IEnumerator PlaySoundByTime(AudioSource source,float time)
    {
        float remainingTime = time;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (!source.isPlaying)
                source.Play();
            yield return null;
        }
        source.Stop();
    }
    public void Watering()
    {   
        if(animating)
            return;
        
        if (!IsHavePlantPlace())
            return;

        if (inventory?.invItem?.name != waterItem.name)
            return;

        Vector3Int cell = products.GetCell(transform.position - Vector3.up * threshold);

        if (!products.IsNotEmptySpace(cell) || products.IsWatered(cell))
            return;

        inventory.invItem.quantity -= 1;
        inventory.CheckItem();
        inventory.Visualise();
        StartCoroutine(WateringProcess(cell));
    }

    private IEnumerator WateringProcess(Vector3Int cell)
    {
        if (need2Animate == true)
        {
            if (animating == true)
                yield break;

            StartCoroutine(PlaySoundByTime(as_water,wateringTime));
            
            animating = true;
            anim.SetBool("Watering", true);
            StartCoroutine(StartChangeProgressBar(wateringTime));
            yield return new WaitForSeconds(wateringTime);

            animating = false;
            anim.SetBool("Watering", false);
        }
        

        if (products.IsPlanted(cell))
        {
            products.IndividualPlantLoop(cell);
        }
        else
        {
            products.WaterGarden(cell);
        }
    }

    public void Plant()
    {
        if(animating)
            return;
        
        if (!IsHavePlantPlace())
            return;

        if (inventory?.invItem?.name != seedItem.name)
            return;

        Vector3Int cell = products.GetCell(transform.position - Vector3.up * threshold);

        if (!products.IsWatered(cell))
            return;

        inventory.invItem.quantity -= 1;
        inventory.CheckItem();
        inventory.Visualise();
        StartCoroutine(PlantingProcess(cell));
    }

    private IEnumerator PlantingProcess(Vector3Int cell)
    {
        if (need2Animate == true)
        {
            if (animating == true)
                yield break;

            StartCoroutine(PlaySoundByTime(as_plant,wateringTime));
            
            animating = true;
            anim.SetBool("Hilling", true);
            StartCoroutine(StartChangeProgressBar(plantTime));
            yield return new WaitForSeconds(plantTime);

            animating = false;
            anim.SetBool("Hilling", false);
        }
        

        products.PlantSeed(cell);

    }

    public void Harvest()
    {
        if(animating)
            return;
        
        if (!IsHavePlantPlace())
            return;

        Vector3Int cell = products.GetCell(transform.position - Vector3.up * threshold);

        if (!products.IsGrown(cell))
            return;

        StartCoroutine(HarvestProcess(cell));
    }

    private IEnumerator HarvestProcess(Vector3Int cell)
    {
        if (need2Animate == true)
        {
            if (animating == true)
                yield break;

            StartCoroutine(PlaySoundByTime(as_harvest,wateringTime));
            
            animating = true;
            anim.SetBool("Harvesting", true);
            StartCoroutine(StartChangeProgressBar(harvestTime));
            yield return new WaitForSeconds(harvestTime);

            animating = false;
            anim.SetBool("Harvesting", false);
        }

        
        products.HarvestPlant(cell);

    }

    public bool IsHavePlantPlace()
    {
        products = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>();
        return products != null;
    }
}
