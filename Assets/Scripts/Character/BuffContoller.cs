using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffContoller : MonoBehaviour
{
    public event Action<Buffs,float> BuffGain;
    public enum Buffs {Anim, Speed, Loop};
    
    private float t_speedBuff;
    private float t_animBuff;
    private float t_loopBuff;

    private bool f_animBuff = false;
    private bool f_speedBuff = false;
    private bool f_loopBuff = false;
    
    private CharacterInput _speed;
    private CharacterFunctionality _animations;
    private GameDificultyManager _dificulty;

    private void Awake()
    {
        _speed = GetComponent<CharacterInput>();
        _animations = GetComponent<CharacterFunctionality>();
        _dificulty = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameDificultyManager>();
        
        if (!_speed)
            Debug.LogError($"No element: {nameof(_speed)}");
        if (!_animations)
            Debug.LogError($"No element: {nameof(_animations)}");
        if (!_dificulty)
            Debug.LogError($"No element: {nameof(_dificulty)}");
    }

    public void AddTime(Buffs buff, float time, float effectMultiplier)
    {
        switch (buff)
        {
            case Buffs.Anim:
                if (!f_animBuff)
                {
                    t_animBuff = time;
                    StartCoroutine(AnimBuff(effectMultiplier));
                    f_animBuff = true;
                }
                else
                    t_animBuff += time;
                break;
            case Buffs.Speed:
                if (!f_speedBuff)
                {
                    t_speedBuff = time;
                    StartCoroutine(SpeedBuff(effectMultiplier));
                    f_speedBuff = true;
                }
                else
                    t_speedBuff += time;
                break;
            case Buffs.Loop:
                if (!f_loopBuff)
                {
                    t_loopBuff = time;
                    StartCoroutine(LoopBuff(effectMultiplier));
                    f_loopBuff = true;
                }
                else
                    t_loopBuff += time;
                break;
        }
        BuffGain.Invoke(buff,time);
    }
    
    private IEnumerator AnimBuff(float effectMultiplier)
    {
        _animations.harvestTime /= effectMultiplier;
        _animations.plantTime /= effectMultiplier;
        _animations.wateringTime /= effectMultiplier;
        while (t_animBuff > 0)
        {
            t_animBuff -= Time.deltaTime;
            yield return null;
        }
        _animations.harvestTime *= effectMultiplier;
        _animations.plantTime *= effectMultiplier;
        _animations.wateringTime *= effectMultiplier;
        f_animBuff = false;
    }
    
    private IEnumerator SpeedBuff(float effectMultiplier)
    {
        _speed.ch_speed *= effectMultiplier;

        while (t_speedBuff > 0)
        {
            t_speedBuff -= Time.deltaTime;
            yield return null;
        }
        _speed.ch_speed /= effectMultiplier;
    }
    
    private IEnumerator LoopBuff(float effectMultiplier)
    {
        _dificulty.perLoopTime /= effectMultiplier;

        while (t_loopBuff > 0)
        {
            t_speedBuff -= Time.deltaTime;
            yield return null;
        }
        _dificulty.perLoopTime /= effectMultiplier;
    }
    
}
