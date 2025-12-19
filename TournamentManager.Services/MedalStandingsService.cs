using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de gestion et d'affichage du tableau des médailles olympiques.
    /// </summary>
    public class MedalStandingsService : IMedalStandingsService
    {
        public void UpdateStandings(WinterOlympics olympics)
        {
            if (olympics == null)
            {
                Console.WriteLine("✗ Jeux Olympiques invalides");
                return;
            }

            // Recalculer les médailles de chaque pays
            foreach (var country in olympics.ParticipatingCountries)
            {
                country.RecalculateMedals();
            }

            Console.WriteLine($"✓ Tableau des médailles mis à jour pour {olympics.Name}");
        }

        public MedalStandings GetCurrentStandings(WinterOlympics olympics)
        {
            if (olympics == null)
            {
                return new MedalStandings();
            }

            UpdateStandings(olympics);

            var standings = new MedalStandings();
            foreach (var country in olympics.ParticipatingCountries)
            {
                standings.UpdateCountryMedals(country);
            }

            return standings;
        }

        public CountryMedalCount GetCountryStanding(WinterOlympics olympics, string countryCode)
        {
            if (olympics == null)
            {
                return null;
            }

            var country = olympics.ParticipatingCountries.Find(c =>
                c.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase));

            if (country == null)
            {
                Console.WriteLine($"✗ Pays {countryCode} non trouvé");
                return null;
            }

            country.RecalculateMedals();

            return new CountryMedalCount
            {
                CountryName = country.CountryName,
                CountryCode = country.CountryCode,
                FlagEmoji = country.FlagEmoji,
                Gold = country.GoldMedals,
                Silver = country.SilverMedals,
                Bronze = country.BronzeMedals
            };
        }

        public List<CountryMedalCount> GetTopCountries(WinterOlympics olympics, int count = 10)
        {
            var standings = GetCurrentStandings(olympics);
            return standings.GetRanking().Take(count).ToList();
        }

        public void DisplayStandings(WinterOlympics olympics)
        {
            if (olympics == null)
            {
                Console.WriteLine("✗ Jeux Olympiques invalides");
                return;
            }

            var standings = GetCurrentStandings(olympics);
            standings.Display();
        }

        public Dictionary<string, int> GetMedalsBySport(WinterOlympics olympics, string countryCode)
        {
            var medalsBySport = new Dictionary<string, int>();

            if (olympics == null)
            {
                return medalsBySport;
            }

            var country = olympics.ParticipatingCountries.Find(c =>
                c.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase));

            if (country == null)
            {
                return medalsBySport;
            }

            // Pour chaque athlète du pays
            foreach (var athlete in country.Athletes)
            {
                // Pour chaque résultat avec médaille
                foreach (var result in athlete.Results.Where(r => r.Medal != MedalType.None))
                {
                    // Trouver l'événement correspondant
                    var olympicEvent = olympics.Events.Find(e => e.Id == result.EventId);
                    if (olympicEvent != null)
                    {
                        // Trouver la discipline et le sport
                        foreach (var sport in olympics.Sports)
                        {
                            var discipline = sport.Disciplines.Find(d => d.Id == olympicEvent.DisciplineId);
                            if (discipline != null)
                            {
                                if (!medalsBySport.ContainsKey(sport.Name))
                                {
                                    medalsBySport[sport.Name] = 0;
                                }
                                medalsBySport[sport.Name]++;
                                break;
                            }
                        }
                    }
                }
            }

            return medalsBySport;
        }
    }
}
