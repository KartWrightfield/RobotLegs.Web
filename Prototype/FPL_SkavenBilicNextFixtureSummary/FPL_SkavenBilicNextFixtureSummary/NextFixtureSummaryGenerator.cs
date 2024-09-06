using FPLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class NextFixtureSummaryGenerator
    {
        const int SlavenBilicLeagueId = 173120;
        private const int KickOffOffset = 0;

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
                    string playerDisplayName = PlayerNicknameCheck(playerInfo);

                    if (playerSelected.Position < 12)
                    {
                        if (playerSelected.IsCaptain)
                        {
                            if (playerSelected.Multiplier == 3)
                                first11Players.Add(playerDisplayName + " (*3xc*)");
                            else
                                first11Players.Add(playerDisplayName + " (c)");
                        }
                        else if (playerSelected.IsViceCaptain)
                            first11Players.Add(playerDisplayName + " (v)");
                        else
                            first11Players.Add(playerDisplayName);
                    }
                    else
                    {
                        hasPlayersOnBench = true;

                        if (playerSelected.IsCaptain)
                            benchPlayers.Add(playerDisplayName + " (c)");
                        else if (playerSelected.IsViceCaptain)
                            benchPlayers.Add(playerDisplayName + " (v)");
                        else
                            benchPlayers.Add(playerDisplayName);
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

        private string PlayerNicknameCheck(Element playerInfo)
        {
            switch (playerInfo.WebName)
            {
                case "Adama Traoré":
                    return PickRandomNickname(new List<string>()
                    {
                        "Fastest Cum in the West",
                        "Onana Traoré",
                        "Wankama Wankoré",
                        "Wanks Onana"
                    });
                case "Adams":
                    if (playerInfo.FirstName == "Che")
                        return "Che Guevadams";
                    break;
                case "Aguerd":
                    return "Aguero";
                case "Alexander-Arnold":
                    return PickRandomNickname(new List<string>()
                    {
                        "TAA",
                        "Trent Asexualander-Arnold"
                    });
                case "Alisson":
                    return "Thini Alissoj";
                case "Alonso":
                    return "Fucking Murderer";
                case "Antonio":
                    return "The Mage";
                case "Arblaster":
                    return PickRandomNickname(new List<string>()
                    {
                        "Arseblaster"
                    });
                case "Areola":
                    return PickRandomNickname(new List<string>()
                    {
                        "'Arry Ola"
                    });
                case "Aribo":
                    return PickRandomNickname(new List<string>()
                    {
                       "Starmix",
                       "Tangfastics"
                    });
                case "Arthur":
                    return PickRandomNickname(new List<string>()
                    {
                        "King Arthur",
                        "Marsh Melo"
                    });
                case "Bailey":
                    return PickRandomNickname(new List<string>()
                    {
                        "Bay Leaf",
                        "Leon Goaley"
                    });
                case "Baleba":
                    return PickRandomNickname(new List<string>()
                    {
                        ":notes _Then I Saw Her Face, Now I'm a Baleba_ :notes"
                    });
                case "Barnes":
                    return PickRandomNickname(new List<string>()
                    {
                        "Harvey Fucking Barnes"
                    });
                case "Bernardo":
                    return PickRandomNickname(new List<string>()
                    {
                        "ACABernado Silva",
                        "Banano Silva"
                    });
                case "Botman":
                    return PickRandomNickname(new List<string>()
                    {
                        "Batman",
                        "Bottleman",
                        "Sven Bossman"
                    });
                case "Bowen":
                    return PickRandomNickname(new List<string>()
                    {
                        "David Blowie"
                    });
                case "Brobbey":
                    return "Mr Brobbey";
                case "Broja":
                    return "Do you even lift Broja?";
                case "Burn":
                    return "Father Dick Byrne";
                case "Calvert-Lewin":
                    return PickRandomNickname(new List<string>()
                    {
                        "Calvin Lewis",
                        "Goalvert-Lewin"
                    });
                case "Cancelo":
                    return "Cancelo Culture";
                case "Carvalho":
                    return "Lee Carvallo's Putting Challenge";
                case "Casemiro":
                    return "Cantpassemiro";
                case "Cash":
                    return PickRandomNickname(new List<string>()
                    {
                        "Mateusz Kashinski",
                        "Matty Cashback",
                        "Wonga"
                    });
                case "Chamberlain":
                    return "Alex Oxlade-Changingroom";
                case "Chong":
                    return "Cheech & Chong";
                case "Coady":
                    return "Connor Codeine";
                case "Coleman":
                    return "Shameless Coleman";
                case "Coutinho":
                    return PickRandomNickname(new List<string>()
                    {
                        "It's Fucking Coutinho",
                        "Couptinho"
                    });
                case "Cucurella":
                    return "Coca-Cola";
                case "Cunha":
                    return PickRandomNickname(new List<string>()
                    {
                        "Cunha Matata"
                    });
                case "Daka":
                    return "DakkaDakka";
                case "Dasilva":
                    return PickRandomNickname(new List<string>()
                    {
                        "Josh the Silver"
                    });
                case "Darwin":
                    return PickRandomNickname(new List<string>()
                    {
                        "Agent of Chaos",
                        "Bollock Yoghurt",
                        "Galapagos",
                        "Mr Sitter",
                        "On the Origin of Species",
                        "The Theory of Goalvolution"
                    });
                case "Davis":
                    return PickRandomNickname(new List<string>()
                    {
                        "Leif in the Wind"
                    });
                case "Dawson":
                    return "Balon D'awson";
                case "De Bruyne":
                    return PickRandomNickname(new List<string>()
                    {
                        "Kevin Me Tooyne"
                    });
                case "de Gea":
                    return "The Gea";
                case "Dele":
                    return "Turkish Dele Allight";
                case "Dennis":
                    return "The Menace";
                case "Dewsbury-Hall":
                    return "Wedding Venue";
                case "Diaby":
                    return PickRandomNickname(new List<string>()
                    {
                        "Yabba-Diaby-Doo"
                    });
                case "Dias":
                    return "Rubbin' de Ass";
                case "Dier":
                    return "Dire";
                case "Digne":
                    return "Lou Kadine";
                case "Diogo J.":
                    return PickRandomNickname(new List<string>()
                    {
                        "_The Jota, The_",
                        "The Right Jota",
                        "Yota"
                    });
                case "Djenepo":
                    return "Meesah Djenepo";
                case "Doku":
                    return PickRandomNickname(new List<string>()
                    {
                        "Cum Thief"
                    });
                case "Dunk":
                    return PickRandomNickname(new List<string>()
                    {
                        "Dairylea Dunkers"
                    });
                case "Duran":
                    return PickRandomNickname(new List<string>()
                    {
                        "Professor Chaos"
                    });
                case "Elanga":
                    return PickRandomNickname(new List<string>()
                    {
                        "Don't Look Back Elanga",
                        "Rhythm is a Dancer",
                        "The Anga"
                    });
                case "El Ghazi":
                    return "The Warrior";
                case "Emerson":
                    return "Emerson Royal with Cheese";
                case "Eze":
                    return PickRandomNickname(new List<string>()
                    {
                        "Easy"
                    });
                case "Ferguson":
                    return PickRandomNickname(new List<string>()
                    {
                        "Evan Almighty"
                    });
                case "B.Fernandes":
                    return PickRandomNickname(new List<string>()
                    {
                        "Penandes",
                        "The Incredible Sulk"
                    });
                case "Firmino":
                    return "Firminho";
                case "Foden":
                    return PickRandomNickname(new List<string>()
                    {
                        "Filth Oden",
                        "Grimace"
                    });
                case "Garnacho":
                    return "Garnacho Nacho Man";
                case "Gomez":
                    return PickRandomNickname(new List<string>()
                    {
                        "Joe Gomez Adams",
                        "Joemez"
                    });
                case "Gordon":
                    return PickRandomNickname(new List<string>()
                    {
                        "_Diners, Drive-Ins & Dives_",
                        "Wankony Dive-don"
                    });
                case "Groß":
                    return ":gross";
                case "Gross":
                    return PickRandomNickname(new List<string>()
                    {
                        ":gross"
                    });
                case "Gusto":
                    return PickRandomNickname(new List<string>()
                    {
                        "HelloFresh",
                        "Mucho Gusto"
                    });
                case "Haaland":
                    return PickRandomNickname(new List<string>()
                    {
                        "Aryan Heskey",
                        "ErLGBTQing Haaland",
                        "Hahahaaland",
                        "The Haalander",
                        "The Terminator"
                    });
                case "Havertz":
                    return PickRandomNickname(new List<string>()
                    {
                        "Hav-some of that-ertz"
                    });
                case "Hee Chan":
                    return PickRandomNickname(new List<string>()
                    {
                        "'The Korean One' - _Pep Guardiola_"
                    });
                case "Henderson":
                    if (playerInfo.FirstName == "Jordan")
                    {
                        return PickRandomNickname(new List<string>()
                        {
                            "Jordan Transgenderson"
                        });
                    }
                    break;
                case "Iheanacho":
                    return PickRandomNickname(new List<string>()
                    {
                        "Macho Man",
                        "Ian Nacho"
                    });
                case "Ings":
                    return "Daddy Ings";
                case "James":
                    if (playerInfo.FirstName == "Reece")
                        return "NFTeece James";
                    if (playerInfo.FirstName == "Dan")
                        return "Dan German";
                    break;
                case "Jesus":
                    return PickRandomNickname(new List<string>()
                    {
                        "xJesus"
                    });
                case "João Pedro":
                    return PickRandomNickname(new List<string>()
                    {
                        "João Penis"
                    });
                case "Jota":
                    if (playerInfo.FirstName == "Diogo")
                    {
                        return PickRandomNickname(new List<string>()
                        {
                            "_The Jota, The_",
                            "The Right Jota",
                            "Yota"
                        });
                    }
                    break;
                case "Kaboré":
                    return PickRandomNickname(new List<string>()
                    {
                        "When the moon hits the sky like a big pizza pie... that's Kaboré"
                    });
                case "Kane":
                    return PickRandomNickname(new List<string>()
                    {
                        "Sir Harold Edward Kane of England, Duke of Tottenhamshire",
                        "Kané"
                    });
                case "Keita":
                    return "Crappy Shite-a";
                case "Kristensen":
                    return "The Rasmus";
                case "Krul":
                    return "Cruel";
                case "Kulusevski":
                    return "Coolusevski";
                case "Lacazette":
                    return PickRandomNickname(new List<string>()
                    {
                        "Alexandre the Cazette",
                        "Lackofthreat"
                    });
                case "Lamptey":
                    return "Lamprey";
                case "Laporte":
                    return PickRandomNickname(new List<string>()
                    {
                        "Who's Eric?",
                        "The Port"
                    });
                case "Lingard":
                    return "Lingod";
                case "Livramento":
                    return PickRandomNickname(new List<string>()
                    {
                        "Livramento Laughramento Lovramento"
                    });
                case "Longstaff":
                    return PickRandomNickname(new List<string>()
                    {
                        "al-Ramh"
                    });
                case "Luis Díaz":
                    return "Pauly D";
                case "Lukaku":
                    return "Training, training, playing, training, playing, training, sleeping, eating good, training, playing, sleeping, eat good, training, drink a lot of water, sleep, train, and don't give interviews";
                case "M.Salah":
                    if (DateTime.Now.Month > 11 || DateTime.Now.Month < 3)
                        return "Snow Salah";
                    else
                        return PickRandomNickname(new List<string>()
                        {
                            "Flavour Town",
                            "Slag",
                            "The Ultimate Predator"
                        });
                case "Mac Allister":
                    return PickRandomNickname(new List<string>()
                    {
                        "Alex is Mac Allister",
                        "Mac Tonight :moon",
                        "Max Allister"
                    });
                case "Maddison":
                    return PickRandomNickname(new List<string>()
                    {
                        "Mad Laddison"
                    });
                case "Madueke":
                    return PickRandomNickname(new List<string>()
                    {
                        "Tripadvisor"
                    });
                case "Maguire":
                    return PickRandomNickname(new List<string>()
                    {
                        "M'Guire"
                    });
                case "Mahrez":
                    return "Mother Rez";
                case "March":
                    return "Idus Martiae";
                case "Martinelli":
                    return PickRandomNickname(new List<string>()
                    {
                        "Mother Tinelli"
                    });
                case "Martinez":
                    return PickRandomNickname(new List<string>()
                    {
                        "Bantinez"
                    });
                case "Mbeumo":
                    return "Kinder Mbeumo";
                case "McGinn":
                    return PickRandomNickname(new List<string>()
                    {
                        "McGinniesta",
                        "Scottish Messi"
                    });
                case "Mee":
                    return "Bahn Mi";
                case "Miley":
                    return PickRandomNickname(new List<string>()
                    {
                        "Hannah Montana"
                    });
                case "Minamino":
                    return "Minaminho";
                case "Mitoma":
                    return "With Grandma";
                case "Mings":
                    return "Mings the Merciless";
                case "Mitrović":
                    return "Shitrobitch";
                case "Mount":
                    return PickRandomNickname(new List<string>()
                    {
                        "Jason Count",
                        "Mountain Mase",
                        "Pickey Pount"
                    });
                case "Moutinho":
                    return "Pass-Master Joao Moutinho";
                case "Mudryk":
                    return PickRandomNickname(new List<string>()
                    {
                        "Ghost of Kiev",
                        "My Name is Mudryk",
                        "Ukraine Bolt"
                    });
                case "Mubama":
                    return PickRandomNickname(new List<string>()
                    {
                        "Thanks Mubama"
                    });
                case "Mykolenko":
                    return PickRandomNickname(new List<string>()
                    {
                        "Mick O'lenko"
                    });
                case "N.Jackson":
                    return PickRandomNickname(new List<string>()
                    {
                        "Micky Jackson"
                    });
                case "Ødegaard":
                    return PickRandomNickname(new List<string>()
                    {
                        ":frog",
                        "Ohdeargod"
                    });
                case "Ogbonna":
                    return "Ogbanger";
                case "Olise":
                    return "Für Olise";
                case "Olsen":
                    return PickRandomNickname(new List<string>()
                    {
                        "Robin Nogoalsconcededson",
                        "His name is Robin Olsen"
                    });
                case "Onana":
                    return PickRandomNickname(new List<string>()
                    {
                        ":notes _Onanananana, it's the motherfucking D. O. Double G_ :notes",
                        "Hoochie mama show Onana",
                        "Oh no-na",
                        "Thanks Onana"
                    });
                case "Palmer":
                    return PickRandomNickname(new List<string>()
                    {
                        "Keke Palmer is Wearing My Jeans",
                        "Who Killed Laura Palmer?"
                    });
                case "Partey":
                    return "S-Club Partey";
                case "Pau":
                    return PickRandomNickname(new List<string>()
                    {
                        "Paul Torres"
                    });
                case "Perišić":
                    return PickRandomNickname(new List<string>()
                    {
                        "Paris Itch",
                        "Penisic",
                    });
                case "Pogba":
                    return PickRandomNickname(new List<string>()
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
                case "Pedro Porro":
                    return "Pedro Porno";
                case "Pope":
                    return PickRandomNickname(new List<string>()
                    {
                        "The Pope"                        
                    });
                case "Pulisic":
                    return PickRandomNickname(new List<string>()
                    {
                        "Captain America",
                        "EPL Proud Boy",
                        "NRAmar",
                        "Puligod"                        
                    });
                case "Quansah":
                    return PickRandomNickname(new List<string>()
                    {
                        "Kwanzaa-Bot"
                    });
                case "Ramsdale":
                    return PickRandomNickname(new List<string>()
                    {
                        "Hologramsdale"                        
                    });
                case "Rashford":
                    return "Trashford";
                case "Richarlison":
                    return PickRandomNickname(new List<string>()
                    {
                        "Shitcharlison",
                        "The Pigeon"
                    });
                case "Rodon":
                    return "Joe Rogan";
                case "Rodrigo":
                    return PickRandomNickname(new List<string>()
                    {
                        "Rodney"
                    });
                case "Romeo":
                    return "Romeo & Juliet";
                case "Ronaldo":
                    return PickRandomNickname(new List<string>()
                    {
                        "Omicronaldo", 
                        "Penaldo",
                        "Penisaldo"
                    });
                case "Sabitzer":
                    return "Sabitcher";
                case "Saka":
                    return PickRandomNickname(new List<string>()
                    {
                        ":salad",
                        "Labourkayo Saka",
                        "Starboy"
                    });
                case "Salah":
                    if (DateTime.Now.Month > 11 || DateTime.Now.Month < 3)
                        return "Snow Salah";
                    else
                        return PickRandomNickname(new List<string>()
                        {
                            "Flavour Town",
                            "Slag",
                            "The Ultimate Predator"
                        });
                case "Saliba":
                    return PickRandomNickname(new List<string>()
                    {
                        ":trumpet Der der der-der der-der der der, Saliba! :trumpet"
                    });
                case "Schär":
                    return "Cher";
                case "Semedo":
                    return "Nelson's Emedo";
                case "Semenyo":
                    return PickRandomNickname(new List<string>()
                    {
                        "Cum"
                    });
                case "Smith Rowe":
                    return PickRandomNickname(new List<string>()
                    {
                        "Smith-Row Z",
                        "Spliff Row"
                    });
                case "Solanke":
                    return PickRandomNickname(new List<string>()
                    {
                        "Maclunkey"
                    });
                case "Sterling":
                    return "Sterling-PreOrder";
                case "Szmodics":
                    return PickRandomNickname(new List<string>()
                    {
                        "Sammie Smalldicks"
                    });
                case "Szoboszlai":
                    return PickRandomNickname(new List<string>()
                    {
                        "Dommy Schlobbers",
                        "The Schlobmeister"
                    });
                case "Tielemans":
                    return PickRandomNickname(new List<string>()
                    {
                        "Tieletubby",
                        "Yourit Mealdealemans"
                    });
                case "Toney":
                    return PickRandomNickname(new List<string>()
                    {
                        "Big Dog's Back",
                        "I've an Toney"
                    });
                case "Townsend":
                    if (playerInfo.FirstName == "Andros")
                        return "Clownsend";
                    break;
                case "Trezeguet":
                    return "Aston Villa's Trezegoaaaaaaaaaaaaaaaaaaal";
                case "Trippier":
                    return "Strippier";
                case "Trossard":
                    return PickRandomNickname(new List<string>()
                    {
                        "Soccer Guy",
                        "Tosser...d"
                    });
                case "van de Beek":
                    return "Donny On de Beench";
                case "Virgil":
                    return PickRandomNickname(new List<string>()
                    {
                        "Virgil van Shite"
                    });
                case "Varane":
                    return "Barbara Ann";
                case "Vestergaard":
                    return "Bestergaard";
                case "Vicario":
                    return PickRandomNickname(new List<string>()
                    {
                        "Dracula"
                    });
                case "Walcott":
                    return "WalGott";
                case "Walker":
                    return PickRandomNickname(new List<string>()
                    {
                        "Texas Ranger"
                    });
                case "Ward-Prowse":
                    return PickRandomNickname(new List<string>()
                    {
                        "'It's perfect, it's precise, it's James Ward-Prowse'",
                        "Set Piece Specialist",
                        "Tesco Beckham"
                    });
                case "Watkins":
                    return PickRandomNickname(new List<string>()
                    {
                        "Twatkins"
                    });
                case "Werner":
                    return "Ronson";
                case "Willian":
                    return "William";
                case "Wilson":
                    return ":volleyball";
                case "Wood":
                    return PickRandomNickname(new List<string>()
                    {
                        "The Ghost of Christmas Past",
                        "Woodigol"
                    });
                case "Zaniolo":
                    return PickRandomNickname(new List<string>()
                    {
                        "Cornholio"
                    });
                case "Zinchenko":
                    return PickRandomNickname(new List<string>()
                    {
                        "Zinky Winky"
                    });
                case "Zirkzee":
                    return PickRandomNickname(new List<string>()
                    {
                        "Xerxes"
                    });
            }
            return playerInfo.WebName;
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
                case 682721:
                    return "Felix";                
                case 6941143:
                    return "Higgins";
                case 6982871:
                    return "Joe";
                case 4512820:
                    return "Meme";
                case 24036:
                    return "Nodge";
                case 2852054:
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
                            var arsenal = PickRandomNickname(new List<string>()
                            {
                                "Arse-anal",
                                "The Arsenal"
                            });
                            return arsenal;
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
                                "Billion Pound Bottlejobs",
                                "FC London Cowboys",
                                "Spreadsheet FC"
                            });
                            return chelsea;
                        case "Crystal Palace":
                            return "Crystal Phallus";
                        case "Derby":
                        case "Derby County":
                            return "Wayne Rooney's Financially Troubled Derby County";
                        case "Everton":
                            return "~Frank Lampard's~ Everton FC";
                        case "Ipswich":
                        case "Ipswich Town":
                            return PickRandomNickname(new List<string>()
                            {
                                "Ispwich",
                                "Los Chicos del Tractor"
                            });
                        case "Liverpool":
                            return "Arne's Lot";
                        case "Man City":
                            return PickRandomNickname(new List<string>()
                            {
                                "115 City",
                                "United Arab Emirates",
                                "Wankbastards Shitty"
                            });
                        case "Man Utd":
                            return PickRandomNickname(new List<string>()
                            {
                                "aan u",
                                "UK Gold FC"
                            });
                        case "Newcastle":
                            return PickRandomNickname(new List<string>()
                            {
                                "Poocastle",
                                "Saudi Arabia"
                            });
                        case "Norwich":
                            return "~Frank Lampard's~ Norwich City";
                        case "Southampton":
                            return "Southam9t0n";
                        case "Spurs":
                            var spurs = PickRandomNickname(new List<string>()
                            {
                                "Lads, it's Topspur",
                                "The Premier League's Grandest Punchline",
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
                if (_Fixtures[i].KickoffTime > DateTime.Now.AddMinutes(KickOffOffset))
                { 
                    _NextFixtures.Add(_Fixtures[i]);

                    if ((i == 0 && _Fixtures[i].Event == 1) || (_Fixtures[i].Event == _Fixtures[i-1].Event + 1))
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
