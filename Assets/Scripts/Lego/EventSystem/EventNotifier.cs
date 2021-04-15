using System;

public class EventNotifier
{
    private EventController eventController;

    public EventNotifier(EventController eventController)
    {
        this.eventController = eventController;
    }

    public void OnEvent(string eventName)
    {
        this.eventController.OnEvent(eventName);
    }
}