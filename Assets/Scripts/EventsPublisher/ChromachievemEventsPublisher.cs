using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "EventsPublisher/ChromachievemEventsPublisher")]
public class ChromachievemEventsPublisher : ScriptableObject
{
    public UnityEvent<Chromachievement> ChromachievementUnlocked;

}
