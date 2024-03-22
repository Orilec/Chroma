using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedOnLanding : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEventsPublisher;
    [SerializeField] private Animator _animator;
    protected PlayerController _player;
    private void Start()
    {
        _player = ChroManager.GetManager<PlayerManager>().GetPlayer();
        _playerEventsPublisher.EnteringGround.AddListener(StartAnim);
    }

    private void StartAnim(Transform objectUnderFeet)
    {
        if (transform == objectUnderFeet)
        {
            _animator.SetBool("isUsed", true);
            StartCoroutine(ResetAfterAFrame());
        }
    }

    private IEnumerator ResetAfterAFrame()
    {
        yield return new WaitForEndOfFrame();
        _animator.SetBool("isUsed", false);
    }
}
