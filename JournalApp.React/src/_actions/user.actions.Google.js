import { userConstants } from '../_constants';
import { userServiceGoogle } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActionsGoogle = {
    login,
    logout
};

function login(accessToken) {
    return dispatch => {
        dispatch(request({ accessToken }));

        userServiceGoogle.login(accessToken).then(user => {
            dispatch(success(user));
            history.push('/');
        },
        error => {
            dispatch(failure(error));
            dispatch(alertActions.error(error));
        });
    };

    function request(token) { return { type: userConstants.LOGIN_REQUEST_GOOGLE, token } }
    function success(user) { return { type: userConstants.LOGIN_SUCCESS_GOOGLE, user } }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE_GOOGLE, error } }
}

function logout() {
    userServiceGoogle.logout();
    return { type: userConstants.LOGOUT };
}