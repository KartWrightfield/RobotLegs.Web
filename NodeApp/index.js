const express = require("express");
const app = express();
const path = require('path');
const ejsMate = require('ejs-mate');
const Joi = require('joi');
const mongoose = require('mongoose');
const methodOverride = require('method-override');
const catchAsync = require('./utils/catchAsync.js');
const ExpressError = require('./utils/ExpressError.js');
const session = require('express-session');
const flash = require("connect-flash");
const passport = require('passport');
const LocalStrategy = require('passport-local');

const User = require('./models/user');

const mainGameInfo = require('./seeds/maingameinfo.js');
const leagueInfo = require('./seeds/leagueinfo.js');
const fixtures = require('./seeds/fixtures.js');

const week1Picks = require('./seeds/week1picks.js');
const week1Transfers = require('./seeds/week1transfers.js');

const nicknameRoutes = require('./routes/nicknames');
const userRoutes = require('./routes/users');

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
const sessionConfig = {
    secret: 'tempsecret',
    resave: false,
    saveUninitialized: true,
    cookie: {
        httpOnly: true,
        expires: Date.now() + 1000 * 60 * 60 * 24 * 7,
        maxAge: 1000 * 60 * 60 * 24 * 7
    }
}
app.use(session(sessionConfig));
app.use(flash());

app.use(passport.initialize());
app.use(passport.session());
passport.use(new LocalStrategy(User.authenticate()));

passport.serializeUser(User.serializeUser());
passport.deserializeUser(User.deserializeUser());

app.use((req, res, next) => {
    res.locals.currentUser = req.user;
    res.locals.success = req.flash('success');
    res.locals.error = req.flash('error');
    next();
})

app.use('/nicknames', nicknameRoutes);
app.use('/', userRoutes);

app.get('/', (req, res) => {
    var gameweekFixtures = fixtures.filter(function (fix) {
        return fix.event === 1;
    });

    res.render('home', { mainGameInfo, gameweekFixtures, leagueInfo })
});

app.all('*', (req, res, next) => {
    next(new ExpressError('Page Not Found', 404))
})

app.use((err, req, res, next) => {
    const { statusCode = 500 } = err;
    if (!err.message) err.message = 'Something went wrong';
    res.status(statusCode).render('error', { err });
})

app.listen(3000, () => {
    console.log("LISTENING ON PORT 3000")
})