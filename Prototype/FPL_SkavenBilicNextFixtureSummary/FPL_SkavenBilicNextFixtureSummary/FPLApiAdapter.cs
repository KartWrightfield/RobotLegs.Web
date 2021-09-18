using FPL_SkavenBilicNextFixtureSummary.Classes.External;
using FPLCore;
using System;
using System.Net;

namespace FPL_SkavenBilicNextFixtureSummary
{
    public static class FPLApiAdapter
    {
        public static GameInfo GetMainGameInfo()
        {
            string url = "https://fantasy.premierleague.com/api/bootstrap-static/";
            var jsonData = string.Empty;

            using (var webClient = new WebClient())
            {
                try
                {
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed downloading base FPL JSON object", ex);
                }
            }

            GameInfo gameInfo;

            try
            {
                gameInfo = GameInfo.FromJson(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize base FPL JSON", ex);
            }

            return gameInfo;
        }

        public static LeagueInfo GetLeagueInfo(int leagueId)
        {
            string url = $"https://fantasy.premierleague.com/api/leagues-classic/{leagueId}/standings/";
            var jsonData = string.Empty;

            using (var webClient = new WebClient())
            {
                try
                {
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed downloading league JSON", ex);
                }
            }

            LeagueInfo leagueInfo;

            try
            {
                leagueInfo = LeagueInfo.FromJson(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize league JSON", ex);
            }

            return leagueInfo;
        }

        public static TeamGameweekSelections GetGameweekSelections(long teamId, long gameweekId)
        {
            string url = $"https://fantasy.premierleague.com/api/entry/{teamId}/event/{gameweekId}/picks/";
            var jsonData = string.Empty;

            using (var webClient = new WebClient())
            {
                try
                {
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed downloading team gameweek JSON", ex);
                }
            }

            TeamGameweekSelections teamGameweekSelections;

            try
            {
                teamGameweekSelections = TeamGameweekSelections.FromJson(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize team gameweek JSON", ex);
            }

            return teamGameweekSelections;
        }

        public static TeamTransfers[] GetTeamTransfers(long teamId)
        {
            string url = $"https://fantasy.premierleague.com/api/entry/{teamId}/transfers/";
            var jsonData = string.Empty;

            using (var webClient = new WebClient())
            {
                try
                {
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed downloading team gameweek JSON", ex);
                }
            }

            TeamTransfers[] teamTransfers;

            try
            {
                teamTransfers = TeamTransfers.FromJson(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize team gameweek JSON", ex);
            }

            return teamTransfers;
        }

        public static Fixtures[] GetFixtures()
        {
            string url = "https://fantasy.premierleague.com/api/fixtures/";
            var jsonData = string.Empty;

            using (var webClient = new WebClient())
            {
                try
                {
                    jsonData = webClient.DownloadString(url);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed downloading fixture JSON", ex);
                }
            }

            Fixtures[] fixtures;

            try
            {
                fixtures = Fixtures.FromJson(jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize fixture JSON", ex);
            }

            return fixtures;
        }
    }
}
