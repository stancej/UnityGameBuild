using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant:MonoBehaviour
{
    public List<Tile> plantStepTiles;
    public int loopsToGrow  ;
    public int remLoopsToGrow { get; set; }

    public Vector3Int TilePosition { get; set; }

    public Plant(int loops,List<Tile> tiles, Vector3Int pos)
    {
        remLoopsToGrow = loops;
        loopsToGrow = loops;
        plantStepTiles = tiles;
        TilePosition = pos;
    }

    public Tile GetCurrentTile()
    {
        int l = plantStepTiles.Count;
        switch (l)
        {
            case 0:
                Debug.LogError("Отсутствует тайлы для прорисовки растения");
                return null;
            case 1:
                return plantStepTiles[0];
            case 2:
                if (remLoopsToGrow == 0)
                    return plantStepTiles[1];
                return plantStepTiles[0];
            default:
                float k = (float)(remLoopsToGrow * (l-1)) / (float)loopsToGrow;
                return plantStepTiles[l - Mathf.CeilToInt(k)-1];
        }
    }

    public void DecreaseRemainingLoops(int value = 1)
    {
        remLoopsToGrow -= value;
        if (remLoopsToGrow < 0)
            remLoopsToGrow = 0;
    }
}
