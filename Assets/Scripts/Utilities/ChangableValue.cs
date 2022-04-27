using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangableValue <T>
{
    [SerializeField] private T initialValue;

    private T _value;
    public T value
    {
        get => _value;
        set
        {
            _value = value;
            OnChange();
        }
    }
    public virtual void OnChange(){}
    
}
