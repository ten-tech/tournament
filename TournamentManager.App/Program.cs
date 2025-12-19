using System;
using TournamentManager.App.Core;
using TournamentManager.App.Helpers;
using TournamentManager.App.Modules;

namespace TournamentManager.App
{
    /// <summary>
    /// Point d'entrée de l'application interactive de gestion des Jeux Olympiques d'hiver.
    /// Logique métier : Orchestre la navigation entre les différents modules et maintient
    /// la session utilisateur active.
    /// </summary>
    class Program
    {
        // ═══════════════════════════════════════════════════════════
        // POINT D'ENTRÉE PRINCIPAL
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Méthode principale de démarrage de l'application.
        /// Logique métier : Configure l'environnement et lance la boucle principale du menu.
        /// </summary>
        static void Main(string[] args)
        {
            // Configuration de l'encodage pour supporter les émojis et caractères Unicode
            // Logique métier : Nécessaire pour afficher correctement les drapeaux et symboles
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Affichage du splash screen
            DisplayWelcome();

            // Boucle principale de l'application
            // Logique métier : Continue tant que l'utilisateur ne quitte pas
            MainMenu();

            // Message de fin
            DisplayGoodbye();
        }

        // ═══════════════════════════════════════════════════════════
        // ÉCRANS D'ACCUEIL ET DE SORTIE
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche l'écran de bienvenue avec le logo de l'application.
        /// Logique métier : Première impression de l'utilisateur, doit être accueillante.
        /// </summary>
        static void DisplayWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║        SYSTÈME DE GESTION DES JEUX OLYMPIQUES D'HIVER      ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("║                    VERSION INTERACTIVE                     ║");
            Console.WriteLine("║                                                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n🏔️  Bienvenue dans le gestionnaire olympique professionnel 🏔️\n");
            Console.WriteLine("Ce système vous permet de gérer tous les aspects des");
            Console.WriteLine("Jeux Olympiques d'hiver : sports, athlètes, compétitions,");
            Console.WriteLine("résultats et tableau des médailles.\n");
            ConsoleHelper.PressKeyToContinue("Appuyez sur Entrée pour commencer...");
        }

