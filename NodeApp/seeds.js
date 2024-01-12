const mongoose = require('mongoose');

const Nickname = require('./models/nickname');

mongoose.connect('mongodb://localhost:27017/robotLegs')
    .then(() => {
        console.log("MONGO CONNECTION OPEN!")
    })
    .catch(err => {
        console.log("MONGO CONNECTION ERROR!")
        console.log(err)
    })

const seedData = [
    {
        name: 'Cunha',
        nicknames: ["Cunha Matata"]
    },
    {
        name: 'Jesus',
        nicknames: ['xJesus']
    },
    {
        name: 'Haaland',
        nicknames: ['Aryan Heskey', 'Hahahaaland', 'The Haalander', 'The Terminator']
    },
    {
        name: 'Longstaff',
        nicknames: ['al-Ramh']
    }
]

Nickname.insertMany(seedData)
    .then(res => {
        console.log(res)
    })
    .catch(err => {
        console.log(err)
    })