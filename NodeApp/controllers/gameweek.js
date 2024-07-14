const fixtures = require("../seeds/fixtures");
const mainGameInfo = require("../seeds/maingameinfo");
const leagueInfo = require("../seeds/leagueinfo");
const week1Picks = require('../seeds/week1picks.js');
const week1Transfers = require('../seeds/week1transfers.js');

function getTeamNameFromId(id) {
    const team = mainGameInfo.teams.find((t) => t.id === id)

    if (team){
        return team.name
    }
}

function getManagerNameFromId(id) { //Will need to eventually turn this into a method that gets the ids from the actual game API
    switch(id){
        case 5569230:
            return "Felix"
        case 5569231:
            return "Sam"
        case 5872775:
            return "Nodge"
        case 10765688:
            return "Meme Team"
        case 13003098:
            return "Higgins"
        case 40784045:
            return "Joe"
        default:
            console.log("Manager ID not found")
    }
}

function getManagerPicks(managerId, teamId) {
    let rawPicks = null;
    
    switch(managerId) { //This will need to be completely changed once we're getting actual data from the API, the process of getting picks will need to be done as one call for each manager
        case 5569230:
            rawPicks = week1Picks[0]
            break;
        case 5569231:
            rawPicks = week1Picks[1]
            break;
        case 5872775:
            rawPicks = week1Picks[2]
            break;
        case 10765688:
            rawPicks = week1Picks[3]
            break;
        case 13003098:
            rawPicks = week1Picks[4]
            break;
        case 40784045:
            rawPicks = week1Picks[5]
            break;
        default:
            console.log("Manager ID not found")
    }
    
    if (rawPicks){
        let picks = [];
        
        for (const rawPick of rawPicks.picks){
            const player = mainGameInfo.elements.find((p) => p.id === rawPick.element);
            
            if (player.team === teamId) {
                let pick = {
                    playerName: player.web_name,
                    isCaptain: rawPick.is_captain,
                    isViceCaptain: rawPick.is_vice_captain,
                    isBenched: rawPick.position > 11 //could also use rawpick.multiplier === 0?
                }

                picks.push(pick);
            }
        }
        
        return picks;
    }
}

module.exports.renderGameweek = (req, res) => {
    const gameweekFixtures = fixtures.filter(function (fix) {
        return fix.event === 1;
    });
    
    let populatedFixtures = [];
    
    for (const rawFixture of gameweekFixtures) {
        let fixture = {
            awayTeam: getTeamNameFromId(rawFixture.team_a),
            homeTeam: getTeamNameFromId(rawFixture.team_h),
            managers: []
        }
        
        for (const rawManager of leagueInfo.standings.results) 
        {
            let manager = {
                name: getManagerNameFromId(rawManager.id),
                awayPicks: getManagerPicks(rawManager.id, rawFixture.team_a),
                homePicks: getManagerPicks(rawManager.id, rawFixture.team_h)                
            }
            
            fixture.managers.push(manager);
        }
        
        for (const rawNewManager of leagueInfo.new_entries.results)
        {
            let manager = {
                name: getManagerNameFromId(rawNewManager.id),
                awayPicks: getManagerPicks(rawNewManager.id, rawFixture.team_a),
                homePicks: getManagerPicks(rawNewManager.id, rawFixture.team_h)
            }

            fixture.managers.push(manager);
        }
        
        populatedFixtures.push(fixture)
    }
    
    let gameweekObject = {
        eventName: mainGameInfo.events[0].name,
        fixtures: populatedFixtures
    }
    
    res.render('home', { gameweekObject })
}