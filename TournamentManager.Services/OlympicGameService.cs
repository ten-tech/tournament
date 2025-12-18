using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de gestion des Jeux Olympiques d'hiver.
    /// </summary>
    public class OlympicGameService : IOlympicGameService
    {
        private List<WinterOlympics> _olympics;

        public OlympicGameService()
        {
            _olympics = new List<WinterOlympics>();
        }

        public void CreateWinterOlympics(string name, string hostCity, string hostCountry, DateTime startDate, DateTime endDate, string motto = "")
        {
            int id = _olympics.Count + 1;
            var olympics = new WinterOlympics(id, name, hostCity, hostCountry, startDate, endDate, motto);
            _olympics.Add(olympics);
            Console.WriteLine($"✓ Jeux Olympiques créés: {olympics}");
        }

        public void AddSport(int olympicsId, Sport sport)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics != null)
            {
                olympics.AddSport(sport);
                Console.WriteLine($"✓ Sport ajouté: {sport.Name}");
            }
            else
            {
                Console.WriteLine($"✗ Jeux Olympiques avec l'ID {olympicsId} non trouvés.");
            }
        }

        public void AddCountry(int olympicsId, NationalTeam country)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics != null)
            {
                olympics.AddCountry(country);
                Console.WriteLine($"✓ Pays ajouté: {country.CountryName} ({country.CountryCode})");
            }
            else
            {
                Console.WriteLine($"✗ Jeux Olympiques avec l'ID {olympicsId} non trouvés.");
            }
        }

        public void AddEvent(int olympicsId, Event olympicEvent)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics != null)
            {
                olympics.AddEvent(olympicEvent);
                Console.WriteLine($"✓ Événement ajouté: {olympicEvent.Name}");
            }
            else
            {
                Console.WriteLine($"✗ Jeux Olympiques avec l'ID {olympicsId} non trouvés.");
            }
        }

        public WinterOlympics GetWinterOlympics(int id)
        {
            return _olympics.Find(o => o.Id == id);
        }

        public List<WinterOlympics> GetAllWinterOlympics()
        {
            return _olympics;
        }

        public List<Event> GetEventsByDate(int olympicsId, DateTime date)
        {
            var olympics = GetWinterOlympics(olympicsId);
            return olympics?.GetEventsByDate(date) ?? new List<Event>();
        }

        public MedalStandings GetMedalStandings(int olympicsId)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics == null) return new MedalStandings();

            var standings = new MedalStandings();
            foreach (var country in olympics.ParticipatingCountries)
            {
                country.RecalculateMedals();
                standings.UpdateCountryMedals(country);
            }

            return standings;
        }

        public void DisplayOlympicsInfo(int olympicsId)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics == null)
            {
                Console.WriteLine($"✗ Jeux Olympiques avec l'ID {olympicsId} non trouvés.");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {olympics.Name,-56} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Ville hôte: {olympics.HostCity}, {olympics.HostCountry}");
            Console.WriteLine($"Dates: {olympics.StartDate.ToShortDateString()} - {olympics.EndDate.ToShortDateString()}");
            if (!string.IsNullOrEmpty(olympics.Motto))
                Console.WriteLine($"Devise: \"{olympics.Motto}\"");
            Console.WriteLine($"\nNombre de sports: {olympics.Sports.Count}");
            Console.WriteLine($"Pays participants: {olympics.ParticipatingCountries.Count}");
            Console.WriteLine($"Événements programmés: {olympics.Events.Count}");
        }

        public Athlete FindAthlete(int olympicsId, string firstName, string lastName)
        {
            var olympics = GetWinterOlympics(olympicsId);
            if (olympics == null) return null;

            foreach (var country in olympics.ParticipatingCountries)
            {
                var athlete = country.Athletes.Find(a =>
                    a.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    a.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
                if (athlete != null) return athlete;
            }

            return null;
        }

        public NationalTeam FindCountry(int olympicsId, string countryCode)
        {
            var olympics = GetWinterOlympics(olympicsId);
            return olympics?.ParticipatingCountries.Find(c =>
                c.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}
