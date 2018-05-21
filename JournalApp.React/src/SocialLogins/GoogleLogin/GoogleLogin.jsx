import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { userActions } from '../../_actions';

class GoogleLogin extends React.Component {

    constructor() {
        super();
        this.popup = {};
        this.launchGoogleLogin = this.launchGoogleLogin.bind(this);
        this.handleMessage = this.handleMessage.bind(this);
    }

    componentDidMount() {
        if (window.addEventListener) {

            window.addEventListener("message", this.handleMessage, false);
        } else {
        }
    }

    handleMessage(e) {
        // Only handle messages coming from the Auth window
        if (e && e.source && e.source.location &&
            e.source.location.pathname !== "/GoogleAuth") {
            return;
        }

        this.popup.close();
        let result = JSON.parse(e.data);

        if (!result.status) return;

        let accessToken = result.accessToken;
        console.log(accessToken);
    }

    launchGoogleLogin() {
        let client_id = '425042187496-r380dek5d1333hmvp4p4tfkf5podeaja.apps.googleusercontent.com';
        let scopes = 'https://www.googleapis.com/auth/userinfo.profile';
        let redirect_uri = 'http://localhost:8081/GoogleAuth';
        let url = `https://accounts.google.com/o/oauth2/v2/auth?scope=${scopes}&include_granted_scopes=true&state=state_parameter_passthrough_value&redirect_uri=${redirect_uri}&response_type=token&client_id=${client_id}`;

        this.popup = window.open(url, null, 'width=600,height=400');
    }

    render() {
        const { user, users } = this.props;
        return (
            <div>
                <button onClick={this.launchGoogleLogin}
                >Log in with Google!</button>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { loggingIn } = state.authentication;
    return {
        loggingIn
    };
}

const connectedGoogleLogin = connect(mapStateToProps)(GoogleLogin);
export { connectedGoogleLogin as GoogleLogin };