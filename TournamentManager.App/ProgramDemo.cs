using System;
using TournamentManager.Models;
using TournamentManager.Models.Enums;
using TournamentManager.Services;

namespace TournamentManager.App
{
    /// <summary>
    /// Programme de démonstration avec simulation automatique des Jeux Olympiques.
    /// </summary>
    class ProgramDemo
    {
        static void RunDemo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        SYSTÈME DE GESTION DES JEUX OLYMPIQUES D'HIVER      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            // ═══════════════════════════════════════════════════════════
            // 1. INITIALISATION DES SERVICES
            // ═══════════════════════════════════════════════════════════
            var olympicService = new OlympicGameService();
            var sportService = new SportService();
            var scoringService = new ScoringService();
            var competitionService = new CompetitionService(scoringService);
            var scheduleService = new ScheduleService();
            var medalStandingsService = new MedalStandingsService();

            // ═══════════════════════════════════════════════════════════
            // 2. CRÉATION DES JEUX OLYMPIQUES
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ CRÉATION DES JEUX OLYMPIQUES ━━━");
            olympicService.CreateWinterOlympics(
                "Beijing 2026",
                "Beijing",
                "China",
                new DateTime(2026, 2, 4),
                new DateTime(2026, 2, 20),
                "Together for a Shared Future"
            );

            var olympics = olympicService.GetWinterOlympics(1);

            // ═══════════════════════════════════════════════════════════
            // 3. CRÉATION DES SPORTS ET DISCIPLINES
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ CRÉATION DES SPORTS ━━━");

            // Ski Alpin
            var alpineSkiing = sportService.CreateSport("Ski Alpin", CompetitionType.Timed, "Courses de vitesse sur pistes enneigées");
            var menDownhill = sportService.CreateDiscipline("Descente Hommes", alpineSkiing.Id, Gender.Male, 50, false, "BestTime");
            var womenSlalom = sportService.CreateDiscipline("Slalom Femmes", alpineSkiing.Id, Gender.Female, 50, true, "BestTime");
            sportService.AddDisciplineToSport(alpineSkiing, menDownhill);
            sportService.AddDisciplineToSport(alpineSkiing, womenSlalom);

            // Patinage Artistique
            var figureSkating = sportService.CreateSport("Patinage Artistique", CompetitionType.Scored, "Performances artistiques sur glace");
            var womenSingle = sportService.CreateDiscipline("Simple Femmes", figureSkating.Id, Gender.Female, 30, false, "TotalScore");
            sportService.AddDisciplineToSport(figureSkating, womenSingle);

            // Hockey sur glace
            var iceHockey = sportService.CreateSport("Hockey sur Glace", CompetitionType.HeadToHead, "Sport d'équipe sur glace");
            var menHockey = sportService.CreateDiscipline("Hockey Hommes", iceHockey.Id, Gender.Male, 12, false, "TotalScore");
            sportService.AddDisciplineToSport(iceHockey, menHockey);

            // Ajouter les sports aux Jeux Olympiques
            olympicService.AddSport(1, alpineSkiing);
            olympicService.AddSport(1, figureSkating);
            olympicService.AddSport(1, iceHockey);

            // ═══════════════════════════════════════════════════════════
            // 4. CRÉATION DES LIEUX (VENUES)
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ CRÉATION DES LIEUX ━━━");
            var alpineCenter = new Venue(1, "National Alpine Skiing Centre", "Yanqing", 5000, "Alpine Skiing");
            var iceArena = new Venue(2, "Capital Indoor Stadium", "Beijing", 18000, "Figure Skating");
            var hockeyVenue = new Venue(3, "National Indoor Stadium", "Beijing", 17000, "Ice Hockey");
            Console.WriteLine($"✓ Lieu créé: {alpineCenter}");
            Console.WriteLine($"✓ Lieu créé: {iceArena}");
            Console.WriteLine($"✓ Lieu créé: {hockeyVenue}");

            // ═══════════════════════════════════════════════════════════
            // 5. CRÉATION DES PAYS ET ATHLÈTES
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ INSCRIPTION DES PAYS ET ATHLÈTES ━━━");

            // France
            var france = new NationalTeam(1, "France", "FRA", "🇫🇷");
            var alexisPinturault = new Athlete(1, "Alexis", "Pinturault", 33, "France", "FRA", Gender.Male, 1);
            alexisPinturault.AddSport(alpineSkiing.Id);
            france.AddAthlete(alexisPinturault);
            olympicService.AddCountry(1, france);

