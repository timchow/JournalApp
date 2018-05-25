import { userConstants } from '../_constants';

let data = JSON.parse(localStorage.getItem('userData'));
const initialState = data ? {
    loggedIn: true,
    user: data.user
} : {};

export function authentication(state = initialState, action) {
    switch (action.type) {
        case userConstants.LOGIN_REQUEST:
            return {
                loggingIn: true,
                user: action.user
            };

        case userConstants.LOGIN_REQUEST_GOOGLE:
        case userConstants.LOGIN_REQUEST_FACEBOOK:
            return {
                loggingIn: true
            };
        case userConstants.LOGIN_SUCCESS:
        case userConstants.LOGIN_SUCCESS_GOOGLE:
        case userConstants.LOGIN_SUCCESS_FACEBOOK:
            return {
                loggedIn: true,
                user: action.data.user
            };
        case userConstants.LOGIN_FAILURE:
        case userConstants.LOGIN_FAILURE_GOOGLE:
        case userConstants.LOGIN_FAILURE_FACEBOOK:
            return {};

        case userConstants.SIGNUP_REQUEST:
            return {
                signingUp: true,
                user: action.user
            };
        case userConstants.SIGNUP_SUCCESS:
            return {
                signedUp: true,
                user: action.user
            };
        case userConstants.LOGOUT:
            return {};
        default:
            return state
    }
}