const getAllFixtures = async () => {
    try{
        const res = await axios.get('https://fantasy.premierleague.com/api/fixtures/');
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
};

const getGameweekSelections = async (gameweekId, teamId) => {
    try{
        const res = await axios.get(`https://fantasy.premierleague.com/api/entry/${teamId}/event/${gameweekId}/picks/`);
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
};

const getMainGameInfo = async () => {
    try{
        const res = await axios.get('https://fantasy.premierleague.com/api/bootstrap-static/');
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
};

const getMiniLeagueInfo = async (leagueId) => {
    try{
        const res = await axios.get(`https://fantasy.premierleague.com/api/leagues-classic/${leagueId}/standings/`);
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
};

const getTeamTransfers = async (teamId) => {
    try{
        const res = await axios.get(`https://fantasy.premierleague.com/api/entry/${teamId}/transfers/`);
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
};