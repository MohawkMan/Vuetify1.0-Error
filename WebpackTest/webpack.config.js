var path = require('path');

module.exports = {
    entry: './wwwroot/src/js/app.js',
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'bundle.js'
    }
};