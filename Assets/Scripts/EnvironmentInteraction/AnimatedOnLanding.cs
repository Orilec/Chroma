using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedOnLanding : MonoBehaviour
{
    [SerializeField] private PlayerEventsPublisher _playerEventsPublisher;
    [SerializeField] private AK.Wwise.Event _bounceNoise;
    [SerializeField] private Animator _animator;
    protected PlayerController _player;
    private bool _bounce = true;
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
            if (_bounce)
            {
                _bounceNoise.Post(this.gameObject);
                StartCoroutine(ResetBounceNoise());
            }
            StartCoroutine(ResetAfterAFrame());
        }
    }

    private IEnumerator ResetAfterAFrame()
    {
        yield return new WaitForEndOfFrame();
        _animator.SetBool("isUsed", false);
    }

    private IEnumerator ResetBounceNoise()
    {
        _bounce = false;
        yield return new WaitForSeconds(0.2f);
        _bounce = true;
    }
}
