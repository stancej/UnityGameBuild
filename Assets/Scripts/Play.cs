using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [SerializeField] private Scene gameScene;

    public void Activate()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
