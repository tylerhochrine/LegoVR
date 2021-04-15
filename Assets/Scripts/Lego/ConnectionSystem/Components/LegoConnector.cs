using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LegoConnector : MonoBehaviour
{
    // indicates the side of the lego that the connector is located
    public Side side;
    // indicates the type of connector
    public ConnectorType type;
    // specifies the max distance that this connector will seek another connector to snap to
    public double maxSnapDistance = 1;
    // flag to indicate if the connector is currentlyAcceptingConnections
    private bool currentlyAcceptingConnections = true;
    // reference to the connectors connection data
    private ConnectionData connectionData;
    // a reference to the BoxCollider of the Connector
    private BoxCollider connectorCollider;

    void Start()
    {
        // the side and type should never be equal to NONE
        if (side == Side.NONE || type == ConnectorType.NONE) throw new System.Exception("Incomplete/Invalid configuration for LegoConnector");
        connectorCollider = GetComponent<BoxCollider>();
        this.connectionData = ConnectionData.EMPTY;
    }

    public Ray GetRay()
    {
        return new Ray(connectorCollider.bounds.center, GetConnectorDirectionVector3());
    }

    public Vector3 GetConnectorDirectionVector3()
    {
        switch (this.side)
        {
            case Side.TOP:
                return connectorCollider.transform.up;
            case Side.FRONT:
                return connectorCollider.transform.forward;
            case Side.RIGHT:
                return connectorCollider.transform.right;
            case Side.BOTTOM:
                return connectorCollider.transform.up * -1;
            case Side.BACK:
                return connectorCollider.transform.forward * -1;
            case Side.LEFT:
                return connectorCollider.transform.right * -1;
            default:
                return Vector3.zero;
        }
    }

    public bool CanConnectTo(LegoConnector other)
    {
        if (other == null || !IsAcceptingConnections()) return false;

        return this.type.CanConnectTo(other.type);
    }

    public bool CanConnectTo(string otherTag)
    {
        if (!IsAcceptingConnections()) return false;

        return this.type.CanConnectTo(otherTag);
    }

    public bool IsAcceptingConnections()
    {
        return currentlyAcceptingConnections && connectionData == ConnectionData.EMPTY;
    }

    public void SetConnectionData(ConnectionData connectionData)
    {
        this.connectionData = connectionData;
    }

    public void ResetConnection()
    {
        if (this.connectionData != ConnectionData.EMPTY)
            this.connectionData.unlink();
    }

    public Vector3 GetConnectionPosition(LegoConnector incoming)
    {
        switch (this.type)
        {
            case ConnectorType.STUD:
                Debug.Log(this.transform.position);
                Debug.Log(incoming.transform.position);
                return this.transform.position - (this.transform.rotation * new Vector3(incoming.transform.localPosition.x, 0, incoming.transform.localPosition.z));
            case ConnectorType.STUD_RECEPTACLE:
                return this.transform.position - incoming.transform.localPosition;  // - (this.transform.rotation * incoming.transform.localPosition);
            default:
                return Vector3.zero;
        }
    }

    public Quaternion GetConnectionRotation(LegoConnector incoming)
    {
        return Quaternion.Euler(this.transform.eulerAngles.x, incoming.transform.rotation.eulerAngles.y, this.transform.rotation.z);
    }

    public float GetConnectionYRotationDifference(LegoConnector incoming)
    {
        float yRotationDifference = ((transform.rotation.eulerAngles.y - incoming.transform.rotation.eulerAngles.y) % 90);
        return yRotationDifference < 45 ? yRotationDifference : yRotationDifference - 90;
    }
}