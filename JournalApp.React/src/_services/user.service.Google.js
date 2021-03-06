import { authHeader } from '../_helpers';
import { apiConstants } from '../_constants';
import { Config } from '../../web.config';
import { serviceUtility } from './utility.service';

export const userServiceGoogle = {
    login,
    logout,
    getAll
};

function login(accessToken) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
            'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token'
        },
        body: JSON.stringify({ accessToken })
    };

    const requestURL = [Config.SERVER_URL, Config.SERVER_API_BASE, apiConstants.USER_LOGIN_GOOGLE_URL].join('/');

    return fetch(requestURL, requestOptions)
        .then(serviceUtility.handleResponse)
        .then(serviceUtility.handleDataResponse);
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