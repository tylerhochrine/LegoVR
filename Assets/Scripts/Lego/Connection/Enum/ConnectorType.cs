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

    public static string getColliderLayer(this ConnectorType connectorType)
    {
        switch (connectorType)
        {
            case ConnectorType.STUD:
                return "Stud";
            case ConnectorType.STUD_RECEPTACLE:
                return "StudReceptacle";
            default:
                return "undefined";
        }
    }
}