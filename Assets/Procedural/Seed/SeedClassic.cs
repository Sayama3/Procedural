using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeedClassic : MonoBehaviour
{
    [SerializeField] private int floorNumberMedian;
    [SerializeField] private int floorNumberMaxVariation;
    [SerializeField] private int seed;
    [SerializeField] private int manualSeed;

    [SerializeField] private GameObject targetCross;

    [SerializeField] private List<GameObject> prefabs;

    private List<GameObject> generatedObjects;

    private void Awake()
    {
        this.generatedObjects = new List<GameObject>();
    }


    [Button(ButtonSizes.Large)]
    public void Randomize()
    {
        this.seed = Random.Range(10000, 64000);

        GenerateTower(this.seed);
    }

    private void GenerateTower(int localSeed)
    {
        if (this.generatedObjects.Count > 0)
        {
            foreach (var generatedObject in this.generatedObjects)
            {
                Destroy(generatedObject);
            }
        }

        this.generatedObjects.Clear();

        //number of floor
        int floorTotal = this.floorNumberMedian;
        int floorNumberToAdd = localSeed % this.floorNumberMaxVariation;
        if (localSeed % 2 == 0)
        {
            floorTotal = this.floorNumberMedian + floorNumberToAdd;
        }
        else
        {
            floorTotal = this.floorNumberMedian - floorNumberToAdd;
        }

        var crossectionGenerated = false;
        var crossection = localSeed % floorTotal;
        var height = 0f;
        var seedEnhanced = localSeed;
        for (int i = 0; i < floorTotal; i++)
        {
            var prefabToUse = seedEnhanced % this.prefabs.Count;
            this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(0, height, 0),
                Quaternion.identity));

            if (crossection == i && this.prefabs[prefabToUse] == targetCross)
            {
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(1, height, 0),
                Quaternion.identity));
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(2, height, 0),
                Quaternion.identity));
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(-1, height, 0),
                Quaternion.identity));
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(-2, height, 0),
                Quaternion.identity));
                crossectionGenerated = true;
            }
            if (i > crossection && crossectionGenerated)
            {
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(2, height, 0),
                Quaternion.identity));
                this.generatedObjects.Add(Instantiate(
                this.prefabs[prefabToUse],
                new Vector3(-2, height, 0),
                Quaternion.identity));

            }

            height+= this.prefabs[prefabToUse].GetComponent<MeshFilter>().sharedMesh.bounds.size.y;
            seedEnhanced = (int) ((localSeed * height) / Mathf.Pow(prefabToUse + 1, 2));
        }
    }

    [Button(ButtonSizes.Large)]
    public void UseManualSeed()
    {
        GenerateTower(this.manualSeed);
    }
}
