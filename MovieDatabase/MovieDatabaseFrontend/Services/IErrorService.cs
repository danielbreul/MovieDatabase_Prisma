namespace MovieDatabaseFrontend.Services
{
    public interface IErrorService
    {
        public event Action<string>? OnError;
        public void LogHttpResponse(HttpResponseMessage response, string? message = null);
        public void LogMessage(string message);
        public void LogError(Exception ex);
    }
}