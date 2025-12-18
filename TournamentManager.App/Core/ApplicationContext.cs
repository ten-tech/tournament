using TournamentManager.Models;
using TournamentManager.Services;

namespace TournamentManager.App.Core
{
    /// <summary>
    /// Contexte global de l'application contenant tous les services et l'état actuel.
    /// Logique métier : Pattern Singleton pour partager l'état entre tous les modules
    /// et éviter de passer les services en paramètre partout.
    /// </summary>
    public class ApplicationContext
    {
        // ═══════════════════════════════════════════════════════════
        // INSTANCE SINGLETON
        // ═══════════════════════════════════════════════════════════

        private static ApplicationContext? _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Obtient l'instance unique du contexte applicatif.
        /// Logique métier : Thread-safe singleton pour garantir une seule instance.
        /// </summary>
        public static ApplicationContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ApplicationContext();
                        }
                    }
                }
                return _instance;
            }
        }

        // ═══════════════════════════════════════════════════════════
        // SERVICES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Service de gestion des Jeux Olympiques.
        /// Logique métier : Gère la création et la configuration des événements olympiques.
        /// </summary>
        public IOlympicGameService OlympicService { get; private set; }

        /// <summary>
        /// Service de gestion des sports et disciplines.
        /// Logique métier : Gère le catalogue des sports disponibles.
        /// </summary>
        public ISportService SportService { get; private set; }

        /// <summary>
        /// Service de calcul des scores.
        /// Logique métier : Centralise la logique de calcul pour tous les types de compétitions.
        /// </summary>
        public IScoringService ScoringService { get; private set; }

        /// <summary>
        /// Service de gestion des compétitions.
        /// Logique métier : Orchestre le déroulement des événements et l'enregistrement des performances.
        /// </summary>
        public ICompetitionService CompetitionService { get; private set; }

        /// <summary>
        /// Service de gestion du calendrier.
        /// Logique métier : Organise les événements dans le temps et évite les conflits.
        /// </summary>
        public IScheduleService ScheduleService { get; private set; }

        /// <summary>
        /// Service de gestion du tableau des médailles.
        /// Logique métier : Maintient le classement des pays en temps réel.
        /// </summary>
        public IMedalStandingsService MedalStandingsService { get; private set; }

        // ═══════════════════════════════════════════════════════════
        // ÉTAT ACTUEL DE L'APPLICATION
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Les Jeux Olympiques actuellement gérés.
        /// Logique métier : Null si aucun JO n'est créé, sinon référence aux JO actifs.
        /// </summary>
        public WinterOlympics? CurrentOlympics { get; set; }

        /// <summary>
        /// Indique si l'application est initialisée avec des JO.
        /// Logique métier : Utilisé pour vérifier si les fonctionnalités nécessitant des JO sont disponibles.
        /// </summary>
        public bool IsInitialized => CurrentOlympics != null;

        // ═══════════════════════════════════════════════════════════
        // CONSTRUCTEUR PRIVÉ (SINGLETON)
        // ═══════════════════════════════════════════════════════════

        private ApplicationContext()
        {
            // Initialisation des services avec injection de dépendances
            // Logique métier : Les services sont créés une seule fois au démarrage
            OlympicService = new OlympicGameService();
            SportService = new SportService();
            ScoringService = new ScoringService();
            CompetitionService = new CompetitionService(ScoringService);
            ScheduleService = new ScheduleService();
            MedalStandingsService = new MedalStandingsService();

            // État initial : aucun JO créé
            CurrentOlympics = null;
        }

        // ═══════════════════════════════════════════════════════════
        // MÉTHODES UTILITAIRES
        // ═══════════════════════════════════════════════════════════

        /// <summary>
        /// Réinitialise le contexte à son état initial.
        /// Logique métier : Permet de recommencer à zéro sans redémarrer l'application.
        /// Utilisé pour "Nouvelle session" ou après sauvegarde.
        /// </summary>
        public void Reset()
        {
            // Recréer les services pour effacer toutes les données en mémoire
            OlympicService = new OlympicGameService();
            SportService = new SportService();
            ScoringService = new ScoringService();
            CompetitionService = new CompetitionService(ScoringService);
            ScheduleService = new ScheduleService();
            MedalStandingsService = new MedalStandingsService();

            // Réinitialiser l'état
            CurrentOlympics = null;
        }

        /// <summary>
        /// Vérifie si l'application est prête pour une opération nécessitant des JO.
        /// Logique métier : Empêche les erreurs en vérifiant que les prérequis sont remplis.
        /// </summary>
        public bool CheckInitialized()
        {
            if (!IsInitialized)
            {
                Helpers.ConsoleHelper.DisplayError("Vous devez d'abord créer des Jeux Olympiques.");
                Helpers.ConsoleHelper.PressKeyToContinue();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Obtient les JO actuels avec vérification.
        /// Logique métier : Retourne les JO ou null si non initialisés,
        /// avec message d'erreur automatique.
        /// </summary>
        public WinterOlympics? GetCurrentOlympicsOrWarn()
        {
            if (!IsInitialized)
            {
                Helpers.ConsoleHelper.DisplayError("Aucun Jeux Olympiques n'est actuellement créé.");
                Helpers.ConsoleHelper.PressKeyToContinue();
                return null;
            }
            return CurrentOlympics;
        }

        /// <summary>
        /// Met à jour les JO actuels.
        /// Logique métier : Centralise la mise à jour pour garantir la cohérence.
        /// </summary>
        public void SetCurrentOlympics(WinterOlympics olympics)
        {
            CurrentOlympics = olympics;
        }
    }
}
