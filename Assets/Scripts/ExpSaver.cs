using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExpSaver : MonoBehaviour
{
    
    [SerializeField] private List<int> _levelsExp;
    public static List<int> levelsExp;

    private static int _exp;

    public static int exp
    {
        get => _exp;
        set
        {
            _exp = value;
            PlayerPrefs.SetInt("exp", value);
        }
    }
    
    private void Awake()
    {
        levelsExp = _levelsExp;
        
        if (!PlayerPrefs.HasKey("exp"))
        {
            PlayerPrefs.SetInt("exp", 1);
        }
        else
        {
            _exp = PlayerPrefs.GetInt("exp");
        }
        print(_exp);
    }
    
    public static int GetCurrentLvl()
    {
        for (int i = 0; i < levelsExp.Count; i++)
        {
            if (ExpSaver.exp < levelsExp[i])
            {
                return i;
            }
        }

        return levelsExp.Count;
    }
}
