import "@babel/polyfill";
import React from 'react'
import ReactDOM from 'react-dom'
import { Router } from 'react-router'
import { setAuthToken, handleResponse } from './axiosExtensions';
import history from './history'

import App from './App';
import './index.css';

if (localStorage.getItem('cs-token-access')) {
    setAuthToken(localStorage.getItem('cs-token-access'));
}

handleResponse();

ReactDOM.render(
    <Router history={history}>
        <App />
    </Router>,
    document.getElementById('root')
)
