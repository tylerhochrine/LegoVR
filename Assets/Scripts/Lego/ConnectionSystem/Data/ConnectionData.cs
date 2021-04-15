using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionData
{
    // the GameObject that initiated the connection
    private LegoConnector origin;
    // the GameObject that accepted the connection
    private LegoConnector target;

    private Lego originLegoComponent;
    private Lego targetLegoComponent;

    private ConnectionManager originConnectionManager;
    private ConnectionManager targetConnectionManager;

    // variable to represent if the connection is locked, if it is locked, the connection cannot be unlinked
    // private bool locked = false;

    public static ConnectionData EMPTY = new ConnectionData(null, null);

    public ConnectionData(LegoConnector sender, LegoConnector target)
    {
        this.origin = sender;
        this.target = target;
    }

    public bool unlink()
    {
        // INFO: return true when unlinking is successful, false otherwise.
        // TODO: write code to be executed when two objects are disconnected
        // return !locked;
        origin.SetConnectionData(ConnectionData.EMPTY);
        target.SetConnectionData(ConnectionData.EMPTY);
        return true;
    }

    // public void lockConnection()
    // {
    //     locked = true;
    // }

    // public void unlockConnection()
    // {
    //     locked = false;
    // }

    public LegoConnector getSender()
    {
        return origin;
    }

    public LegoConnector getTarget()
    {
        return target;
    }

    public ConnectionManager getSenderConnectionManager()
    {
        if (originConnectionManager == null) this.setOriginVariables();
        return originConnectionManager;
    }

    public ConnectionManager getTargetConnectionManager()
    {
        if (targetConnectionManager == null) this.setTargetVariables();
        return targetConnectionManager;
    }

    public bool isSender(ConnectionManager manager)
    {
        if (originConnectionManager == null) this.setOriginVariables();
        return manager == originConnectionManager;
    }

    public bool isTarget(ConnectionManager manager)
    {
        if (targetConnectionManager == null) this.setTargetVariables();
        return manager == targetConnectionManager;
    }

    private void setTargetVariables()
    {
        if (target != null)
            targetLegoComponent = target.GetComponentInParent<Lego>();
        if (targetLegoComponent != null)
            targetConnectionManager = targetLegoComponent.GetConnectionManager();
    }

    private void setOriginVariables()
    {
        if (origin != null)
            originLegoComponent = origin.GetComponentInParent<Lego>();
        if (originLegoComponent != null)
            originConnectionManager = originLegoComponent.GetConnectionManager();
    }
}
