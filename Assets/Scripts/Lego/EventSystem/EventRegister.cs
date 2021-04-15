using System;

public class EventRegister
{
    private EventController eventController;

    public EventRegister(EventController eventController)
    {
        this.eventController = eventController;
    }

    public bool RegisterForEvent(string eventName, Action callback)
    {
        return this.eventController.RegisterForEvent(eventName, callback);
    }
}