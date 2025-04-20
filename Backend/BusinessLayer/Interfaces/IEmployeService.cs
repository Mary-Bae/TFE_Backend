namespace Interfaces
{
    public interface IEmployeService
    {
        Task<T?> GetMailManagerByUser<T>(string auth0Id);
        Task<T?> GetMailByDemande<T>(int demId);
    }
}