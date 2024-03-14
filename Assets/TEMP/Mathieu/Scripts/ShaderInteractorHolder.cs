using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInteractorHolder : MonoBehaviour
{
    public static ShaderInteractorHolder instance;
    public Dictionary<int, InteractorScript> interactorDic;

    private int currentIndex;


    Vector4[] positions = new Vector4[30];
    float[] radiuses = new float[30];

    [Range(0, 1)]
    public float shapeCutoff;
    [Range(0, 1)]
    public float shapeSmoothness = .1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            interactorDic = new Dictionary<int, InteractorScript>();
        }
        else
        {
            Destroy(this);
        }

    }


    public void SubcribeInteractor(InteractorScript interactorToSub)
    {
        if(interactorDic.ContainsValue(interactorToSub))
        {
            return;
        }
        interactorDic.Add(currentIndex, interactorToSub);
        currentIndex++;
        if(currentIndex>=100)
        {
            currentIndex = 0;
        }
    }

    public void UnsubscribeInteractor(InteractorScript interactorToSub)
    {
        int temp = -1;
        foreach (var entry in interactorDic)
        {
            if(entry.Value == interactorToSub)
            {
                temp = entry.Key;
                break;
            }
        }
        if(temp>=0)
        {
            interactorDic.Remove(temp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //FindInteractors();
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        if(interactorDic.Keys.Count<=0)
        { return; }

        //FindInteractors();
        foreach (var key in interactorDic.Keys)
        {
            positions[key] = interactorDic[key].transform.position;
            radiuses[key] = interactorDic[key].radius;
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
