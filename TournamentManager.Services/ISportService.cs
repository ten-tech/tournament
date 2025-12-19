using System.Collections.Generic;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de gestion des sports et disciplines.
    /// </summary>
    public interface ISportService
    {
        Sport CreateSport(string name, CompetitionType competitionType, string description = "");
        Discipline CreateDiscipline(string name, int sportId, Gender gender, int maxParticipants = 100, bool hasMultipleRounds = false, string scoringMethod = "BestTime");
        void AddDisciplineToSport(Sport sport, Discipline discipline);
        List<Sport> GetAllSports();
        Sport GetSport(int id);
        Discipline GetDiscipline(int id);
        List<Discipline> GetDisciplinesBySport(int sportId);
        bool ValidateAthlete(Athlete athlete, Discipline discipline);
    }
}
