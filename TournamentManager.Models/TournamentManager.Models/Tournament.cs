using System.Collections.Generic;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un tournoi composé de plusieurs matchs.
    /// </summary>
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Match> Matches { get; set; }

        public Tournament(int id, string name)
        {
            Id = id;
            Name = name;
            Matches = new List<Match>();
        }

        /// <summary>
        /// Ajoute un match au tournoi.
        /// </summary>
        public void AddMatch(Match match)
        {
            Matches.Add(match);
        }

        /// <summary>
        /// Affiche tous les matchs du tournoi.
        /// </summary>
        public void DisplayMatches()
        {
            foreach (var match in Matches)
            {
                Console.WriteLine($"Match {match.Id}: {match.Team1.Name} {match.Team1Score} - {match.Team2Score} {match.Team2.Name} (Gagnant: {match.GetWinner()})");
            }
        }
    }
}
