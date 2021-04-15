public class ConnectionActionResult
{
    public static ConnectionActionResult FAILURE = new ConnectionActionResult(false, ConnectionData.EMPTY);

    private ConnectionData result;
    private bool success;


    public ConnectionActionResult(ConnectionData connectionData) : this(true, connectionData) { }

    private ConnectionActionResult(bool success, ConnectionData connectionData)
    {
        this.success = success;
        this.result = connectionData;
    }

    public bool isSuccess()
    {
        return success;
    }

    public ConnectionData getResult()
    {
        return result;
    }
}