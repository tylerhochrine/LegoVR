using UnityEngine;

public class EventNotifierProvider : MonoBehaviour
{
    [SerializeField]
    private EventNotifier eventNotifier;

    public void SetEventNotifier(EventNotifier eventNotifier)
    {
        this.eventNotifier = eventNotifier;
    }

    public EventNotifier GetEventNotifier()
    {
        return this.eventNotifier;
    }
}