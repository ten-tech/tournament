using TournamentManager.Models;
using TournamentManager.Services;

namespace TournamentManager.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Injection de dépendances
            ITournamentService tournamentService = new TournamentService();

            // Création de joueurs et d'équipes
            var player1 = new Player(1, "Alice", 25, "Attaquant");
            var player2 = new Player(2, "Bob", 28, "Défenseur");
            var team1 = new Team(1, "Équipe A");
            team1.AddPlayer(player1);
            var team2 = new Team(2, "Équipe B");
            team2.AddPlayer(player2);

            // Création d'un match
            var match = new Match(1, team1, team2);
            match.UpdateScore(3, 1);

            // Création d'un tournoi et ajout du match
            tournamentService.CreateTournament("Tournament 2025");
            tournamentService.AddMatchToTournament(1, match);

            // Affichage des tournois
            tournamentService.DisplayAllTournaments();
        }
    }
}
