using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaKill : MonoBehaviour
{
    InteractorScript[] interactors;
    // Start is called before the first frame update
    void Start()
    {
        FindInteractors(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindInteractors()
    {
        interactors = FindObjectsOfType<InteractorScript>();
    }
}
