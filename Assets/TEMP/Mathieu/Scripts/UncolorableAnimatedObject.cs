using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncolorableAnimatedObject : MonoBehaviour
{
    InteractorScript[] interactors;

    private bool isColored = false; 



    private void Awake()
    {
       
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
                    StartAnimation(); 
                }
            }
        }
        
    }

    private void StartAnimation()
    {
        GetComponentInChildren<Animator>().SetBool("isActive", true);

        isColored = true; 
    }


    private void FindInteractors()
    {
        interactors = FindObjectsOfType<InteractorScript>();
    }
}
