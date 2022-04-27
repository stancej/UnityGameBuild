using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExp : MonoBehaviour
{
    public void Add()
    {
        PlayerPrefs.SetInt("exp",PlayerPrefs.GetInt("exp") + 250);
    }
}
