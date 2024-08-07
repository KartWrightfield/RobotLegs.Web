﻿const Joi = require("joi");
const Nickname = require("../models/nickname");
const ExpressError = require("../utils/ExpressError");

module.exports.addNickname = async (req, res) => {
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
};

module.exports.createPlayerWithNickname = async (req, res) => {
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
};

module.exports.deletePlayerWithNicknames = async (req, res) => {
    const { id } = req.params;
    await Nickname.findByIdAndDelete(id);

    req.flash('success', 'Successfully deleted player & nicknames');
    res.redirect('/nicknames');
};

module.exports.getAll = async (req, res) => {
    const nicknames = await Nickname.find({});
    res.render('nicknames/index', { nicknames });
};

module.exports.getPlayerNicknames = async (req, res) => {
    const { id } = req.params;
    const nickname = await Nickname.findById(id);

    if (!nickname){
        req.flash('error', 'Unable to find nicknames for that player!')
    }

    res.render('nicknames/details', { nickname });
};

module.exports.renderAddNicknameForm = (req, res) => {
    const { id } = req.params;
    res.render(`nicknames/addNickname`, { id });
};

module.exports.renderPlayerNicknameForm = (req, res) => {
    res.render('nicknames/new');
};