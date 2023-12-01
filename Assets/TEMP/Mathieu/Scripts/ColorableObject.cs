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
        GetComponentInChildren<InteractorScript>().isActive = true;

        //if (GetComponent<ColorableGrowingObject>())
        //{

        //}
        //else
        //{
        //    GetComponentInChildren<Animator>().SetBool("isActive", true);
        //}

    }

}



