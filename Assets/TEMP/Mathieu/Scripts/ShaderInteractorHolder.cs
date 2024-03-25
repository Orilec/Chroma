using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInteractorHolder : MonoBehaviour
{

    public static InteractorScript[] interactors;
    Vector4[] positions = new Vector4[100];
    float[] radiuses = new float[100];

    [Range(0, 1)]
    public float shapeCutoff;
    [Range(0, 1)]
    public float shapeSmoothness = .1f; 

    // Start is called before the first frame update
    void Start()
    {
        //FindInteractors();
        Debug.Log(interactors.Length);
    }

    private void OnEnable()
    {
        interactors = new InteractorScript[100]; 
    }

    private void Update()
    {

        //FindInteractors();

        for (int i = 0; i < interactors.Length; i++)
        {
            if (interactors[i] != null)
            {
                positions[i] = interactors[i].transform.position;
                radiuses[i] = interactors[i].radius;
            }

        }

        Shader.SetGlobalVectorArray("_ShaderInteractorsPositions", positions);
        Shader.SetGlobalFloatArray("_ShaderInteractorsRadiuses", radiuses); 

        Shader.SetGlobalFloat("_ShapeCutoff", shapeCutoff); 
        Shader.SetGlobalFloat("_ShapeSmoothness", shapeSmoothness); 

    }

    private void FindInteractors()
    {
       
    }

    
}
