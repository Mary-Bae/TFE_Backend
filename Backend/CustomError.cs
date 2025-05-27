using System;

namespace CustomErrors
{
    public enum ErreurCodeEnum
    {
        UK_CHAINE_NOM,
        UK_CINEMA_NOM,
        UK_SALLE_NUMBER,
        UK_FILM_NOM,
        UK_TRADUCTION,
        UK_PROGRAMMATION,
        UK_PROJECTION,
        FK_SALLE_CINEMA,
        FK_PROGRAMMATION_FILMTRADUIT,
        FK_SEANCE_PROGRAMMATION,
        FK_PROJECTION_SEANCE,
        FK_SALLE_PROJECTION,
        ErreurSQL,
        QuantiteMinimaleDePlaces,
        QuantiteMaxPlacesParRangee,
        ChampVide,
        NumeroInvalide,
        ChampsSelectionnes,
        DateSeance,
        ConflitProjection,
        AbonnementInvalide,
        QuantiteRestanteAbonnement,
        AbonnementVide,
        DateFinAbonnement,
        FK_Cine_Film_Programmation,
        FK_Projection_Reservation,
        UK_Programmation,
        UK_SEANCE,
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
                case ErreurCodeEnum.UK_CHAINE_NOM:
                    _messageToReturn = "La chaine de cinéma doit être unique ";
                    break;
                case ErreurCodeEnum.UK_CINEMA_NOM:
                    _messageToReturn = "Le cinéma doit être unique ";
                    break;
                case ErreurCodeEnum.UK_FILM_NOM:
                    _messageToReturn = "Il ne peut pas y avoir deux fois le même film à l'affiche ";
                    break;
                case ErreurCodeEnum.UK_SALLE_NUMBER:
                    _messageToReturn = "Il ne peut pas y avoir deux fois le même numero de salle pour un cinéma ";
                    break;
                case ErreurCodeEnum.UK_Programmation:
                    _messageToReturn = "Deux programmations ne peuvent pas être complètement identiques";
                    break;
                case ErreurCodeEnum.UK_PROJECTION:
                    _messageToReturn = "Deux projections identiques se passant à la même heure ne peuvent pas coexister dans la même salle de cinéma";
                    break;
                case ErreurCodeEnum.UK_TRADUCTION:
                    _messageToReturn = "La même traduction pour le même film ne peut pas exister deux fois";
                    break;
                case ErreurCodeEnum.UK_PROGRAMMATION:
                    _messageToReturn = "La même programmation pour le même film ne peut pas exister deux fois";
                    break;
                case ErreurCodeEnum.UK_SEANCE:
                    _messageToReturn = "La même séance ne peut pas être programée deux fois pour le même film avec la même traduction à la même heure";
                    break;
                case ErreurCodeEnum.FK_SALLE_CINEMA:
                    _messageToReturn = "Une salle de cinema doit appartenir à un cinema ";
                    break;
                case ErreurCodeEnum.FK_PROGRAMMATION_FILMTRADUIT:
                    _messageToReturn = "Une traduction de film ne peut pas être supprimée en ayant toujours des programmations actifs. Supprimez d'abord vos programmations";
                    break;
                case ErreurCodeEnum.FK_PROJECTION_SEANCE:
                    _messageToReturn = "Une séance ne peut pas être supprimée tant qu'il y a des projections en cours. Supprimez d'abord les projections de la séance sélectionnée avant de pouvoir supprimer la séance";
                    break;
                case ErreurCodeEnum.FK_SALLE_PROJECTION:
                    _messageToReturn = "Une salle de cinéma ne peut pas être supprimée tant qu'il y a des projections en cours. Supprimez d'abord les projections qui ont lieu dans cette salle avant de supprimer le cinéma et la salle en elle-même";
                    break;
                case ErreurCodeEnum.FK_Projection_Reservation:
                    _messageToReturn = "Vous ne pouvez pas supprimer des projections avec des réservations futures déjà réservées par des clients";
                    break;
                case ErreurCodeEnum.ErreurSQL:
                    _messageToReturn = "Erreur liée à la base de données SQL.";
                    break;
                case ErreurCodeEnum.ErreurGenerale:
                    _messageToReturn = "Une erreur générale s'est produite.";
                    break;
                case ErreurCodeEnum.QuantiteMinimaleDePlaces:
                    _messageToReturn = "Le nombre de places de cinéma n'est pas correct.";
                    break;
                case ErreurCodeEnum.QuantiteMaxPlacesParRangee:
                    _messageToReturn = "Le nombre de places par rangée ne doit pas dépasser 10";
                    break;
                case ErreurCodeEnum.NumeroInvalide:
                    _messageToReturn = "Veuillez rentrer un numero de salle valide.";
                    break;
                case ErreurCodeEnum.ChampVide:
                    _messageToReturn = "Les champs obligatoires ne peuvent pas être vide.";
                    break;
                case ErreurCodeEnum.ChampsSelectionnes:
                    _messageToReturn = "Tous les champs requis doivent être sélectionnés.";
                    break;
                case ErreurCodeEnum.DateSeance:
                    _messageToReturn = "La date de fin de la séance doit être d'au moins un mois après le début de la programmation.";
                    break;
                case ErreurCodeEnum.AbonnementInvalide:
                    _messageToReturn = "Abonnement invalide, vérifiez les données que vous avez rentrées";
                    break;
                case ErreurCodeEnum.AbonnementVide:
                    _messageToReturn = "Attention, votre abonnement est vide, vous devriez le recharger avant de valider votre réservation";
                    break;
                case ErreurCodeEnum.DateFinAbonnement:
                    _messageToReturn = "Votre réservation dépasse la fin de validité de votre abonnement, vous pouvez réserver à une autre date ou changer votre abonnement";
                    break;
                case ErreurCodeEnum.QuantiteRestanteAbonnement:
                    _messageToReturn = "Attention, il ne reste pas assez de places sur votre abonnement pour valider la réservation";
                    break;
                case ErreurCodeEnum.FK_Cine_Film_Programmation:
                    _messageToReturn = "Un cinema et un film doivent être sélectionnés pour permettre une programmation";
                    break;
                case ErreurCodeEnum.FK_SEANCE_PROGRAMMATION:
                    _messageToReturn = "Vous ne pouvez pas supprimer une programmation liée à une séance active, pour pouvoir supprimer la programmation, supprimez les séances associées avant.";
                    break;
                case ErreurCodeEnum.ConflitProjection:
                    _messageToReturn = "Conflit d'horaire et de periode pour une projection dans la même salle de cinéma. Choisissez un autre horaire/date de projection ou une autre salle";
                    break;
                default:
                    _messageToReturn = "Erreur non reconnue";
                    break;
            }
            return _messageToReturn;
        }
    }
}
