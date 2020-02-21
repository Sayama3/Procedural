using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomizerHelp : MonoBehaviour {

    [SerializeField] private int from;
    [SerializeField] private int to;
    public int LastRandom;
    
    void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    [Button(ButtonSizes.Small)]
    public void RandomizeFromTo() {
        this.LastRandom = Random.Range(from, to + 1);
    }
    
}
