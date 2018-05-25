import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { userActionsFacebook } from '../../../../_actions';
import { apiConstants } from '../../../../_constants';
import { Config } from '../../../../../web.config';
import logo from './fb.png';

class FacebookLogin extends React.Component {

	constructor() {
		super();
		this.popup = {};
		this.launchFacebookLogin = this.launchFacebookLogin.bind(this);
		this.handleMessage = this.handleMessage.bind(this);
	}

	componentDidMount() {
		if (window.addEventListener) {

			window.addEventListener("message", this.handleMessage, false);
		} else {
		}
	}

	componentWillUnmount() {
		window.removeEventListener('message', this.handleMessage);
	}

	handleMessage(e) {
		// Only handle messages coming from the Auth window
		if (e && e.source && e.source.location &&
			e.source.location.pathname !== Config.FACEBOOK.REDIRECT_PATH) {
			return;
		}

		this.popup.close();
		let result = JSON.parse(e.data);

		if (!result.status) return;

		this.setState({ accessToken: result.accessToken });

		const { accessToken } = this.state;
		const { dispatch } = this.props;

		if (accessToken) {
			dispatch(userActionsFacebook.login(accessToken));
		}
	}

	launchFacebookLogin() {
		let client_id = Config.FACEBOOK.API_CLIENT_ID,
			scopes = Config.FACEBOOK.SCOPES,
			redirect_uri = Config.FACEBOOK.REDIRECT_URI,
			url = this.getFacebookAuthUrl(client_id, scopes, redirect_uri);

		if (Config.MOCK.ON) {
			url = `${Config.FACEBOOK.REDIRECT_URI}#access_token=${Config.MOCK.TOKEN}`;
		}

		this.popup = window.open(url, null, 'width=600,height=400');
	}

	getFacebookAuthUrl(client_id, scopes, redirect_uri) {
		return `https://www.facebook.com/v2.11/dialog/oauth?&
				response_type=token&
				display=popup&
				client_id=${client_id}&
				display=popup&
				redirect_uri=${redirect_uri}&
				scope=${scopes}`;
	}

	render() {
		const { user, users } = this.props;
		return (
			<a onClick={this.launchFacebookLogin}>
				<img src={logo}>
				</img>
			</a>
		);
	}
}

const styles = {

}

function mapStateToProps(state) {
	const { loggingIn } = state.authentication;
	return {
		loggingIn
	};
}

const connectedFacebookLogin = connect(mapStateToProps)(FacebookLogin);
export { connectedFacebookLogin as FacebookLogin };