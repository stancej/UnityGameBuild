using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSystem : MonoBehaviour
{

    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private Text title;
    [SerializeField] private Color winColor = Color.cyan;
    
    [SerializeField] private Button addButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private Button addButtonTimer;
    [SerializeField] private Text addTimer;
    [SerializeField] private float addTime;

    [SerializeField] private Text goldInfo;
    [SerializeField] private Text expInfo;
    
    [SerializeField] private AudioMixerGroup _mixer;

    public string mySurfacingId = "Interstitial_Android";
#if UNITY_IOS
    private string gameId = "4214132";
#elif UNITY_ANDROID
    private string gameId = "4214133";
#endif


    private int exp;

    private bool isAddWatched;
    
    private GameDificultyManager _gameDificultyManager;
    
    private void Start()
    {
        SetButtonsListeners();
        Advertisement.Initialize(gameId, true);
        _gameDificultyManager = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();
        if (_gameDificultyManager)
        {
            _gameDificultyManager.GameOver += ShowEndGameWindow;
            _gameDificultyManager.WinGameOver += ShowWinWindow;
        }
    }

    public void ShowEndGameWindow(int _gold, int _penalty, int _exp)
    {
        if (_mixer != null)
            StartCoroutine(ChangeSound(1));
        
        exp = _exp;

        ExpSaver.exp += exp;
        
        gameOverWindow.gameObject.SetActive(true);
        
        //set texts
        goldInfo.text = $"{_gold-_penalty}";
        expInfo.text = $"+{_exp}";
        
        StartCoroutine(nameof(ButtonTimer));
    }

    public void ShowWinWindow(int _gold, int _penalty, int _exp)
    {
        if (_mixer != null)
            StartCoroutine(ChangeSound(1));
        
        exp = _exp;

        ExpSaver.exp += exp;
        
        gameOverWindow.gameObject.SetActive(true);
        title.text = "lvl comlpleted!";
        title.color = winColor;
        
        //set texts
        goldInfo.text = $"+{_gold - _penalty}";
        goldInfo.color = Color.yellow;
        expInfo.text = $"+{_exp}";

        if (PlayerPrefs.HasKey("CompletedLvls"))
        {
            var c = PlayerPrefs.GetInt("CompletedLvls");
            if (_gameDificultyManager.gameLevel > c)
                PlayerPrefs.SetInt("CompletedLvls", _gameDificultyManager.gameLevel);
        }
        else
        {
            PlayerPrefs.SetInt("CompletedLvls", _gameDificultyManager.gameLevel);
        }
        
        StartCoroutine(nameof(ButtonTimer));
    }

    private IEnumerator ChangeSound(float time)
    {
        float t = 0;
        
        while (t < time)
        {
            t += Time.deltaTime;

            float p = t / time;
            _mixer.audioMixer.SetFloat("MasterVolume", -80 * p);
            
            yield return null;
        }
    }

    private IEnumerator ButtonTimer()
    {
        addButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        
        
        addButtonTimer.gameObject.SetActive(true);
        addTimer.gameObject.SetActive(true);

        float t = 0;
        while (t < addTime)
        {
            t += Time.deltaTime;
            addTimer.text = $"{Mathf.CeilToInt(addTime - t)}";
            yield return null;
        }
        
        addButtonTimer.gameObject.SetActive(false);
        addTimer.gameObject.SetActive(false);

        if (!isAddWatched)
        {
            if (PlayerPrefs.HasKey("AddCount"))
            {
                int c = PlayerPrefs.GetInt("AddCount");
                if ((c % 2 == 0 && c > 0) || c > 6)
                {
                    ShowInterstitialAd();
                    AddSytem.IncreaseAddCounter();
                }
            }
            
            addButton.gameObject.SetActive(true);
        }
        
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);

        Time.timeScale = 0;

    }
    
    public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) {
            Advertisement.Show(mySurfacingId);
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    private void SetButtonsListeners()
    {
        try
        {
            mainMenuButton.onClick.AddListener(LoadMenuScene);
            restartButton.onClick.AddListener(RestartScene);
        }
        catch (Exception e)
        {
            Debug.LogError("Some button is doesn't setted \n"+e);
            throw;
        }
        
    }

    public void RestartScene()
    {
        AddSytem.IncreaseAddCounter();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenuScene()
    {
        AddSytem.IncreaseAddCounter();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void FullAddWathced()
    {
        expInfo.text = $"{2 * exp}";
        isAddWatched = true;
        ExpSaver.exp += exp;
        try
        {
            addTimer.gameObject.SetActive(false);
        }
        catch
        {
            
        }
        try
        {
            addButton.gameObject.SetActive(false);
        }
        catch
        {
            
        }
        try
        {
            addButtonTimer.gameObject.SetActive(false);
        }
        catch
        {
            
        }
        PlayerPrefs.SetInt("AddCount",-1);
    }

}
