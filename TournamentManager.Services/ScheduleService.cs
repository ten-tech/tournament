using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de gestion du calendrier des compétitions olympiques.
    /// </summary>
    public class ScheduleService : IScheduleService
    {
        private List<Event> _schedule;

        public ScheduleService()
        {
            _schedule = new List<Event>();
        }

        public void AddEventToSchedule(Event olympicEvent)
        {
            if (olympicEvent == null)
            {
                Console.WriteLine("✗ Événement invalide");
                return;
            }

            _schedule.Add(olympicEvent);
            Console.WriteLine($"✓ Événement ajouté au calendrier: {olympicEvent.Name} - {olympicEvent.Date.ToShortDateString()}");
        }

        public List<Event> GetEventsByDate(DateTime date)
        {
            return _schedule.Where(e => e.Date.Date == date.Date).OrderBy(e => e.Time).ToList();
        }

        public List<Event> GetEventsByVenue(Venue venue)
        {
            return _schedule.Where(e => e.Venue.Id == venue.Id).OrderBy(e => e.Date).ThenBy(e => e.Time).ToList();
        }

        public List<Event> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _schedule
                .Where(e => e.Date.Date >= startDate.Date && e.Date.Date <= endDate.Date)
                .OrderBy(e => e.Date)
                .ThenBy(e => e.Time)
                .ToList();
        }

        public bool IsVenueAvailable(Venue venue, DateTime date, TimeSpan time)
        {
            var eventsAtVenue = _schedule.Where(e =>
                e.Venue.Id == venue.Id &&
                e.Date.Date == date.Date &&
                e.Status != EventStatus.Cancelled
            ).ToList();

            foreach (var existingEvent in eventsAtVenue)
            {
                // Vérifier si l'horaire se chevauche (on suppose 2h par événement)
                var existingStart = existingEvent.Time;
                var existingEnd = existingStart.Add(TimeSpan.FromHours(2));
                var newEnd = time.Add(TimeSpan.FromHours(2));

                if ((time >= existingStart && time < existingEnd) ||
                    (newEnd > existingStart && newEnd <= existingEnd))
                {
                    Console.WriteLine($"⚠ Conflit d'horaire au {venue.Name} le {date.ToShortDateString()} à {time}");
                    return false;
                }
            }

            return true;
        }

        public void RescheduleEvent(Event olympicEvent, DateTime newDate, TimeSpan newTime)
        {
            if (olympicEvent == null)
            {
                Console.WriteLine("✗ Événement invalide");
                return;
            }

            if (!IsVenueAvailable(olympicEvent.Venue, newDate, newTime))
            {
                Console.WriteLine($"✗ Impossible de reprogrammer: le lieu n'est pas disponible");
                return;
            }

            var oldDate = olympicEvent.Date;
            var oldTime = olympicEvent.Time;

            olympicEvent.Date = newDate;
            olympicEvent.Time = newTime;
            olympicEvent.UpdateStatus(EventStatus.Postponed);

            Console.WriteLine($"✓ Événement reprogrammé: {olympicEvent.Name}");
            Console.WriteLine($"  Ancien: {oldDate.ToShortDateString()} à {oldTime}");
            Console.WriteLine($"  Nouveau: {newDate.ToShortDateString()} à {newTime}");
        }

        public void DisplayDailySchedule(DateTime date)
        {
            var events = GetEventsByDate(date);

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  CALENDRIER DU {date.ToShortDateString(),-43} ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            if (events.Count == 0)
            {
                Console.WriteLine("Aucun événement programmé pour cette date.");
                return;
            }

            foreach (var evt in events)
            {
                string statusIcon = evt.Status switch
                {
                    EventStatus.Scheduled => "📅",
                    EventStatus.InProgress => "▶️",
                    EventStatus.Completed => "✅",
                    EventStatus.Cancelled => "❌",
                    EventStatus.Postponed => "⏸️",
                    _ => "📋"
                };

                Console.WriteLine($"{statusIcon} {evt.Time:hh\\:mm} | {evt.Name}");
                Console.WriteLine($"   Lieu: {evt.Venue.Name} | Phase: {evt.Phase}");
                Console.WriteLine();
            }

            Console.WriteLine($"Total: {events.Count} événement(s)");
        }

        public void DisplayFullSchedule()
        {
            if (_schedule.Count == 0)
            {
                Console.WriteLine("Aucun événement au calendrier.");
                return;
            }

            var groupedByDate = _schedule
                .GroupBy(e => e.Date.Date)
                .OrderBy(g => g.Key);

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              CALENDRIER COMPLET DES JEUX                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            foreach (var dateGroup in groupedByDate)
            {
                Console.WriteLine($"\n📅 {dateGroup.Key.ToLongDateString()}");
                Console.WriteLine(new string('-', 60));

                foreach (var evt in dateGroup.OrderBy(e => e.Time))
                {
                    Console.WriteLine($"  {evt.Time:hh\\:mm} - {evt.Name} ({evt.Venue.Name}) [{evt.Status}]");
                }
            }

            Console.WriteLine($"\n\nTotal: {_schedule.Count} événement(s) programmé(s)");
        }
    }
}