            // Norvège
            var norway = new NationalTeam(2, "Norway", "NOR", "🇳🇴");
            var akselSvindal = new Athlete(2, "Aksel", "Svindal", 41, "Norway", "NOR", Gender.Male, 2);
            akselSvindal.AddSport(alpineSkiing.Id);
            norway.AddAthlete(akselSvindal);
            olympicService.AddCountry(1, norway);

            // États-Unis
            var usa = new NationalTeam(3, "United States", "USA", "🇺🇸");
            var micaelaShiffrin = new Athlete(3, "Mikaela", "Shiffrin", 29, "United States", "USA", Gender.Female, 3);
            micaelaShiffrin.AddSport(alpineSkiing.Id);
            var nathanChen = new Athlete(4, "Nathan", "Chen", 25, "United States", "USA", Gender.Male, 4);
            nathanChen.AddSport(figureSkating.Id);
            usa.AddAthlete(micaelaShiffrin);
            usa.AddAthlete(nathanChen);
            olympicService.AddCountry(1, usa);

            // Suisse
            var switzerland = new NationalTeam(4, "Switzerland", "SUI", "🇨🇭");
            var laraGutBehrami = new Athlete(5, "Lara", "Gut-Behrami", 33, "Switzerland", "SUI", Gender.Female, 5);
            laraGutBehrami.AddSport(alpineSkiing.Id);
            switzerland.AddAthlete(laraGutBehrami);
            olympicService.AddCountry(1, switzerland);

            // Italie
            var italy = new NationalTeam(5, "Italy", "ITA", "🇮🇹");
            var sofiaGoggia = new Athlete(6, "Sofia", "Goggia", 32, "Italy", "ITA", Gender.Female, 6);
            sofiaGoggia.AddSport(alpineSkiing.Id);
            italy.AddAthlete(sofiaGoggia);
            olympicService.AddCountry(1, italy);

            // Japon
            var japan = new NationalTeam(6, "Japan", "JPN", "🇯🇵");
            var yuzuruHanyu = new Athlete(7, "Yuzuru", "Hanyu", 30, "Japan", "JPN", Gender.Male, 7);
            yuzuruHanyu.AddSport(figureSkating.Id);
            japan.AddAthlete(yuzuruHanyu);
            olympicService.AddCountry(1, japan);

            // Russie
            var russia = new NationalTeam(7, "Russia", "RUS", "🇷🇺");
            var kamila = new Athlete(8, "Kamila", "Valieva", 20, "Russia", "RUS", Gender.Female, 8);
            kamila.AddSport(figureSkating.Id);
            russia.AddAthlete(kamila);
            olympicService.AddCountry(1, russia);

            // ═══════════════════════════════════════════════════════════
            // 6. CRÉATION DES ÉVÉNEMENTS
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ PROGRAMMATION DES ÉVÉNEMENTS ━━━");

            // Événement 1: Descente Hommes en Ski Alpin
            var menDownhillEvent = competitionService.CreateEvent(
                "Descente Hommes - Finale",
                menDownhill.Id,
                new DateTime(2026, 2, 6),
                new TimeSpan(10, 0, 0),
                alpineCenter,
                "Finale",
                1
            );
            olympicService.AddEvent(1, menDownhillEvent);
            scheduleService.AddEventToSchedule(menDownhillEvent);

            // Événement 2: Slalom Femmes en Ski Alpin
            var womenSlalomEvent = competitionService.CreateEvent(
                "Slalom Femmes - Finale",
                womenSlalom.Id,
                new DateTime(2026, 2, 8),
                new TimeSpan(14, 0, 0),
                alpineCenter,
                "Finale",
                1
            );
            olympicService.AddEvent(1, womenSlalomEvent);
            scheduleService.AddEventToSchedule(womenSlalomEvent);

            // Événement 3: Patinage Artistique Simple Femmes
            var womenFigureSkatingEvent = competitionService.CreateEvent(
                "Patinage Artistique Simple Femmes - Finale",
                womenSingle.Id,
                new DateTime(2026, 2, 10),
                new TimeSpan(19, 0, 0),
                iceArena,
                "Finale",
                1
            );
            olympicService.AddEvent(1, womenFigureSkatingEvent);
            scheduleService.AddEventToSchedule(womenFigureSkatingEvent);

