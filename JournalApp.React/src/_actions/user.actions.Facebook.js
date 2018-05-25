import { userConstants,messageConstants } from '../_constants';
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

        userServiceFacebook.login(accessToken).then(data => {
                dispatch(success(data));
                history.push('/');
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(messageConstants.SERVER_DOWN));
            });
    };

    function request(token) { return { type: userConstants.LOGIN_REQUEST_FACEBOOK, token } }

    function success(data) { return { type: userConstants.LOGIN_SUCCESS_FACEBOOK, data } }

    function failure(error) { return { type: userConstants.LOGIN_FAILURE_FACEBOOK, error } }
}

function logout() {
    userServiceFacebook.logout();
    return { type: userConstants.LOGOUT };
}