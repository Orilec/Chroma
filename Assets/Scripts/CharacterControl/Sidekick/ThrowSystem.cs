using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSystem : MonoBehaviour
{
    [SerializeField] private float _throwSidekickCooldown = 2f;
    
    [SerializeField] private ParticleSystem _correctTrajectoryEmission;
    [SerializeField] private ParticleSystem _untargetedTrajectoryEmission;
    [SerializeField] private ParticleSystem _retrieveTrajectoryEmission;
    [SerializeField] private Transform _sidekickThrowOrigin;
    [SerializeField] private SkinnedMeshRenderer _charaMeshRenderer;
    [SerializeField] private Material _sidekickMat;
    [SerializeField] private Material _transparentMat;
    [SerializeField] private Material _noaMat;

    private TargetManager _targetManager;
    private InputReader _input;
    
    private Target _lockedTarget, _lastActivatedTarget;
    private Transform _retrievePos;
    private bool _sidekickIsAvailable = true, _throwWasPressedLastFrame, _isRetrieving;
    
    public Transform RetrievePos { get { return _retrievePos; } set { _retrievePos = value; } }

    private void Awake()
    {
        _retrievePos = new GameObject().transform;
        _targetManager = ChroManager.GetManager<TargetManager>();
        _input = ChroManager.GetManager<InputReader>();
    }

    private void Update()
    {
        if (_input.ThrowIsPressed && _sidekickIsAvailable && _targetManager.currentTarget != null && !_throwWasPressedLastFrame)
        {
            ThrowSidekickOnTarget();
        }
        else if (_input.ThrowIsPressed && _sidekickIsAvailable && _targetManager.currentTarget == null && !_throwWasPressedLastFrame)
        {
            ThrowSidekickUntargeted();
        }
    }

    private void LateUpdate()
    {
        _throwWasPressedLastFrame = _input.ThrowIsPressed;
    }

    private void ThrowSidekickOnTarget()
    {
        _charaMeshRenderer.materials = new Material[2] {_noaMat, _transparentMat};
        _lockedTarget = _targetManager.currentTarget;
        _retrievePos.position = _lockedTarget.transform.position;
        _correctTrajectoryEmission.transform.position = _lockedTarget.transform.position;
        var shape = _correctTrajectoryEmission.shape;
        shape.position = _correctTrajectoryEmission.transform.InverseTransformPoint(_sidekickThrowOrigin.position);
        _correctTrajectoryEmission.Play();
        _sidekickIsAvailable = false;
    }

    private void ThrowSidekickUntargeted()
    {
        _charaMeshRenderer.materials = new Material[2] {_noaMat, _transparentMat};
        _sidekickIsAvailable = false;
        _untargetedTrajectoryEmission.Play();
    }

    public void RetrieveSidekick()
    {
        if (_retrievePos != null)
        {
            _isRetrieving = true;
            StartCoroutine(UpdateSidekickRetrievePoint());
            var shape = _retrieveTrajectoryEmission.shape;
            shape.position = _retrieveTrajectoryEmission.transform.InverseTransformPoint(_retrievePos.position);
            _retrieveTrajectoryEmission.Play();
        }
    }

    public void StopRetrieve()
    {
        _isRetrieving = false;
        StartCoroutine(SidekickCooldown());
        _charaMeshRenderer.materials = new Material[2] {_noaMat, _sidekickMat};
    }

    private IEnumerator SidekickCooldown()
    {
        yield return new WaitForSeconds(_throwSidekickCooldown);
        _sidekickIsAvailable = true;
    }

    private IEnumerator UpdateSidekickRetrievePoint()
    {
        while (_isRetrieving)
        {
            _retrieveTrajectoryEmission.transform.position = _sidekickThrowOrigin.position;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