            // ═══════════════════════════════════════════════════════════
            // 7. INSCRIPTION DES PARTICIPANTS AUX ÉVÉNEMENTS
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ INSCRIPTION DES PARTICIPANTS ━━━");

            // Descente Hommes
            competitionService.RegisterParticipant(menDownhillEvent, alexisPinturault);
            competitionService.RegisterParticipant(menDownhillEvent, akselSvindal);

            // Slalom Femmes
            competitionService.RegisterParticipant(womenSlalomEvent, micaelaShiffrin);
            competitionService.RegisterParticipant(womenSlalomEvent, laraGutBehrami);
            competitionService.RegisterParticipant(womenSlalomEvent, sofiaGoggia);

            // Patinage Artistique Femmes
            competitionService.RegisterParticipant(womenFigureSkatingEvent, kamila);

            // ═══════════════════════════════════════════════════════════
            // 8. SIMULATION DES COMPÉTITIONS
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n━━━ SIMULATION DES COMPÉTITIONS ━━━\n");

            // COMPÉTITION 1: Descente Hommes
            Console.WriteLine("🎿 DESCENTE HOMMES - SKI ALPIN");
            competitionService.RecordTimedPerformance(menDownhillEvent, alexisPinturault, TimeSpan.FromSeconds(95.23), 0);
            competitionService.RecordTimedPerformance(menDownhillEvent, akselSvindal, TimeSpan.FromSeconds(94.85), 0);
            competitionService.FinalizeEvent(menDownhillEvent, menDownhill);

            // COMPÉTITION 2: Slalom Femmes
            Console.WriteLine("\n🎿 SLALOM FEMMES - SKI ALPIN");
            competitionService.RecordTimedPerformance(womenSlalomEvent, micaelaShiffrin, TimeSpan.FromSeconds(102.45), 0);
            competitionService.RecordTimedPerformance(womenSlalomEvent, laraGutBehrami, TimeSpan.FromSeconds(103.12), 0);
            competitionService.RecordTimedPerformance(womenSlalomEvent, sofiaGoggia, TimeSpan.FromSeconds(102.98), 0.5); // Pénalité de 0.5s

            competitionService.FinalizeEvent(womenSlalomEvent, womenSlalom);

            // COMPÉTITION 3: Patinage Artistique Simple Femmes
            Console.WriteLine("\n⛸️  PATINAGE ARTISTIQUE SIMPLE FEMMES");
            // Scores de 9 juges pour Kamila Valieva
            var kamilaScores = new System.Collections.Generic.List<double> { 9.5, 9.8, 9.7, 9.6, 9.9, 9.5, 9.8, 9.7, 9.6 };
            competitionService.RecordScoredPerformance(womenFigureSkatingEvent, kamila, kamilaScores, 0);

            competitionService.FinalizeEvent(womenFigureSkatingEvent, womenSingle);

            // ═══════════════════════════════════════════════════════════
            // 9. AFFICHAGE DES RÉSULTATS
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                    RÉSULTATS DES COMPÉTITIONS                ");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            competitionService.DisplayEventResults(menDownhillEvent);
            competitionService.DisplayEventResults(womenSlalomEvent);
            competitionService.DisplayEventResults(womenFigureSkatingEvent);

            // ═══════════════════════════════════════════════════════════
            // 10. AFFICHAGE DU CALENDRIER
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                        CALENDRIER                            ");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            scheduleService.DisplayDailySchedule(new DateTime(2026, 2, 6));
            scheduleService.DisplayDailySchedule(new DateTime(2026, 2, 8));

            // ═══════════════════════════════════════════════════════════
            // 11. TABLEAU DES MÉDAILLES
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("                   TABLEAU DES MÉDAILLES                      ");
            Console.WriteLine("    ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            medalStandingsService.DisplayStandings(olympics);

            // ═══════════════════════════════════════════════════════════
            // 12. INFORMATIONS GÉNÉRALES
            // ═══════════════════════════════════════════════════════════
            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("              INFORMATIONS SUR LES JEUX OLYMPIQUES            ");
            Console.WriteLine("    ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            olympicService.DisplayOlympicsInfo(1);

            Console.WriteLine("\n\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                    FIN DE LA SIMULATION                    ║");
            Console.WriteLine("    ╚════════════════════════════════════════════════════════════╝");
        }
    }
}
