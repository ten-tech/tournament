using System;
using TournamentManager.App.Core;
using TournamentManager.App.Helpers;
using TournamentManager.Models.Enums;

namespace TournamentManager.App.Modules
{
    /// <summary>
    /// Module de gestion interactive des sports et disciplines.
    /// Logique métier : Gère le catalogue des sports disponibles pour les JO.
    /// </summary>
    public static class SportsModule
    {
        private static ApplicationContext Context => ApplicationContext.Instance;

        // ═══════════════════════════════════════════════════════════
        // MENU PRINCIPAL
        // ═══════════════════════════════════════════════════════════

        public static void ShowMenu()
        {
            while (true)
            {
                int choice = ConsoleHelper.DisplayMenu(
                    "GESTION DES SPORTS ET DISCIPLINES",
                    "➕ Créer un nouveau sport",
                    "➕ Ajouter une discipline à un sport",
                    "📋 Voir tous les sports",
                    "🔍 Voir les disciplines d'un sport"
                );

                switch (choice)
                {
                    case 1:
                        CreateSport();
                        break;
                    case 2:
                        AddDiscipline();
                        break;
                    case 3:
                        DisplayAllSports();
                        break;
                    case 4:
                        DisplaySportDisciplines();
                        break;
                    case 0:
                        return;
                }
            }
        }

        // ═══════════════════════════════════════════════════════════
        // CRÉATION DE SPORT
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Processus de création d'un sport.
        /// Logique métier : Collecte les informations et crée un sport dans le catalogue.
        /// </summary>
        private static void CreateSport()
        {
            ConsoleHelper.DisplayTitle("CRÉATION D'UN SPORT");

            // Nom du sport
            string name = ConsoleHelper.ReadString("Nom du sport (ex: Ski Alpin, Hockey sur Glace)");

            // Type de compétition avec menu
            ConsoleHelper.DisplaySubTitle("Type de compétition");
            Console.WriteLine("1. ⏱️  Chronométrée (meilleur temps gagne)");
            Console.WriteLine("2. 🎯 Par points/score (jury, meilleur score gagne)");
            Console.WriteLine("3. 🏒 En face-à-face (matchs avec scores)");
            Console.WriteLine("4. 🏁 Par élimination (tournoi à élimination)");
            Console.WriteLine("5. 🔀 Mixte (combinaison de formats)");
            Console.WriteLine();

            int typeChoice = ConsoleHelper.ReadInt("Choisissez le type", 1, 5);

            CompetitionType competitionType = typeChoice switch
            {
                1 => CompetitionType.Timed,
                2 => CompetitionType.Scored,
                3 => CompetitionType.HeadToHead,
                4 => CompetitionType.Elimination,
                5 => CompetitionType.Mixed,
                _ => CompetitionType.Timed
            };

            // Description optionnelle
            string description = ConsoleHelper.ReadString("Description (optionnel, Entrée pour passer)", allowEmpty: true);

            // Création du sport
            var sport = Context.SportService.CreateSport(name, competitionType, description);

            // Ajout aux JO si initialisés
            if (Context.IsInitialized)
            {
                Context.OlympicService.AddSport(Context.CurrentOlympics!.Id, sport);
                ConsoleHelper.DisplaySuccess($"Sport \"{name}\" ajouté aux Jeux Olympiques.");
            }

            ConsoleHelper.PressKeyToContinue();
        }

