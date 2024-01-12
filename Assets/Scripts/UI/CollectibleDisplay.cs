using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDisplay : MonoBehaviour
{
    [SerializeField] private List<CardSlot> _cardSlots;
    [SerializeField] private PlayerEventsPublisher _playerEvents;

    private void Awake()
    {
        //_playerEvents.CardCollected.AddListener(DisplayCard);
    }

    private void DisplayCard(int index)
    {
        var card = _cardSlots[index].transform.GetChild(0);
        if(card != null) card.gameObject.SetActive(true);
    }
}
