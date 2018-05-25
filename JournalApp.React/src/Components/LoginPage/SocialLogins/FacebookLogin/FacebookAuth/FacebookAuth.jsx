import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { userActions } from '../../../../../_actions';

class FacebookAuth extends React.Component {

	getParameterByName(name, url) {
		if (!url) url = window.location.href;
		name = name.replace(/[\[\]]/g, "\\$&");
		var regex = new RegExp("[?&#]" + name + "(=([^&#]*)|&|#|$)"),
			results = regex.exec(url);
		if (!results) return null;
		if (!results[2]) return '';
		return decodeURIComponent(results[2].replace(/\+/g, " "));
	}

	componentDidMount() {
		// if we don't receive an access token then login failed and/or the user has not connected properly

		var accessToken = this.getParameterByName("access_token");
		var message = {};
		if (accessToken) {
			message.status = true;
			message.accessToken = accessToken;
		}
		else {
			message.status = false;
			message.error = getParameterByName("error");
			message.errorDescription = getParameterByName("error_description");
		}
		window.opener.postMessage(JSON.stringify(message), window.location.origin);
	}

	render() {
		return null;
	}
}

function mapStateToProps(state) {
	const { loggingIn } = state.authentication;
	return {
		loggingIn
	};
}

const connectedFacebookAuth = connect(mapStateToProps)(FacebookAuth);
export { connectedFacebookAuth as FacebookAuth };