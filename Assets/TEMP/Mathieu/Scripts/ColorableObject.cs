using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableObject : MonoBehaviour
{

    public InteractorEvent interactorEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetObjectActive()
    {
        GetComponentInChildren<InteractorScript>().SetInteractorActive();


        //if (GetComponent<ColorableGrowingObject>())
        //{

        //}
        //else
        //{
        //    GetComponentInChildren<Animator>().SetBool("isActive", true);
        //}
    }

    //Draw coloration interactor limits in scene
    void OnDrawGizmosSelected()
    {
        Vector3 interactorPosition = GetComponentInChildren<InteractorScript>().transform.position;
        float interactorRadius = GetComponentInChildren<InteractorScript>().maxRadius;

        // Draw a sphere and wireframe at the transform's position
        Gizmos.color = new Color(1,0,0,0.2f);
        Gizmos.DrawSphere(interactorPosition, interactorRadius);
        Gizmos.color = new Color(1,0,0,1);
        Gizmos.DrawWireSphere(interactorPosition, interactorRadius);
    }

}



