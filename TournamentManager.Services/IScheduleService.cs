using System;
using System.Collections.Generic;
using TournamentManager.Models;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de gestion du calendrier olympique.
    /// </summary>
    public interface IScheduleService
    {
        void AddEventToSchedule(Event olympicEvent);
        List<Event> GetEventsByDate(DateTime date);
        List<Event> GetEventsByVenue(Venue venue);
        List<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate);
        bool IsVenueAvailable(Venue venue, DateTime date, TimeSpan time);
        void RescheduleEvent(Event olympicEvent, DateTime newDate, TimeSpan newTime);
        void DisplayDailySchedule(DateTime date);
        void DisplayFullSchedule();
    }
}
