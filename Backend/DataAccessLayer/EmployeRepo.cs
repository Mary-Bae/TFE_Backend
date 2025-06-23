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
                var parameters = new DynamicParameters();
                parameters.Add("@Auth0Id", auth0Id);

               var eMail =  await _connection.QueryAsync<T>("[shUser].[SelectMailManager]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
        }
        public async Task<T?> GetUserByAuth<T>(string auth0Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Auth0Id", auth0Id);

            var user = await _connection.QueryAsync<T>("[shUser].[SelectUserByAuth]", parameters, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
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
                var parameters = new DynamicParameters();
                parameters.Add("@DemId", demId);

                var eMail = await _connectManager.QueryAsync<T>("[shManager].[SelectMailEmploye]", parameters, commandType: CommandType.StoredProcedure);
                return eMail.FirstOrDefault();
           
        }
        public async Task<List<T>> GetUsers<T>()
        {
            var users = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectUsers]", commandType: CommandType.StoredProcedure);
            return users.ToList();
        }
        public async Task<List<T>> GetDeletedUsers<T>()
        {
            var users = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectDeletedUsers]", commandType: CommandType.StoredProcedure);
            return users.ToList();
        }
        public async Task<List<T>> GetManagers<T>()
        {
            var managers = await _connectAdmin.QueryAsync<T>("[shAdmin].[SelectManagers]", commandType: CommandType.StoredProcedure);
            return managers.ToList();
        }
        public async Task CreateUser(EmployeDTO employe)
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
            parameters.Add("@ModifiedBy", employe.EMP_ModifiedBy);


            parameters.Add("@TypeContrat", employe.CON_Type);
            parameters.Add("@JoursSemaine", employe.CON_JoursSemaine);
            parameters.Add("@Description", employe.CON_Description);
            parameters.Add("@DateDebut", employe.CON_DteDebut);
            parameters.Add("@DateFin", employe.CON_DteFin);

        await _connectAdmin.ExecuteAsync("[shAdmin].[CreateUser]", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task UpdateEmploye(int pId, EmployeDTO employe)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EMP_id", pId);
            parameters.Add("@EMP_Nom", employe.EMP_Nom);
            parameters.Add("@EMP_Prenom", employe.EMP_Prenom);
            parameters.Add("@EMP_Email", employe.EMP_Email);
            parameters.Add("@EMP_ROL_id", employe.EMP_ROL_id);
            parameters.Add("@EMP_Pren2", employe.EMP_Pren2);
            parameters.Add("@EMP_Sexe", employe.EMP_Sexe);
            parameters.Add("@EMP_Manager_id", employe.EMP_Manager_id);
            parameters.Add("@ModifiedBy", employe.EMP_ModifiedBy);

            parameters.Add("@TypeContrat", employe.CON_Type);
            parameters.Add("@JoursSemaine", employe.CON_JoursSemaine);
            parameters.Add("@Description", employe.CON_Description);
            parameters.Add("@DateDebut", employe.CON_DteDebut);
            parameters.Add("@DateFin", employe.CON_DteFin);

            await _connectAdmin.ExecuteAsync("[shAdmin].[UpdateEmploye]", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<T?> GetEmployeById<T>(int employeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EMP_id", employeId);

            var employe = await _connectAdmin.QuerySingleOrDefaultAsync<T>("[shAdmin].[SelectEmployeById]", parameters, commandType: CommandType.StoredProcedure);
            return employe;
        }
        public async Task DeleteEmploye(int pId, int? modifiedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EMP_id", pId);
            parameters.Add("@EMP_ModifiedBy", modifiedBy);

            await _connectAdmin.ExecuteAsync("[shAdmin].[DeleteEmploye]", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task RestoreEmploye(int pId, int? modifiedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EMP_id", pId);
            parameters.Add("@EMP_ModifiedBy", modifiedBy);

            await _connectAdmin.ExecuteAsync("[shAdmin].[RestoreEmploye]", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
