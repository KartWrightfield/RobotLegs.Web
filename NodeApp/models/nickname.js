const mongoose = require('mongoose');

const nicknameSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    nicknames: {
        type: [String],
        required: true
    }
});

const Nickname = mongoose.model('Nickname', nicknameSchema);

module.exports = Nickname;