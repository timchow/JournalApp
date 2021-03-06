import React from 'react';
import { Router, Route } from 'react-router-dom';
import { connect } from 'react-redux';

import { history } from '../_helpers';
import { alertActions } from '../_actions';
import { PrivateRoute } from '../_components';
import { HomePage } from '../Components/HomePage';
import { LoginPage } from '../Components/LoginPage';
import { SignUpPage } from '../Components/SignUpPage';
import { GoogleAuth } from '../Components/LoginPage/SocialLogins/GoogleLogin/GoogleAuth';
import { FacebookAuth } from '../Components/LoginPage/SocialLogins/FacebookLogin/FacebookAuth';

class App extends React.Component {
	constructor(props) {
		super(props);

		const { dispatch } = this.props;
		history.listen((location, action) => {
			// clear alert on location change
			dispatch(alertActions.clear());
		});
	}

	render() {
		const { alert } = this.props;
		return (
			<div className="jumbotron">
				<div className="container">
					<div className="col-sm-8 col-sm-offset-2">
						{alert.message &&
							<div className={`alert ${alert.type}`}>{alert.message}</div>
						}
						<Router history={history}>
							<div>
								<PrivateRoute exact path="/" component={HomePage} />
								<Route path="/login" component={LoginPage} />
								<Route path="/GoogleAuth" component={GoogleAuth} />
								<Route path="/FacebookAuth" component={FacebookAuth} />
								<Route path="/signup" component={SignUpPage} />
							</div>
						</Router>
					</div>
				</div>
			</div>
		);
	}
}

function mapStateToProps(state) {
	const { alert } = state;
	return {
		alert
	};
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App }; 