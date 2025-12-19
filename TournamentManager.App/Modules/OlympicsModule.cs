using System;
using TournamentManager.App.Core;
using TournamentManager.App.Helpers;

namespace TournamentManager.App.Modules
{
    /// <summary>
    /// Module de gestion interactive des Jeux Olympiques d'hiver.
    /// Logique métier : Responsable de la création, modification et visualisation
    /// des informations relatives aux Jeux Olympiques.
    /// </summary>
    public static class OlympicsModule
    {
        // Référence au contexte applicatif pour accès aux services
        private static ApplicationContext Context => ApplicationContext.Instance;

        // ═══════════════════════════════════════════════════════════
        // MENU PRINCIPAL DU MODULE
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche le menu principal de gestion des Jeux Olympiques.
        /// Logique métier : Point d'entrée du module, orchestre toutes les opérations.
        /// </summary>
        public static void ShowMenu()
        {
            while (true)
            {
                int choice = ConsoleHelper.DisplayMenu(
                    "GESTION DES JEUX OLYMPIQUES",
                    "🏔️  Créer de nouveaux Jeux Olympiques",
                    "📋 Voir les informations des JO actuels",
                    "📊 Afficher le tableau de bord",
                    "🗑️  Réinitialiser et recommencer"
                );

                switch (choice)
                {
                    case 1:
                        CreateOlympics();
                        break;
                    case 2:
                        DisplayOlympicsInfo();
                        break;
                    case 3:
                        DisplayDashboard();
                        break;
                    case 4:
                        ResetOlympics();
                        break;
                    case 0:
                        return; // Retour au menu principal
                }
            }
        }

