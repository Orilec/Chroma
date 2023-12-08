using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractorScript : MonoBehaviour
{
    
    public float radius;
    public float maxRadius;
    public bool isActive = false;
    public float colorationSpeed = 0.03f; 

    private bool swapFinished = false;
    private GameObject parent; 


    private void Awake()
    {
        parent = transform.root.gameObject;
    }

    private void Start()
    {
        //maxRadius = Random.Range(3f, 5f); 
    }

    // Update is called once per frame
    void Update()
    {
        if (radius < maxRadius && isActive && !swapFinished)
        {
            radius = Mathf.Lerp(radius, maxRadius, colorationSpeed); 
        }

        if ((maxRadius - radius) <= (maxRadius/10) && !swapFinished)
        {
            swapFinished = true;

            if (parent.GetComponent<ColorableObject>() != null)
            {
                parent.GetComponent<ColorableObject>().interactorEvent.CallInteractor();
            }


        }

        //Shader.SetGlobalFloat("_RadiusInteractor", radius);
    }

    public bool GetSwapFinished()
    {
        return swapFinished; 
    }
}