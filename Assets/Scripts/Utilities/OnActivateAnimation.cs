using System;
using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class OnActivateAnimation : MonoBehaviour
    {
        [SerializeField] private float animatingTime;
        
        private Vector3 initialSize;
        private void OnEnable()
        {
            StartCoroutine("Increase");
        }

        private void OnDisable()
        {
            transform.localScale = initialSize;
            StopAllCoroutines();
        }

        private IEnumerator Increase()
        {
            initialSize = transform.localScale;
            float time = 0;

            float p = 0;
            
            while (p < 1)
            {
                time += Time.deltaTime;
                p = time / animatingTime;

                transform.localScale = initialSize * p;
                
                yield return null;
            }

            transform.localScale = initialSize;
        }
    }
}
