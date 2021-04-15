public static class RequestProcessor
{
    public static bool isRequestValid(ConnectionRequest request)
    {
        return request.isValid();
    }

    public static ConnectionActionResult processRequest(ConnectionRequest request)
    {
        ConnectionData connectionData = new ConnectionData(request.GetSender(), request.GetTarget());
        bool targetSuccess = connectionData.getTargetConnectionManager().TryAcceptConnection(request.GetTarget(), request.GetSender(), connectionData);
        bool senderSuccess = connectionData.getSenderConnectionManager().TryAcceptConnection(request.GetSender(), request.GetTarget(), connectionData);

        if (targetSuccess && senderSuccess)
        {
            request.GetSender().transform.root.transform.position = request.GetTarget().GetConnectionPosition(request.GetSender());
            request.GetSender().transform.root.transform.rotation = request.GetTarget().GetConnectionRotation(request.GetSender());
            // request.GetSender().transform.RotateAround(, request.GetTarget().transform.up, request.GetTarget().GetConnectionYRotationDifference(request.GetSender()));

            return new ConnectionActionResult(connectionData);
        }
        else if (targetSuccess) connectionData.getTargetConnectionManager().ConnectionFailure(request.GetTarget());
        else if (senderSuccess) connectionData.getSenderConnectionManager().ConnectionFailure(request.GetSender());
        return ConnectionActionResult.FAILURE;
    }
}