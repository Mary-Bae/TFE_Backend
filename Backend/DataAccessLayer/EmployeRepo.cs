using Dapper;
using System.Data;
using Interfaces;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Models;

namespace DataAccessLayer
{
    public class EmployeRepo : IEmployeRepo
    {
        private readonly IDbConnection _connection;
        private readonly IDbConnection _connectManager;
        private readonly IDbConnection _connectAdmin;

        public EmployeRepo(IDbChoixConnRepo connection)
        {
            _connection = connection.CreateConnection("ConnectDb");
            _connectManager = connection.CreateConnection("ConnectDbManager");
            _connectAdmin = connection.CreateConnection("ConnectDbAdmin");
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

               var eMail =  await _connection.QueryAsync<T>("[shUser].[SelectMailManager]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<int> GetManagerId(string auth0Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Auth0Id", auth0Id);
            var id = await _connection.QueryAsync<int>("[shUser].[SelectIdEmploye]", parameters, commandType: CommandType.StoredProcedure);
            return id.FirstOrDefault();
        }
        public async Task<T?> GetMailByDemande<T>(int demId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DemId", demId);

                var eMail = await _connectManager.QueryAsync<T>("[shManager].[SelectMailEmploye]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task<List<T>> GetUsers<T>()
        {
            try
            {
                var users = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectUsers]", commandType: CommandType.StoredProcedure);
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
        }
        public async Task CreateUser(EmployeDTO employe)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nom", employe.EMP_Nom);
                parameters.Add("@Prenom", employe.EMP_Prenom);
                parameters.Add("@Pren2", employe.EMP_Pren2);
                parameters.Add("@Sexe", employe.EMP_Sexe);
                parameters.Add("@Email", employe.EMP_Email);
                parameters.Add("@Auth0Id", employe.EMP_Auth);
                parameters.Add("@RoleId", employe.EMP_ROL_id);
                parameters.Add("@ManagerId", employe.EMP_Manager_id == 0 ? null : employe.EMP_Manager_id);

                await _connectAdmin.ExecuteAsync("[shAdmin].[CreateUser]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new DBConcurrencyException("Erreur: ", ex);
            }
            
        }
    }
}
