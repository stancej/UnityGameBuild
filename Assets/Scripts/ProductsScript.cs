using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class ProductsScript : MonoBehaviour
{
    [SerializeField] private Tile UnwateredGarden;
    [SerializeField] private Tile ToxedGarden;
    [SerializeField] private Tile WateredGarden;
    [SerializeField] private Plant plt;

    [SerializeField] private List<Collectable> dropCarrotsVariants;
    [SerializeField] private Drop dropHandler;

    [SerializeField] private float _plantLoopMultipleier = 1;
    public float plantLoopMultipleier { get; set; }

    private Tilemap tm;
    private List<Plant> plants = new List<Plant>();

    public HashSet<Vector3Int> deletedTiles = new HashSet<Vector3Int>();

    [SerializeField] private int initialMinSpawnCarrots = 1;
    private int _minSpawnCarrots;
    public int minSpawnCarrots
    {
        get => _minSpawnCarrots;
        set => _minSpawnCarrots = value;
    }
    
    [SerializeField] private int initialMaxSpawnCarrots = 1;
    private int _maxSpawnCarrots;
    public int maxSpawnCarrots
    {
        get => _maxSpawnCarrots;
        set => _maxSpawnCarrots = value;
    }

    //Spawn Rate by Penalty
    [SerializeField] private float addOnSpawnCarrotTypeEveryX = 10;
    [SerializeField] private float rCarrotCountMultiplierBySpawn = 1;
    private void Awake()
    {
        minSpawnCarrots = initialMinSpawnCarrots;
        maxSpawnCarrots = initialMaxSpawnCarrots;
        tm = GetComponent<Tilemap>();

        plantLoopMultipleier = _plantLoopMultipleier;
    }

    public Vector3Int GetCell(Vector3 position)
    {
        Vector3Int cell = tm.WorldToCell(position);
        return cell;
    }

    public void PlantLoop(int count = 1)
    {
        foreach (var p in plants)
        {
            p.DecreaseRemainingLoops(Mathf.FloorToInt(count * plantLoopMultipleier));
            var t = p.GetCurrentTile();
            tm.SetTile(p.TilePosition, t);
        }
    }

    public void IndividualPlantLoop(Vector3Int cell, int count = 1)
    {
        var p = GetPlant(cell);
        if (p is Plant)
        {
            p.DecreaseRemainingLoops(Mathf.FloorToInt(count * plantLoopMultipleier));
            var t = p.GetCurrentTile();
            tm.SetTile(p.TilePosition, t);
        }
    }

    public Plant GetPlant(Vector3Int cell)
    {
        return plants.Where(x => x.TilePosition == cell)?.First();
    }


    public bool IsNotEmptySpace(Vector3Int cell)
    {
        if (tm.GetTile(cell) != null)
        {
            return true;
        }
        return false;
    }

    public bool IsPlanted(Vector3Int cell)
    {
        if (IsNotEmptySpace(cell))
        {
            if (plt.plantStepTiles.Contains(tm.GetTile(cell)))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsWatered(Vector3Int cell)
    {
        if (IsNotEmptySpace(cell))
        {
            if (tm.GetTile(cell) == WateredGarden)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsGrown(Vector3Int cell)
    {
        if (IsPlanted(cell))
        {
            var p = GetPlant(cell);
            if (p?.remLoopsToGrow == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void WaterGarden(Vector3Int cell)
    {
        var t = tm.GetTile(cell);
        if (IsNotEmptySpace(cell) && !IsPlanted(cell) && t != ToxedGarden)
        {
            tm.SetTile(cell, WateredGarden);
        }
    }

    public void PlantSeed(Vector3Int cell)
    {
        if (IsWatered(cell))
        {
            var p = new Plant(plt.loopsToGrow, plt.plantStepTiles, cell);
            plants.Add(p);
            tm.SetTile(cell, p.GetCurrentTile());
        }
    }

    public void HarvestPlant(Vector3Int cell)
    {
        if(IsGrown(cell))
        {
            tm.SetTile(cell, UnwateredGarden);

            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].TilePosition == cell)
                {
                    plants.RemoveAt(i);
                    try
                    {
                        //Нужно определить какие морковки и в каком кол-ве нужно их спавнить и записывать в Collectable их
                        //...
                        List<Collectable> collectables = new List<Collectable>();

                        int typeSpawnCount = Mathf.CeilToInt(GameDificultyManager.diffuclty / addOnSpawnCarrotTypeEveryX)+1;
                        
                        print(GameDificultyManager.diffuclty);
                        print(typeSpawnCount);
                        
                        for (int j = 0; j < typeSpawnCount; j++)
                        {
                            var c = dropCarrotsVariants[Random.Range(0, dropCarrotsVariants.Count)];
                            collectables.Add(c);
                        }
                        //...


                        var p = tm.CellToWorld(cell);
                        var pos = new Vector3(p.x + tm.cellSize.x / 2, p.y + tm.cellSize.y / 2, p.z);
                        var d = GameObject.Instantiate(dropHandler, pos, Quaternion.identity) as Drop;
                        
                        //
                        int min = Mathf.FloorToInt(minSpawnCarrots + (GameDificultyManager.diffuclty * rCarrotCountMultiplierBySpawn));
                        int max = Mathf.FloorToInt(maxSpawnCarrots + (GameDificultyManager.diffuclty * rCarrotCountMultiplierBySpawn));
                        foreach (var c in collectables)
                        {
                            int q = Random.Range(min, max+1);
                            d.CreateObject(c,q);
                        }
                    }
                    catch
                    {
                        if (dropHandler == null)
                        {
                            Debug.LogError("Не установлен сборщик выбрасываемых предметов");
                        }
                        else if (dropCarrotsVariants == null)
                        {
                            Debug.LogError("Не установлен варинаты для выброски предметов");
                        }
                        else
                        {
                            Debug.LogError("Не работает!");
                        }
                    }
                    break;
                }

            }
        }
    }


    public void SetToUnwateredGarden(Vector3Int cell)
    {
        tm.SetTile(cell, UnwateredGarden);
    }

    public void RemoveTile(Vector3Int cell)
    {
        try
        {
            tm.SetTile(cell, null);
            deletedTiles.Add(cell);
        }
        catch
        {
            Debug.LogError("Удалите не удалось");
        }
    }

    public void ToxinTheSoil(Vector3Int cell)
    {

        print((tm.GetTile(cell)));

        if (tm.GetTile(cell) == null)
            return;
       
        try
        {
            tm.SetTile(cell, ToxedGarden);
            deletedTiles.Add(cell);
        }
        catch
        {
            Debug.LogError("Удалите не удалось");
        }
    }
}
