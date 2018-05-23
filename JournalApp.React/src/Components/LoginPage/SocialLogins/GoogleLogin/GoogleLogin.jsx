import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { userActionsGoogle } from '../../../../_actions';
import { apiConstants } from '../../../../_constants';
import { Config } from '../../../../../web.config';
import logo from './btn_google_signin_light_normal_web.png';

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

		this.setState({ accessToken: result.accessToken });

		const { accessToken } = this.state;
		const { dispatch } = this.props;

		if (accessToken) {
			dispatch(userActionsGoogle.login(accessToken));
		}

	}

	launchGoogleLogin() {
		let client_id = Config.GOOGLE.API_CLIENT_ID;
		let scopes = Config.GOOGLE.SCOPES;
		let redirect_uri = Config.GOOGLE.REDIRECT_URI;
		let url = this.getGoogleAuthUrl(client_id, scopes, redirect_uri);

		this.popup = window.open(url, null, 'width=600,height=400');
	}

	getGoogleAuthUrl(client_id, scopes, redirect_uri) {
		return `https://accounts.google.com/o/oauth2/v2/auth?
			scope=${scopes}&
			include_granted_scopes=true&
			state=state_parameter_passthrough_value&
			redirect_uri=${redirect_uri}&
			response_type=token&
			client_id=${client_id}`;
	}

	render() {
		const { user, users } = this.props;
		return (
			<a onClick={this.launchGoogleLogin}
				style={styles}>
				<img src={logo}>
				</img>
			</a>
		);
	}
}

const styles = {
	//margin: '10px'
};

function mapStateToProps(state) {
	const { loggingIn } = state.authentication;
	return {
		loggingIn
	};
}

const connectedGoogleLogin = connect(mapStateToProps)(GoogleLogin);
export { connectedGoogleLogin as GoogleLogin };