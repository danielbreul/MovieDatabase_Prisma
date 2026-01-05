namespace MovieDatabaseFrontend.Services
{
    public class ErrorService : IErrorService
    {
        public event Action<string>? OnError;

        public void LogHttpResponse(HttpResponseMessage response, string? message = null)
        {
            message = message is not null ? $"Nachricht: {message} " : string.Empty;
            OnError?.Invoke($"{message}Unerwartete HTTP-Response. Statuscode: {response.StatusCode}({(int)response.StatusCode})");
        }

        public void LogMessage(string message)
        {
            OnError?.Invoke(message);
        }

        public void LogError(Exception ex)
        {
            OnError?.Invoke($"Unerwartete Exception: {ex.Message}; Type: {ex.GetType()}");
        }
    }
}
