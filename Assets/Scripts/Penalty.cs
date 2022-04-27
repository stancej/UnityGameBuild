using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penalty : MonoBehaviour
{
    [SerializeField] private int ticks;
    [SerializeField] private Color minusColor = Color.red;
    [SerializeField] private Color plusColor = Color.yellow;
    [SerializeField] private Money money;
    public Text text;

    private GameDificultyManager difficulty;
    
    
    
    private void Awake()
    {
        difficulty = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();
        if (difficulty == null)
        {
            Debug.LogError($"No object:{nameof(GameDificultyManager)}");
            text.gameObject.SetActive(false);
        }

        money.onMoneyChanged += ShowPenalty;
    }
    


    private void ShowPenalty(int _money)
    {
        var m = _money - difficulty.GetNextPenalty;

        if (m > 0)
        {
            text.text = $"+{Mathf.CeilToInt(m)}";
            text.color = plusColor;
        }
        else
        {
            text.text = $"{Mathf.CeilToInt(m)}";
            text.color = minusColor;
        }
            
    }
}
