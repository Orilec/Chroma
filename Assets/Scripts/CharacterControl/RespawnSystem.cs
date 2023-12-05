using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private List<RespawnPoint> _respawnPoints;
    private RespawnPoint _currentRespawnPoint;

    public RespawnPoint CurrentRespawnPoint { get { return _currentRespawnPoint; } }

    private void Awake()
    {
        _currentRespawnPoint = _respawnPoints[0];
    }

    public void UpdateRespawnPoint(RespawnPoint point)
    {
        if (_respawnPoints.IndexOf(point) > _respawnPoints.IndexOf(_currentRespawnPoint))
        {
            _currentRespawnPoint = point;
        }
    }
}
