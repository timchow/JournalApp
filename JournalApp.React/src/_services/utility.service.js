export const serviceUtility = {
    handleResponse,
    handleDataResponse
};


function handleResponse(response) {
    if (!response.ok) {
        return Promise.reject(response.json());
    }

    return response.json();
}

function handleDataResponse(data) {
    // login successful if there's a jwt token in the response
    // store user details and jwt token in local storage to keep user logged in between page refreshes
    if (data.token && data.token.auth_token) {
        localStorage.setItem('userData', JSON.stringify(data));
    }

    return data;
}