using System;

namespace TournamentManager.App.Helpers
{
    /// <summary>
    /// Classe utilitaire pour la gestion de l'affichage et de l'interaction console.
    /// Centralise toutes les opérations d'interface utilisateur pour une meilleure maintenabilité.
    /// </summary>
    public static class ConsoleHelper
    {
        // ═══════════════════════════════════════════════════════════
        // AFFICHAGE DE TITRES ET SÉPARATEURS
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche un titre principal avec bordures décoratives.
        /// Logique métier : Standardise l'apparence des écrans principaux.
        /// </summary>
        public static void DisplayTitle(string title)
        {
            Console.Clear();
            Console.WriteLine(" ╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║                   {title.PadRight(56)}                     ║");
            Console.WriteLine(" ╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        /// <summary>
        /// Affiche un sous-titre pour les sections.
        /// Logique métier : Organise visuellement les différentes parties d'un écran.
        /// </summary>
        public static void DisplaySubTitle(string subtitle)
        {
            Console.WriteLine($"\n━━━ {subtitle} ━━━");
        }

        /// <summary>
        /// Affiche une ligne de séparation pour délimiter les sections.
        /// </summary>
        public static void DisplaySeparator()
        {
            Console.WriteLine(new string('─', 65));
        }

        /// <summary>
        /// Affiche une ligne de séparation épaisse pour les sections importantes.
        /// </summary>
        public static void DisplayThickSeparator()
        {
            Console.WriteLine(new string('═', 65));
        }

        // ═══════════════════════════════════════════════════════════
        // MESSAGES ET FEEDBACK UTILISATEUR
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche un message de succès avec icône verte.
        /// Logique métier : Confirme visuellement une opération réussie.
        /// </summary>
        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Affiche un message d'erreur avec icône rouge.
        /// Logique métier : Alerte l'utilisateur d'un problème de manière visible.
        /// </summary>
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Affiche un avertissement avec icône jaune.
        /// Logique métier : Informe l'utilisateur d'une situation nécessitant attention.
        /// </summary>
        public static void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Affiche une information neutre avec icône.
        /// </summary>
        public static void DisplayInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }

        // ═══════════════════════════════════════════════════════════
        // SAISIES UTILISATEUR AVEC VALIDATION
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Demande une saisie texte à l'utilisateur avec prompt personnalisé.
        /// Logique métier : Centralise la lecture d'entrée pour cohérence visuelle.
        /// </summary>
        public static string ReadString(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                // Validation : vérifier si l'entrée est vide
                if (string.IsNullOrWhiteSpace(input) && !allowEmpty)
                {
                    DisplayError("Cette valeur ne peut pas être vide. Veuillez réessayer.");
                    continue;
                }

                return input?.Trim() ?? string.Empty;
            }
        }

        /// <summary>
        /// Demande un nombre entier avec validation automatique.
        /// Logique métier : Empêche les erreurs de saisie et garantit des données valides.
        /// </summary>
        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                // Validation : vérifier si c'est un entier
                if (!int.TryParse(input, out int result))
                {
                    DisplayError("Veuillez entrer un nombre entier valide.");
                    continue;
                }

                // Validation : vérifier la plage
                if (result < min || result > max)
                {
                    DisplayError($"Le nombre doit être entre {min} et {max}.");
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Demande un nombre décimal avec validation.
        /// Logique métier : Utilisé pour les scores, pénalités, etc.
        /// </summary>
        public static double ReadDouble(string prompt, double min = double.MinValue, double max = double.MaxValue)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                // Validation : vérifier si c'est un nombre décimal
                if (!double.TryParse(input, out double result))
                {
                    DisplayError("Veuillez entrer un nombre décimal valide.");
                    continue;
                }

                // Validation : vérifier la plage
                if (result < min || result > max)
                {
                    DisplayError($"Le nombre doit être entre {min} et {max}.");
                    continue;
                }

                return result;
            }
        }

        /// <summary>
        /// Demande une date avec validation de format.
        /// Logique métier : Assure que toutes les dates dans le système sont valides.
        /// </summary>
        public static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (JJ/MM/AAAA): ");
                string? input = Console.ReadLine();

                // Validation : vérifier le format de date
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }

                DisplayError("Format de date invalide. Utilisez JJ/MM/AAAA (ex: 15/02/2026).");
            }
        }

        /// <summary>
        /// Demande une heure avec validation.
        /// Logique métier : Utilisé pour programmer les événements à des heures précises.
        /// </summary>
        public static TimeSpan ReadTime(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (HH:MM): ");
                string? input = Console.ReadLine();

                // Validation : vérifier le format d'heure
                if (TimeSpan.TryParseExact(input, "hh\\:mm", null, out TimeSpan result))
                {
                    return result;
                }

                DisplayError("Format d'heure invalide. Utilisez HH:MM (ex: 14:30).");
            }
        }

        /// <summary>
        /// Demande une confirmation Oui/Non.
        /// Logique métier : Utilisé pour les validations et confirmations d'actions critiques.
        /// </summary>
        public static bool ReadConfirmation(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (O/N): ");
                string? input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "O" || input == "OUI" || input == "Y" || input == "YES")
                    return true;

                if (input == "N" || input == "NON" || input == "NO")
                    return false;

                DisplayError("Veuillez répondre par O (Oui) ou N (Non).");
            }
        }

        // ═══════════════════════════════════════════════════════════
        // MENUS ET NAVIGATION
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Affiche un menu avec options numérotées et retourne le choix.
        /// Logique métier : Standardise tous les menus du système pour une UX cohérente.
        /// </summary>
        public static int DisplayMenu(string title, params string[] options)
        {
            DisplayTitle(title);

            // Afficher toutes les options numérotées
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.WriteLine($"0. 🔙 Retour/Quitter");
            Console.WriteLine();

            // Lire le choix avec validation
            return ReadInt("Votre choix", 0, options.Length);
        }

        /// <summary>
        /// Pause l'exécution jusqu'à ce que l'utilisateur appuie sur Entrée.
        /// Logique métier : Permet à l'utilisateur de lire les informations avant de continuer.
        /// </summary>
        public static void PressKeyToContinue(string message = "Appuyez sur Entrée pour continuer...")
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche un message de chargement/traitement.
        /// Logique métier : Feedback visuel pendant les opérations longues.
        /// </summary>
        public static void DisplayLoading(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"⏳ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Affiche un tableau de données formaté.
        /// Logique métier : Présente les données de manière structurée et lisible.
        /// </summary>
        public static void DisplayTable(string[] headers, string[][] rows)
        {
            // Calculer la largeur de chaque colonne
            int[] columnWidths = new int[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                columnWidths[i] = headers[i].Length;
                foreach (var row in rows)
                {
                    if (i < row.Length && row[i].Length > columnWidths[i])
                        columnWidths[i] = row[i].Length;
                }
            }

            // Afficher l'en-tête
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Ligne de séparation
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(new string('─', columnWidths[i] + 2));
            }
            Console.WriteLine();

            // Afficher les lignes
            foreach (var row in rows)
            {
                for (int i = 0; i < headers.Length && i < row.Length; i++)
                {
                    Console.Write(row[i].PadRight(columnWidths[i] + 2));
                }
                Console.WriteLine();
            }
        }
    }
}
