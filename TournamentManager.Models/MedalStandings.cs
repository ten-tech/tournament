using System.Collections.Generic;
using System.Linq;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente le tableau des médailles olympiques.
    /// </summary>
    public class MedalStandings
    {
        public List<CountryMedalCount> Countries { get; set; }

        public MedalStandings()
        {
            Countries = new List<CountryMedalCount>();
        }

        /// <summary>
        /// Ajoute ou met à jour les médailles d'un pays.
        /// </summary>
        public void UpdateCountryMedals(NationalTeam team)
        {
            var existing = Countries.Find(c => c.CountryCode == team.CountryCode);
            if (existing != null)
            {
                existing.Gold = team.GoldMedals;
                existing.Silver = team.SilverMedals;
                existing.Bronze = team.BronzeMedals;
            }
            else
            {
                Countries.Add(new CountryMedalCount
                {
                    CountryName = team.CountryName,
                    CountryCode = team.CountryCode,
                    FlagEmoji = team.FlagEmoji,
                    Gold = team.GoldMedals,
                    Silver = team.SilverMedals,
                    Bronze = team.BronzeMedals
                });
            }
        }

        /// <summary>
        /// Obtient le classement trié par nombre de médailles d'or, puis argent, puis bronze.
        /// </summary>
        public List<CountryMedalCount> GetRanking()
        {
            return Countries
                .OrderByDescending(c => c.Gold)
                .ThenByDescending(c => c.Silver)
                .ThenByDescending(c => c.Bronze)
                .ThenBy(c => c.CountryName)
                .ToList();
        }

        /// <summary>
        /// Obtient le nombre total de médailles distribuées.
        /// </summary>
        public int GetTotalMedalsDistributed()
        {
            return Countries.Sum(c => c.GetTotal());
        }

        /// <summary>
        /// Affiche le tableau des médailles.
        /// </summary>
        public void Display()
        {
            var ranking = GetRanking();
            System.Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            System.Console.WriteLine("║              TABLEAU DES MÉDAILLES OLYMPIQUES              ║");
            System.Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            System.Console.WriteLine("\n{0,-5} {1,-25} {2,6} {3,6} {4,6} {5,6}", "Rang", "Pays", "Or", "Argent", "Bronze", "Total");
            System.Console.WriteLine(new string('-', 65));

            int rank = 1;
            foreach (var country in ranking)
            {
                System.Console.WriteLine("{0,-5} {1} {2,-22} {3,6} {4,6} {5,6} {6,6}",
                    rank,
                    country.FlagEmoji,
                    country.CountryName,
                    country.Gold,
                    country.Silver,
                    country.Bronze,
                    country.GetTotal());
                rank++;
            }
            System.Console.WriteLine(new string('-', 65));
            System.Console.WriteLine($"Total de médailles distribuées: {GetTotalMedalsDistributed()}");
        }
    }

    /// <summary>
    /// Représente le compte de médailles d'un pays.
    /// </summary>
    public class CountryMedalCount
    {
        public required string CountryName { get; set; }
        public required string CountryCode { get; set; }
        public required string FlagEmoji { get; set; }
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }

        public int GetTotal()
        {
            return Gold + Silver + Bronze;
        }

        public override string ToString()
        {
            return $"{FlagEmoji} {CountryName} - Or:{Gold} Argent:{Silver} Bronze:{Bronze} Total:{GetTotal()}";
        }
    }
}
