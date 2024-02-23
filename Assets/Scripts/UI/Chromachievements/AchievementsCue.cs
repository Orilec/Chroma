using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsCue : MonoBehaviour
{
    [SerializeField] private ChromachievemEventsPublisher _chromachievemEvents;

    private Animator _cueAnimator;
    private Image _backgroundImage;

    private void Awake()
    {
        _cueAnimator = GetComponentInChildren<Animator>();
        _backgroundImage = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        StartCoroutine(Fade());
    }
    
    private IEnumerator Fade()
    {
        _backgroundImage.canvasRenderer.SetAlpha(0);
        _backgroundImage.CrossFadeAlpha(1f, 5f, true);
        yield break;
    }
    
}
