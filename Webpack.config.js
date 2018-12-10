const webpack = require('webpack');
const path = require('path');
module.exports = {
    watch: true,
    entry: './wwwroot/src/js/index.js',
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: 'bundle.js'
    },
    module: {
        rules: [
            {
                test: /\.css$/, use: ['style-loader', 'css-loader'],
            },
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader', options: {
                        presets:
                            ['babel-preset-react', 'babel-preset-env', "stage-2"]
                    }
                }
            }
        ]
    },
    resolve: {
        extensions: ['*', '.js', '.jsx']
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            'window.jQuery': 'jquery',
        })
    ],
    devServer: {
        contentBase: './dist',
        hot: true
    }
};