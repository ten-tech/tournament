using System.Collections.Generic;
using TournamentManager.Models;
using TournamentManager.Models.Enums;

namespace TournamentManager.Services
{
    /// <summary>
    /// Interface pour le service de gestion du tableau des médailles.
    /// </summary>
    public interface IMedalStandingsService
    {
        void UpdateStandings(WinterOlympics olympics);
        MedalStandings GetCurrentStandings(WinterOlympics olympics);
        CountryMedalCount GetCountryStanding(WinterOlympics olympics, string countryCode);
        List<CountryMedalCount> GetTopCountries(WinterOlympics olympics, int count = 10);
        void DisplayStandings(WinterOlympics olympics);
        Dictionary<string, int> GetMedalsBySport(WinterOlympics olympics, string countryCode);
    }
}
