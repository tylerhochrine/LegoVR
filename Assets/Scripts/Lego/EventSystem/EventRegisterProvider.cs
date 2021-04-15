using UnityEngine;

public class EventRegisterProvider : MonoBehaviour
{
    [SerializeField]
    private EventRegister eventRegister;

    public void SetEventRegister(EventRegister eventRegister)
    {
        this.eventRegister = eventRegister;
    }

    public EventRegister GetEventRegister()
    {
        return this.eventRegister;
    }
}