using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private TargetSystem _targetSystem;
    [SerializeField] private Transform _playerTransform;
    // [SerializeField] private ColorableObject _colorable;
    private MeshRenderer _renderer;
    public bool isReachable, isActivated;

    private Camera _mainCam;
    private void OnEnable()
    {
        _targetSystem = FindObjectOfType<TargetSystem>();
        _playerTransform = FindObjectOfType<PlayerController>().transform;
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, Camera.main.transform.right);
        transform.rotation *= Quaternion.Euler(90,0,0);
        
        Vector3 playerToObject = transform.position - _playerTransform.position;
        bool isBehindPlayer = Vector3.Dot(_mainCam.transform.forward, playerToObject) < 0;
        
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.x > 0 + _targetSystem.HorizontalAimTreshold && screenPoint.x < 1 - _targetSystem.HorizontalAimTreshold && screenPoint.y > 0 + _targetSystem.VerticalAimTreshold && screenPoint.y < 1 -_targetSystem.VerticalAimTreshold ;
        

        if (Vector3.Distance(transform.position, _playerTransform.position) < _targetSystem.minReachDistance && !isBehindPlayer && !isReachable && !isActivated && onScreen)
        {
            isReachable = true;
            if (_targetSystem.visibleTargets.Contains(this))
            {
                _targetSystem.reachableTargets.Add(this);
            }
        }

        if ((Vector3.Distance(transform.position, _playerTransform.position) > _targetSystem.minReachDistance || isBehindPlayer || !onScreen) && isReachable )
        {
            isReachable = false;
            if (_targetSystem.reachableTargets.Contains(this))
            {
                _targetSystem.reachableTargets.Remove(this);
            }
        }

        //debug current target
        if (_targetSystem.currentTarget == this && !isActivated)
        {
            _renderer.material.color = Color.red;
        }
        else if (_targetSystem.currentTarget != this && !isActivated)
        {
            _renderer.material.color = Color.gray;
        }
    }

    private void OnBecameVisible()
    {
        if (!_targetSystem.visibleTargets.Contains(this))
        {
            _targetSystem.visibleTargets.Add(this);
            
            if(isReachable)
                _targetSystem.reachableTargets.Add(this);
        }
    }

    private void OnBecameInvisible()
    {
        if (_targetSystem.visibleTargets.Contains(this))
        {
            _targetSystem.visibleTargets.Remove(this);
            
            if (_targetSystem.reachableTargets.Contains(this))
            {
                _targetSystem.reachableTargets.Remove(this);
            }
        }
    }

    public void OnActivate()
    {
        isActivated = true;
        _renderer.material.color = Color.green;
        // if (_colorable != null)
        // {
        //     _colorable.SetObjectActive();
        // }
        if (_targetSystem.reachableTargets.Contains(this))
        {
            _targetSystem.reachableTargets.Remove(this);
        }
    }
}
