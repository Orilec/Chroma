using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableAnimatedObject : ColorableObject
{

    
    public bool shouldAnimateAfterColoration;
    public bool fillColorAfterColoration;
    [SerializeField] private Renderer coloredRenderer;


    InteractorScript[] interactors;
    private bool isColored = false;


    private void Awake()
    {
        interactorEvent = GetComponent<InteractorEvent>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        FindInteractors();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColored)
        {
            for (int i = 0; i < interactors.Length; i++)
            {
                float interactorRadius = interactors[i].radius;
                Vector3 interactorPosition = interactors[i].transform.position;
                Vector3 objectPosition = transform.position;


                bool isPointInRadiusRange = Mathf.Pow((objectPosition.x - interactorPosition.x), 2) + Mathf.Pow((objectPosition.y - interactorPosition.y), 2) + Mathf.Pow((objectPosition.z - interactorPosition.z), 2) < Mathf.Pow(interactorRadius, 2);

                if (isPointInRadiusRange)
                {
                    SetObjectActive();
                }
            }
        }
    }

    private void StartAnimation()
    {
        GetComponentInChildren<Animator>().SetBool("isActive", true);
        GetComponentInChildren<Animator>().SetBool("isReversed", false);
    }

    private void StartReverseAnimation()
    {
        GetComponentInChildren<Animator>().SetBool("isReversed", true); 
        GetComponentInChildren<Animator>().SetBool("isActive", false); 
    }

    private void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        if (shouldAnimateAfterColoration)
        {
            StartAnimation();
        }
        if (fillColorAfterColoration)
        {
            SetMaterialToColored(); 
        } 

    }

    private void InteractorEvent_OnDecolorationFinished(InteractorEvent arg1, InteractorEventArgs arg2)
    {
        SetObjectInactive(); 
    }


    private void OnEnable()
    {
        interactorEvent.OnColorationFinished += InteractorEvent_OnColorationFinished; 
        interactorEvent.OnDecolorationFinished += InteractorEvent_OnDecolorationFinished; 
    }

    private void OnDisable()
    {
        interactorEvent.OnColorationFinished -= InteractorEvent_OnColorationFinished;
        interactorEvent.OnDecolorationFinished -= InteractorEvent_OnDecolorationFinished;
    }

 

    public override void SetObjectActive()
    {
        base.SetObjectActive();
        isColored = true;

        //Animate object and color at the same time

        if (!shouldAnimateAfterColoration)
        {
            StartAnimation(); 
        }

        if (!fillColorAfterColoration)
        {
            SetMaterialToColored(); 
        }
    }

    public void SetObjectInactive()
    {
        isColored = false;
        StartReverseAnimation();
    }

    private void SetMaterialToColored()
    {
        coloredRenderer.material.SetInt("_Debug", 1);
        coloredRenderer.material.SetFloat("_Blend", 1);
    }

    private void FindInteractors()
    {
        interactors = FindObjectsOfType<InteractorScript>();
    }
}
