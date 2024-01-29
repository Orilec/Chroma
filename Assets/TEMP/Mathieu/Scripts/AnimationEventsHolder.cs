using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHolder : MonoBehaviour
{
    [SerializeField] Collider meshCollider;


    public void ActivateObject()
    {
        ActivateCollider();
       
    }

    public void DesactivateObject()
    {
        DesactivateCollider();

    }

    private void ActivateCollider()
    {
        meshCollider.enabled = true; 
    }

    private void DesactivateCollider()
    {
        meshCollider.enabled = false;
    }

    private void ActivateScript()
    {
      
    }
}
