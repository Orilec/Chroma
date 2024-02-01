using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableAnimatedObject : ColorableObject
{

    
    public bool shouldAnimateAfterColoration;
    

    private void Awake()
    {
        interactorEvent = GetComponent<InteractorEvent>(); 
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update(); 
        
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
        //if (fillColorAfterColoration)
        //{
        //    SetMaterialToColored(); 
        //} 

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
        

        //Animate object and color at the same time

        if (!shouldAnimateAfterColoration)
        {
            StartAnimation(); 
        }

       
    }

    public override void SetObjectInactive()
    {
        base.SetObjectInactive();
        StartReverseAnimation();
    }

    
}
