using System.Collections.Generic;
using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un athlète participant aux Jeux Olympiques d'hiver.
    /// </summary>
    public class Athlete
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string CountryCode { get; set; } // Ex: "FRA", "USA", "CAN"
        public int BibNumber { get; set; } // Numéro de dossard
        public Gender Gender { get; set; }
        public List<int> SportIds { get; set; } // Sports pratiqués
        public List<Result> Results { get; set; } // Historique des résultats
        public List<MedalType> Medals { get; set; } // Médailles gagnées

        public Athlete(int id, string firstName, string lastName, int age, string nationality, string countryCode, Gender gender, int bibNumber = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Nationality = nationality;
            CountryCode = countryCode;
            Gender = gender;
            BibNumber = bibNumber;
            SportIds = new List<int>();
            Results = new List<Result>();
            Medals = new List<MedalType>();
        }

        /// <summary>
        /// Obtient le nom complet de l'athlète.
        /// </summary>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Ajoute un sport pratiqué par l'athlète.
        /// </summary>
        public void AddSport(int sportId)
        {
            if (!SportIds.Contains(sportId))
            {
                SportIds.Add(sportId);
            }
        }

        /// <summary>
        /// Ajoute un résultat à l'historique de l'athlète.
        /// </summary>
        public void AddResult(Result result)
        {
            Results.Add(result);
            if (result.Medal != MedalType.None)
            {
                Medals.Add(result.Medal);
            }
        }

        /// <summary>
        /// Compte le nombre de médailles d'un type spécifique.
        /// </summary>
        public int GetMedalCount(MedalType medalType)
        {
            return Medals.FindAll(m => m == medalType).Count;
        }

        /// <summary>
        /// Obtient le nombre total de médailles.
        /// </summary>
        public int GetTotalMedals()
        {
            return Medals.Count;
        }

        public override string ToString()
        {
            return $"{GetFullName()} ({CountryCode}) - Dossard #{BibNumber} - {GetTotalMedals()} médaille(s)";
        }
    }
}
