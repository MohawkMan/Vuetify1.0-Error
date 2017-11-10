var path = require('path');
var webpack = require('webpack');

module.exports = {
    entry: './TypeScript/app.ts',
    module: {
        rules: [
            {
                test: /.ts$/,
                loader: 'ts-loader',
                exclude: /node_modules/
            }
        ]
    },
    resolve: {
        extensions: [".ts", ".js"]
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            Popper: 'popper.js'
        })
    ],
    output: {
        path: path.resolve(__dirname, 'wwwroot/js'),
        filename: '[name].js'
    }
};