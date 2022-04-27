using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class HideOnEnable : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objectsToHide;

        private void OnEnable()
        {
            foreach (var o in objectsToHide)
            {
                o.SetActive(false);
            }
        }
        
        private void OnDisable()
        {
            foreach (var o in objectsToHide)
            {
                o.SetActive(true);
            }
        }
    }
}
