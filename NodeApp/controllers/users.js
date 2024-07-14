const User = require('../models/user');

module.exports.login = (req, res)=> {
    req.flash('success', 'Logged In');
    const redirectUrl = res.locals.returnTo || '/';
    res.redirect(redirectUrl);
};

module.exports.logout = (req, res, next)=> {
    req.logout(function (err) {
        if (err){
            return next(err);
        }
        req.flash('success', 'Logged Out');
        res.redirect('/');
    });
};

module.exports.register = async (req, res, next) => {
    try {
        const {username, email, password} = req.body;
        const user = new User({email, username});
        const registeredUser = await User.register(user, password);

        req.login(registeredUser, (err) => {
            if (err) next(err);

            req.flash('success', 'Account Created');
            res.redirect('/');
        })

    } catch (e) {
        req.flash('error', e.message)
        res.redirect('/register')
    }
}

module.exports.renderLoginForm = (req, res)=> {
    res.render('users/login');
};

module.exports.renderRegistrationForm = (req, res)=>{
    res.render('users/register');
};