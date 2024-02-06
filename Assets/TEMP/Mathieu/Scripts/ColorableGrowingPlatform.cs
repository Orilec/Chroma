using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableGrowingPlatform : ColorableObject
{
    [SerializeField] private float growthValue; 
    [SerializeField] private float timeToGrow;
    private Vector3 startScale; 

    [SerializeField] private AnimationCurve easeGrowCurve;

    private void Awake()
    {
        interactorEvent = GetComponent<InteractorEvent>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        startScale = transform.localScale; 
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    private void StartGrow()
    {
        StartCoroutine(Grow()); 
    }

    private void StartWither()
    {
        StartCoroutine(Wither());
    }

    protected override void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {

        base.InteractorEvent_OnColorationFinished(interactorEvent, interactorEventArgs);
    }

    protected override void InteractorEvent_OnDecolorationFinished(InteractorEvent arg1, InteractorEventArgs arg2)
    {
        base.InteractorEvent_OnDecolorationFinished(arg1, arg2);
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


        StartGrow(); 

    }

    public override void SetObjectInactive()
    {
        base.SetObjectInactive();

        StartWither(); 
    }


    IEnumerator Grow()
    {
        float timeElapsed = 0;

        Vector3 scaleToReach = new Vector3(startScale.x, growthValue, startScale.z);

        while (timeElapsed < timeToGrow)
        {
            var normalizedProgress = timeElapsed / timeToGrow; // 0-1
            var easing = easeGrowCurve.Evaluate(normalizedProgress);
            transform.localScale = Vector3.LerpUnclamped(startScale, scaleToReach, easing);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = scaleToReach; // Set final scale

        //Call event for animation/fill
        if (GetComponent<ColorableObject>() != null)
        {
            GetComponent<ColorableObject>().interactorEvent.CallInteractor();
        }
    }

    IEnumerator Wither()
    {
        float timeElapsed = 0;

        Vector3 actualScale = transform.localScale;

        while (timeElapsed < timeToGrow)
        {
            transform.localScale = Vector3.Lerp(actualScale, startScale, timeElapsed / timeToGrow);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startScale; // Set final scale

        //Call event for animation/fill
        if (GetComponent<ColorableObject>() != null)
        {
            GetComponent<ColorableObject>().interactorEvent.CallInteractor();
        }
    }

}
