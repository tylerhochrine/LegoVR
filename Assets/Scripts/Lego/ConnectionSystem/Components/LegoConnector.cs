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
        switch (type)
        {
            case ConnectorType.STUD:
                // Vector3 offset = (incoming.transform.rotation * new Vector3(incoming.transform.parent.transform.localPosition.x, 0, incoming.transform.parent.transform.localPosition.z));
                Vector3 result = transform.position;
                // Vector3 offset = incoming.transform.rotation * incoming.transform.parent.transform.localPosition;
                // Vector3 offset = Quaternion.Euler(0, incoming.transform.rotation.y, 0) * incoming.transform.parent.transform.localPosition;// new Vector3(incoming.transform.parent.transform.localPosition.x, 0, incoming.transform.parent.transform.localPosition.z);
                Vector3 offset = incoming.transform.rotation * incoming.transform.parent.transform.localPosition;
                result -= offset;
                // result = incoming.transform.rotation * result;

                return result;
            case ConnectorType.STUD_RECEPTACLE:
                return Vector3.zero;
                // return this.transform.position - this.transform.localPosition;  // - (this.transform.rotation * incoming.transform.localPosition);
            default:
                return Vector3.zero;
        }
    }

    public Quaternion GetConnectionRotation(LegoConnector incoming)
    {
        float yRotationOffset = ((transform.rotation.eulerAngles.y - incoming.transform.rotation.eulerAngles.y) % 90);
        yRotationOffset = (yRotationOffset < 45 ? yRotationOffset : yRotationOffset - 90);
        return Quaternion.Euler(transform.eulerAngles.x, incoming.transform.rotation.eulerAngles.y + yRotationOffset, transform.eulerAngles.z);
    }

    private float GetConnectionYRotation(Transform referenceTransform)
    {
        float yRotationOffset = ((transform.rotation.eulerAngles.y - referenceTransform.rotation.eulerAngles.y) % 90);
        return referenceTransform.rotation.eulerAngles.y + ( (  (0 < yRotationOffset && yRotationOffset < 45) ? yRotationOffset : ((0 < yRotationOffset) ? (yRotationOffset - 90) : (90 +  yRotationOffset))  ) );
    }

    public void SetConnectionTransform(Transform reference, Transform target)
    {
        switch (type)
        {
            case ConnectorType.STUD:
                target.position = transform.position;
                target.rotation = Quaternion.Euler(transform.eulerAngles.x, GetConnectionYRotation(reference), transform.rotation.eulerAngles.z);
                target.position -= target.rotation * reference.parent.transform.localPosition;
                break;
            case ConnectorType.STUD_RECEPTACLE:
                break;
        }
    }
}