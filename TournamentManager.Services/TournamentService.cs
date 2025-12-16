using System;
using System.Collections.Generic;
using TournamentManager.Models;

namespace TournamentManager.Services
{
    /// <summary>
    /// Fournit des services pour gérer les tournois, les équipes et les matchs.
    /// Implémente l'interface ITournamentService.
    /// </summary>
    public class TournamentService : ITournamentService
    {
        private List<Tournament> _tournaments;

        public TournamentService()
        {
            _tournaments = new List<Tournament>();
        }

        /// <summary>
        /// Crée un nouveau tournoi.
        /// </summary>
        public void CreateTournament(string name)
        {
            int tournamentId = _tournaments.Count + 1;
            var newTournament = new Tournament(tournamentId, name);
            _tournaments.Add(newTournament);
            Console.WriteLine($"Tournament créé : {name}");
        }

        /// <summary>
        /// Ajoute un match à un tournoi spécifique.
        /// </summary>
        public void AddMatchToTournament(int tournamentId, Match match)
        {
            var tournament = _tournaments.Find(t => t.Id == tournamentId);
            if (tournament != null)
            {
                tournament.AddMatch(match);
                Console.WriteLine($"Match ajouté au tournoi {tournament.Name}.");
            }
            else
            {
                Console.WriteLine($"Tournament avec l'ID {tournamentId} non trouvé.");
            }
        }

        /// <summary>
        /// Affiche tous les tournois et leurs matchs.
        /// </summary>
        public void DisplayAllTournaments()
        {
            foreach (var tournament in _tournaments)
            {
                Console.WriteLine($"\nTournament: {tournament.Name}");
                tournament.DisplayMatches();
            }
        }
    }
}
