using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent (typeof (Button))]
public class AddSytem : MonoBehaviour
{

#if UNITY_IOS
    private string gameId = "4214132";
#elif UNITY_ANDROID
    private string gameId = "4214133";
#endif
    bool testMode = true;
    void Start () {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
    }
    
    public static void IncreaseAddCounter()
    {
        string key = "AddCount";
        if (PlayerPrefs.HasKey(key))
        {
            var c = PlayerPrefs.GetInt(key);
            
            PlayerPrefs.SetInt(key,c+1);
        }
        else
        {
            PlayerPrefs.SetInt(key,1);
        }
    }
    
}