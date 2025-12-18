using System;
using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente le résultat d'un athlète dans une compétition.
    /// </summary>
    public class Result
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int AthleteId { get; set; }
        public Athlete Athlete { get; set; }

        // Pour les compétitions chronométrées
        public TimeSpan? Time { get; set; }

        // Pour les compétitions par points/score
        public double? Score { get; set; }

        // Classement et médaille
        public int Rank { get; set; }
        public MedalType Medal { get; set; }

        // Informations additionnelles
        public bool IsDisqualified { get; set; }
        public string DisqualificationReason { get; set; }
        public double? Penalty { get; set; } // Pénalité en secondes ou points
        public string Notes { get; set; }

        public Result(int id, int eventId, int athleteId, Athlete athlete)
        {
            Id = id;
            EventId = eventId;
            AthleteId = athleteId;
            Athlete = athlete;
            Rank = 0;
            Medal = MedalType.None;
            IsDisqualified = false;
            DisqualificationReason = "";
            Notes = "";
        }

        /// <summary>
        /// Définit le temps pour une compétition chronométrée.
        /// </summary>
        public void SetTime(TimeSpan time, double penalty = 0)
        {
            Time = time;
            Penalty = penalty;
        }

        /// <summary>
        /// Définit le score pour une compétition par points.
        /// </summary>
        public void SetScore(double score, double penalty = 0)
        {
            Score = score;
            Penalty = penalty;
        }

        /// <summary>
        /// Obtient la performance finale (temps ou score avec pénalités).
        /// </summary>
        public string GetFinalPerformance()
        {
            if (IsDisqualified)
                return "DSQ";

            if (Time.HasValue)
            {
                var totalSeconds = Time.Value.TotalSeconds + (Penalty ?? 0);
                return $"{TimeSpan.FromSeconds(totalSeconds):mm\\:ss\\.ff}";
            }
            else if (Score.HasValue)
            {
                var finalScore = Score.Value - (Penalty ?? 0);
                return $"{finalScore:F2} pts";
            }

            return "N/A";
        }

        /// <summary>
        /// Définit le classement et attribue la médaille si applicable.
        /// </summary>
        public void SetRank(int rank)
        {
            Rank = rank;
            Medal = rank switch
            {
                1 => MedalType.Gold,
                2 => MedalType.Silver,
                3 => MedalType.Bronze,
                _ => MedalType.None
            };
        }

        /// <summary>
        /// Disqualifie le résultat.
        /// </summary>
        public void Disqualify(string reason)
        {
            IsDisqualified = true;
            DisqualificationReason = reason;
            Rank = 0;
            Medal = MedalType.None;
        }

        public override string ToString()
        {
            string medalStr = Medal switch
            {
                MedalType.Gold => "🥇",
                MedalType.Silver => "🥈",
                MedalType.Bronze => "🥉",
                _ => ""
            };

            return $"{Rank}. {Athlete.GetFullName()} ({Athlete.CountryCode}) - {GetFinalPerformance()} {medalStr}";
        }
    }
}
