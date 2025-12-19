using System;
using System.Collections.Generic;
using System.Linq;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Service de calcul des scores et classements olympiques.
    /// </summary>
    public class ScoringService : IScoringService
    {
        public double CalculateJudgedScore(List<double> judgeScores, bool removeExtremes = true)
        {
            if (judgeScores == null || judgeScores.Count == 0)
            {
                Console.WriteLine("✗ Aucun score de juge fourni");
                return 0;
            }

            var scores = new List<double>(judgeScores);

            // Enlever les notes extrêmes (la plus haute et la plus basse)
            if (removeExtremes && scores.Count >= 3)
            {
                scores.Remove(scores.Max());
                scores.Remove(scores.Min());
            }

            // Calculer la moyenne
            double average = scores.Average();
            Console.WriteLine($"✓ Score calculé: {average:F2} (basé sur {scores.Count} notes)");
            return average;
        }

        public TimeSpan CalculateFinalTime(TimeSpan baseTime, double penaltySeconds = 0)
        {
            double totalSeconds = baseTime.TotalSeconds + penaltySeconds;
            var finalTime = TimeSpan.FromSeconds(totalSeconds);

            if (penaltySeconds > 0)
            {
                Console.WriteLine($"✓ Temps final: {finalTime:mm\\:ss\\.ff} (pénalité: +{penaltySeconds}s)");
            }

            return finalTime;
        }

        public List<Result> RankResults(List<Result> results, string scoringMethod)
        {
            if (results == null || results.Count == 0)
            {
                return new List<Result>();
            }

            // Filtrer les disqualifications
            var validResults = results.Where(r => !r.IsDisqualified).ToList();
            var disqualifiedResults = results.Where(r => r.IsDisqualified).ToList();

            List<Result> ranked;

            switch (scoringMethod.ToLower())
            {
                case "besttime":
                case "fastesttime":
                    // Pour les compétitions chronométrées: le temps le plus court gagne
                    ranked = validResults
                        .OrderBy(r => r.Time?.TotalSeconds + (r.Penalty ?? 0))
                        .ToList();
                    break;

                case "totalscore":
                case "highestscore":
                    // Pour les compétitions par points: le score le plus élevé gagne
                    ranked = validResults
                        .OrderByDescending(r => (r.Score ?? 0) - (r.Penalty ?? 0))
                        .ToList();
                    break;

                case "averagescore":
                    // Similaire à totalscore
                    ranked = validResults
                        .OrderByDescending(r => (r.Score ?? 0) - (r.Penalty ?? 0))
                        .ToList();
                    break;

                default:
                    Console.WriteLine($"⚠ Méthode de notation inconnue: {scoringMethod}, utilisation du classement par temps");
                    ranked = validResults
                        .OrderBy(r => r.Time?.TotalSeconds ?? double.MaxValue)
                        .ToList();
                    break;
            }

            // Assigner les rangs
            for (int i = 0; i < ranked.Count; i++)
            {
                ranked[i].SetRank(i + 1);
            }

            // Ajouter les disqualifiés à la fin
            ranked.AddRange(disqualifiedResults);

            Console.WriteLine($"✓ {ranked.Count} résultats classés ({disqualifiedResults.Count} disqualifications)");
            return ranked;
        }

        public void AssignMedals(List<Result> rankedResults)
        {
            if (rankedResults == null || rankedResults.Count == 0)
            {
                return;
            }

            foreach (var result in rankedResults)
            {
                if (result.IsDisqualified)
                {
                    result.Medal = MedalType.None;
                    continue;
                }

                switch (result.Rank)
                {
                    case 1:
                        result.Medal = MedalType.Gold;
                        Console.WriteLine($"🥇 Médaille d'or: {result.Athlete.GetFullName()} ({result.Athlete.CountryCode})");
                        break;
                    case 2:
                        result.Medal = MedalType.Silver;
                        Console.WriteLine($"🥈 Médaille d'argent: {result.Athlete.GetFullName()} ({result.Athlete.CountryCode})");
                        break;
                    case 3:
                        result.Medal = MedalType.Bronze;
                        Console.WriteLine($"🥉 Médaille de bronze: {result.Athlete.GetFullName()} ({result.Athlete.CountryCode})");
                        break;
                }
            }
        }

        public bool ValidatePerformance(Result result, string scoringMethod)
        {
            if (result.IsDisqualified)
            {
                return false;
            }

            switch (scoringMethod.ToLower())
            {
                case "besttime":
                case "fastesttime":
                    if (!result.Time.HasValue || result.Time.Value.TotalSeconds <= 0)
                    {
                        Console.WriteLine($"✗ Temps invalide pour {result.Athlete.GetFullName()}");
                        return false;
                    }
                    // Vérifier un temps maximum raisonnable (ex: 10 minutes pour la plupart des épreuves)
                    if (result.Time.Value.TotalMinutes > 10)
                    {
                        Console.WriteLine($"⚠ Temps suspect pour {result.Athlete.GetFullName()}: {result.Time.Value}");
                    }
                    break;

                case "totalscore":
                case "averagescore":
                case "highestscore":
                    if (!result.Score.HasValue)
                    {
                        Console.WriteLine($"✗ Score manquant pour {result.Athlete.GetFullName()}");
                        return false;
                    }
                    // Les scores négatifs peuvent être valides dans certains sports
                    break;

                default:
                    Console.WriteLine($"⚠ Méthode de notation non reconnue: {scoringMethod}");
                    break;
            }

            return true;
        }
    }
}
