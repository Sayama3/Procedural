using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Procedural03
{

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
            CreateTheClass();
        }

        private void CreateTheClass()
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

        public bool CheckClass(ClassChoose classOfPlayer)
        {
            if (classes.Length == 0)
            {
                //Debug.LogError("Youre trying to acces to " + this.name + " which not suppose to have a script ClassHolder. Anyway I will return false;");
                CreateTheClass();
                return classes[(int)classOfPlayer];
            }
            else
            {
                return classes[(int)classOfPlayer];
            }
        }
    }
}
