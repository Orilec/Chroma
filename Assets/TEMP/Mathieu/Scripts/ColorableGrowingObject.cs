using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableGrowingObject : ColorableObject
{
    public List<MeshRenderer> growMeshes;
    public float timeToGrow = 5f;
    public float refreshRate = 0.05f;
    public float minGrow = 0.2f;
    [Range(0, 1)]
    public float maxGrow = 0.97f;

    private List<Material> growMaterials = new List<Material>();
    private bool fullyGrown;




    private void Awake()
    {
        interactorEvent = GetComponent<InteractorEvent>(); 
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < growMeshes.Count; i++)
        {
            for (int j = 0; j < growMeshes[i].materials.Length; j++)
            {
                if (growMeshes[i].materials[j].HasProperty("_Grow"))
                {
                    growMeshes[i].materials[j].SetFloat("_Grow", maxGrow);
                    growMaterials.Add(growMeshes[i].materials[j]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GrowAll()
    {
        for (int i = 0; i < growMaterials.Count; i++)
        {
            StartCoroutine(Grow(growMaterials[i]));
        }
    }


    IEnumerator Grow(Material mat)
    {
        float growValue = mat.GetFloat("_Grow");

        fullyGrown = CheckFullyGrown(mat);

        if (!fullyGrown)
        {
            while(growValue < maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("_Grow", growValue);

                yield return new WaitForSeconds(refreshRate); 
            }
        }
        else
        {
            while (growValue > minGrow)
            {
                growValue -= 1 / (timeToGrow / refreshRate);
                mat.SetFloat("_Grow", growValue);

                yield return new WaitForSeconds(refreshRate);
            }
        }

        fullyGrown = CheckFullyGrown(mat);
    }

    private bool CheckFullyGrown(Material mat)
    {
        float growValue = mat.GetFloat("_Grow");

        return growValue >= maxGrow; 
    }

    private void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        GrowAll();
    }


    private void OnEnable()
    {
        interactorEvent.OnColorationFinished += InteractorEvent_OnColorationFinished; 
    }

    private void OnDisable()
    {
        interactorEvent.OnColorationFinished -= InteractorEvent_OnColorationFinished;
    }

    public override void SetObjectActive()
    {
        base.SetObjectActive();

    }
}
