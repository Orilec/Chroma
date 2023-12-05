using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHolder : MonoBehaviour
{
    [SerializeField] MeshCollider meshCollider;


    public void ActivateObject()
    {
        ActivateCollider();
       
    }

    private void ActivateCollider()
    {
        meshCollider.enabled = true; 
    }

    private void ActivateScript()
    {
      
    }
}
