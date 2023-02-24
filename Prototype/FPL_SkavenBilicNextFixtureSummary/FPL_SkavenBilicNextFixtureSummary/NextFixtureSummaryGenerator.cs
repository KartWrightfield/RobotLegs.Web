using FPLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class NextFixtureSummaryGenerator
    {
        const int SlavenBilicLeagueId = 204960;

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
            switch (playerInfo.WebName)
            {
                case "Adams":
                    if (playerInfo.FirstName == "Che")
                        playerInfo.WebName = "Che Guevadams";
                    return;
                case "Aguerd":
                    playerInfo.WebName = "Aguero";
                    return;
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
                case "Aribo":
                    playerInfo.WebName = "Starmix";
                    return;
                case "Arthur":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "King Arthur",
                        "Marsh Melo"
                    });
                    return;
                case "Bernardo":
                    playerInfo.WebName = "Banano Silva";
                    return;
                case "Botman":
                    playerInfo.WebName = "Sven Bossman";
                    return;
                case "Brobbey":
                    playerInfo.WebName = "Mr Brobbey";
                    return;
                case "Broja":
                    playerInfo.WebName = "Do you even lift Broja?";
                    return;
                case "Burn":
                    playerInfo.WebName = "Father Dick Byrne";
                    return;
                case "Calvert-Lewin":
                    playerInfo.WebName = "Goalvert-Lewin";
                    return;
                case "Cancelo":
                    playerInfo.WebName = "Cancelo Culture";
                    return;
                case "Carvalho":
                    playerInfo.WebName = "Lee Carvallo's Putting Challenge";
                    return;
                case "Casemiro":
                    playerInfo.WebName = "Cantpassemiro";
                    return;
                case "Cash":
                    playerInfo.WebName = "Wonga";
                    return;
                case "Chamberlain":
                    playerInfo.WebName = "Alex Oxlade-Changingroom";
                    return;
                case "Chong":
                    playerInfo.WebName = "Cheech & Chong";
                    return;
                case "Coady":
                    playerInfo.WebName = "Connor Codeine";
                    return;
                case "Coleman":
                    playerInfo.WebName = "Shameless Coleman";
                    return;
                case "Coutinho":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "It's Fucking Coutinho",
                        "Couptinho"
                    });
                    return;
                case "Cucurella":
                    playerInfo.WebName = "Coca-Cola";
                    return;
                case "Daka":
                    playerInfo.WebName = "DakkaDakka";
                    return;
                case "Darwin":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Agent of Chaos",
                        "Galapagos",
                        "On the Origin of Species",
                        "The Theory of Goalvolution"
                    });
                    return;
                case "Dawson":
                    playerInfo.WebName = "Balon D'awson";
                    return;
                case "de Gea":
                    playerInfo.WebName = "The Gea";
                    return;
                case "Dele":
                    playerInfo.WebName = "Turkish Dele Allight";
                    return;
                case "Dennis":
                    playerInfo.WebName = "The Menace";
                    return;
                case "Dewsbury-Hall":
                    playerInfo.WebName = "Wedding Venue";
                    return;
                case "Dias":
                    playerInfo.WebName = "Rubbin' de Ass";
                    return;
                case "Dier":
                    playerInfo.WebName = "Dire";
                    return;
                case "Digne":
                    playerInfo.WebName = "Lou Kadine";
                    return;
                case "Djenepo":
                    playerInfo.WebName = "Meesah Djenepo";
                    return;
                case "Elanga":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Don't Look Back Elanga",
                        "Rhythm is a Dancer",
                        "The Anga"
                    });
                    return;
                case "El Ghazi":
                    playerInfo.WebName = "The Warrior";
                    return;
                case "Emerson":
                    playerInfo.WebName = "Emerson Royal with Cheese";
                    return;
                case "Fernandes":
                    playerInfo.WebName = "Penandes";
                    return;
                case "Firmino":
                    playerInfo.WebName = "Firminho";
                    return;
                case "Gomez":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Joe Gomez Adams",
                        "Joemez"
                    });
                    return;
                case "Gordon":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Diners, Drive-Ins & Dives",
                        "Wankony Dive-don"
                    });
                    return;
                case "Groß":
                    playerInfo.WebName = ":gross";
                    return;
                case "Haaland":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Hahahaaland",
                        "The Terminator"
                    });
                    return;
                case "Iheanacho":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Macho Man",
                        "Ian Nacho"
                    });
                    return;
                case "Ings":
                    playerInfo.WebName = "Daddy Ings";
                    return;
                case "James":
                    if (playerInfo.FirstName == "Reece")
                        playerInfo.WebName = "NFTeece James";
                    if (playerInfo.FirstName == "Dan")
                        playerInfo.WebName = "Dan German";
                    return;
                case "Jota":
                    playerInfo.WebName = "Yota";
                    return;
                case "Kane":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Sir Harold Edward Kane of England, Duke of Tottenhamshire",
                        "Kané"
                    });
                    return;
                case "Keita":
                    playerInfo.WebName = "Crappy Shite-a";
                    return;
                case "Kristensen":
                    playerInfo.WebName = "The Rasmus";
                    return;
                case "Krul":
                    playerInfo.WebName = "Cruel";
                    return;
                case "Kulusevski":
                    playerInfo.WebName = "Coolusevski";
                    return;
                case "Lacazette":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Alexandre the Cazette",
                        "Lackofthreat"
                    });
                    return;
                case "Lamptey":
                    playerInfo.WebName = "Lamprey";
                    return;
                case "Laporte":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Who's Eric?",
                        "The Port"
                    });
                    return;
                case "Lingard":
                    playerInfo.WebName = "Lingod";
                    return;
                case "Luis Díaz":
                    playerInfo.WebName = "Pauly D";
                    return;
                case "Lukaku":
                    playerInfo.WebName = "Training, training, playing, training, playing, training, sleeping, eating good, training, playing, sleeping, eat good, training, drink a lot of water, sleep, train, and don't give interviews";
                    return;
                case "Maguire":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "M'Guire"
                    });
                    return;
                case "Mahrez":
                    playerInfo.WebName = "Mother Rez";
                    return;
                case "Martinelli":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Mother Tinelli"
                    });                    
                    return;
                case "Mbeumo":
                    playerInfo.WebName = "Kinder Mbeumo";
                    return;
                case "Mee":
                    playerInfo.WebName = "Bahn Mi";
                    return;
                case "Minamino":
                    playerInfo.WebName = "Minaminho";
                    return;
                case "Mings":
                    playerInfo.WebName = "Mings the Merciless";
                    return;
                case "Mitrović":
                    playerInfo.WebName = "Shitrobitch";
                    return;
                case "Mount":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Jason Count",
                        "Mountain Mase",
                        "Pickey Pount"
                    });
                    return;
                case "Moutinho":
                    playerInfo.WebName = "Pass-Master Joao Moutinho";
                    return;
                case "Mudryk":
                    playerInfo.WebName = "Ukraine Bolt";
                    return;
                case "Ogbonna":
                    playerInfo.WebName = "Ogbanger";
                    return;
                case "Olise":
                    playerInfo.WebName = "Für Olise";
                    return;
                case "Olsen":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Robin Nogoalsconcededson",
                        "His name is Robin Olsen"
                    });
                    return;
                case "Onana":
                    playerInfo.WebName = "Hoochie mama show Onana";
                    return;
                case "Partey":
                    playerInfo.WebName = "S-Club Partey";
                    return;
                case "Perišić":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Paris Itch",
                        "Penisic",
                    });
                    return;
                case "Pogba":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Deep Lying Social Media Influencer",
                        "Pau Pogba",
                        "Salon d'Or",
                        "Paul Podcast",
                        "Jogba",
                        "Barber-to-Barber Midfielder",
                        "Ball Hogba",
                        "Paul Cryptogba"
                    });
                    return;
                case "Porro":
                    playerInfo.WebName = "Pedro Porno";
                    return;
                case "Pulisic":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Captain America",
                        "EPL Proud Boy",
                        "NRAmar",
                        "Puligod"                        
                    });
                    return;
                case "Rashford":
                    playerInfo.WebName = "Trashford";
                    return;
                case "Richarlison":
                    playerInfo.WebName = "The Pigeon";
                    return;
                case "Rodon":
                    playerInfo.WebName = "Joe Rogan";
                    return;
                case "Romeo":
                    playerInfo.WebName = "Romeo & Juliet";
                    return;
                case "Ronaldo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Omicronaldo", 
                        "Penaldo",
                        "Penisaldo"
                    });
                    return;
                case "Sabitzer":
                    playerInfo.WebName = "Sabitcher";
                    return;
                case "Saka":
                    playerInfo.WebName = ":salad";
                    return;
                case "Salah":
                    if (DateTime.Now.Month > 11 || DateTime.Now.Month < 3)
                        playerInfo.WebName = "Snow Salah";
                    else
                        playerInfo.WebName = PickRandomNickname(new List<string>()
                        {
                            "Flavour Town",
                            "Slag"
                        });                    
                    return;
                case "Saliba":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":trumpet Der der der-der der-der der der, Saliba! :trumpet"
                    });
                    return;
                case "Schär":
                    playerInfo.WebName = "Cher";
                    return;
                case "Semedo":
                    playerInfo.WebName = "Nelson's Emedo";
                    return;
                case "Smith Rowe":
                    playerInfo.WebName = "Smith-Row Z";
                    return;
                case "Sterling":
                    playerInfo.WebName = "Sterling-PreOrder";
                    return;
                case "Tielemans":
                    playerInfo.WebName = "Tieletubby";
                    return;
                case "Toney":
                    playerInfo.WebName = "I've an Toney";
                    return;
                case "Townsend":
                    if (playerInfo.FirstName == "Andros")
                        playerInfo.WebName = "Clownsend";
                    return;
                case "Trezeguet":
                    playerInfo.WebName = "Aston Villa's Trezegoaaaaaaaaaaaaaaaaaaal";
                    return;
                case "Trippier":
                    playerInfo.WebName = "Strippier";
                    return;
                case "Trossard":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Soccer Guy",
                        "Tosser...d"
                    });
                    return;
                case "van de Beek":
                    playerInfo.WebName = "Donny On de Beench";
                    return;
                case "Varane":
                    playerInfo.WebName = "Barbara Ann";
                    return;
                case "Vestergaard":
                    playerInfo.WebName = "Bestergaard";
                    return;
                case "Walcott":
                    playerInfo.WebName = "WalGott";
                    return;
                case "Ward-Prowse":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "'It's perfect, it's precise, it's James Ward-Prowse'",
                        "Set Piece Specialist",
                        "Tesco Beckham"
                    });
                    return;
                case "Werner":
                    playerInfo.WebName = "Ronson";
                    return;
                case "Willian":
                    playerInfo.WebName = "William";
                    return;
                case "Wilson":
                    playerInfo.WebName = ":wilson:";
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
                case 710741:
                    return "Felix";                
                case 2352608:
                    return "Higgins";
                case 5859854:
                    return "Joe";
                case 1983521:
                    return "Meme";
                case 1114406:
                    return "Nodge";
                case 963153:
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
                            var villa = PickRandomNickname(new List<string>()
                            {
                                "Ass Town Village",
                                "Aston Billa",
                                "Aston Vanilla",
                                "Real Villa",
                                "~Stevie G's~ Claret & Blue Army",
                                "Villareal"
                            });
                            return villa;
                        case "Chelsea":
                            var chelsea = PickRandomNickname(new List<string>() 
                            {
                                "FC London Cowboys"
                            });
                            return chelsea;
                        case "Crystal Palace":
                            return "Crystal Phallus";
                        case "Derby":
                        case "Derby County":
                            return "Wayne Rooney's Financially Troubled Derby County";
                        case "Everton":
                            return "~Frank Lampard's~ Everton FC";
                        case "Liverpool":
                            return "Jürgen Klopp's Mentality Monsters";
                        case "Norwich":
                            return "~Frank Lampard's~ Norwich City";
                        case "Southampton":
                            return "Southam9t0n";
                        case "Spurs":
                            var spurs = PickRandomNickname(new List<string>()
                            {
                                "Lads, it's Topspur",
                                "Tinpottenham"
                            });
                            return spurs;
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
