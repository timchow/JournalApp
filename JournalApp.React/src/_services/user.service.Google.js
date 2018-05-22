import { authHeader } from '../_helpers';

export const userServiceGoogle = {
    login,
    logout,
    getAll
};

function login(accessToken) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Access-Control-Allow-Methods': 'GET, POST, PATCH, PUT, DELETE, OPTIONS',
                    'Access-Control-Allow-Headers': 'Origin, Content-Type, X-Auth-Token' },
        body: JSON.stringify({ accessToken })
    };

    return fetch('http://localhost:61121/api/auth/login/Google', requestOptions)
        .then(response => {
            if (!response.ok) { 
                return Promise.reject(response.statusText);
            }
            return response.json();
        })
        .then(data => {
            // login successful if there's a jwt token in the response
            debugger;
            let tokenInfo = data[0];
            let userInfo = data[1];
            if (tokenInfo && tokenInfo.auth_token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(tokenInfo));
            }

            return userInfo;
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