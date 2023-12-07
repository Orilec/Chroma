using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ValidateLevel : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _holdButtonTime;
    [SerializeField] private CinemachineVirtualCamera _newCam;
    private CountdownTimer _timer;
    private PlayerController _player;
    private IEnumerator _coroutine;
    private bool _timerStarted, _validated;
    private Miasma[] miasmaObstacles;

    private ColorableObject _colorableObject;
    private InteractorEvent _interactorEvent;
     
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && !_validated)
        {
            _player = player;
            _input.DisableCharacterControl();
        }
    }

    private void Awake()
    {
        _timer = new CountdownTimer(_holdButtonTime);
        miasmaObstacles = FindObjectsOfType<Miasma>();

        _colorableObject = GetComponent<ColorableObject>(); 
        _interactorEvent = GetComponent<InteractorEvent>(); 
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
        if (_player != null)
        {
            if (_input.ValidateLevel)
            {
                if (!_timerStarted)
                {
                    _timerStarted = true;
                    _timer.Start();
                }
            }
            else
            {
                _timerStarted = false;
                _timer.Stop();
            }
        }

        if (_timer.IsFinished && _timerStarted && !_validated)
        {
            Validate();
        }
    }

    private void Validate()
    {
        _validated = true;
        _newCam.Priority = 15;
        
        _colorableObject.SetObjectActive();
    }

    private void ReturnToPlayMode()
    {
        _newCam.Priority = 2;
        _input.EnableCharacterControl();
    }

    private void DestroyMiasma()
    {
        for (int i = 0; i < miasmaObstacles.Length; i++)
        {
            Destroy(miasmaObstacles[i]);
        }
    }
    
    private void InteractorEvent_OnColorationFinished(InteractorEvent interactorEvent, InteractorEventArgs interactorEventArgs)
    {
        DestroyMiasma();
        ReturnToPlayMode();
    }
    
    private void OnEnable()
    {
        _interactorEvent.OnColorationFinished += InteractorEvent_OnColorationFinished; 
    }

    private void OnDisable()
    {
        _interactorEvent.OnColorationFinished -= InteractorEvent_OnColorationFinished;
    }

}
