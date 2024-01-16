using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostcardManager : MonoBehaviour
{
    [SerializeField] private List<Postcard> _postcards;
    [SerializeField] private Postcard _postcardPrefab;
    [SerializeField] private Transform _postcardContainer;
    [SerializeField] private PlayerEventsPublisher _playerEvents;
    [SerializeField] private float _additionalRotation;

    private float _currentRotation;
    public List<Postcard> Postcards { get { return _postcards; } }

    private void Awake()
    {
        _playerEvents.CardCollected.AddListener(AddNewPostCard);
    }

    private void AddNewPostCard(Sprite sprite)
    {
        var card = Instantiate(_postcardPrefab, _postcardContainer);
        _currentRotation += _additionalRotation;
        card.InitCard(sprite, _currentRotation);
    }
}
