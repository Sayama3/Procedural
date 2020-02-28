using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using PsychoticLab;


namespace Procedural03
{

    public class SeedGenerator : MonoBehaviour
    {
        int seed;
        [SerializeField] private int from = 10000;
        [SerializeField] private int to = 70000;

        [SerializeField] GameObject LaOuLeProgrammeDoitAfficherLaSeed;
        [SerializeField] GameObject LaSeedQueLeProgrammeDoitRecuperer;

        private bool hasGenerate = false;
        private bool hasSend = false;

        CharacterGenerator generator;
        private void Awake()
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        private void Start()
        {
            generator = GetComponent<CharacterGenerator>();
        }

        private void Update()
        {
            if (hasGenerate && !hasSend)
            {
                if (seed > 0)
                {
                    
                    LaOuLeProgrammeDoitAfficherLaSeed.GetComponent<TextMeshProUGUI>().SetText(seed.ToString());
                    hasSend = true;
                    generator.GenerateWithSeed(seed);
                    seed = -1;
                }
                else
                {
                    hasGenerate = false;
                }
            }
        }

        public void HasGenerate()
        {
            hasSend = false;
            hasGenerate = false;
            seed = -1;
            Debug.Log("FinishTheGeneration");
        }

        public void GenerateWarrior()
        {
            int indexToTest = (int)ClassChoose.Warrior;
            GenerateCharacter(indexToTest);
        }
        public void GenerateRogue()
        {
            int indexToTest = (int)ClassChoose.Rogue;
            GenerateCharacter(indexToTest);
        }
        public void GenerateMage()
        {
            int indexToTest = (int)ClassChoose.Mage;
            GenerateCharacter(indexToTest);
        }
        public void GeneratePriest()
        {
            int indexToTest = (int)ClassChoose.Priest;
            GenerateCharacter(indexToTest);
        }
        public void GenerateBerserk()
        {
            int indexToTest = (int)ClassChoose.Berserk;
            GenerateCharacter(indexToTest);
        }


        private void GenerateCharacter(int indexOfType)
        {
            if (!hasGenerate)
            {
                seed = Random.Range(from, to);
                while (seed % 5 != indexOfType)
                {
                    seed = Random.Range(from, to);
                }
                hasGenerate = true;
            }
            else
            {
                Debug.LogError("The programm is actually generate a characters");
            }
        }
        public void GenerateRandom()
        {
            seed = Random.Range(from, to);
            hasGenerate = true;
        }

        public void GenerateFromSeed()
        {
            //Debug.
            string seedGiven =  LaSeedQueLeProgrammeDoitRecuperer.GetComponent<TextMeshProUGUI>().text;
            seedGiven = seedGiven.Remove(seedGiven.Length - 1);
            seed = int.Parse(seedGiven);
            hasGenerate = true;


        }
    }
}
