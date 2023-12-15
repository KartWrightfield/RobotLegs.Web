const express = require("express");
const app = express();
const path = require('path');
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

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

app.listen(3000, () => {
    console.log("LISTENING ON PORT 3000")
})

app.get('/nicknames', (req, res) => {
    res.send('HAVE ALL THE NICKNAMES IN THE WORLD!')
})