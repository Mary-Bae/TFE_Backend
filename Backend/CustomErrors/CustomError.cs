using System;

namespace CustomErrors
{
    public enum ErreurCodeEnum
    {
        JoursEnTrop,
        DemandesExistantes,
        DemandesPassé,
        DatesSimilaires,
        SoldeInexistant,
        HeuresRestant,
        ModifierDemEnAttente,
        SuperieurInexistant,
        SuppressionEchouée,
        SuppressionAuth0Echouée,
        SexeInvalide,
        RoleInconnu,
        TypeJournee,
        HeuresHebdo,
        ErreurSQL,
        ErreurGenerale
    }
    public class CustomError : Exception
    {
        int _codeError;

        public CustomError(ErreurCodeEnum pCodeError) : base(SetBaseMessage(pCodeError))
        {
            _codeError = (int)pCodeError;
        }
        public CustomError(ErreurCodeEnum pCodeError, Exception inner) : base(SetBaseMessage(pCodeError), inner)
        {
            _codeError = (int)pCodeError;
        }
        public int CodeError
        { get { return _codeError; } }

        private static string SetBaseMessage(ErreurCodeEnum pCodeError)
        {
            string _messageToReturn;

            switch (pCodeError)
            {
                case ErreurCodeEnum.JoursEnTrop:
                    _messageToReturn = "Il n'est pas possible de d'avoir plus de 5 jours de télétravail par semaine.";
                    break;
                case ErreurCodeEnum.DemandesExistantes:
                    _messageToReturn = "Il n'est pas possible de supprimer un type absence où des demandes existent pour cet employé durant cette année.";
                    break;
                case ErreurCodeEnum.DemandesPassé:
                    _messageToReturn = "Vous ne pouvez pas faire une demande de congé pour un jour dans le passé.";
                    break;
                case ErreurCodeEnum.DatesSimilaires:
                    _messageToReturn = "Il y a déjà une demande de congé pour ces dates.";
                    break;
                case ErreurCodeEnum.SoldeInexistant:
                    _messageToReturn = "Solde non trouvé.";
                    break;
                case ErreurCodeEnum.HeuresRestant:
                    _messageToReturn = "Il ne vous reste pas assez de congés dans votre compteur pour cette demande.";
                    break;
                case ErreurCodeEnum.ModifierDemEnAttente:
                    _messageToReturn = "Impossible de modifier ou supprimer une demande qui n'est plus en attente.";
                    break;
                case ErreurCodeEnum.SuperieurInexistant:
                    _messageToReturn = "Aucun supérieur trouvé pour cet employé.";
                    break;
                case ErreurCodeEnum.SuppressionEchouée:
                    _messageToReturn = "La suppression de l''employé a échoué. Vérifiez les enregistrements liés (demandes, absences ou contrats).";
                    break;
                case ErreurCodeEnum.SuppressionAuth0Echouée:
                    _messageToReturn = "La suppression de l''employé a échoué. Vérifiez l'interface Auth0.";
                    break;
                case ErreurCodeEnum.SexeInvalide:
                    _messageToReturn = "Sexe invalide : doit être F (Féminin) ou M (Masculin).";
                    break;
                case ErreurCodeEnum.RoleInconnu:
                    _messageToReturn = "Le rôle sélectionné n'existe pas dans Auth0. Veuillez vérifier dans l'interface Auth0";
                    break;
                case ErreurCodeEnum.TypeJournee:
                    _messageToReturn = "Type de journée non valide.";
                    break;
                case ErreurCodeEnum.HeuresHebdo:
                    _messageToReturn = "Vous avez dépassé votre quota hebdomadaire autorisé.";
                    break;
                case ErreurCodeEnum.ErreurSQL:
                    _messageToReturn = "Erreur liée à la base de données SQL.";
                    break;
                case ErreurCodeEnum.ErreurGenerale:
                    _messageToReturn = "Une erreur générale s'est produite.";
                    break;
                default:
                    _messageToReturn = "Erreur non reconnue";
                    break;
            }
            return _messageToReturn;
        }
    }
}
