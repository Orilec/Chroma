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

    public bool isCast;
    public bool isTemporary;
    public float decolorationSpeed;

    private bool swapFinished = false;
    private GameObject parent; 


    private void Awake()
    {
        parent = transform.root.gameObject;
    }

    private void Start()
    {
        //maxRadius = Random.Range(3f, 5f); 
        if (isCast) isActive = true; // activate temporary interactor as soon as it spawns
    }

    // Update is called once per frame
    void Update()
    {
        if (radius < maxRadius && isActive && !swapFinished) //Process coloration for interactor object and temporary interactors
        {
            radius = Mathf.Lerp(radius, maxRadius, colorationSpeed); 
        }

        if ((maxRadius - radius) <= (maxRadius/10) && !swapFinished) //Coloration limit : finish swap for event
        {
            swapFinished = true;

            if (parent.GetComponent<ColorableObject>() != null)
            {
                parent.GetComponent<ColorableObject>().interactorEvent.CallInteractor();
            }

        }

        if(swapFinished && isTemporary) //if temporary, swap back to uncolored 
        {
            radius = Mathf.Lerp(radius, 0, decolorationSpeed);
            if(radius <= maxRadius / 10 && isCast) // if interactor is casted, destroy 
            {
                Destroy(gameObject); 
            }
        }

        //Shader.SetGlobalFloat("_RadiusInteractor", radius);
    }

    public bool GetSwapFinished()
    {
        return swapFinished; 
    }
}