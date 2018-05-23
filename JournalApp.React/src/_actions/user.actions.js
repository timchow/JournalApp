import { userConstants, messageConstants } from '../_constants';
import { userService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActions = {
    login,
    logout,
    signup,
    getAll
};

function signup(user) {
    return dispatch => {
        dispatch(request(user));

        userService.signup(user)
            .then(user => {
                    dispatch(success(user));
                    history.push('/login');
                },
                error => {
                    error.then(error => {
                        if (error.length) {
                            dispatch(failure(error[0]));
                            dispatch(alertActions.error(error[0].description));
                        }
                    })
                });
    };


    function request(user) { return { type: userConstants.SIGNUP_REQUEST, user } }

    function success(user) { return { type: userConstants.SIGNUP_SUCCESS, user } }

    function failure(error) { return { type: userConstants.SIGNUP_FAILURE, error } }
}

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
                user => {
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(messageConstants.SERVER_DOWN));
                }
            );
    };

    function request(user) { return { type: userConstants.LOGIN_REQUEST, user } }

    function success(user) { return { type: userConstants.LOGIN_SUCCESS, user } }

    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error } }
}

function logout() {
    userService.logout();
    return { type: userConstants.LOGOUT };
}

function getAll() {
    return dispatch => {
        dispatch(request());

        userService.getAll()
            .then(
                users => dispatch(success(users)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: userConstants.GETALL_REQUEST } }

    function success(users) { return { type: userConstants.GETALL_SUCCESS, users } }

    function failure(error) { return { type: userConstants.GETALL_FAILURE, error } }
}