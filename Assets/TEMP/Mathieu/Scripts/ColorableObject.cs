using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableObject : MonoBehaviour
{

    public InteractorEvent interactorEvent;

    private InteractorScript interactor;

    [SerializeField] float interactorRadius;

    InteractorScript[] interactors;

    public bool isColored = false;

    [SerializeField] private Renderer coloredRenderer;

    public bool fillColorAfterColoration;

    public bool isSwitch; //pas la console

    public TriggerObject associatedDoor; 

    public float switchReactivationTime; 



    private void Awake()
    {
        interactorEvent = GetComponent<InteractorEvent>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        FindInteractors();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isColored)
        {
            TrackInteractors(); 
        }
    }

    public virtual void SetObjectActive()
    {
        GetComponentInChildren<InteractorScript>().SetInteractorActive();

        isColored = true;

        if (!fillColorAfterColoration)
        {
            SetMaterialToColored();
        }

    }

    public virtual void SetObjectInactive()
    {
        isColored = false;
        SetMaterialToBlackAndWhite(); 
    }


    private void FindInteractors()
    {
        interactors = FindObjectsOfType<InteractorScript>();
    }

    private void TrackInteractors()
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

    private void SetMaterialToColored()
    {
        coloredRenderer.material.SetInt("_Debug", 1);
        coloredRenderer.material.SetFloat("_Blend", 1);
    }

    private void SetMaterialToBlackAndWhite()
    {
        coloredRenderer.material.SetInt("_Debug", 0);
    }

    protected virtual void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        //Event on Coloration Finished


    }

    protected virtual void InteractorEvent_OnDecolorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
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

    // EDITOR 

    //Draw coloration interactor limits in scene
    void OnDrawGizmosSelected()
    {
        InteractorScript interactor = GetComponentInChildren<InteractorScript>();

        Vector3 interactorPosition = interactor.transform.position;
        float interactorRadius = interactor.maxRadius;

        // Draw a sphere and wireframe at the transform's position
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(interactorPosition, interactorRadius);
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(interactorPosition, interactorRadius);



    }


    private void OnValidate()
    {
        InteractorScript interactor = GetComponentInChildren<InteractorScript>();

        interactor.maxRadius = Mathf.Max(1, interactorRadius);
    }


}



