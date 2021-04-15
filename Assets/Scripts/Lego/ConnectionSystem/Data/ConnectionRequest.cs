public class ConnectionRequest
{
    private LegoConnector origin;
    private LegoConnector target;

    public ConnectionRequest(LegoConnector sender, LegoConnector target)
    {
        this.origin = sender;
        this.target = target;
    }

    public bool isValid()
    {
        return target != null && target.CanConnectTo(origin);
    }

    public LegoConnector GetSender()
    {
        return origin;
    }

    public LegoConnector GetTarget()
    {
        return target;
    }

    public static bool operator ==(ConnectionRequest a, ConnectionRequest b)
    {
        if (object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
        {
            return object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null);
        }
        return a.origin == b.origin && a.target == b.target;
    }

    public static bool operator !=(ConnectionRequest a, ConnectionRequest b)
    {
        return !(a == b);
    }

    public override bool Equals(object o)
    {
        if (this.GetType() == o.GetType())
            return this == (ConnectionRequest)o;
        else return false;
    }

    public override int GetHashCode()
    {
        int hash = 3049;
        hash = hash * 5039 + origin.GetHashCode();
        hash = hash * 883 + target.GetHashCode();
        return hash;
    }
}