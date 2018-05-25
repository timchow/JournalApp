import { authHeader } from '../_helpers';
import { apiConstants } from '../_constants';
import { Config } from '../../web.config';
import { serviceUtility } from './utility.service';

export const userService = {
    login,
    logout,
    signup,
    getAll
};

function login(username, password) {
    let data = {
        username,
        password
    };

    const requestOptions = {
        method: 'POST',
        headers: getLooseHeaders(),
        body: JSON.stringify(data)
    };

    const requestURL = [Config.SERVER_URL, Config.SERVER_API_BASE, apiConstants.USER_LOGIN_URL].join('/');

    return fetch(requestURL, requestOptions)
        .then(serviceUtility.handleResponse)
        .then(serviceUtility.handleDataResponse);
}

function signup(user) {
    const requestOptions = {
        method: 'POST',
        headers: getLooseHeaders(),
        body: JSON.stringify(user)
    };

    const requestURL = [Config.SERVER_URL, Config.SERVER_API_BASE, apiConstants.USER_SIGNUP_URL].join('/');

    return fetch(requestURL, requestOptions)
        .then(serviceUtility.handleResponse);

}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch('/users', requestOptions).then(handleResponse);
}

function getLooseHeaders() {
    return {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token'
    };
}