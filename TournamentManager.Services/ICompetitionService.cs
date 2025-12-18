using System;
using System.Collections.Generic;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de gestion des compétitions.
    /// </summary>
    public interface ICompetitionService
    {
        Event CreateEvent(string name, int disciplineId, DateTime date, TimeSpan time, Venue venue, string phase = "Finale", int roundNumber = 1);
        void RegisterParticipant(Event olympicEvent, Athlete athlete);
        Result RecordTimedPerformance(Event olympicEvent, Athlete athlete, TimeSpan time, double penaltySeconds = 0);
        Result RecordScoredPerformance(Event olympicEvent, Athlete athlete, List<double> judgeScores, double penaltyPoints = 0);
        void DisqualifyAthlete(Result result, string reason);
        void FinalizeEvent(Event olympicEvent, Discipline discipline);
        void DisplayEventResults(Event olympicEvent);
        List<Event> GetAllEvents();
        Event GetEvent(int id);
    }
}
