using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente une discipline spécifique dans un sport (ex: Slalom, Super-G pour le ski alpin).
    /// </summary>
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SportId { get; set; }
        public Gender Gender { get; set; }
        public int MaxParticipants { get; set; }
        public bool HasMultipleRounds { get; set; }
        public string ScoringMethod { get; set; } // Ex: "BestTime", "TotalScore", "AverageScore"

        public Discipline(int id, string name, int sportId, Gender gender, int maxParticipants = 100, bool hasMultipleRounds = false, string scoringMethod = "BestTime")
        {
            Id = id;
            Name = name;
            SportId = sportId;
            Gender = gender;
            MaxParticipants = maxParticipants;
            HasMultipleRounds = hasMultipleRounds;
            ScoringMethod = scoringMethod;
        }

        public override string ToString()
        {
            return $"{Name} - {Gender} (Max participants: {MaxParticipants})";
        }
    }
}
