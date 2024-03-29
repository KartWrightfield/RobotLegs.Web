const express = require("express");
const app = express();
const path = require('path');
const ejsMate = require('ejs-mate');
const mongoose = require('mongoose');
const methodOverride = require('method-override');

const mainGameInfo = require('./seeds/maingameinfo.js');
const leagueInfo = require('./seeds/leagueinfo.js');
const fixtures = require('./seeds/fixtures.js');

const week1Picks = require('./seeds/week1picks.js');
const week1Transfers = require('./seeds/week1transfers.js');

const Nickname = require('./models/nickname');

mongoose.connect('mongodb://localhost:27017/robotLegs')
    .then(() => {
        console.log("MONGO CONNECTION OPEN!")
    })
    .catch(err => {
        console.log("MONGO CONNECTION ERROR!")
        console.log(err)
    })

app.engine('ejs', ejsMate);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');
app.use(express.urlencoded({extended: true}));
app.use(methodOverride('_method'));


app.get('/', (req, res) => {
    var gameweekFixtures = fixtures.filter(function (fix) {
        return fix.event === 1;
    });

    res.render('home', { mainGameInfo, gameweekFixtures, leagueInfo })
});

app.get('/nicknames', async (_req, res) => {
    const nicknames = await Nickname.find({});

    res.render('nicknames/index', { nicknames });
})

app.get('/nicknames/new', (req, res) => {
    res.render('nicknames/new');
})

app.post('/nicknames', async (req, res) => {
    const newNickname = new Nickname(req.body);
    newNickname.nicknames.push(req.body.nickname);

    await newNickname.save();

    res.redirect(`/nicknames/${newNickname.id}`);
})

app.patch('/nicknames/:id', async (req, res) => {
    const { id } = req.params;
    const nickname = req.body.nickname;

    const playerNicknames = await Nickname.findById(id);

    playerNicknames.nicknames.push(nickname);

    await playerNicknames.save();

    res.redirect(`/nicknames/${id}`);
})

app.delete('/nicknames/:id', async (req, res) => {
    const { id } = req.params;
    await Nickname.findByIdAndDelete(id);
    
    res.redirect('/nicknames');
})

app.get('/nicknames/:id', async (req, res) => {
    const { id } = req.params;
    const nickname = await Nickname.findById(id);

    res.render('nicknames/details', { nickname });
})

app.get('/nicknames/:id/addNickname', (req, res) => {
    const { id } = req.params;
    res.render(`nicknames/addNickname`, { id });
})

app.listen(3000, () => {
    console.log("LISTENING ON PORT 3000")
})