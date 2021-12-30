using FPLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class NextFixtureSummaryGenerator
    {
        const int SlavenBilicLeagueId = 691446;

        GameInfo _MainGameInfo;
        LeagueInfo _LeagueInfo;
        Fixtures[] _Fixtures;

        List<Fixtures> _NextFixtures;

        bool _openingFixture = false;

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
                Console.WriteLine($"*{GetTeamNameFromId(fixture.TeamH)} vs. {GetTeamNameFromId(fixture.TeamA)}* ({DateTime.Parse(fixture.KickoffTime.ToString()).ToShortTimeString()})");

                foreach (var fplPlayer in _LeagueInfo.Standings.Results) //Needs to look at _LeagueInfo.NewEntries for the first gameweek
                {
                    string shortName = GetShortNameFromId(fplPlayer.Entry);
                    Console.WriteLine($"{shortName}: {GetPlayerList(fplPlayer.Entry, fixture)}");
                }

                Console.WriteLine();
            }

            if (_openingFixture)
            {
                Console.WriteLine();

                Console.WriteLine($"Gameweek {_NextFixtures.First().Event} Transfer Summary");

                foreach (var fplPlayer in _LeagueInfo.Standings.Results)
                {
                    string shortName = GetShortNameFromId(fplPlayer.Entry);
                    Console.WriteLine($"{shortName}: {GetPlayerTransfersForGameweek(fplPlayer.Entry, _NextFixtures.First().Event)}");
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }

        private object GetPlayerTransfersForGameweek(long teamId, long? @event)
        {
            var fplPlayerTransfers = FPLApiAdapter.GetTeamTransfers(teamId);
            StringBuilder playerTransfersString = new StringBuilder();

            List<string> playersIn = new List<string>();
            List<string> playersOut = new List<string>();

            foreach (var transfer in fplPlayerTransfers.Where(t => t.Event == @event))
            {
                var playerInfo = _MainGameInfo.Elements.Where(p => p.Id == transfer.ElementIn).First();
                PlayerNicknameCheck(playerInfo);

                playersIn.Add(playerInfo.WebName);


                var playerOutInfo = _MainGameInfo.Elements.Where(p => p.Id == transfer.ElementOut).First();
                PlayerNicknameCheck(playerOutInfo);

                playersOut.Add(playerOutInfo.WebName);
            }

            playerTransfersString.AppendLine();
            playerTransfersString.Append("IN: ");
            foreach (var player in playersIn)
            {
                playerTransfersString.Append(player);

                if (playersIn.Last() != player)
                    playerTransfersString.Append(", ");
                else
                    playerTransfersString.AppendLine(" ");
            }

            playerTransfersString.Append("OUT: ");
            foreach (var player in playersOut)
            {
                playerTransfersString.Append(player);

                if (playersOut.Last() != player)
                    playerTransfersString.Append(", ");
                else
                    playerTransfersString.Append(" ");
            }

            return playerTransfersString.ToString();
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
                case "Alexander-Arnold":
                    playerInfo.WebName = "TAA";
                    return;
                case "Alisson":
                    playerInfo.WebName = "Thini Alissoj";
                    return;
                case "Alonso":
                    playerInfo.WebName = "Fucking Murderer";
                    return;
                case "Antonio":
                    playerInfo.WebName = "The Mage";
                    return;
                case "Bernardo":
                    playerInfo.WebName = "Banano Silva";
                    return;
                case "Broja":
                    playerInfo.WebName = "Armando 'Do you even lift?' Broja";
                    return;
                case "Calvert-Lewin":
                    playerInfo.WebName = "Goalvert-Lewin";
                    return;
                case "Cancelo":
                    playerInfo.WebName = "Cancelo Culture";
                    return;
                case "Dennis":
                    playerInfo.WebName = "The Menace";
                    return;
                case "Dewsbury-Hall":
                    playerInfo.WebName = "Wedding Venue";
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
                case "Iheanacho":
                    playerInfo.WebName = "Macho Man";
                    return;
                case "Ings":
                    playerInfo.WebName = "Daddy Ings";
                    return;
                case "Lingard":
                    playerInfo.WebName = "Lingod";
                    return;
                case "Lukaku":
                    playerInfo.WebName = "RO-ME-LU";
                    return;
                case "Mbeumo":
                    playerInfo.WebName = "Kinder Mbeumo";
                    return;
                case "Minamino":
                    playerInfo.WebName = "Minaminho";
                    return;
                case "Mount":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Mountain Mase",
                        "Pickey Pount"
                    });
                    return;
                case "Ogbonna":
                    playerInfo.WebName = "Ogbanger";
                    return;
                case "Pogba":
                    playerInfo.WebName = "Pau Pogba";
                    return;
                case "Pulisic":
                    playerInfo.WebName = "NRAmar";
                    return;
                case "Ronaldo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Omicronaldo", 
                        "Penaldo"
                    });
                    return;
                case "Salah":
                    if (DateTime.Now.Month > 11 || DateTime.Now.Month < 3)
                        playerInfo.WebName = "Snow Salah";
                    return;
                case "Townsend":
                    if (playerInfo.FirstName == "Andros")
                        playerInfo.WebName = "Clownsend";
                    return;
                case "Vestergaard":
                    playerInfo.WebName = "Bestergaard";
                    return;
                case "Walcott":
                    playerInfo.WebName = "WalGott";
                    return;
                case "Werner":
                    playerInfo.WebName = "Ronson";
                    return;
                default:
                    return;
            }
        }

        private string PickRandomNickname(List<string> nicknames)
        {
            var rand = new Random();

            return nicknames[rand.Next(nicknames.Count)];
        }

        private string GetShortNameFromId(long id)
        {
            switch (id)
            {
                case 3096301:
                    return "Felix";                
                case 3132245:
                    return "Higgins";
                case 4111347:
                    return "Joe";
                case 1034751:
                    return "Meme";
                case 87787:
                    return "Nodge";
                case 2731988:
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
                    switch (team.Name)
                    {
                        case "Arsenal":
                            return "The Arsenal";
                        case "Aston Villa":
                            return "Stevie G's Claret & Blue Army";
                        case "Chelsea":
                            return "The Tucheliban";
                        case "Norwich":
                            return "~Frank Lampard's~ Norwich City";
                        case "Spurs":
                            return "Lads, it's Tottenham";
                        case "West Ham":
                            return "Did You Just Say 'West Ham'?";
                        default:
                            return team.Name;
                    }
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

                    if (_Fixtures[i].Event == _Fixtures[i-1].Event + 1)
                    {
                        _openingFixture = true;
                    }

                    bool moreFixtures = true;
                    int extraFixtureCount = 1;

                    while (moreFixtures)
                    {
                        if (extraFixtureCount == 10)
                        {
                            moreFixtures = false;
                            continue;
                        }
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
