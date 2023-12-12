using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNormals : MonoBehaviour
{
    private MeshFilter[] childMeshFilters;


    private void Awake()
    {
        childMeshFilters = GetComponentsInChildren<MeshFilter>(); 
    }

    // Start is called before the first frame update
    void Start()
    {

        foreach (var childMesh in childMeshFilters)
        {
            Mesh mesh = childMesh.mesh; 

            Vector3[] normals = mesh.vertices;
            int length = normals.Length;
            for (int i = 0; i < length; i++)
            {
                normals[i] = normals[i].normalized;
            }
            mesh.normals = normals;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
