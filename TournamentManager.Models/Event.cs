using System;
using System.Collections.Generic;
using TournamentManager.Models.Enums;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un événement de compétition olympique (ex: "Finale du slalom géant hommes").
    /// </summary>
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisciplineId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Venue Venue { get; set; }
        public EventStatus Status { get; set; }
        public List<Athlete> Participants { get; set; }
        public List<Result> Results { get; set; }
        public string Phase { get; set; } // Ex: "Qualification", "Demi-finale", "Finale"
        public int RoundNumber { get; set; } // Numéro de la manche
        public string WeatherConditions { get; set; }

        public Event(int id, string name, int disciplineId, DateTime date, TimeSpan time, Venue venue, string phase = "Finale", int roundNumber = 1)
        {
            Id = id;
            Name = name;
            DisciplineId = disciplineId;
            Date = date;
            Time = time;
            Venue = venue;
            Phase = phase;
            RoundNumber = roundNumber;
            Status = EventStatus.Scheduled;
            Participants = new List<Athlete>();
            Results = new List<Result>();
            WeatherConditions = "Normal";
        }

        /// <summary>
        /// Ajoute un participant à l'événement.
        /// </summary>
        public void AddParticipant(Athlete athlete)
        {
            if (!Participants.Contains(athlete))
            {
                Participants.Add(athlete);
            }
        }

        /// <summary>
        /// Ajoute un résultat à l'événement.
        /// </summary>
        public void AddResult(Result result)
        {
            if (!Results.Contains(result))
            {
                Results.Add(result);
            }
        }

        /// <summary>
        /// Change le statut de l'événement.
        /// </summary>
        public void UpdateStatus(EventStatus newStatus)
        {
            Status = newStatus;
        }

        /// <summary>
        /// Obtient le nombre de participants.
        /// </summary>
        public int GetParticipantCount()
        {
            return Participants.Count;
        }

        /// <summary>
        /// Vérifie si l'événement est terminé.
        /// </summary>
        public bool IsCompleted()
        {
            return Status == EventStatus.Completed;
        }

        public override string ToString()
        {
            return $"{Name} - {Phase} (Manche {RoundNumber}) - {Date.ToShortDateString()} {Time} - {Venue.Name} [{Status}]";
        }
    }
}
