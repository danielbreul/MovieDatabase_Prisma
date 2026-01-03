namespace MovieDatabaseFrontend.Services
{
    public class ErrorService : IErrorService
    {
        public event Action<string>? OnError;

        public void LogHttpResponse(HttpResponseMessage response, string? message = null)
        {
            if (message is not null)
            {
                message = "Nachricht: " + message + "\n";
            }
            OnError?.Invoke("Unerwartete HTTP-Response.\n" + message + "Statuscode: " + response.StatusCode + "\nInhalt: " + response.RequestMessage);
        }

        public void LogMessage(string message)
        {
            OnError?.Invoke(message);
        }

        public void LogError(Exception ex)
        {
            OnError?.Invoke("Unerwartete Exception: " + ex.Message + "type: " + ex.GetType());
        }
    }
}
