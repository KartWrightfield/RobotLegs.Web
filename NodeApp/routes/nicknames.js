﻿const express = require('express');
const catchAsync = require("../utils/catchAsync");
const Nickname = require("../models/nickname");
const Joi = require("joi");
const ExpressError = require("../utils/ExpressError");
const router = express.Router();

router.get('/', catchAsync(async (req, res) => {
    const nicknames = await Nickname.find({});

    res.render('nicknames/index', { nicknames });
}))

router.get('/new', (req, res) => {
    res.render('nicknames/new');
})

router.post('/', catchAsync(async (req, res) => {
    const nicknameSchema = Joi.object({
        name: Joi.string().required(),
        nickname: Joi.string().required()
    })

    const { error } = nicknameSchema.validate(req.body);
    if (error){
        const msg = error.details.map(el => el.message).join(',');
        throw new ExpressError(msg, 400);
    }

    const newNickname = new Nickname(req.body);
    newNickname.nicknames.push(req.body.nickname);

    await newNickname.save();

    req.flash('success', 'Successfully added new player & nickname');
    res.redirect(`/nicknames/${newNickname.id}`);
}))

router.patch('/:id', catchAsync(async (req, res) => {
    if (!req.body.nickname){
        throw new ExpressError("Nickname is required", 400);
    }

    const { id } = req.params;
    const nickname = req.body.nickname;
    const playerNicknames = await Nickname.findById(id);

    playerNicknames.nicknames.push(nickname);

    await playerNicknames.save();

    req.flash('success', 'Successfully added new nickname');
    res.redirect(`/nicknames/${id}`);
}))

router.delete('/:id', catchAsync(async (req, res) => {
    const { id } = req.params;
    await Nickname.findByIdAndDelete(id);

    req.flash('success', 'Successfully deleted player & nicknames');
    res.redirect('/nicknames');
}))

router.get('/:id', catchAsync(async (req, res) => {
    const { id } = req.params;
    const nickname = await Nickname.findById(id);

    res.render('nicknames/details', { nickname });
}))

router.get('/:id/addNickname', (req, res) => {
    const { id } = req.params;
    res.render(`nicknames/addNickname`, { id });
})

module.exports = router;