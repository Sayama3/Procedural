using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;

namespace Procedural01{
    
   [ExecuteInEditMode]
    public class SimpleWall : MonoBehaviour
    {

        [SerializeField] private bool updateOn = false;
        
        
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefabToInstantiate;
        [SerializeField] private int height;
        [SerializeField] private float quinconceOffset;
        private List<GameObject> createdObjects;

        private Vector3 meshBoundingBox;
        
        private void Start()
        {
            this.createdObjects = new List<GameObject>();
            var tmpObject = Instantiate(this.prefabToInstantiate);
            var meshData = tmpObject.GetComponentInChildren<MeshFilter>();
            this.meshBoundingBox = meshData.mesh.bounds.size;
            Destroy(tmpObject);
        }

        private void UpdateWall()
        {
            this.container.transform.position = this.start.position;
            this.container.transform.rotation = this.start.rotation;

            var distance = Vector3.Distance(this.start.position, this.end.position);
            var objNumber = Mathf.Floor(distance / this.meshBoundingBox.x);
            var rot = Vector3.SignedAngle(this.start.right, this.end.position - this.start.position, this.start.right);

            foreach (var obj in this.createdObjects)
            {
                DestroyImmediate(obj);
            }
            this.createdObjects.Clear();
            
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < objNumber; j++)
                {
                    var pos = this.start.position + new Vector3(
                                  this.meshBoundingBox.x * j + this.quinconceOffset * (i % 2.0f),
                                  this.meshBoundingBox.y * i,
                                  0);
                    var createdObject = Instantiate(
                        this.prefabToInstantiate,
                        pos, Quaternion.identity,
                        this.container);
                    this.createdObjects.Add(createdObject);
                }
            }

            if (this.end.position.z > 0)
            {
                rot = rot * -1;
            }
            this.container.rotation = Quaternion.Euler(new Vector3(0, rot, 0));
            
        }

        private void Update()
        {
            if(this.updateOn)
            {
                UpdateWall();
            }
        }
    }
}