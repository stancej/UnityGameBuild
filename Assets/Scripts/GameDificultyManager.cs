using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;

public class GameDificultyManager : MonoBehaviour
{

    public int gameLevel = 1;
    public int loopsCountToWin = 5;
    private int _loopsCompleted = 0;
    public int spawnSeeds = 5;

    [SerializeField] private float minuteTimePenalty = 4; // in minutes
    public float timeToPenalty => minuteTimePenalty;
    [SerializeField] private int initialPenaltySize = 1000;
    public int penaltySize => initialPenaltySize;
    
    //Global penalty
    [SerializeField] private AnimationCurve penaltyCurve;
    public static float diffuclty = 1f;
    public float difficultyMultiplier { get; set; } = 1;

    [SerializeField] private float initialPerLoopTime; // in seconds
    private float _perLoopTime; 
    public float perLoopTime
    {
        get => _perLoopTime;
        set => _perLoopTime = value;
    }
    
    public float loopSpeedMultiplier = 1;
        
    public Collectable seed;
    public Collectable carrot;
    public Drop drop;

    public event System.Action<int,int,int> GameOver; // gold - penaltygold - exp
    public event System.Action<int,int,int> WinGameOver; // gold - penaltygold - exp
    
    private Money money;
    private ExpManager expManager;
    private ProductsScript product;

    private float startTime;

    [SerializeField] private AudioSource penaltySound;
    [SerializeField] private AudioSource gameOverSound;
    
    

    private void Awake()
    {
        money = GameObject.FindGameObjectWithTag("Money")?.GetComponent<Money>();
        expManager = GameObject.FindGameObjectWithTag("ExpManager")?.GetComponent<ExpManager>();
        product = GameObject.FindGameObjectWithTag("Product")?.GetComponent<ProductsScript>();


        if (money == null)
            Debug.LogError($"Отсутствует обьект {typeof(Money)}");
        
        if (money == null)
            Debug.LogWarning($"Отсутствует обьект {typeof(ExpManager)}");
        
        
        startTime = Time.time;
        perLoopTime = initialPerLoopTime;
    }


    private void Start()
    {
        var x = Instantiate(drop, (Vector2)transform.position,quaternion.identity) as Drop;
        x.CreateObject(seed, spawnSeeds);

        StartCoroutine(nameof(MoneyPenalty));
        StartCoroutine(nameof(GameLoop));
    }

    private void Update()
    {
        diffuclty = penaltyCurve.Evaluate((Time.time - startTime) / 60) * difficultyMultiplier;
    }

    private IEnumerator MoneyPenalty()
    {
        while (true)
        {
            float t = minuteTimePenalty * 60;
            yield return new WaitForSeconds(t);
            int c = Mathf.CeilToInt(initialPenaltySize * diffuclty);
            print(c);
            if (money.amountOfMoney - c < 0)
            {
                GameEnd(money.amountOfMoney, c, expManager.exp);
                if (gameOverSound != null)
                {
                    gameOverSound.Play();
                    break;
                }
            }
            else
            {
                _loopsCompleted += 1;
                if (_loopsCompleted >= loopsCountToWin)
                {
                    WinEnd(money.amountOfMoney, c, expManager.exp);
                    break;;
                }
                money.ReduceMoney(c);
                if (penaltySound != null)
                    penaltySound.Play();
                
            }

        }
    }


    private IEnumerator GameLoop()
    {
        WaitForSeconds tick = new WaitForSeconds(0.25f);
        while (true)
        {
            float t = perLoopTime;
            while (t > 0)
            {
                t -= 0.25f * loopSpeedMultiplier;
                yield return tick;
            }
            if (product == null)
                break;
            
            product.PlantLoop(1);
        }
    }


    private void GameEnd(int gold, int penalty, int exp = 0)
    {
        if (GameOver != null)
        {
            GameOver(gold,penalty,exp);
        }
        StopAllCoroutines();
    }

    private void WinEnd(int gold, int penalty, int exp = 0)
    {
        if (WinGameOver != null)
        {
            WinGameOver(gold,penalty,exp);
        }
        StopAllCoroutines();
    }

    public int GetNextPenalty => Mathf.CeilToInt(penaltyCurve.Evaluate((_loopsCompleted+1) * timeToPenalty) * difficultyMultiplier * initialPenaltySize);


}
