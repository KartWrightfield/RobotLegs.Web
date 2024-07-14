const express = require('express');
const passport = require('passport');
const router = express.Router();
const users = require('../controllers/users');
const catchAsync = require('../utils/catchAsync');
const { storeReturnTo } = require('../middleware');

router.route('/login')
    .get(users.renderLoginForm)
    .post(storeReturnTo, passport.authenticate('local', { failureFlash: true, failureRedirect:'/login' }), users.login)

router.get('/logout', users.logout)

router.route('/register')
    .get(users.renderRegistrationForm)
    .post(catchAsync(users.register))

module.exports = router;