using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager
{
    GameObject legoObject;
    LegoConnector[] connectors;

    public ConnectionManager(GameObject gameObject)
    {
        legoObject = gameObject;
        connectors = legoObject.GetComponentsInChildren<LegoConnector>();
    }

    public List<Side> getSupportedSides()
    {
        List<Side> sides = new List<Side>();
        foreach (LegoConnector connector in connectors)
            sides.Insert(0, connector.side);
        return sides;
    }
}