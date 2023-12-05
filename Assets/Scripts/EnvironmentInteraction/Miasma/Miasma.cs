using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    private PlayerController _player;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (!player.IsInMiasma)
            {
                player.MiasmaTimer.Start();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.IsInMiasma = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.IsInMiasma = false;
            _player = player;
            Invoke(nameof(StopMiasmaTimer), 0.2f);
        }
    }

    private void StopMiasmaTimer()
    {
        if (!_player.IsInMiasma)
        {
            _player.MiasmaTimer.Stop();
        }
    }

}