        // ═══════════════════════════════════════════════════════════
        // AJOUT DE DISCIPLINE
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Ajoute une discipline à un sport existant.
        /// Logique métier : Les disciplines sont les épreuves spécifiques d'un sport.
        /// </summary>
        private static void AddDiscipline()
        {
            ConsoleHelper.DisplayTitle("AJOUT D'UNE DISCIPLINE");

            var sports = Context.SportService.GetAllSports();

            if (sports.Count == 0)
            {
                ConsoleHelper.DisplayError("Aucun sport n'existe. Créez d'abord un sport.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            // Sélection du sport
            ConsoleHelper.DisplaySubTitle("Sélectionnez le sport");
            for (int i = 0; i < sports.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sports[i].Name} ({sports[i].CompetitionType})");
            }

            int sportChoice = ConsoleHelper.ReadInt("Numéro du sport", 1, sports.Count);
            var selectedSport = sports[sportChoice - 1];

            // Informations de la discipline
            Console.WriteLine();
            string disciplineName = ConsoleHelper.ReadString($"Nom de la discipline dans {selectedSport.Name}");

            // Catégorie de genre
            ConsoleHelper.DisplaySubTitle("Catégorie");
            Console.WriteLine("1. 👨 Hommes");
            Console.WriteLine("2. 👩 Femmes");
            Console.WriteLine("3. 👥 Mixte");
            int genderChoice = ConsoleHelper.ReadInt("Choisissez la catégorie", 1, 3);

            Gender gender = genderChoice switch
            {
                1 => Gender.Male,
                2 => Gender.Female,
                3 => Gender.Mixed,
                _ => Gender.Male
            };

            // Nombre maximum de participants
            int maxParticipants = ConsoleHelper.ReadInt("Nombre maximum de participants", 1, 200);

            // Manches multiples
            bool hasMultipleRounds = ConsoleHelper.ReadConfirmation("Cette discipline a-t-elle plusieurs manches ?");

            // Méthode de notation selon le type de compétition
            string scoringMethod = selectedSport.CompetitionType == CompetitionType.Timed ? "BestTime" : "TotalScore";

            // Création de la discipline
            var discipline = Context.SportService.CreateDiscipline(
                disciplineName,
                selectedSport.Id,
                gender,
                maxParticipants,
                hasMultipleRounds,
                scoringMethod
            );

            Context.SportService.AddDisciplineToSport(selectedSport, discipline);

            ConsoleHelper.PressKeyToContinue();
        }

        // ═══════════════════════════════════════════════════════════
        // AFFICHAGE
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche tous les sports avec leurs statistiques.
        /// </summary>
        private static void DisplayAllSports()
        {
            ConsoleHelper.DisplayTitle("LISTE DES SPORTS");

            var sports = Context.SportService.GetAllSports();

            if (sports.Count == 0)
            {
                ConsoleHelper.DisplayWarning("Aucun sport créé pour le moment.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            foreach (var sport in sports)
            {
                Console.WriteLine($"\n🎿 {sport.Name}");
                Console.WriteLine($"   Type: {sport.CompetitionType}");
                Console.WriteLine($"   Disciplines: {sport.Disciplines.Count}");
                if (!string.IsNullOrEmpty(sport.Description))
                {
                    Console.WriteLine($"   Description: {sport.Description}");
                }
            }

            ConsoleHelper.PressKeyToContinue();
        }

        /// <summary>
        /// Affiche les disciplines d'un sport sélectionné.
        /// </summary>
        private static void DisplaySportDisciplines()
        {
            var sports = Context.SportService.GetAllSports();

            if (sports.Count == 0)
            {
                ConsoleHelper.DisplayError("Aucun sport disponible.");
                ConsoleHelper.PressKeyToContinue();
                return;
            }

            ConsoleHelper.DisplayTitle("DISCIPLINES D'UN SPORT");

            for (int i = 0; i < sports.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sports[i].Name}");
            }

            int choice = ConsoleHelper.ReadInt("Numéro du sport", 1, sports.Count);
            var sport = sports[choice - 1];

            ConsoleHelper.DisplayTitle($"DISCIPLINES - {sport.Name}");

            if (sport.Disciplines.Count == 0)
            {
                ConsoleHelper.DisplayWarning("Aucune discipline dans ce sport.");
            }
            else
            {
                foreach (var discipline in sport.Disciplines)
                {
                    Console.WriteLine($"\n📋 {discipline.Name}");
                    Console.WriteLine($"   Catégorie: {discipline.Gender}");
                    Console.WriteLine($"   Max participants: {discipline.MaxParticipants}");
                    Console.WriteLine($"   Manches multiples: {(discipline.HasMultipleRounds ? "Oui" : "Non")}");
                    Console.WriteLine($"   Méthode de notation: {discipline.ScoringMethod}");
                }
            }

            ConsoleHelper.PressKeyToContinue();
        }
    }
}
