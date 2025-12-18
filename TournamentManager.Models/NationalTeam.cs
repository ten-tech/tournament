using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente une délégation nationale aux Jeux Olympiques d'hiver.
    /// </summary>
    public class NationalTeam
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; } // Ex: "FRA", "USA", "CAN"
        public string FlagEmoji { get; set; } // Ex: "🇫🇷", "🇺🇸", "🇨🇦"
        public List<Athlete> Athletes { get; set; }
        public int GoldMedals { get; set; }
        public int SilverMedals { get; set; }
        public int BronzeMedals { get; set; }

        public NationalTeam(int id, string countryName, string countryCode, string flagEmoji = "")
        {
            Id = id;
            CountryName = countryName;
            CountryCode = countryCode;
            FlagEmoji = flagEmoji;
            Athletes = new List<Athlete>();
            GoldMedals = 0;
            SilverMedals = 0;
            BronzeMedals = 0;
        }

        /// <summary>
        /// Ajoute un athlète à la délégation.
        /// </summary>
        public void AddAthlete(Athlete athlete)
        {
            if (!Athletes.Contains(athlete))
            {
                Athletes.Add(athlete);
            }
        }

        /// <summary>
        /// Ajoute une médaille au tableau du pays.
        /// </summary>
        public void AddMedal(MedalType medalType)
        {
            switch (medalType)
            {
                case MedalType.Gold:
                    GoldMedals++;
                    break;
                case MedalType.Silver:
                    SilverMedals++;
                    break;
                case MedalType.Bronze:
                    BronzeMedals++;
                    break;
            }
        }

        /// <summary>
        /// Obtient le nombre total de médailles.
        /// </summary>
        public int GetTotalMedals()
        {
            return GoldMedals + SilverMedals + BronzeMedals;
        }

        /// <summary>
        /// Obtient le nombre d'athlètes dans la délégation.
        /// </summary>
        public int GetAthleteCount()
        {
            return Athletes.Count;
        }

        /// <summary>
        /// Recalcule les médailles en fonction des résultats des athlètes.
        /// </summary>
        public void RecalculateMedals()
        {
            GoldMedals = 0;
            SilverMedals = 0;
            BronzeMedals = 0;

            foreach (var athlete in Athletes)
            {
                GoldMedals += athlete.GetMedalCount(MedalType.Gold);
                SilverMedals += athlete.GetMedalCount(MedalType.Silver);
                BronzeMedals += athlete.GetMedalCount(MedalType.Bronze);
            }
        }

        public override string ToString()
        {
            return $"{FlagEmoji} {CountryName} ({CountryCode}) - Or:{GoldMedals} Argent:{SilverMedals} Bronze:{BronzeMedals} | Total:{GetTotalMedals()} - {GetAthleteCount()} athlète(s)";
        }
    }
}
