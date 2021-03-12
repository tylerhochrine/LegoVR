using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LegoConnector : MonoBehaviour
{
    // indicates the side of the lego that the connector is located
    public Side side;
    // indicates the type of connector
    public ConnectorType type;

    // a reference to the BoxCollider of the Connector
    private BoxCollider connectorCollider;

    // holds a reference to the GameObject that the lego is currently connected to
    private GameObject connectedObject;

    void Start()
    {
        // the side and type should never be equal to NONE
        if (side == Side.NONE || type == ConnectorType.NONE) throw new System.Exception("Incomplete/Invalid configuration for LegoConnector");
        connectorCollider = GetComponent<BoxCollider>();
    }
}