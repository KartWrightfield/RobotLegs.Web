﻿using FPLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPL_SkavenBilicNextFixtureSummary
{
    class NextFixtureSummaryGenerator
    {
        const int SlavenBilicLeagueId = 65200;
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
                case "Adama Traoré":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Fastest Cum in the West",
                        "Onana Traoré",
                        "Wankama Wankoré",
                        "Wanks Onana"
                    });
                    return;
                case "Adams":
                    if (playerInfo.FirstName == "Che")
                        playerInfo.WebName = "Che Guevadams";
                    return;
                case "Aguerd":
                    playerInfo.WebName = "Aguero";
                    return;
                case "Alexander-Arnold":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "TAA",
                        "Trent Asexualander-Arnold"
                    });
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
                case "Arblaster":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Arseblaster"
                    });
                    return;
                case "Areola":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "'Arry Ola"
                    });
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
                case "Bailey":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Bay Leaf",
                        "Leon Goaley"
                    });
                    return;
                case "Baleba":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":notes _Then I Saw Her Face, Now I'm a Baleba_ :notes"
                    });
                    return;
                case "Barnes":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Harvey Fucking Barnes"
                    });
                    return;
                case "Bernardo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "ACABernado Silva",
                        "Banano Silva"
                    });
                    return;
                case "Botman":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Batman",
                        "Bottleman",
                        "Sven Bossman"
                    });
                    return;
                case "Bowen":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "David Blowie"
                    });
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
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Calvin Lewis",
                        "Goalvert-Lewin"
                    });
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
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Mateusz Kashinski",
                        "Matty Cashback",
                        "Wonga"
                    });
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
                case "Cunha":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Cunha Matata"
                    });
                    return;
                case "Daka":
                    playerInfo.WebName = "DakkaDakka";
                    return;
                case "Dasilva":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Josh the Silver"
                    });
                    return;
                case "Darwin":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Agent of Chaos",
                        "Bollock Yoghurt",
                        "Galapagos",
                        "Mr Sitter",
                        "On the Origin of Species",
                        "The Theory of Goalvolution"
                    });
                    return;
                case "Dawson":
                    playerInfo.WebName = "Balon D'awson";
                    return;
                case "De Bruyne":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Kevin Me Tooyne"
                    });
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
                case "Diaby":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Yabba-Diaby-Doo"
                    });
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
                case "Diogo J.":
                    playerInfo.WebName = "Yota";
                    return;
                case "Djenepo":
                    playerInfo.WebName = "Meesah Djenepo";
                    return;
                case "Doku":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Cum Thief"
                    });
                    return;
                case "Dunk":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Dairylea Dunkers"
                    });
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
                case "Eze":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Easy"
                    });
                    return;
                case "Ferguson":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Evan Almighty"
                    });
                    return;
                case "B.Fernandes":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Penandes",
                        "The Incredible Sulk"
                    });
                    return;
                case "Firmino":
                    playerInfo.WebName = "Firminho";
                    return;
                case "Foden":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Filth Oden",
                        "Grimace"
                    });
                    return;
                case "Garnacho":
                    playerInfo.WebName = "Garnacho Nacho Man";
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
                        "_Diners, Drive-Ins & Dives_",
                        "Wankony Dive-don"
                    });
                    return;
                case "Groß":
                    playerInfo.WebName = ":gross";
                    return;
                case "Gross":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":gross"
                    });
                    return;
                case "Gusto":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "HelloFresh",
                        "Mucho Gusto"
                    });
                    return;
                case "Haaland":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Aryan Heskey",
                        "ErLGBTQing Haaland",
                        "Hahahaaland",
                        "The Haalander",
                        "The Terminator"
                    });
                    return;
                case "Havertz":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Hav-some of that-ertz"
                    });
                    return;
                case "Hee Chan":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "'The Korean One' - _Pep Guardiola_"
                    });
                    return;
                case "Henderson":
                    if (playerInfo.FirstName == "Jordan")
                    {
                        playerInfo.WebName = PickRandomNickname(new List<string>()
                        {
                            "Jordan Transgenderson"
                        });
                    }
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
                case "Jesus":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "xJesus"
                    });
                    return;
                case "João Pedro":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "João Penis"
                    });
                    return;
                case "Jota":
                    playerInfo.WebName = "Yota";
                    return;
                case "Kaboré":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "When the moon hits the sky like a big pizza pie... that's Kaboré"
                    });
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
                case "Livramento":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Livramento Laughramento Lovramento"
                    });
                    return;
                case "Longstaff":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "al-Ramh"
                    });
                    return;
                case "Luis Díaz":
                    playerInfo.WebName = "Pauly D";
                    return;
                case "Lukaku":
                    playerInfo.WebName = "Training, training, playing, training, playing, training, sleeping, eating good, training, playing, sleeping, eat good, training, drink a lot of water, sleep, train, and don't give interviews";
                    return;
                case "Mac Allister":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Alex is Mac Allister",
                        "Mac Tonight :moon",
                        "Max Allister"
                    });
                    return;
                case "Maddison":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Mad Laddison"
                    });
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
                case "March":
                    playerInfo.WebName = "Idus Martiae";
                    return;
                case "Martinelli":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Mother Tinelli"
                    });                    
                    return;
                case "Martinez":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Bantinez"
                    });                    
                    return;
                case "Mbeumo":
                    playerInfo.WebName = "Kinder Mbeumo";
                    return;
                case "McGinn":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "McGinniesta",
                        "Scottish Messi"
                    });
                    return;
                case "Mee":
                    playerInfo.WebName = "Bahn Mi";
                    return;
                case "Miley":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Hannah Montana"
                    });
                    return;
                case "Minamino":
                    playerInfo.WebName = "Minaminho";
                    return;
                case "Mitoma":
                    playerInfo.WebName = "With Grandma";
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
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "My Name is Mudryk",
                        "Ukraine Bolt"
                    });
                    return;
                case "Mubama":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Thanks Mubama"
                    });
                    return;
                case "N.Jackson":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Micky Jackson"
                    });
                    return;
                case "Ødegaard":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":frog",
                        "Ohdeargod"
                    });
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
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":notes _Onanananana, it's the motherfucking D. O. Double G_ :notes",
                        "Hoochie mama show Onana",
                        "Oh no-na",
                        "Thanks Onana"
                    });
                    return;
                case "Palmer":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Keke Palmer is Wearing My Jeans",
                        "Who Killed Laura Palmer?"
                    });
                    return;
                case "Partey":
                    playerInfo.WebName = "S-Club Partey";
                    return;
                case "Pau":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Paul Torres"
                    });
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
                case "Pedro Porro":
                    playerInfo.WebName = "Pedro Porno";
                    return;
                case "Pope":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "The Pope"                        
                    });
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
                case "Ramsdale":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Hologramsdale"                        
                    });
                    return;
                case "Rashford":
                    playerInfo.WebName = "Trashford";
                    return;
                case "Richarlison":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Shitcharlison",
                        "The Pigeon"
                    });
                    return;
                case "Rodon":
                    playerInfo.WebName = "Joe Rogan";
                    return;
                case "Rodrigo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Rodney"
                    });
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
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        ":salad",
                        "Labourkayo Saka",
                        "Starboy"
                    });
                    return;
                case "Salah":
                    if (DateTime.Now.Month > 11 || DateTime.Now.Month < 3)
                        playerInfo.WebName = "Snow Salah";
                    else
                        playerInfo.WebName = PickRandomNickname(new List<string>()
                        {
                            "Flavour Town",
                            "Slag",
                            "The Ultimate Predator"
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
                case "Semenyo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Cum"
                    });
                    return;
                case "Smith Rowe":
                    playerInfo.WebName = "Smith-Row Z";
                    return;
                case "Sterling":
                    playerInfo.WebName = "Sterling-PreOrder";
                    return;
                case "Szoboszlai":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Dommy Schlobbers",
                        "The Schlobmeister"
                    });
                    return;
                case "Tielemans":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Tieletubby",
                        "Yourit Mealdealemans"
                    });
                    return;
                case "Toney":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Big Dog's Back",
                        "I've an Toney"
                    });
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
                case "Virgil":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Virgil van Shite"
                    });
                    return;
                case "Varane":
                    playerInfo.WebName = "Barbara Ann";
                    return;
                case "Vestergaard":
                    playerInfo.WebName = "Bestergaard";
                    return;
                case "Vicario":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Dracula"
                    });
                    return;
                case "Walcott":
                    playerInfo.WebName = "WalGott";
                    return;
                case "Walker":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Texas Ranger"
                    });
                    return;
                case "Ward-Prowse":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "'It's perfect, it's precise, it's James Ward-Prowse'",
                        "Set Piece Specialist",
                        "Tesco Beckham"
                    });
                    return;
                case "Watkins":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Twatkins"
                    });
                    return;
                case "Werner":
                    playerInfo.WebName = "Ronson";
                    return;
                case "Willian":
                    playerInfo.WebName = "William";
                    return;
                case "Wilson":
                    playerInfo.WebName = ":volleyball";
                    return;
                case "Wood":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "The Ghost of Christmas Past",
                        "Woodigol"
                    });
                    return;
                case "Zaniolo":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Cornholio"
                    });
                    return;
                case "Zinchenko":
                    playerInfo.WebName = PickRandomNickname(new List<string>()
                    {
                        "Zinky Winky"
                    });
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
                case 349242:
                    return "Felix";                
                case 2937916:
                    return "Higgins";
                case 3256550:
                    return "Joe";
                case 4115929:
                    return "Meme";
                case 563456:
                    return "Nodge";
                case 1075495:
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
                        case "Ipswich":
                        case "Ipswich Town":
                            return "Los Chicos del Tractor";
                        case "Liverpool":
                            return "Jürgen Klopp's Mentality Monsters";
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
