using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterGeneration : MonoBehaviour
{
    private Transform[] test;
    [SerializeField] private List<Transform> categoryHeaders;
    [SerializeField] private List<int> categorySize;
    [SerializeField] private TextMeshProUGUI seedText;
    private List<int> headersID;
    private void Awake()
    {
        test = GetComponentsInChildren<Transform>(true);
        int i = 0;
        headersID = new List<int>();
        for (int j = 0; j < test.Length; j++)
        {
            if (test[j] == categoryHeaders[i])
            {
                test[j+ Random.Range(1, categorySize[i])].gameObject.SetActive(true);
                headersID.Add(j);
                i = Mathf.Min(i + 1, categoryHeaders.Count-1);
            }
        }
    }

    private int RecusiveDigitCounter(int totest)
    {
        int i = 1;
        if (totest > 0) i += RecusiveDigitCounter(Mathf.FloorToInt(totest / 10));
        else i--;
        return (i);
    }
    public void GenerateRandom()
    {
        int seed = GenerateSeedWithClass(Random.Range(0, 5));
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    public void GenerateWarrior()
    {
        int seed = GenerateSeedWithClass(0);
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    public void GenerateRogue()
    {
        int seed = GenerateSeedWithClass(1);
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    public void GenerateMage()
    {
        int seed = GenerateSeedWithClass(2);
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    public void GeneratePriest()
    {
        int seed = GenerateSeedWithClass(3);
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    public void GenerateBerserk()
    {
        int seed = GenerateSeedWithClass(4);
        GenerateAvatarFromSeed(seed);
        seedText.text = seed.ToString();
    }
    private int GenerateSeedWithClass(int _class)
    {
        int sexe = Random.Range(1, 3);
        int seed = sexe * 10 + _class;
        Debug.Log(seed);
        return (seed);
    }
    public void GenerateAvatarFromSeed(int _seed)
    {
    }
}