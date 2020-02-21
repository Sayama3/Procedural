using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassHolder : MonoBehaviour
{
    [SerializeField] private bool warrior;
    [SerializeField] private bool rogue;
    [SerializeField] private bool mage;
    [SerializeField] private bool priest;
    [SerializeField] private bool berserk;

    [HideInInspector] public bool[] classes;
    private void Awake()
    {
        classes = new bool[]
            {
            warrior,
            rogue,
            mage,
            priest,
            berserk
            };
    }
}
