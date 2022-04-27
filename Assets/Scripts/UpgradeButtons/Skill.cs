using System;
using UnityEngine;

namespace UpgradeButtons
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] public int buff;
        [SerializeField] private string description;
        
        
        private void OnMouseEnter()
        {
            //show skill description
        }
    }
}
