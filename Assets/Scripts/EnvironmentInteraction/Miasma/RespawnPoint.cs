using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var respawnSystem = other.GetComponent<RespawnSystem>();
        if (respawnSystem != null)
        {
            respawnSystem.UpdateRespawnPoint(this);
        }
    }
}
