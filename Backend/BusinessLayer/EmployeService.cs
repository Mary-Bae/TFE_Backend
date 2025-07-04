﻿using Interfaces;
using Models;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using DataAccessLayer;
using CustomErrors;
using Microsoft.Data.SqlClient;
using System.Globalization;
using Azure;

namespace BusinessLayer
{
    public class EmployeService : IEmployeService
    {
        private readonly IEmployeRepo _employeRepo;
        private readonly IAuthService _authService;
        public EmployeService(IEmployeRepo employeRepo, IAuthService authService)
        {
            _employeRepo = employeRepo;
            _authService = authService;
        }
        public async Task<T?> GetMailManagerByUser<T>(string auth0Id)
        {
            try
            {
                return await _employeRepo.GetMailManagerByUser<T>(auth0Id);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[EMP01]"))
                    throw new CustomError(ErreurCodeEnum.SuperieurInexistant, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }

        }
        public async Task<T?> GetUserByAuth<T>(string auth0Id)
        {
            return await _employeRepo.GetUserByAuth<T>(auth0Id);
        }
        public async Task<T?> GetMailByDemande<T>(int demId)
        {
            try
            {
                return await _employeRepo.GetMailByDemande<T>(demId);
            }
            catch (SqlException ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }
        public async Task<List<T>> GetUsers<T>()
        {
            return await _employeRepo.GetUsers<T>();
        }
        public async Task<List<T>> GetManagers<T>()
        {
            return await _employeRepo.GetManagers<T>();
        }
        public async Task<List<T>> GetDeletedUsers<T>()
        {
            return await _employeRepo.GetDeletedUsers<T>();
        }

        private string RemoveAccents(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                    .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            );
        }
        public async Task<EmployeDTO> CreateUser(EmployeDTO employe)
        {
            //Récupération du token de mon API Management dans Auth0 (connecté à Machine-To-Machine)
            var bearerToken = await _authService.GetManagementApiToken();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var nomSansAccent = RemoveAccents(employe.EMP_Nom);
            var prenomSansAccent = RemoveAccents(employe.EMP_Prenom);

            employe.EMP_Email = (nomSansAccent + "." + prenomSansAccent + "@gmail.com").ToLower();
            if (!string.IsNullOrEmpty(employe.EMP_Sexe))
            {
                employe.EMP_Sexe = employe.EMP_Sexe.Trim().Substring(0, 1).ToUpper();

                if (employe.EMP_Sexe != "F" && employe.EMP_Sexe != "M")
                {
                    throw new CustomError(ErreurCodeEnum.SexeInvalide);
                }
            }

            //Envoi de la requete pour enregistrer le nouvel user dans Auth0
            var payload = new
            {
                name = employe.EMP_Prenom + " " +  employe.EMP_Nom,
                email = employe.EMP_Email,
                password = "Unoeuf1978",
                connection = "Username-Password-Authentication",
                given_name = employe.EMP_Prenom,
                family_name = employe.EMP_Nom,
                email_verified = false,
                verify_email = false
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UserAuthDTO>(json);

            //Récupération de l'identifiant Auth0 de l'user crée
            employe.EMP_Auth = result?.UserId;

            //Rajout du rôle dans Auth0 selon ce qu'on a sélectionné
            string roleAuth0 = "";
            switch (employe.EMP_ROL_id)
            {
                case 1:
                    roleAuth0 = "rol_ToUSfkJn71LRxDQi"; // Identifiant du rôle Auth0 pour "Employé"
                    break;
                case 2:
                    roleAuth0 = "rol_5YZuuMwk7lJC9EWh"; // Identifiant du rôle Auth0 pour "Manager"
                    break;
                case 3:
                    roleAuth0 = "rol_ZJgssQPaESZIlVNX"; // Identifiant du rôle Auth0 pour "Admin"
                    break;
                case 4:
                    roleAuth0 = "rol_VfXb1ZcRCmZbJG9c"; // Identifiant du rôle Auth0 pour "CEO"
                    break;
                default:
                    throw new CustomError(ErreurCodeEnum.RoleInconnu);
            }
            var assignRoleAuth0 = new
                {
                    roles = new[] { roleAuth0 }
                };

                var assignRoleContent = new StringContent(JsonSerializer.Serialize(assignRoleAuth0), Encoding.UTF8, "application/json");

                var assignRoleResponse = await client.PostAsync(
                    $"https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users/{employe.EMP_Auth}/roles",
                    assignRoleContent
                );
                assignRoleResponse.EnsureSuccessStatusCode();
            
            // Appel du repo pour enregistrer l'employé
            await _employeRepo.CreateUser(employe);

            return employe;
        }

        public async Task<EmployeDTO> UpdateEmploye(int pId, EmployeDTO employe)
        {
            //Récupération du token de mon API Management dans Auth0 (connecté à Machine-To-Machine)
            var bearerToken = await _authService.GetManagementApiToken();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var nomSansAccent = RemoveAccents(employe.EMP_Nom);
            var prenomSansAccent = RemoveAccents(employe.EMP_Prenom);

            employe.EMP_Email = (nomSansAccent + "." + prenomSansAccent + "@gmail.com").ToLower();
            if (!string.IsNullOrEmpty(employe.EMP_Sexe))
            {
                employe.EMP_Sexe = employe.EMP_Sexe.Trim().Substring(0, 1).ToUpper();

                if (employe.EMP_Sexe != "F" && employe.EMP_Sexe != "M")
                {
                    throw new CustomError(ErreurCodeEnum.SexeInvalide);
                }
            }

            //Envoi de la requete pour faire un update de l'user dans Auth0
            var payload = new
            {
                name = employe.EMP_Prenom + " " + employe.EMP_Nom,
                email = employe.EMP_Email,
                given_name = employe.EMP_Prenom,
                family_name = employe.EMP_Nom
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync($"https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users/{employe.EMP_Auth}", content);
            response.EnsureSuccessStatusCode();

            //Modification du rôle dans Auth0 selon ce qu'on a sélectionné
            string roleAuth0 = "";
            switch (employe.EMP_ROL_id)
            {
                case 1:
                    roleAuth0 = "rol_ToUSfkJn71LRxDQi"; // Identifiant du rôle Auth0 pour "Employé"
                    break;
                case 2:
                    roleAuth0 = "rol_5YZuuMwk7lJC9EWh"; // Identifiant du rôle Auth0 pour "Manager"
                    break;
                case 3:
                    roleAuth0 = "rol_ZJgssQPaESZIlVNX"; // Identifiant du rôle Auth0 pour "Admin"
                    break;
                case 4:
                    roleAuth0 = "rol_VfXb1ZcRCmZbJG9c"; // Identifiant du rôle Auth0 pour "CEO"
                    break;
                default:
                    throw new CustomError(ErreurCodeEnum.RoleInconnu);
            }
            var assignRoleAuth0 = new
            {
                roles = new[] { roleAuth0 }
            };

            var assignRoleContent = new StringContent(JsonSerializer.Serialize(assignRoleAuth0), Encoding.UTF8, "application/json");

            var assignRoleResponse = await client.PostAsync($"https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users/{employe.EMP_Auth}/roles", assignRoleContent);
            assignRoleResponse.EnsureSuccessStatusCode();

            // Appel du repo pour modifier l'employé
            await _employeRepo.UpdateEmploye(pId, employe);

            return employe;
        }
        public async Task<T?> GetEmployeById<T>(int employeId)
        {
            return await _employeRepo.GetEmployeById<T>(employeId);
        }
        public async Task DeleteEmploye(int pId, int? modifiedBy)
        {
            try
            {
                // Récupérer l'utilisateur en DB pour obtenir son ID Auth0
                var employe = await _employeRepo.GetEmployeById<EmployeDTO>(pId);
                if (!string.IsNullOrEmpty(employe?.EMP_Auth))
                {
                    // Bloquer l'utilisateur dans Auth0 (au lieu de le supprimer)
                    var bearerToken = await _authService.GetManagementApiToken();
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

                    var payload = new { blocked = true };
                    var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync(
                        $"https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users/{employe.EMP_Auth}",
                        content
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomError(ErreurCodeEnum.SuppressionAuth0Echouée);
                    }
                }

                // Supprimer dans SQL
                await _employeRepo.DeleteEmploye(pId, modifiedBy);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[EMP02]"))
                    throw new CustomError(ErreurCodeEnum.SuppressionEchouée, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }
        }

        public async Task RestoreEmploye(int pId, int? modifiedBy)
        {
            try
            {
                var employe = await _employeRepo.GetEmployeById<EmployeDTO>(pId);
                if (!string.IsNullOrEmpty(employe?.EMP_Auth))
                {
                    var bearerToken = await _authService.GetManagementApiToken();
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer " + bearerToken);

                    var payload = new { blocked = false };
                    var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                    var response = await client.PatchAsync(
                        $"https://dev-r0omvlrob02srgfr.us.auth0.com/api/v2/users/{employe.EMP_Auth}",
                        content
                    );
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomError(ErreurCodeEnum.RestaurationAuth0Echouée);
                    }
                }

                await _employeRepo.RestoreEmploye(pId, modifiedBy);
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("[EMP02]"))
                    throw new CustomError(ErreurCodeEnum.RestaurationEchouée, ex);
                throw new CustomError(ErreurCodeEnum.ErreurSQL, ex);
            }
            catch (Exception ex)
            {
                throw new CustomError(ErreurCodeEnum.ErreurGenerale, ex);
            }

        }
    }
}
