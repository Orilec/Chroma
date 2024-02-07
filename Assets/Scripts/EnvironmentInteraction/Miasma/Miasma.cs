using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    private PlayerController _player;
    private InteractorScript[] _interactors;
    private bool _wasInInteractorRadiusLastFrame;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        FindInteractors();
    }

    private void FixedUpdate()
    {
        foreach (var interactor in _interactors)
        {
            float interactorRadius = interactor.radius; 
            Vector3 interactorPosition = interactor.transform.position;
            Vector3 objectPosition = transform.position;


            bool isPointInRadiusRange = Mathf.Pow((objectPosition.x - interactorPosition.x), 2) + Mathf.Pow((objectPosition.y - interactorPosition.y), 2) + Mathf.Pow((objectPosition.z - interactorPosition.z), 2) < Mathf.Pow(interactorRadius, 2);

            if (isPointInRadiusRange)   
            {
                ExitMiasmaImmediate();
            }
            _wasInInteractorRadiusLastFrame = isPointInRadiusRange;
        }
    }

    private IEnumerator OnTriggerStay(Collider other)
    {
        if (!_player.IsInMiasma && !_wasInInteractorRadiusLastFrame)
        {
            Debug.Log("starting timer");
            _player.MiasmaTimer.Start();
            _player.IsInMiasma = true;
        }
        yield return new WaitForFixedUpdate();
    }
    private void OnTriggerExit(Collider other)
    {
          ExitMiasma();  
    }

    private void ExitMiasma()
    {
        _player.IsInMiasma = false;
        Invoke(nameof(StopMiasmaTimer), 0.2f);
    }
    private void ExitMiasmaImmediate()
    {
        _player.IsInMiasma = false;
        StopMiasmaTimer();
    }
    
    private void StopMiasmaTimer()
    {
        if (!_player.IsInMiasma)
        {
            _player.MiasmaTimer.Stop();
        }
    }
    
    private void FindInteractors()
    {
        _interactors = FindObjectsOfType<InteractorScript>();
    }

}
