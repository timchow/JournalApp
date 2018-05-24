import { userConstants } from '../_constants';
import { userServiceFacebook } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userActionsFacebook = {
    login,
    logout
};

function login(accessToken) {
    return dispatch => {
        dispatch(request({ accessToken }));

        userServiceFacebook.login(accessToken).then(user => {
                dispatch(success(user));
                history.push('/');
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(error));
            });
    };

    function request(token) { return { type: userConstants.LOGIN_REQUEST_FACEBOOK, token } }

    function success(user) { return { type: userConstants.LOGIN_SUCCESS_FACEBOOK, user } }

    function failure(error) { return { type: userConstants.LOGIN_FAILURE_FACEBOOK, error } }
}

function logout() {
    userServiceFacebook.logout();
    return { type: userConstants.LOGOUT };
}