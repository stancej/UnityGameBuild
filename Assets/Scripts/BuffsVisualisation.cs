using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffsVisualisation : MonoBehaviour
{
    [SerializeField] private GameObject animBuff;
    [SerializeField] private GameObject speedBuff;
    [SerializeField] private GameObject loopBuff;

    [Header("Visual")]
    [SerializeField] private float timeToMaxColor = 30;
    [SerializeField][Range(0,1f)] private float colorMinSize = 0.5f;
    [SerializeField] private Color maxColor = Color.red;
    [SerializeField] private Color minColor = Color.grey;
    
    private float t_animBuff;
    private float t_speedBuff;
    private float t_loopBuff;
    
    private bool f_animBuff = false;
    private bool f_speedBuff = false;
    private bool f_loopBuff = false;

    private void Awake()
    {
        BuffContoller buffContoller = GameObject.FindGameObjectWithTag("Player")?.GetComponent<BuffContoller>();
        if (buffContoller)
            buffContoller.BuffGain += AnimateBuff;
        else
            Debug.LogError($"No element: {nameof(buffContoller)}");

    }

    public void AnimateBuff(BuffContoller.Buffs buff,float time)
    {
        switch (buff)
        {
            case BuffContoller.Buffs.Anim:
                if (!f_animBuff)
                {
                    t_animBuff = time;
                    StartCoroutine(nameof(AnimEnum));
                    f_animBuff = true;
                }
                else
                {
                    t_animBuff += time;
                }

                break;
            case BuffContoller.Buffs.Loop:
                if (!f_loopBuff)
                {
                    t_loopBuff = time;
                    StartCoroutine(nameof(LoopEnum));

                    f_loopBuff = true;
                }
                else
                {
                    t_loopBuff += time;
                }

                break;
            case BuffContoller.Buffs.Speed:
                if (!f_speedBuff)
                {
                    t_speedBuff = time;
                    StartCoroutine(nameof(SpeedEnum));
                    
                    f_speedBuff = true;
                }
                else
                {
                    t_speedBuff += time;
                }

                break;
        }
    }

    private IEnumerator AnimEnum()
    {
        animBuff.SetActive(true);
        var t = animBuff.GetComponentInChildren<Text>();
        var s = t.transform.localScale;
        while (t_animBuff > 0)
        {
            t_animBuff -= Time.deltaTime;
            
            var p = Mathf.Clamp01(t_animBuff/timeToMaxColor);
            t.color = Color.Lerp(minColor, maxColor, p);
            t.text = $"{Mathf.FloorToInt(t_animBuff)}";
            t.transform.localScale = Vector3.Lerp(s * colorMinSize, s, p);
            
            yield return null;
        }
        animBuff.SetActive(false);
        f_animBuff = false;
    }
    
    private IEnumerator SpeedEnum()
    {
        speedBuff.SetActive(true);
        var t = speedBuff.GetComponentInChildren<Text>();
        var s = t.transform.localScale;
        while (t_speedBuff > 0)
        {
            t_speedBuff -= Time.deltaTime;

            var p = Mathf.Clamp01(t_speedBuff/timeToMaxColor);
            t.color = Color.Lerp(minColor, maxColor, p);
            t.text = $"{Mathf.FloorToInt(t_speedBuff)}";
            t.transform.localScale = Vector3.Lerp(s * colorMinSize, s, p);
            
            yield return null;
        }
        speedBuff.SetActive(false);
        f_speedBuff = false;
    }
    
    private IEnumerator LoopEnum()
    {
        loopBuff.SetActive(true);
        var t = loopBuff.GetComponentInChildren<Text>();
        var s = t.transform.localScale;
        while (t_loopBuff > 0)
        {
            t_loopBuff -= Time.deltaTime;

            var p = Mathf.Clamp01(t_loopBuff / timeToMaxColor);
            t.color = Color.Lerp(minColor, maxColor, p);
            t.text = $"{Mathf.FloorToInt(t_loopBuff)}";
            t.transform.localScale = Vector3.Lerp(s * colorMinSize , s, p);
            
            yield return null;
        }
        loopBuff.SetActive(false);
        f_loopBuff = false;
    }
    
} 
