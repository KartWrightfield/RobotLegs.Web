const express = require('express');
const catchAsync = require("../utils/catchAsync");
const router = express.Router();
const { isLoggedIn } = require('../middleware');
const nicknames = require('../controllers/nicknames');

router.route('/')
    .get(isLoggedIn, catchAsync(nicknames.getAll))
    .post(isLoggedIn, catchAsync(nicknames.createPlayerWithNickname))

router.get('/new', isLoggedIn, nicknames.renderPlayerNicknameForm)

router.route('/:id')
    .patch(isLoggedIn, catchAsync(nicknames.addNickname))
    .delete(isLoggedIn, catchAsync(nicknames.deletePlayerWithNicknames))
    .get(isLoggedIn, catchAsync(nicknames.getPlayerNicknames))

router.get('/:id/addNickname', isLoggedIn, nicknames.renderAddNicknameForm)

module.exports = router;