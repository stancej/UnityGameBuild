using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuDifficulty : MonoBehaviour
{
    [SerializeField] private GameObject states;
    [SerializeField] private Text mode;
    [SerializeField] private Color easyMode = Color.green;
    [SerializeField] private Color normalMode = Color.white;
    [SerializeField] private Color hardMode = Color.red;

    public int needLvl;
    
    
    private Button _button;
    
    private void Start()
    {
        int lvl = ExpSaver.GetCurrentLvl();
        _button = GetComponent<Button>();
        if (lvl >= needLvl)
        {
            _button.onClick.AddListener(ChangeDifficulty);
            states.SetActive(true);
            SetDifficulty();
        }
        else
        {
            _button.enabled = false;
            states.SetActive(false);
        }
    }

    public void SetDifficulty()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            var k = PlayerPrefs.GetInt("Difficulty");
            if (k == 1)
            {
                mode.text = "hard";
                mode.color = hardMode;
            }
            else if (k == 0)
            {
                mode.text = "normal";
                mode.color = normalMode;
            }
            else
            {
                mode.text = "easy";
                mode.color = easyMode;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 0);
            mode.text = "normal";
            mode.color = normalMode;
        }
        
    }

    public void ChangeDifficulty()
    {
        var k = PlayerPrefs.GetInt("Difficulty");
        if (k == 1)
        {
            k = -1;
            mode.text = "easy";
            mode.color = easyMode;
        }
        else if (k == -1)
        {
            k = 0;
            mode.text = "normal";
            mode.color = normalMode;
        }
        else
        {
            k = 1;
            mode.text = "hard";
            mode.color = hardMode;
        }
        PlayerPrefs.SetInt("Difficulty",k);
    }
}
