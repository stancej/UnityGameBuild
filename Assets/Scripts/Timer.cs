using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text loopsText;
    [SerializeField] private Text timeText;
    

    private void Start()
    {
        var gameDifficulty =
            GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();
        if (gameDifficulty != null)
            StartCoroutine(TimerEnum(gameDifficulty.loopsCountToWin, Mathf.FloorToInt(gameDifficulty.timeToPenalty*60)));
    }


    private IEnumerator TimerEnum(int loops,int seconds)
    {
        var waitSec = new WaitForSeconds(1);
        while (loops > 0)
        {
            loops -= 1;
            loopsText.text = $"x{loops}";

            var min = seconds / 60;
            var sec = seconds - min * seconds;
            while (min > 0 || sec > 0)
            {
                sec -= 1;
                if (sec < 0)
                {
                    sec = 59;
                    min -= 1;
                    if (min < 0)
                        break;
                }
                timeText.text = $"{min}:{sec}";
                yield return waitSec;
            }
        }
    }

}
