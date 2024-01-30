using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{

    [SerializeField] private List<ColorableObject> pathObjects; 
    [SerializeField] private float activationTime; 
    [SerializeField] private float deactivationTime; 

    private List<InteractorScript> pathInteractors;

    // Start is called before the first frame update
    void Start()
    {
        GetInteractorsFromPathObjects();
        AssignColorationAndDecolorationTimes();
        SetChildrenInteractorsToTemporary(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetInteractorsFromPathObjects()
    {
        pathInteractors = new List<InteractorScript>();

        for (int i = 0; i < pathObjects.Count; i++)
        {
            pathInteractors.Add(pathObjects[i].GetComponentInChildren<InteractorScript>()); 
        }
    }

    private void AssignColorationAndDecolorationTimes()
    {
        InteractorScript triggerObjectInteractor = GetComponentInChildren<InteractorScript>();
        triggerObjectInteractor.decolorationTime = deactivationTime; 

        int numbersOfInteractorsInPath = pathInteractors.Count; 

        for (int i = 0; i < numbersOfInteractorsInPath; i++)
        {
            pathInteractors[i].colorationTime = activationTime / numbersOfInteractorsInPath; 
            pathInteractors[i].decolorationTime = deactivationTime; 
        }
    }

    private void SetChildrenInteractorsToTemporary()
    {
        InteractorScript triggerObjectInteractor = GetComponentInChildren<InteractorScript>();

        int numbersOfInteractorsInPath = pathInteractors.Count;

        for (int i = 0; i < numbersOfInteractorsInPath; i++)
        {
            pathInteractors[i].isTemporary = true;

        }
    }
}
