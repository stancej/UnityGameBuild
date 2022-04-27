using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneLvl
{
    [RequireComponent(typeof(Button))]
    public class SceneLevel : MonoBehaviour
    {
        private string prefix = "Level";
        public int level = 1;
        
        private Button _button;

        private int c = 0;

        /*private void Start()
        {
            c = PlayerPrefs.GetInt("CompletedLvls");
            _button = GetComponent<Button>();
            if (c + 1 < level)
                _button.interactable = false;
        }*/

        public void LoadScene()
        {
            SceneManager.LoadScene($"{prefix} {level}");
        }

    }
}
