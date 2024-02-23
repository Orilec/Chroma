using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsCue : MonoBehaviour
{
    [SerializeField] private ChromachievemEventsPublisher _chromachievemEvents;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _icon;

    private List<MaskableGraphic> _uiElements;
    private GameObject _container;
    

    private void Awake()
    {
        _chromachievemEvents.ChromachievementUnlocked.AddListener(SendCue);

        _container = transform.GetChild(0).gameObject;
        
        _uiElements = new List<MaskableGraphic>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            var element = child.GetComponent<MaskableGraphic>();
            if(element != null) _uiElements.Add(element);
        }
        
        foreach (var element in _uiElements)
        {
            element.canvasRenderer.SetAlpha(0f);
        }
    }

    private void SendCue(Chromachievement chromachievement)
    {
        _title.text = chromachievement.Name;
        _description.text = chromachievement.DescriptionText;
        _icon.sprite = chromachievement.Icon;
        StartCoroutine(Fade());
    }

    
    private IEnumerator Fade()
    {
        foreach (var element in _uiElements)
        {
            element.CrossFadeAlpha(1f, 2f, true);
        }
        
        yield return new WaitForSecondsRealtime(5f);
        
        foreach (var element in _uiElements)
        {
            element.CrossFadeAlpha(0f, 1f, true);
        }
    }
    
}
