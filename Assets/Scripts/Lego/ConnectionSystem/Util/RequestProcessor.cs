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
            request.GetTarget().SetConnectionTransform(request.GetSender().transform, request.GetSender().GetComponentInParent<Lego>().transform);
            request.GetSender().UpdateGroup(request.GetTarget());
            return new ConnectionActionResult(connectionData);
        }
        else if (targetSuccess) connectionData.getTargetConnectionManager().ConnectionFailure(request.GetTarget());
        else if (senderSuccess) connectionData.getSenderConnectionManager().ConnectionFailure(request.GetSender());
        return ConnectionActionResult.FAILURE;
    }
}