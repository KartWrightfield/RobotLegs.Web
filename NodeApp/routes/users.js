const express = require('express');
const router = express.Router();
const passport = require('passport');
const catchAsync = require('../utils/catchAsync');
const User = require('../models/user');

router.get('/login', (req, res) => {
    res.render('users/login');
})

router.post('/login', passport.authenticate('local', { failureFlash: true, failureRedirect:'/login' }), (req, res) =>{
    req.flash('success', 'Logged In');
    res.redirect('/');
})

router.get('/logout', (req, res) => {
    req.logout(function (err) {
        if (err){
            return next(err);
        }
        req.flash('success', 'Logged Out');
        res.redirect('/');
    });    
})

router.get('/register', (req, res) =>{
    res.render('users/register');
});

router.post('/register', catchAsync(async(req, res) => {
    try {
        const {username, email, password} = req.body;
        const user = new User({email, username});
        const registeredUser = await User.register(user, password);

        req.flash('success', 'Account Created');
        res.redirect('/');
    } catch (e) {
        req.flash('error', e.message)
        res.redirect('/register')
    }
}));



module.exports = router;