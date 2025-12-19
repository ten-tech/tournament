namespace TournamentManager.Models.Enums
{
    /// <summary>
    /// Définit les différents types de compétitions olympiques.
    /// </summary>
    public enum CompetitionType
    {
        /// <summary>
        /// Compétition chronométrée (ex: ski alpin, patinage de vitesse)
        /// </summary>
        Timed,

        /// <summary>
        /// Compétition par points/score avec jury (ex: patinage artistique, saut à ski)
        /// </summary>
        Scored,

        /// <summary>
        /// Compétition en face-à-face avec score (ex: hockey sur glace, curling)
        /// </summary>
        HeadToHead,

        /// <summary>
        /// Compétition par élimination directe (ex: snowboard cross, ski cross)
        /// </summary>
        Elimination,

        /// <summary>
        /// Compétition mixte combinant plusieurs formats
        /// </summary>
        Mixed
    }
}
