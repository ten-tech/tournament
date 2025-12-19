using System;
using System.Collections.Generic;
using TournamentManager.Models;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de calcul des scores et classements.
    /// </summary>
    public interface IScoringService
    {
        double CalculateJudgedScore(List<double> judgeScores, bool removeExtremes = true);
        TimeSpan CalculateFinalTime(TimeSpan baseTime, double penaltySeconds = 0);
        List<Result> RankResults(List<Result> results, string scoringMethod);
        void AssignMedals(List<Result> rankedResults);
        bool ValidatePerformance(Result result, string scoringMethod);
    }
}
