using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBoost : MonoBehaviour
{
    private bool _started;
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null && !_started)
        {
            _started = true;
            player.SlideBoostTimer.Start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            _started = false;
        }
    }
    
}