        // ═══════════════════════════════════════════════════════════
        // CRÉATION DE JEUX OLYMPIQUES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Processus interactif de création de Jeux Olympiques.
        /// Logique métier : Guide l'utilisateur étape par étape avec validation à chaque saisie.
        /// Assure que toutes les données requises sont collectées et valides.
        /// </summary>
        private static void CreateOlympics()
        {
            ConsoleHelper.DisplayTitle("CRÉATION DE JEUX OLYMPIQUES D'HIVER");

            // Vérification : Avertir si des JO existent déjà
            if (Context.IsInitialized)
            {
                ConsoleHelper.DisplayWarning("Des Jeux Olympiques existent déjà.");
                if (!ConsoleHelper.ReadConfirmation("Voulez-vous les remplacer ?"))
                {
                    return;
                }
            }

            ConsoleHelper.DisplaySubTitle("Informations générales");

            // Étape 1 : Nom des Jeux Olympiques
            // Logique métier : Le nom identifie l'édition (ex: "Beijing 2026")
            string name = ConsoleHelper.ReadString("Nom des Jeux Olympiques (ex: Beijing 2026)");

            // Étape 2 : Ville hôte
            // Logique métier : Ville où se déroulent principalement les JO
            string hostCity = ConsoleHelper.ReadString("Ville hôte");

            // Étape 3 : Pays hôte
            // Logique métier : Pays organisateur des JO
            string hostCountry = ConsoleHelper.ReadString("Pays hôte");

            ConsoleHelper.DisplaySubTitle("Dates");

            // Étape 4 : Date de début
            DateTime startDate = ConsoleHelper.ReadDate("Date de début");

            // Étape 5 : Date de fin avec validation
            // Logique métier : La date de fin doit être cohérente avec la date de début
            DateTime endDate;
            while (true)
            {
                endDate = ConsoleHelper.ReadDate("Date de fin");

                // Validation des dates
                if (InputValidator.ValidateOlympicDates(startDate, endDate, out string errorMessage))
                {
                    break; // Dates valides
                }

                ConsoleHelper.DisplayError(errorMessage);
            }

            // Étape 6 : Devise olympique (optionnelle)
            // Logique métier : Chaque édition a une devise inspirante
            string motto = ConsoleHelper.ReadString("Devise olympique (optionnel, Entrée pour passer)", allowEmpty: true);

            // ═══════════════════════════════════════════════════════════
            // CONFIRMATION ET CRÉATION
            // ═══════════════════════════════════════════════════════════

            ConsoleHelper.DisplaySubTitle("Récapitulatif");
            Console.WriteLine($"Nom          : {name}");
            Console.WriteLine($"Lieu         : {hostCity}, {hostCountry}");
            Console.WriteLine($"Dates        : {startDate.ToShortDateString()} - {endDate.ToShortDateString()}");
            Console.WriteLine($"Durée        : {(endDate - startDate).Days} jours");
            if (!string.IsNullOrEmpty(motto))
                Console.WriteLine($"Devise       : \"{motto}\"");

            Console.WriteLine();

            // Confirmation finale
            if (!ConsoleHelper.ReadConfirmation("Confirmer la création des Jeux Olympiques ?"))
            {
                ConsoleHelper.DisplayWarning("Création annulée.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            // Création via le service
            // Logique métier : Délègue la création au service qui gère la logique métier
            ConsoleHelper.DisplayLoading("Création en cours...");
            Context.OlympicService.CreateWinterOlympics(name, hostCity, hostCountry, startDate, endDate, motto);

            // Récupération et stockage des JO créés
            var olympics = Context.OlympicService.GetWinterOlympics(1);
            Context.SetCurrentOlympics(olympics);

            Console.WriteLine(); // Nouvelle ligne après le loading
            ConsoleHelper.DisplaySuccess($"Jeux Olympiques \"{name}\" créés avec succès !");
            ConsoleHelper.DisplayInfo("Vous pouvez maintenant ajouter des sports, des pays et des athlètes.");
            ConsoleHelper.PressKeyToContinue();
        }

        // ═══════════════════════════════════════════════════════════
        // AFFICHAGE DES INFORMATIONS
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche les informations détaillées des Jeux Olympiques actuels.
        /// Logique métier : Présente un résumé complet de l'état des JO.
        /// </summary>
        private static void DisplayOlympicsInfo()
        {
            // Vérification : Des JO doivent exister
            if (!Context.CheckInitialized())
                return;

            var olympics = Context.CurrentOlympics!;

            ConsoleHelper.DisplayTitle("INFORMATIONS DES JEUX OLYMPIQUES");

            // Section 1 : Informations générales
            ConsoleHelper.DisplaySubTitle("Informations générales");
            Console.WriteLine($"Nom          : {olympics.Name}");
            Console.WriteLine($"Ville hôte   : {olympics.HostCity}");
            Console.WriteLine($"Pays hôte    : {olympics.HostCountry}");
            Console.WriteLine($"Date début   : {olympics.StartDate.ToLongDateString()}");
            Console.WriteLine($"Date fin     : {olympics.EndDate.ToLongDateString()}");
            Console.WriteLine($"Durée        : {(olympics.EndDate - olympics.StartDate).Days} jours");

            if (!string.IsNullOrEmpty(olympics.Motto))
            {
                Console.WriteLine($"Devise       : \"{olympics.Motto}\"");
            }

            // Section 2 : Statistiques
            ConsoleHelper.DisplaySubTitle("Statistiques");
            Console.WriteLine($"Sports               : {olympics.Sports.Count}");
            Console.WriteLine($"Pays participants    : {olympics.ParticipatingCountries.Count}");
            Console.WriteLine($"Événements programmés: {olympics.Events.Count}");

            // Calcul du nombre total d'athlètes
            int totalAthletes = 0;
            foreach (var country in olympics.ParticipatingCountries)
            {
                totalAthletes += country.GetAthleteCount();
            }
            Console.WriteLine($"Athlètes inscrits    : {totalAthletes}");

            // Section 3 : Progression des événements
            if (olympics.Events.Count > 0)
            {
                int completed = olympics.Events.FindAll(e => e.Status == Models.Enums.EventStatus.Completed).Count;
                int inProgress = olympics.Events.FindAll(e => e.Status == Models.Enums.EventStatus.InProgress).Count;
                int scheduled = olympics.Events.FindAll(e => e.Status == Models.Enums.EventStatus.Scheduled).Count;

                ConsoleHelper.DisplaySubTitle("Progression des événements");
                Console.WriteLine($"✅ Terminés      : {completed}");
                Console.WriteLine($"▶️  En cours      : {inProgress}");
                Console.WriteLine($"📅 Programmés    : {scheduled}");

                // Calcul du pourcentage de progression
                if (olympics.Events.Count > 0)
                {
                    double progress = (completed * 100.0) / olympics.Events.Count;
                    Console.WriteLine($"\nProgression : {progress:F1}%");
                }
            }

            ConsoleHelper.PressKeyToContinue();
        }

        // ═══════════════════════════════════════════════════════════
        // TABLEAU DE BORD
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche un tableau de bord dynamique avec toutes les informations importantes.
        /// Logique métier : Vue d'ensemble en un coup d'œil de l'état des JO.
        /// </summary>
        private static void DisplayDashboard()
        {
            // Vérification : Des JO doivent exister
            if (!Context.CheckInitialized())
                return;

            var olympics = Context.CurrentOlympics!;

            ConsoleHelper.DisplayTitle($"{olympics.Name} - TABLEAU DE BORD");

            // Calcul des statistiques
            int totalAthletes = 0;
            foreach (var country in olympics.ParticipatingCountries)
            {
                totalAthletes += country.GetAthleteCount();
            }

            int totalDisciplines = 0;
            foreach (var sport in olympics.Sports)
            {
                totalDisciplines += sport.Disciplines.Count;
            }

            int eventsCompleted = olympics.Events.FindAll(e => e.IsCompleted()).Count;

            // Affichage des statistiques globales
            ConsoleHelper.DisplaySubTitle("📊 Statistiques globales");
            Console.WriteLine($"{"Sports",-25}: {olympics.Sports.Count}");
            Console.WriteLine($"{"Disciplines",-25}: {totalDisciplines}");
            Console.WriteLine($"{"Pays participants",-25}: {olympics.ParticipatingCountries.Count}");
            Console.WriteLine($"{"Athlètes inscrits",-25}: {totalAthletes}");
            Console.WriteLine($"{"Événements programmés",-25}: {olympics.Events.Count}");
            Console.WriteLine($"{"Événements complétés",-25}: {eventsCompleted} ({(olympics.Events.Count > 0 ? (eventsCompleted * 100.0 / olympics.Events.Count) : 0):F1}%)");

            // Affichage du top 5 des médailles si des événements sont terminés
            if (eventsCompleted > 0)
            {
                ConsoleHelper.DisplaySubTitle("🏅 Top 5 du tableau des médailles");
                var topCountries = Context.MedalStandingsService.GetTopCountries(olympics, 5);

                if (topCountries.Count > 0)
                {
                    Console.WriteLine($"\n{"Rang",-6} {"Pays",-25} {"Or",6} {"Argent",8} {"Bronze",8} {"Total",7}");
                    ConsoleHelper.DisplaySeparator();

                    int rank = 1;
                    foreach (var country in topCountries)
                    {
                        Console.WriteLine($"{rank,-6} {country.FlagEmoji} {country.CountryName,-22} {country.Gold,6} {country.Silver,8} {country.Bronze,8} {country.GetTotal(),7}");
                        rank++;
                    }
                }
                else
                {
                    Console.WriteLine("Aucune médaille distribuée pour le moment.");
                }
            }

            // Événements du jour (simulation avec la date actuelle)
            var todayEvents = Context.ScheduleService.GetEventsByDate(DateTime.Now);
            if (todayEvents.Count > 0)
            {
                ConsoleHelper.DisplaySubTitle($"📅 Événements aujourd'hui ({DateTime.Now.ToShortDateString()})");
                foreach (var evt in todayEvents)
                {
                    string statusIcon = evt.Status switch
                    {
                        Models.Enums.EventStatus.Completed => "✅",
                        Models.Enums.EventStatus.InProgress => "▶️",
                        Models.Enums.EventStatus.Scheduled => "📅",
                        _ => "📋"
                    };
                    Console.WriteLine($"{statusIcon} {evt.Time:hh\\:mm} - {evt.Name} [{evt.Status}]");
                }
            }

            ConsoleHelper.PressKeyToContinue();
        }

        // ═══════════════════════════════════════════════════════════
        // RÉINITIALISATION
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Réinitialise complètement le système.
        /// Logique métier : Permet de repartir à zéro, utilisé pour tester
        /// ou créer une nouvelle session sans redémarrer l'application.
        /// </summary>
        private static void ResetOlympics()
        {
            ConsoleHelper.DisplayTitle("RÉINITIALISATION");

            if (!Context.IsInitialized)
            {
                ConsoleHelper.DisplayWarning("Aucun Jeux Olympiques à réinitialiser.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            ConsoleHelper.DisplayWarning("⚠️  ATTENTION : Cette action est irréversible !");
            ConsoleHelper.DisplayWarning("Toutes les données seront perdues (JO, sports, athlètes, résultats, etc.)");
            Console.WriteLine();

            if (!ConsoleHelper.ReadConfirmation("Êtes-vous sûr de vouloir tout réinitialiser ?"))
            {
                ConsoleHelper.DisplayInfo("Réinitialisation annulée.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            // Double confirmation pour action critique
            if (!ConsoleHelper.ReadConfirmation("Confirmez-vous vraiment la réinitialisation complète ?"))
            {
                ConsoleHelper.DisplayInfo("Réinitialisation annulée.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            // Réinitialisation via le contexte
            Context.Reset();

            ConsoleHelper.DisplaySuccess("Système réinitialisé avec succès.");
            ConsoleHelper.DisplayInfo("Vous pouvez créer de nouveaux Jeux Olympiques.");
            ConsoleHelper.PressKeyToContinue();
        }
    }
}
