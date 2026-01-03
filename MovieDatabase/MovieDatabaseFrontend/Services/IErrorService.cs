namespace MovieDatabaseFrontend.Services
{
    public interface IErrorService
    {
        public event Action<string>? OnError;
        public void LogHttpResponse(HttpResponseMessage response);
        public void LogError(Exception ex);
    }
}
