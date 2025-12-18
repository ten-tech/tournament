using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de gestion des sports et disciplines olympiques.
    /// </summary>
    public class SportService : ISportService
    {
        private List<Sport> _sports;
        private List<Discipline> _disciplines;
        private int _nextSportId;
        private int _nextDisciplineId;

        public SportService()
        {
            _sports = new List<Sport>();
            _disciplines = new List<Discipline>();
            _nextSportId = 1;
            _nextDisciplineId = 1;
        }

        public Sport CreateSport(string name, CompetitionType competitionType, string description = "")
        {
            var sport = new Sport(_nextSportId++, name, competitionType, description);
            _sports.Add(sport);
            Console.WriteLine($"✓ Sport créé: {sport.Name} ({competitionType})");
            return sport;
        }

        public Discipline CreateDiscipline(string name, int sportId, Gender gender, int maxParticipants = 100, bool hasMultipleRounds = false, string scoringMethod = "BestTime")
        {
            var discipline = new Discipline(_nextDisciplineId++, name, sportId, gender, maxParticipants, hasMultipleRounds, scoringMethod);
            _disciplines.Add(discipline);
            Console.WriteLine($"✓ Discipline créée: {discipline.Name} ({gender})");
            return discipline;
        }

        public void AddDisciplineToSport(Sport sport, Discipline discipline)
        {
            if (sport != null && discipline != null)
            {
                sport.AddDiscipline(discipline);
                Console.WriteLine($"✓ Discipline {discipline.Name} ajoutée au sport {sport.Name}");
            }
        }

        public List<Sport> GetAllSports()
        {
            return _sports;
        }

        public Sport GetSport(int id)
        {
            return _sports.Find(s => s.Id == id);
        }

        public Discipline GetDiscipline(int id)
        {
            return _disciplines.Find(d => d.Id == id);
        }

        public List<Discipline> GetDisciplinesBySport(int sportId)
        {
            return _disciplines.FindAll(d => d.SportId == sportId);
        }

        public bool ValidateAthlete(Athlete athlete, Discipline discipline)
        {
            // Vérifier le genre
            if (discipline.Gender != Gender.Mixed && athlete.Gender != discipline.Gender)
            {
                Console.WriteLine($"✗ L'athlète {athlete.GetFullName()} ne peut pas participer à cette discipline (genre incompatible)");
                return false;
            }

            // Vérifier que l'athlète pratique le sport
            if (!athlete.SportIds.Contains(discipline.SportId))
            {
                Console.WriteLine($"✗ L'athlète {athlete.GetFullName()} n'est pas inscrit dans ce sport");
                return false;
            }

            return true;
        }
    }
}
