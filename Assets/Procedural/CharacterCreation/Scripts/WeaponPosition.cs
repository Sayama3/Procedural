using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour {
    
    [SerializeField] private Transform handGrip;

    private void Start() {
        this.transform.position = this.handGrip.position;
        this.transform.rotation = this.handGrip.rotation;
    }

    private void Update() {
        this.transform.position = this.handGrip.position;
        this.transform.rotation = this.handGrip.rotation;
    }
}
