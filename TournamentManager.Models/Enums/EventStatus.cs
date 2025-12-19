namespace TournamentManager.Models.Enums
{
    /// <summary>
    /// Définit les statuts possibles d'un événement de compétition.
    /// </summary>
    public enum EventStatus
    {
        /// <summary>
        /// Événement programmé mais pas encore commencé
        /// </summary>
        Scheduled,

        /// <summary>
        /// Événement en cours
        /// </summary>
        InProgress,

        /// <summary>
        /// Événement terminé avec résultats finaux
        /// </summary>
        Completed,

        /// <summary>
        /// Événement annulé (météo, raisons techniques, etc.)
        /// </summary>
        Cancelled,

        /// <summary>
        /// Événement reporté à une date ultérieure
        /// </summary>
        Postponed
    }
}
