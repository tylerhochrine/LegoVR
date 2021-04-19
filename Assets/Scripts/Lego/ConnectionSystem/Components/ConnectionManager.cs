using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventRegisterProvider))]
[RequireComponent(typeof(EventNotifierProvider))]
public class ConnectionManager : MonoBehaviour
{
    private EventRegister eventRegister;
    private EventNotifier eventNotifier;

    // Array of LegoConnectors that belong to the lego GameObject
    private LegoConnector[] connectors;
    private LegoConnector lastConnector;

    // "cached" request to prevent from multip
    private ConnectionRequest lastConnectionRequest;


    void Start()
    {
        this.eventRegister = this.GetComponent<EventRegisterProvider>().GetEventRegister();
        this.eventNotifier = this.GetComponent<EventNotifierProvider>().GetEventNotifier();
        this.connectors = this.gameObject.GetComponentsInChildren<LegoConnector>();

        this.eventRegister.RegisterForEvent("onGrab", this.OnGrab);
        this.eventRegister.RegisterForEvent("onUngrab", this.OnUngrab);
        this.eventRegister.RegisterForEvent("whileGrabbed", this.WhileGrabbed);
    }

    public void OnGrab()
    {
        // FIXME: This will break after lego is part of a group of legos
        foreach (LegoConnector connector in connectors)
        {
            connector.ResetConnection();
        }
    }

    public void WhileGrabbed()
    {
        // casts rays
        List<ConnectionRequest> connectionRequests = ConnectionSeeker.generatePotentialConnectionRequests(connectors);

        ConnectionRequest newConnectionRequest = null;

        foreach (ConnectionRequest request in connectionRequests)
        {
            if (request.isValid())
            {
                newConnectionRequest = request;
                break;
            }
        }

        if (lastConnectionRequest != newConnectionRequest)
        {
            lastConnectionRequest = newConnectionRequest;
            if (newConnectionRequest == null)
            {
                this.eventNotifier.OnEvent("onNoPotentialConnectionFound");
            }
            else
            {
                this.eventNotifier.OnEvent("onPotentialConnectionFound");
            }
        }
    }

    public void OnUngrab()
    {
        if (lastConnectionRequest != null)
        {
            ConnectionActionResult result = RequestProcessor.processRequest(lastConnectionRequest);
            if (result.isSuccess()) eventNotifier.OnEvent("onSnap");
        }
    }

    public bool TryAcceptConnection(LegoConnector connector, LegoConnector other, ConnectionData connectionData, bool simulate = false)
    {
        if (connector.CanConnectTo(other))
        {
            if (!simulate) connector.SetConnectionData(connectionData);

            return true;
        }
        else return false;
    }

    public void ConnectionFailure(LegoConnector connector)
    {
        connector.ResetConnection();
    }

    public LegoConnector getCurrentSender()
    {
        if (lastConnectionRequest != null)
            return lastConnectionRequest.GetSender();
        else return null;
    }

    public LegoConnector getCurrentTarget()
    {
        if (lastConnectionRequest != null)
            return lastConnectionRequest.GetTarget();
        else return null;
    }
}