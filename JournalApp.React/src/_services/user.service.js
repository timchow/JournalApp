import { authHeader } from '../_helpers';
import { apiConstants } from '../_constants';
import { Config } from '../../web.config';

export const userService = {
    login,
    logout,
    getAll
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
                    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token' },
        body: JSON.stringify({ username, password })
	};
	
	const requestURL = [Config.SERVER_URL,Config.SERVER_API_BASE,apiConstants.USER_LOGIN_URL].join('/');

    return fetch(requestURL, requestOptions)
        .then(response => {
            if (!response.ok) { 
                return Promise.reject(response.statusText);
            }
            return response.json();
        })
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user && user.auth_token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
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

function handleResponse(response) {
    if (!response.ok) { 
        return Promise.reject(response.statusText);
    }

    return response.json();
}