namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un match entre deux équipes dans un tournoi.
    /// </summary>
    public class Match
    {
        // Propriétés de base d'un match
        public int Id { get; set; }             // Identifiant unique du match
        public Team Team1 { get; set; }         // Première équipe
        public Team Team2 { get; set; }         // Deuxième équipe
        public int Team1Score { get; set; }     // Score de la première équipe
        public int Team2Score { get; set; }     // Score de la deuxième équipe

        // Constructeur pour initialiser un match avec ses informations de base
        public Match(int id, Team team1, Team team2)
        {
            Id = id;
            Team1 = team1;
            Team2 = team2;
            Team1Score = 0;
            Team2Score = 0;
        }

        // Méthode pour mettre à jour le score du match
        public void UpdateScore(int team1Score, int team2Score)
        {
            Team1Score = team1Score;
            Team2Score = team2Score;
        }

        public string GetWinner()
        {
            if (Team1Score > Team2Score)
                return Team1.Name;
            else if (Team2Score > Team1Score)
                return Team2.Name;
            else
                return "Match nul";
        }
    }
}
