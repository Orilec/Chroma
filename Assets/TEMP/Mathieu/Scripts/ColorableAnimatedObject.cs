using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableAnimatedObject : ColorableObject
{

    
    public bool shouldAnimateAfterColoration;
    


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
        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetBool("isActive", true);
            GetComponentInChildren<Animator>().SetBool("isReversed", false);
        }
      
    }

    private void StartReverseAnimation()
    {
        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetBool("isReversed", true);
            GetComponentInChildren<Animator>().SetBool("isActive", false);
        }

    }

    protected override void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        base.InteractorEvent_OnColorationFinished(interactorEvent, interactorEventArgs); 

        if (shouldAnimateAfterColoration)
        {
            StartAnimation();
        }
        //if (fillColorAfterColoration)
        //{
        //    SetMaterialToColored(); 
        //} 

    }

    protected override void InteractorEvent_OnDecolorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        base.InteractorEvent_OnDecolorationFinished(interactorEvent, interactorEventArgs);
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
