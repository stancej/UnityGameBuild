using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PerksSystem : MonoBehaviour
{
    [SerializeField] private Text currentLvl;
    [SerializeField] private List<Text> visualLvls;
    [SerializeField] private Color lvlsColours = Color.green;
    [SerializeField] private Color maxLvlColour = Color.red;
    [SerializeField] private Image expBar;
    
    private Vector3 _barLocalScale;
    private int curlvl;

    private void Start()
    {
        curlvl = ExpSaver.GetCurrentLvl();
        currentLvl.text = $"{curlvl}";

        //set colours of lvls texts
        if (visualLvls.Count < curlvl)
        {
            Debug.LogError($"No have such elements to visualise colours. Need {curlvl}");
            return;
        }

        try
        {
            for (int i = 0; i < curlvl; i++)
            {
                if (visualLvls[i] == null) 
                    continue;
                visualLvls[i].color = lvlsColours;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        


        _barLocalScale = expBar.transform.localScale;
        expBar.transform.localScale = new Vector3(_barLocalScale.x * GetCurrentLvlProgress(),_barLocalScale.y,_barLocalScale.z);
        
        if (ExpSaver.exp > ExpSaver.levelsExp.Last())
        {
            expBar.color = maxLvlColour;
            currentLvl.color = maxLvlColour;
            currentLvl.color = maxLvlColour;
        }
    }




    private float GetCurrentLvlProgress()
    {

        if (curlvl >= ExpSaver.levelsExp.Count)
        {
            //do something
            return 1;
        }

        float x, y;
        if (curlvl > 0)
        {
            x = (float) (ExpSaver.exp - ExpSaver.levelsExp[curlvl - 1]);
            y = (float) (ExpSaver.levelsExp[curlvl] - ExpSaver.levelsExp[curlvl - 1]);
        }
        else
        {
            x = ExpSaver.exp;
            y = ExpSaver.levelsExp[0];
        }
        

        float percent = x / y;

        return percent;
    }

    public void AddExp(int quantity)
    {
        ExpSaver.exp += quantity;
    }

}
