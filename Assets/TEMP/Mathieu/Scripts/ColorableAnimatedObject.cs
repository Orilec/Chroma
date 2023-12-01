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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAnimation()
    {
        GetComponentInChildren<Animator>().SetBool("isActive", true);
    }

    private void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        if (shouldAnimateAfterColoration)
        {
            StartAnimation();
        }
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

        //Animate object and color at the same time

        if (!shouldAnimateAfterColoration)
        {
            StartAnimation(); 
        }

    }
}
