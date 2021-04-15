using System;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    Dictionary<string, List<Action>> eventSubscribers;


    public EventController()
    {
        eventSubscribers = new Dictionary<string, List<Action>>();
    }

    public void RegisterEvent(string eventName)
    {
        if (!eventSubscribers.ContainsKey(eventName)) eventSubscribers.Add(eventName, new List<Action>());
    }

    public bool RegisterForEvent(string eventName, Action callback)
    {
        if (eventSubscribers.ContainsKey(eventName))
        {
            eventSubscribers[eventName].Add(callback);
            return true;
        }
        else return false;
    }

    public void OnEvent(string eventName)
    {
        if (eventName == "onPotentialConnectionFound") Debug.Log("Potential Connection Found");

        if (eventSubscribers.ContainsKey(eventName))
            foreach (Action callback in eventSubscribers[eventName])
            {
                callback();
            }
    }
}