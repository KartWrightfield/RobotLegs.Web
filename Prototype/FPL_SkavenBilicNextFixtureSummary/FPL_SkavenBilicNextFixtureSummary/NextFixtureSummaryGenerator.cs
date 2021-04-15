using FPLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class NextFixtureSummaryGenerator
    {
        const int SlavenBilicLeagueId = 14434;

        GameInfo _MainGameInfo;
        LeagueInfo _LeagueInfo;
        Fixtures[] _Fixtures;

        List<Fixtures> _NextFixtures;

        public NextFixtureSummaryGenerator()
        {
            _MainGameInfo = FPLApiAdapter.GetMainGameInfo();
            _LeagueInfo = FPLApiAdapter.GetLeagueInfo(SlavenBilicLeagueId);
            _Fixtures = FPLApiAdapter.GetFixtures();
        }

        public void Start()
        {
            FindNextFixtures();

            foreach (var fixture in _NextFixtures)
            {
                Console.WriteLine($"*{GetTeamNameFromId(fixture.TeamH)} vs. {GetTeamNameFromId(fixture.TeamA)}*");

                foreach (var fplPlayer in _LeagueInfo.Standings.Results)
                {
                    string shortName = GetShortNameFromId(fplPlayer.Entry);
                    Console.WriteLine($"{shortName}: {GetPlayerList(fplPlayer.Entry, fixture)}");
                }

                Console.WriteLine();
            }            

            Console.ReadLine();
        }

        private string GetPlayerList(long teamId, Fixtures fixture)
        {
            var fplPlayerGameweekPicks = FPLApiAdapter.GetGameweekSelections(teamId, (long)fixture.Event);
            List<string> first11Players = new List<string>();
            List<string> benchPlayers = new List<string>();
            StringBuilder playerString = new StringBuilder();
            bool hasPlayersOnBench = false;

            foreach (var playerSelected in fplPlayerGameweekPicks.Picks)
            {
                var playerInfo = _MainGameInfo.Elements.Where(p => p.Id == playerSelected.Element).First();

                if (playerInfo.Team == fixture.TeamA || playerInfo.Team == fixture.TeamH)
                {
                    PlayerNicknameCheck(playerInfo);

                    if (playerSelected.Position < 12)
                    {
                        if (playerSelected.IsCaptain)
                        {
                            if (playerSelected.Multiplier == 3)
                                first11Players.Add(playerInfo.WebName + " (*3xc*)");
                            else
                                first11Players.Add(playerInfo.WebName + " (c)");
                        }
                        else if (playerSelected.IsViceCaptain)
                            first11Players.Add(playerInfo.WebName + " (v)");
                        else
                            first11Players.Add(playerInfo.WebName);
                    }
                    else
                    {
                        hasPlayersOnBench = true;

                        if (playerSelected.IsCaptain)
                            benchPlayers.Add(playerInfo.WebName + " (c)");
                        else if (playerSelected.IsViceCaptain)
                            benchPlayers.Add(playerInfo.WebName + " (v)");
                        else
                            benchPlayers.Add(playerInfo.WebName);
                    }
                }
            }

            foreach (var player in first11Players)
            {
                playerString.Append(player);

                if (first11Players.Last() != player)
                    playerString.Append(", ");
                else
                    playerString.Append(" ");
            }

            if (hasPlayersOnBench)
            {
                playerString.Append("(bench: ");

                foreach (var player in benchPlayers)
                {
                    playerString.Append(player);

                    if (benchPlayers.Last() != player)
                        playerString.Append(", ");
                }

                playerString.Append(")");
            }

            return playerString.ToString();
        }

        private void PlayerNicknameCheck(Element playerInfo)
        {
            switch(playerInfo.WebName)
            {
                case "Antonio":
                    playerInfo.WebName = "The Mage";
                    return;
                case "Calvert-Lewin":
                    playerInfo.WebName = "Goalvert-Lewin";
                    return;
                case "Cancelo":
                    playerInfo.WebName = "Cancelo Culture";
                    return;
                case "El Ghazi":
                    playerInfo.WebName = "The Warrior";
                    return;
                case "Fernandes":
                    playerInfo.WebName = "Penandes";
                    return;
                case "Firmino":
                    playerInfo.WebName = "Firminho";
                    return;
                case "Ings":
                    playerInfo.WebName = "Daddy Ings";
                    return;
                case "Lingard":
                    playerInfo.WebName = "Lingod";
                    return;
                case "Minamino":
                    playerInfo.WebName = "Minaminho";
                    return;
                case "Ogbonna":
                    playerInfo.WebName = "Ogbanger";
                    return;
                case "Pulisic":
                    playerInfo.WebName = "NRAmar";
                    return;
                case "Townsend":
                    playerInfo.WebName = "Clownsend";
                    return;
                case "Vestergaard":
                    playerInfo.WebName = "Bestergaard";
                    return;
                case "Walcott":
                    playerInfo.WebName = "WalGott";
                    return;
                default:
                    return;
            }
        }

        private string GetShortNameFromId(long id)
        {
            switch (id)
            {
                case 72935:
                    return "Felix";                
                case 526187:
                    return "Higgins";
                case 3221875:
                    return "Joe";
                case 1025894:
                    return "Meme";
                case 78795:
                    return "Nodge";
                case 201866:
                    return "Sam";                
            }

            throw new Exception("Player ID not recognised!");
        }

        private string GetTeamNameFromId(long teamId)
        {
            foreach (var team in _MainGameInfo.Teams)
            {
                if (teamId == team.Id)
                {
                    return team.Name;
                }
            }

            throw new Exception("Team ID not found");
        }

        private void FindNextFixtures()
        {
            _NextFixtures = new List<Fixtures>();

            for (int i = 0; i < _Fixtures.Length; i++)
            {
                if (_Fixtures[i].KickoffTime > DateTime.Now.AddMinutes(0))
                { 
                    _NextFixtures.Add(_Fixtures[i]);

                    bool moreFixtures = true;
                    int extraFixtureCount = 1;

                    while (moreFixtures)
                    {
                        if (_Fixtures[i + extraFixtureCount].KickoffTime == null)
                        {
                            moreFixtures = false;
                        }
                        else if (_Fixtures[i + extraFixtureCount].KickoffTime == _Fixtures[i].KickoffTime)
                        {
                            _NextFixtures.Add(_Fixtures[i + extraFixtureCount]);
                            extraFixtureCount++;
                        }
                        else
                        {
                            moreFixtures = false;
                        }
                    }

                    return;
                }
            }
        }
    }
}
