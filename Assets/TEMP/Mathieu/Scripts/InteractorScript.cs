using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractorScript : MonoBehaviour
{
    
    public float radius;
    public float maxRadius;
    public bool isActive = false;
    public float colorationSpeed = 3f;

    [SerializeField] private AnimationCurve easeColorationCurve; 

    public float colorationTime = 0.6f;
    public float decolorationTime = 2f;

    public bool isCast;
    public bool isTemporary;
    public float decolorationSpeed;

    private bool swapFinished = false;
    private GameObject parent;

    //Sphere Collider for miasma particles kill
    private CapsuleCollider _capsuleCollider; 

    public CapsuleCollider CapsuleCollider { get {return _capsuleCollider;  } }

    private void Awake()
    {
        parent = transform.root.gameObject;

        _capsuleCollider = GetComponent<CapsuleCollider>(); 
    }

    private void Start()
    {
        //maxRadius = Random.Range(3f, 5f); 
        if (isCast) SetInteractorActive(); // activate temporary interactor as soon as it spawns
    }

    // Update is called once per frame
    void Update()
    {
        //if (radius < maxRadius && isActive && !swapFinished) //Process coloration for interactor object and temporary interactors
        //{
        //    radius = Mathf.Lerp(radius, maxRadius,  Time.deltaTime); 
        //}

        //if ((maxRadius - radius) <= (maxRadius/10) && !swapFinished) //Coloration limit : finish swap for event
        //{
        //    swapFinished = true;

        //    if (parent.GetComponent<ColorableObject>() != null)
        //    {
        //        parent.GetComponent<ColorableObject>().interactorEvent.CallInteractor();
        //    }

        //}

        //if(swapFinished && isTemporary) //if temporary, swap back to uncolored 
        //{
        //    radius = Mathf.Lerp(radius, 0, decolorationSpeed * Time.deltaTime);
        //    if(radius <= maxRadius / 10 && isCast) // if interactor is casted, destroy 
        //    {
        //        Destroy(gameObject); 
        //    }
        //    else if(radius <= maxRadius / 10 && !isCast) //if interactor is not casted
        //    {
        //        radius = 0f; 
        //        isActive = false;
        //        swapFinished = false;

        //        // ----- > RESET TARGET <-------
        //    }
        //}

        //Shader.SetGlobalFloat("_RadiusInteractor", radius);

        //Update Sphere Collider radius
        _capsuleCollider.radius = radius;
        _capsuleCollider.height = 2 * radius; 
    }


    public void SetInteractorActive()
    {
        AddToInteractorsArray(); 

        if (isTemporary)
        {
            StartCoroutine(SwapTemporaryColor());
        }
        else
        {
            StartCoroutine(SwapColor());
        }

    }

    public bool GetSwapFinished()
    {
        return swapFinished; 
    }

    IEnumerator SwapColor()
    {
        float timeElapsed = 0;
        

        while (timeElapsed < colorationTime)
        {
            var normalizedProgress = timeElapsed / colorationTime; // 0-1
            var easing = easeColorationCurve.Evaluate(normalizedProgress);
            radius = Mathf.Lerp(0, maxRadius, easing);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        radius = maxRadius; // Set final radius


        RemoveFromInteractorsArray(); 

        //Call event for animation/fill
        if (parent.GetComponent<ColorableObject>() != null)
        {
            parent.GetComponent<ColorableObject>().interactorEvent.CallInteractor();
        }
    }

    IEnumerator SwapTemporaryColor()
    {
        float timeElapsed = 0;
        while (timeElapsed < colorationTime)
        {
            radius = Mathf.Lerp(0, maxRadius, timeElapsed / colorationTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }


        radius = maxRadius; // Set final radius


        timeElapsed = 0;
        while (timeElapsed < decolorationTime)
        {
            radius = Mathf.Lerp(maxRadius, 0, timeElapsed / decolorationTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        radius = 0;



        RemoveFromInteractorsArray(); 

        //Call event for reverse animation
        if (!isCast)
        {
            if (parent.GetComponent<ColorableObject>() != null)
            {
                parent.GetComponent<ColorableObject>().interactorEvent.CallDecolorationInteractor();
            }
        }

        if (isCast)
        {
            Destroy(gameObject); 
        }
    }

    private void AddToInteractorsArray()
    {
        ShaderInteractorHolder.instance.SubcribeInteractor(this);
        //for (int i = 0; i < ShaderInteractorHolder.interactors.Length; i++)
        //{
        //    var interactor = ShaderInteractorHolder.interactors[i]; 
        //    //ShaderInteractorHolder.instance.UpdateInteractor();

        //    if (interactor == null)
        //    {
        //        ShaderInteractorHolder.interactors[i] = this;
        //        break; 
        //    }
        //}
    }

    private void RemoveFromInteractorsArray()
    {
        //for (int i = 0; i < ShaderInteractorHolder.interactors.Length; i++)
        //{
        //    var interactor = ShaderInteractorHolder.interactors[i];

        //    if (interactor == this)
        //    {
        //        ShaderInteractorHolder.interactors[i] = null;
        //        break; 
        //    }
        //}
        ShaderInteractorHolder.instance.UnsubscribeInteractor(this);
    }
}