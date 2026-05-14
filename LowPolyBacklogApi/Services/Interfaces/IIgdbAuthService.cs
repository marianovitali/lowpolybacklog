namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IIgdbAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}
