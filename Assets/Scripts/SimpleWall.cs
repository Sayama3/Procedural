using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Procedural01
{
    //[ExecuteInEditMode]
    public class SimpleWall : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform endHorizontal;
        [SerializeField] private Transform endVertical;
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefabToInstantiate;
        [SerializeField] private float quinconceOffset;
        [SerializeField] private float probabilityChance;
        private List<GameObject> createdObjects;
        private Vector3 meshBoundingBox;

        private void Start()
        {
            createdObjects = new List<GameObject>();
            var tmpObject = Instantiate(prefabToInstantiate);
            var meshData = tmpObject.GetComponentInChildren<MeshFilter>();
            //var meshData = this.prefabToInstantiate.GetComponentInChildren<MeshFilter>();

            var boundingBox = meshData.mesh.bounds;
            //var boundingBox = Vector3.one;
            meshBoundingBox = boundingBox.size;
            Destroy(tmpObject);
        }

        private void Genrerate()
        {
            

            var distanceVertical = this.endVertical.position.y - this.start.position.y;
            var distanceHorizontal = Vector3.Distance(this.start.position, this.endHorizontal.position);


            var objNumberVertical = Mathf.Floor(distanceVertical / meshBoundingBox.y);
            var objNumberHorizontal = Mathf.Floor(distanceHorizontal / meshBoundingBox.x);
            //var objNumber = Mathf.Floor(distance / boundingBox.x);

            var rot = Vector3.SignedAngle(this.start.right, this.endVertical.position - this.start.position, this.start.right);
            //var rot = (this.endHorizontal.position - this.start.position).normalized;
            //this.container.rotation = Quaternion.LookRotation(rot);

            foreach (var item in this.createdObjects)
            {
                DestroyImmediate(item);
            }
            createdObjects.Clear();

            for (int j = 0; j < objNumberVertical; j++)
            {
                for (int i = 0; i < objNumberHorizontal; i++)
                {
                    var pos = this.start.position + new Vector3(meshBoundingBox.x * i + quinconceOffset * (j % 2), meshBoundingBox.y * j, 0);
                    //var pos = this.start.position + new Vector3(boundingBox.x * i, 0, 0);
                    if (Random.Range(0, 100) <= probabilityChance)
                    {
                        var createdObject = Instantiate(this.prefabToInstantiate, pos, Quaternion.identity, container);
                        this.createdObjects.Add(createdObject);
                    }
                }
            }
            this.container.rotation = Quaternion.Euler(new Vector3(0, rot, 0));
        }

        private void Update()
        {
            if (this.endHorizontal.hasChanged || this.endVertical.hasChanged)
            {
            Genrerate();
            }
        }
    }
}