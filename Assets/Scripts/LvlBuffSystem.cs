using UnityEditor;
using UnityEngine;

public class LvlBuffSystem : MonoBehaviour
{
    [SerializeField] private float maxCarrotsInBarrelMultiplier = 2; // 1lvl
    [SerializeField] private float harvestedCarrotsMultiplier = 2; // 2lvl
    [SerializeField] private float deliverySpeedUpToX = 10; // 3lvl
    [SerializeField] private float growRateMultiplier = 1.5f; // 4lvl
    [SerializeField] private float animalSpawnRateMultiplier = 1.5f; // 5lvl
    [SerializeField] private float difficultyMultiplier = 2; // 10lvl

    private void Start ()
    {
        var clvl = ExpSaver.GetCurrentLvl();
        ActivateBuffs(clvl);
    }
    

    private void ActivateBuffs(int clvl)
    {
        if (clvl == 1)
        {
            LvlBuff_1();
        }
        if (clvl == 2)
        {
            LvlBuff_1();
            LvlBuff_2();
        }
        if (clvl == 3)
        {
            LvlBuff_1();
            LvlBuff_2();
            LvlBuff_3();
        }
        if (clvl == 4)
        {
            LvlBuff_1();
            LvlBuff_2();
            LvlBuff_3();
            LvlBuff_4();
        }
        if (clvl >= 5)
        {
            LvlBuff_1();
            LvlBuff_2();
            LvlBuff_3();
            LvlBuff_4();
            LvlBuff_5();
        }
        if (clvl >= 10)
        {
            LvlBuff_10();
        }
    }

    private void LvlBuff_1()
    {
        CarrotBarrel[] barrels = FindObjectsOfType<CarrotBarrel>();
        if (barrels != null)
        {
            foreach (var b in barrels)
            {
                b.maxCarrots = Mathf.FloorToInt(b.maxCarrots * maxCarrotsInBarrelMultiplier);
            }
        }
    }
    
    private void LvlBuff_2()
    {
        ProductsScript product = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>();
        if (product != null)
        {
            product.minSpawnCarrots = Mathf.CeilToInt(product.minSpawnCarrots * harvestedCarrotsMultiplier);
            product.maxSpawnCarrots = Mathf.CeilToInt(product.maxSpawnCarrots * harvestedCarrotsMultiplier);
        }
    }
    
    private void LvlBuff_3()
    {
        MoneyManager manager = GameObject.FindGameObjectWithTag("MoneyManager")?.GetComponent<MoneyManager>();
        if (manager != null)
        {
            manager.sellingTime -= deliverySpeedUpToX;
        }
    }
    private void LvlBuff_4()
    {
        GameDificultyManager gameDifficulty =
            GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();

        if (gameDifficulty != null)
        {
            gameDifficulty.loopSpeedMultiplier = growRateMultiplier;
        }
    }
    private void LvlBuff_5()
    {
        Spawner spawner = GameObject.FindGameObjectWithTag("Spawner")?.GetComponent<Spawner>();
        if (spawner != null)
        {
            spawner.spawnRateMultiplier = animalSpawnRateMultiplier;
        }

    }
    private void LvlBuff_10()
    {
        GameDificultyManager gameDifficulty =
            GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();

        if (gameDifficulty != null)
        {
            if(PlayerPrefs.HasKey("Difficulty"))
            {
                var k = PlayerPrefs.GetInt("Difficulty");
                if (k > 0)
                {
                    gameDifficulty.difficultyMultiplier = difficultyMultiplier;
                }

                if (k < 0)
                {
                    gameDifficulty.difficultyMultiplier = 1 / difficultyMultiplier;
                }
            }
        }
    }
    
}
