namespace MovieDatabaseFrontend.Services
{
    public class ErrorService : IErrorService
    {
        public event Action<string>? OnError;

        public void LogHttpResponse(HttpResponseMessage response)
        {
            OnError?.Invoke("Unerwartete HTTP-Response. Statuscode: " + response.StatusCode + "\nInhalt: " + response.RequestMessage);
        }

        public void LogError(Exception ex)
        {
            OnError?.Invoke("Unerwartete Exception: " + ex.Message);
        }
    }
}
