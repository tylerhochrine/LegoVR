using UnityEngine;
using System.Collections.Generic;

public static class ConnectionSeeker
{
    // TODO: Generify by moving layer name to a serializable field.
    private static LayerMask validConnectionLayer = LayerMask.GetMask(new[] { "Lego" });

    public static List<ConnectionRequest> generatePotentialConnectionRequests(LegoConnector[] connectors)
    {
        SortedList<float, ConnectionRequest> sortedConnectionRequests = new SortedList<float, ConnectionRequest>();
        RaycastHit hit;

        for (int i = 0; i < connectors.Length; i++)
        {
            LegoConnector current = connectors[i];
            Debug.DrawRay(current.GetRay().origin, current.GetRay().direction, Color.red);
            bool collision = Physics.Raycast(current.GetRay().origin, current.GetRay().direction, out hit, (float)current.maxSnapDistance, validConnectionLayer);

            if (collision)
            {
                if (current.CanConnectTo(hit.collider.gameObject.tag))
                    sortedConnectionRequests.Add(hit.distance, new ConnectionRequest(current, hit.collider.gameObject.GetComponent<LegoConnector>()));
                
            }
        }

        List<ConnectionRequest> connectionRequests = new List<ConnectionRequest>(sortedConnectionRequests.Values);

        return connectionRequests;
    }
}