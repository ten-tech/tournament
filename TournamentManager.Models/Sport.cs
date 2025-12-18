using System.Collections.Generic;
using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un sport olympique (ex: Ski alpin, Hockey sur glace, Patinage artistique).
    /// </summary>
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompetitionType CompetitionType { get; set; }
        public List<Discipline> Disciplines { get; set; }
        public string Description { get; set; }

        public Sport(int id, string name, CompetitionType competitionType, string description = "")
        {
            Id = id;
            Name = name;
            CompetitionType = competitionType;
            Description = description;
            Disciplines = new List<Discipline>();
        }

        /// <summary>
        /// Ajoute une discipline à ce sport.
        /// </summary>
        public void AddDiscipline(Discipline discipline)
        {
            if (!Disciplines.Contains(discipline))
            {
                Disciplines.Add(discipline);
            }
        }

        /// <summary>
        /// Obtient toutes les disciplines de ce sport.
        /// </summary>
        public List<Discipline> GetDisciplines()
        {
            return Disciplines;
        }

        public override string ToString()
        {
            return $"{Name} ({CompetitionType}) - {Disciplines.Count} discipline(s)";
        }
    }
}
