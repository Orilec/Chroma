using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private UIEventsPublisher _uiEventsPublisher;
    [SerializeField] private PlayerEventsPublisher _playerEventsPublisher;
    [SerializeField] private float respawnFadeSpeed = 5f; 
    [SerializeField] private float respawnBlackScreenTime = 2f; 
    [SerializeField] private float timeBeforeBlackScreen = 2f; 
    private Image _blackImage;
    private float _fadeAmount;
    private void Awake()
    {
        _blackImage = GetComponent<Image>();
        _playerEventsPublisher.Respawn.AddListener(StartRespawnFade);
    }

    private IEnumerator FadeImage(float fadeSpeed, bool toBlack = true)
    {
        Color objectColor = _blackImage.color;
        float fadeAmount;
        if (toBlack)
        {
            while (_blackImage.color.a < 1.1f)
            {
                fadeAmount = _blackImage.color.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                _blackImage.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (_blackImage.color.a > 0)
            {
                fadeAmount = _blackImage.color.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                _blackImage.color = objectColor;
                yield return null;
            }
        }
    }

    private IEnumerator RespawnFadeToBlack(float fadeSpeed, float blackTime)
    {
        yield return new WaitForSeconds(timeBeforeBlackScreen);
        
        bool finished = false;
        bool firstStarted = false;
        bool secondStarted = false;

        while (!finished)
        {
            if (_blackImage.color.a < 1 && !firstStarted)
            {
                firstStarted = true;
                StartCoroutine(FadeImage(fadeSpeed));
            }

            if (firstStarted && _blackImage.color.a >= 1f && !secondStarted)
            {
                _uiEventsPublisher.FirstFadeFinished.Invoke();
                yield return new WaitForSeconds(blackTime);
                secondStarted = true;
                StartCoroutine(FadeImage(fadeSpeed, false));
            }

            if (firstStarted && secondStarted && _blackImage.color.a <= 0f)
            {
                finished = true;
                _uiEventsPublisher.FadeToBlackFinished.Invoke();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void StartRespawnFade()
    {
        StartCoroutine(RespawnFadeToBlack(respawnFadeSpeed, respawnBlackScreenTime));
    }
    
}
