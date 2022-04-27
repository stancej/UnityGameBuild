using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace SceneLvl
{
    
    public class SceneLevelSystem : MonoBehaviour
    {

        private string key = "CompletedLvls";
        [SerializeField] private List<Button> _buttons;
        private void Start()
        {
            var c = PlayerPrefs.GetInt(key);
            for (int i = c+1; i < _buttons.Count; i++)
            {
                _buttons[i].interactable = false;
            }
        }



        [System.Serializable]
        public class Level
        {
            public Color textColor;
            public string sceneName;
        }
    }
}
