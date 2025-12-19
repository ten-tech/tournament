using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de gestion des compétitions olympiques.
    /// </summary>
    public class CompetitionService : ICompetitionService
    {
        private List<Event> _events;
        private List<Result> _results;
        private IScoringService _scoringService;
        private int _nextEventId;
        private int _nextResultId;

        public CompetitionService(IScoringService scoringService)
        {
            _events = new List<Event>();
            _results = new List<Result>();
            _scoringService = scoringService;
            _nextEventId = 1;
            _nextResultId = 1;
        }

        public Event CreateEvent(string name, int disciplineId, DateTime date, TimeSpan time, Venue venue, string phase = "Finale", int roundNumber = 1)
        {
            var olympicEvent = new Event(_nextEventId++, name, disciplineId, date, time, venue, phase, roundNumber);
            _events.Add(olympicEvent);
            Console.WriteLine($"✓ Événement créé: {olympicEvent.Name} - {date.ToShortDateString()} à {time}");
            return olympicEvent;
        }

        public void RegisterParticipant(Event olympicEvent, Athlete athlete)
        {
            if (olympicEvent == null || athlete == null)
            {
                Console.WriteLine("✗ Événement ou athlète invalide");
                return;
            }

            olympicEvent.AddParticipant(athlete);
            Console.WriteLine($"✓ {athlete.GetFullName()} inscrit à {olympicEvent.Name}");
        }

        public Result RecordTimedPerformance(Event olympicEvent, Athlete athlete, TimeSpan time, double penaltySeconds = 0)
        {
            var result = new Result(_nextResultId++, olympicEvent.Id, athlete.Id, athlete);
            result.SetTime(time, penaltySeconds);

            olympicEvent.AddResult(result);
            _results.Add(result);

            Console.WriteLine($"✓ Performance enregistrée: {athlete.GetFullName()} - {result.GetFinalPerformance()}");
            return result;
        }

        public Result RecordScoredPerformance(Event olympicEvent, Athlete athlete, List<double> judgeScores, double penaltyPoints = 0)
        {
            var result = new Result(_nextResultId++, olympicEvent.Id, athlete.Id, athlete);

            // Calculer le score final avec le ScoringService
            double finalScore = _scoringService.CalculateJudgedScore(judgeScores, removeExtremes: true);
            result.SetScore(finalScore, penaltyPoints);

            olympicEvent.AddResult(result);
            _results.Add(result);

            Console.WriteLine($"✓ Performance enregistrée: {athlete.GetFullName()} - {result.GetFinalPerformance()}");
            return result;
        }

        public void DisqualifyAthlete(Result result, string reason)
        {
            if (result == null)
            {
                Console.WriteLine("✗ Résultat invalide");
                return;
            }

            result.Disqualify(reason);
            Console.WriteLine($"✗ {result.Athlete.GetFullName()} disqualifié(e): {reason}");
        }

        public void FinalizeEvent(Event olympicEvent, Discipline discipline)
        {
            if (olympicEvent == null || discipline == null)
            {
                Console.WriteLine("✗ Événement ou discipline invalide");
                return;
            }

            if (olympicEvent.Results.Count == 0)
            {
                Console.WriteLine("✗ Aucun résultat à finaliser");
                return;
            }

            Console.WriteLine($"\n━━━ Finalisation de {olympicEvent.Name} ━━━");

            // Classer les résultats
            var rankedResults = _scoringService.RankResults(olympicEvent.Results, discipline.ScoringMethod);

            // Attribuer les médailles
            _scoringService.AssignMedals(rankedResults);

            // Mettre à jour les résultats des athlètes
            foreach (var result in rankedResults.Where(r => !r.IsDisqualified))
            {
                result.Athlete.AddResult(result);
            }

            // Marquer l'événement comme terminé
            olympicEvent.UpdateStatus(EventStatus.Completed);

            Console.WriteLine($"✓ Événement finalisé: {olympicEvent.Name}");
        }

        public void DisplayEventResults(Event olympicEvent)
        {
            if (olympicEvent == null)
            {
                Console.WriteLine("✗ Événement invalide");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {olympicEvent.Name,-56} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Date: {olympicEvent.Date.ToShortDateString()} à {olympicEvent.Time}");
            Console.WriteLine($"Lieu: {olympicEvent.Venue.Name}");
            Console.WriteLine($"Phase: {olympicEvent.Phase} - Manche {olympicEvent.RoundNumber}");
            Console.WriteLine($"Statut: {olympicEvent.Status}");
            Console.WriteLine($"\nParticipants: {olympicEvent.GetParticipantCount()}");

            if (olympicEvent.Results.Count > 0)
            {
                Console.WriteLine("\n--- RÉSULTATS ---");
                var sortedResults = olympicEvent.Results
                    .Where(r => !r.IsDisqualified)
                    .OrderBy(r => r.Rank)
                    .ToList();

                foreach (var result in sortedResults)
                {
                    Console.WriteLine(result.ToString());
                }

                var disqualified = olympicEvent.Results.Where(r => r.IsDisqualified).ToList();
                if (disqualified.Count > 0)
                {
                    Console.WriteLine("\n--- DISQUALIFICATIONS ---");
                    foreach (var result in disqualified)
                    {
                        Console.WriteLine($"DSQ - {result.Athlete.GetFullName()} ({result.Athlete.CountryCode}): {result.DisqualificationReason}");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nAucun résultat enregistré pour le moment.");
            }
        }

        public List<Event> GetAllEvents()
        {
            return _events;
        }

        public Event GetEvent(int id)
        {
            return _events.Find(e => e.Id == id);
        }
    }
}
