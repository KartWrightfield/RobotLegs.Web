const getAllFixtures = async () => {
    makeRapidProxyRequest('https://fantasy.premierleague.com/api/fixtures/');    
};

const getGameweekSelections = async (gameweekId, teamId) => {
    makeRapidProxyRequest(`https://fantasy.premierleague.com/api/entry/${teamId}/event/${gameweekId}/picks/`);
};

const getMainGameInfo = async () => {
    makeRapidProxyRequest('https://fantasy.premierleague.com/api/bootstrap-static/');
};

const getMiniLeagueInfo = async (leagueId) => {
    makeRapidProxyRequest(`https://fantasy.premierleague.com/api/leagues-classic/${leagueId}/standings/`);
};

const getTeamTransfers = async (teamId) => {
    makeRapidProxyRequest(`https://fantasy.premierleague.com/api/entry/${teamId}/transfers/`);
};

const makeRapidProxyRequest = async (url) =>{

    const options = {
        method: 'POST',
        url: 'https://http-cors-proxy.p.rapidapi.com/',
        headers: {
          'content-type': 'application/json',
          'X-Requested-With': 'www.example.com',
          'X-RapidAPI-Key': '81af909dbbmsh2a2a447ac4af0b3p157331jsn609eb537ea85',
          'X-RapidAPI-Host': 'http-cors-proxy.p.rapidapi.com'
        },
        data: {
          url: url
        }
    };

    try{
        const res = await axios.request(options);
        console.log(res.data);
    } catch (e){
        console.log("ERROR", e);
    }
}