using System;
using System.Linq;
using TournamentManager.Models;

namespace TournamentManager.App.Helpers
{
    /// <summary>
    /// Classe de validation des entrées utilisateur.
    /// Logique métier : Centralise toutes les règles de validation pour assurer la cohérence
    /// et l'intégrité des données dans tout le système.
    /// </summary>
    public static class InputValidator
    {
        // ═══════════════════════════════════════════════════════════
        // VALIDATION DES DONNÉES OLYMPIQUES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide que les dates de début et fin des JO sont cohérentes.
        /// Logique métier : La date de fin doit être après la date de début,
        /// et la durée doit être raisonnable (entre 5 et 30 jours).
        /// </summary>
        public static bool ValidateOlympicDates(DateTime startDate, DateTime endDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : date de fin après date de début
            if (endDate <= startDate)
            {
                errorMessage = "La date de fin doit être après la date de début.";
                return false;
            }

            // Validation : durée raisonnable (5 à 30 jours)
            var duration = (endDate - startDate).Days;
            if (duration < 5)
            {
                errorMessage = "Les Jeux Olympiques doivent durer au moins 5 jours.";
                return false;
            }

            if (duration > 30)
            {
                errorMessage = "Les Jeux Olympiques ne peuvent pas durer plus de 30 jours.";
                return false;
            }

            // Validation : dates dans le futur (ou pas trop anciennes)
            if (startDate.Year < DateTime.Now.Year - 10)
            {
                errorMessage = "Les dates semblent trop anciennes. Vérifiez l'année.";
                return false;
            }

            return true;
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDATION DES ATHLÈTES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide l'âge d'un athlète.
        /// Logique métier : Les athlètes olympiques ont typiquement entre 15 et 60 ans.
        /// Règles spéciales peuvent s'appliquer selon les sports.
        /// </summary>
        public static bool ValidateAthleteAge(int age, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (age < 15)
            {
                errorMessage = "L'âge minimum pour participer aux Jeux Olympiques est 15 ans.";
                return false;
            }

            if (age > 60)
            {
                errorMessage = "L'âge semble suspect. Vérifiez la valeur saisie.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valide un numéro de dossard.
        /// Logique métier : Les dossards sont des nombres positifs uniques,
        /// généralement entre 1 et 999.
        /// </summary>
        public static bool ValidateBibNumber(int bibNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (bibNumber <= 0)
            {
                errorMessage = "Le numéro de dossard doit être positif.";
                return false;
            }

            if (bibNumber > 999)
            {
                errorMessage = "Le numéro de dossard doit être inférieur à 1000.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valide qu'un athlète peut participer à une discipline.
        /// Logique métier : Vérifie la compatibilité de genre et l'inscription au sport.
        /// </summary>
        public static bool ValidateAthleteForDiscipline(Athlete athlete, Discipline discipline, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : compatibilité de genre
            if (discipline.Gender != Models.Enums.Gender.Mixed && athlete.Gender != discipline.Gender)
            {
                errorMessage = $"L'athlète {athlete.GetFullName()} ne peut pas participer à cette discipline (genre incompatible).";
                return false;
            }

            // Validation : inscription au sport
            if (!athlete.SportIds.Contains(discipline.SportId))
            {
                errorMessage = $"L'athlète {athlete.GetFullName()} n'est pas inscrit dans ce sport.";
                return false;
            }

            return true;
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDATION DES PERFORMANCES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide un temps de performance pour les compétitions chronométrées.
        /// Logique métier : Les temps doivent être positifs et dans une plage réaliste.
        /// </summary>
        public static bool ValidateTime(TimeSpan time, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : temps positif
            if (time.TotalSeconds <= 0)
            {
                errorMessage = "Le temps doit être positif.";
                return false;
            }

            // Validation : temps raisonnable (moins de 30 minutes pour la plupart des épreuves)
            if (time.TotalMinutes > 30)
            {
                errorMessage = "Le temps semble suspect. Vérifiez la valeur (format mm:ss.cc).";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valide un score pour les compétitions notées.
        /// Logique métier : Les scores sont généralement entre 0 et 10 pour les sports jugés.
        /// </summary>
        public static bool ValidateScore(double score, out string errorMessage, double min = 0, double max = 10)
        {
            errorMessage = string.Empty;

            if (score < min || score > max)
            {
                errorMessage = $"Le score doit être entre {min} et {max}.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valide une liste de notes de jury.
        /// Logique métier : Doit avoir un nombre minimum de juges (généralement 5 ou 9)
        /// et toutes les notes doivent être dans la plage valide.
        /// </summary>
        public static bool ValidateJudgeScores(double[] scores, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : nombre minimum de juges
            if (scores.Length < 3)
            {
                errorMessage = "Il faut au moins 3 juges.";
                return false;
            }

            // Validation : chaque note doit être valide
            foreach (var score in scores)
            {
                if (!ValidateScore(score, out string scoreError, 0, 10))
                {
                    errorMessage = $"Note invalide : {scoreError}";
                    return false;
                }
            }

            return true;
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDATION DES ÉVÉNEMENTS
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide qu'un événement peut être programmé à une date/heure donnée.
        /// Logique métier : L'événement doit être pendant la période des JO.
        /// </summary>
        public static bool ValidateEventDate(DateTime eventDate, WinterOlympics olympics, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : événement pendant les JO
            if (eventDate.Date < olympics.StartDate.Date || eventDate.Date > olympics.EndDate.Date)
            {
                errorMessage = $"L'événement doit avoir lieu pendant les Jeux Olympiques ({olympics.StartDate.ToShortDateString()} - {olympics.EndDate.ToShortDateString()}).";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valide la capacité d'un lieu.
        /// Logique métier : Les venues olympiques ont généralement une capacité
        /// entre 1,000 et 100,000 spectateurs.
        /// </summary>
        public static bool ValidateVenueCapacity(int capacity, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (capacity < 100)
            {
                errorMessage = "La capacité minimale d'un lieu olympique est de 100 places.";
                return false;
            }

            if (capacity > 100000)
            {
                errorMessage = "La capacité semble trop élevée. Vérifiez la valeur.";
                return false;
            }

            return true;
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDATION DES CODES PAYS
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide un code pays olympique (format ISO 3 lettres).
        /// Logique métier : Les codes pays doivent être exactement 3 lettres majuscules (ex: FRA, USA, CAN).
        /// </summary>
        public static bool ValidateCountryCode(string code, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Validation : longueur exacte de 3 caractères
            if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
            {
                errorMessage = "Le code pays doit contenir exactement 3 lettres (ex: FRA, USA, CAN).";
                return false;
            }

            // Validation : seulement des lettres
            if (!code.All(char.IsLetter))
            {
                errorMessage = "Le code pays ne doit contenir que des lettres.";
                return false;
            }

            return true;
        }

        // ═══════════════════════════════════════════════════════════
        // VALIDATION GÉNÉRALE
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Valide qu'une chaîne n'est pas vide et a une longueur raisonnable.
        /// Logique métier : Empêche les valeurs vides et les chaînes trop longues
        /// qui pourraient causer des problèmes d'affichage.
        /// </summary>
        public static bool ValidateNonEmptyString(string value, string fieldName, out string errorMessage, int maxLength = 100)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                errorMessage = $"{fieldName} ne peut pas être vide.";
                return false;
            }

            if (value.Length > maxLength)
            {
                errorMessage = $"{fieldName} ne peut pas dépasser {maxLength} caractères.";
                return false;
            }

            return true;
        }
    }
}