        /// <summary>
        /// Affiche le message de fin et les statistiques de session.
        /// Logique métier : Résume ce qui a été fait pendant la session.
        /// </summary>
        static void DisplayGoodbye()
        {
            Console.Clear();
            ConsoleHelper.DisplayTitle("FIN DE SESSION");

            var context = ApplicationContext.Instance;

            // Affichage des statistiques si des JO ont été créés
            if (context.IsInitialized)
            {
                var olympics = context.CurrentOlympics!;

                ConsoleHelper.DisplaySubTitle("Récapitulatif de votre session");
                Console.WriteLine($"Jeux Olympiques   : {olympics.Name}");
                Console.WriteLine($"Sports créés      : {olympics.Sports.Count}");
                Console.WriteLine($"Pays inscrits     : {olympics.ParticipatingCountries.Count}");
                Console.WriteLine($"Événements        : {olympics.Events.Count}");

                int completed = olympics.Events.FindAll(e => e.IsCompleted()).Count;
                if (completed > 0)
                {
                    Console.WriteLine($"Compétitions terminées : {completed}");
                }
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Merci d'avoir utilisé le système de gestion olympique !");
            Console.WriteLine("À bientôt ! 👋");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ═══════════════════════════════════════════════════════════
        // MENU PRINCIPAL
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Menu principal de navigation de l'application.
        /// Logique métier : Hub central permettant d'accéder à tous les modules.
        /// La logique de navigation est séparée de la logique métier qui est dans les modules.
        /// </summary>
        static void MainMenu()
        {
            while (true)
            {
                // Affichage du menu avec toutes les options disponibles
                int choice = ConsoleHelper.DisplayMenu(
                    "MENU PRINCIPAL - JEUX OLYMPIQUES D'HIVER",
                    "🏔️  Gérer les Jeux Olympiques",
                    "🎿  Gérer les Sports et Disciplines",
                    "🌍  Gérer les Pays et Athlètes",
                    "📅  Gérer le Calendrier",
                    "🏁  Lancer une Compétition",
                    "📊  Résultats et Statistiques",
                    "🏅  Tableau des Médailles",
                    "ℹ️   À propos"
                );

                // Logique de navigation : Redirection vers le module approprié
                // Pattern : Chaque module est responsable de sa propre logique
                switch (choice)
                {
                    case 1:
                        // Module de gestion des JO (création, infos, tableau de bord)
                        OlympicsModule.ShowMenu();
                        break;

                    case 2:
                        // Module de gestion des sports et disciplines
                        SportsModule.ShowMenu();
                        break;

                    case 3:
                        // Module de gestion des pays et athlètes (à implémenter)
                        ConsoleHelper.DisplayInfo("Module en cours de développement...");
                        ConsoleHelper.PressKeyToContinue();
                        break;

                    case 4:
                        // Module de gestion du calendrier (à implémenter)
                        ConsoleHelper.DisplayInfo("Module en cours de développement...");
                        ConsoleHelper.PressKeyToContinue();
                        break;

                    case 5:
                        // Module de compétition (à implémenter)
                        ConsoleHelper.DisplayInfo("Module en cours de développement...");
                        ConsoleHelper.PressKeyToContinue();
                        break;

                    case 6:
                        // Module de résultats et statistiques (à implémenter)
                        ConsoleHelper.DisplayInfo("Module en cours de développement...");
                        ConsoleHelper.PressKeyToContinue();
                        break;

                    case 7:
                        // Affichage direct du tableau des médailles
                        DisplayMedalStandings();
                        break;

                    case 8:
                        // Écran À propos
                        DisplayAbout();
                        break;

                    case 0:
                        // Confirmation de sortie pour éviter une fermeture accidentelle
                        if (ConsoleHelper.ReadConfirmation("Voulez-vous vraiment quitter l'application ?"))
                        {
                            return; // Sortie de la boucle = fin de l'application
                        }
                        break;
                }
            }
        }

        // ═══════════════════════════════════════════════════════════
        // FONCTIONS UTILITAIRES DU MENU PRINCIPAL
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche le tableau des médailles olympiques.
        /// Logique métier : Accès rapide aux médailles depuis le menu principal.
        /// </summary>
        static void DisplayMedalStandings()
        {
            var context = ApplicationContext.Instance;

            // Vérification : Des JO doivent exister
            if (!context.CheckInitialized())
                return;

            ConsoleHelper.DisplayTitle("TABLEAU DES MÉDAILLES OLYMPIQUES");

            var olympics = context.CurrentOlympics!;

            // Vérification : Au moins une compétition doit être terminée
            int completed = olympics.Events.FindAll(e => e.IsCompleted()).Count;
            if (completed == 0)
            {
                ConsoleHelper.DisplayWarning("Aucune compétition n'est encore terminée.");
                ConsoleHelper.DisplayInfo("Lancez des compétitions pour voir le tableau des médailles.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            // Affichage du tableau via le service
            context.MedalStandingsService.DisplayStandings(olympics);

            ConsoleHelper.PressKeyToContinue();
        }

        /// <summary>
        /// Affiche les informations sur l'application.
        /// Logique métier : Fournit des informations sur le système et son utilisation.
        /// </summary>
        static void DisplayAbout()
        {
            ConsoleHelper.DisplayTitle("À PROPOS");

            Console.WriteLine("Système de Gestion des Jeux Olympiques d'Hiver");
            Console.WriteLine("Version Interactive 1.0");
            Console.WriteLine();
            ConsoleHelper.DisplaySeparator();
            Console.WriteLine();
            Console.WriteLine("📋 Fonctionnalités :");
            Console.WriteLine("  • Gestion complète des Jeux Olympiques");
            Console.WriteLine("  • Création de sports et disciplines");
            Console.WriteLine("  • Inscription de pays et d'athlètes");
            Console.WriteLine("  • Organisation du calendrier");
            Console.WriteLine("  • Simulation de compétitions");
            Console.WriteLine("  • Calcul automatique des résultats");
            Console.WriteLine("  • Tableau des médailles en temps réel");
            Console.WriteLine();
            Console.WriteLine("🎯 Types de compétitions supportés :");
            Console.WriteLine("  • Chronométrées (ski, bobsleigh...)");
            Console.WriteLine("  • Par points (patinage artistique...)");
            Console.WriteLine("  • En face-à-face (hockey...)");
            Console.WriteLine("  • Par élimination (snowboard cross...)");
            Console.WriteLine();
            Console.WriteLine("💡 Astuce :");
            Console.WriteLine("  Commencez par créer des Jeux Olympiques,");
            Console.WriteLine("  puis ajoutez des sports, des pays et des athlètes.");
            Console.WriteLine();
            ConsoleHelper.DisplaySeparator();
            Console.WriteLine();
            Console.WriteLine("Développé avec C# et .NET");
            Console.WriteLine("Architecture modulaire suivant les principes SOLID");

            ConsoleHelper.PressKeyToContinue();
        }
    }
}
