using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.IsInMiasma = true;
            player.MiasmaTimer.Start();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.IsInMiasma = false;
            player.MiasmaTimer.Stop();
        }
    }


}
