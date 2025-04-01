namespace Interfaces
{
    public interface IManagerService
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
    }
}