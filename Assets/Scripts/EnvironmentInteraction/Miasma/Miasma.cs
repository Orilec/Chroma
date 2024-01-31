using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_player.IsInMiasma)
        {
            _player.MiasmaTimer.Start();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _player.IsInMiasma = true;
    }
    private void OnTriggerExit(Collider other)
    {
            _player.IsInMiasma = false;
            Invoke(nameof(StopMiasmaTimer), 0.2f);
    }
    
    private void StopMiasmaTimer()
    {
        if (!_player.IsInMiasma)
        {
            _player.MiasmaTimer.Stop();
        }
    }

}
