using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager
{
    private PlayerController _playerControllerRef;

    public void SubscribePlayer(PlayerController player)
    {
        _playerControllerRef = player;
    }

    public PlayerController GetPlayer()
    {
        return _playerControllerRef;
    }
}
