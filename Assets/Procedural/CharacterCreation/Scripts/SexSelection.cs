using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SexSelection : MonoBehaviour {
   
   [SerializeField] private Animator animator;
  
   [Button(ButtonSizes.Small)]
   public void SetMan() {
      this.animator.SetBool("Sex", false);
   }
   
   [Button(ButtonSizes.Small)]
   public void SetWoman() {
      this.animator.SetBool("Sex", true);
   }
}
