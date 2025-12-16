using TournamentManager.Models;

namespace TournamentManager.Services
{
    public interface ITournamentService
    {
        void CreateTournament(string name);
        void AddMatchToTournament(int tournamentId, Match match);
        void DisplayAllTournaments();
    }
}
