public enum ConnectorType
{
    NONE,
    // a ConnectorType of STUD represents a stud connector which can connect to stud_receptacle
    STUD,
    // a connectorType of STUD_RECEPTACLE represents a stud receptacle which can connect to a stud
    STUD_RECEPTACLE
}

public static class ConnectorTypeMethods
{
    public static string toString(this ConnectorType connectorType)
    {
        switch (connectorType)
        {
            case ConnectorType.STUD:
                return "Stud";
            case ConnectorType.STUD_RECEPTACLE:
                return "Stud Receptacle";
            default:
                return "undefined";
        }
    }

    public static string GetTag(this ConnectorType connectorType)
    {
        switch (connectorType)
        {
            case ConnectorType.STUD:
                return "Stud";
            case ConnectorType.STUD_RECEPTACLE:
                return "StudReceptacle";
            default:
                return "null";
        }
    }

    public static bool CanConnectTo(this ConnectorType thisType, ConnectorType connectorType)
    {
        switch (thisType)
        {
            case ConnectorType.STUD:
                return connectorType == ConnectorType.STUD_RECEPTACLE;
            case ConnectorType.STUD_RECEPTACLE:
                return connectorType == ConnectorType.STUD;
            default:
                return false;
        }
    }

    public static bool CanConnectTo(this ConnectorType connectorType, string tag)
    {
        switch (connectorType)
        {
            case ConnectorType.STUD:
                return tag == ConnectorType.STUD_RECEPTACLE.GetTag();
            case ConnectorType.STUD_RECEPTACLE:
                return tag == ConnectorType.STUD.GetTag();
            default:
                return false;
        }
    }
}