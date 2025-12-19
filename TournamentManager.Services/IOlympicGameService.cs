using System;
using System.Collections.Generic;
using TournamentManager.Models;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de gestion des Jeux Olympiques d'hiver.
    /// </summary>
    public interface IOlympicGameService
    {
        void CreateWinterOlympics(string name, string hostCity, string hostCountry, DateTime startDate, DateTime endDate, string motto = "");
        void AddSport(int olympicsId, Sport sport);
        void AddCountry(int olympicsId, NationalTeam country);
        void AddEvent(int olympicsId, Event olympicEvent);
        WinterOlympics GetWinterOlympics(int id);
        List<WinterOlympics> GetAllWinterOlympics();
        List<Event> GetEventsByDate(int olympicsId, DateTime date);
        MedalStandings GetMedalStandings(int olympicsId);
        void DisplayOlympicsInfo(int olympicsId);
        Athlete FindAthlete(int olympicsId, string firstName, string lastName);
        NationalTeam FindCountry(int olympicsId, string countryCode);
    }
}
