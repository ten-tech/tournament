using System;
using System.Collections.Generic;

namespace TournamentManager.Models
{
    /// <summary>
    /// Représente un événement des Jeux Olympiques d'hiver complet (ex: "Beijing 2022").
    /// </summary>
    public class WinterOlympics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HostCity { get; set; }
        public string HostCountry { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Sport> Sports { get; set; }
        public List<NationalTeam> ParticipatingCountries { get; set; }
        public List<Event> Events { get; set; }
        public string Motto { get; set; }

        public WinterOlympics(int id, string name, string hostCity, string hostCountry, DateTime startDate, DateTime endDate, string motto = "")
        {
            Id = id;
            Name = name;
            HostCity = hostCity;
            HostCountry = hostCountry;
            StartDate = startDate;
            EndDate = endDate;
            Motto = motto;
            Sports = new List<Sport>();
            ParticipatingCountries = new List<NationalTeam>();
            Events = new List<Event>();
        }

        /// <summary>
        /// Ajoute un sport aux Jeux Olympiques.
        /// </summary>
        public void AddSport(Sport sport)
        {
            if (!Sports.Contains(sport))
            {
                Sports.Add(sport);
            }
        }

        /// <summary>
        /// Ajoute un pays participant.
        /// </summary>
        public void AddCountry(NationalTeam country)
        {
            if (!ParticipatingCountries.Contains(country))
            {
                ParticipatingCountries.Add(country);
            }
        }

        /// <summary>
        /// Ajoute un événement de compétition.
        /// </summary>
        public void AddEvent(Event olympicEvent)
        {
            if (!Events.Contains(olympicEvent))
            {
                Events.Add(olympicEvent);
            }
        }

        /// <summary>
        /// Obtient tous les événements d'une date spécifique.
        /// </summary>
        public List<Event> GetEventsByDate(DateTime date)
        {
            return Events.FindAll(e => e.Date.Date == date.Date);
        }

        public override string ToString()
        {
            return $"{Name} - {HostCity}, {HostCountry} ({StartDate.ToShortDateString()} - {EndDate.ToShortDateString()})";
        }
    }
}
