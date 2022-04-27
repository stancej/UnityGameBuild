using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
   [SerializeField]private float animDelay = 1;
   private Animator anim;

   protected void Awake()
   {
      anim = GetComponent<Animator>();
   }

   protected void Start()
   {
      if (anim != null)
      {
         StartCoroutine(nameof(Animate));
      }
   }

   private IEnumerator Animate()
   {
      yield return new WaitForSeconds(animDelay);
      anim.SetTrigger("sleep");
   }     
      
}
