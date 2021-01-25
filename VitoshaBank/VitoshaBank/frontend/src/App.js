import { Route, Switch } from "react-router-dom";
import Home from "./components/Home";
import { PrivateRoute,AdminRoute } from "./Auth";

import Layout from "./components/Layout";

import Login from "./components/UserAuth/Login";
import Logout from "./components/UserAuth/Logout";

import Ebanking from './components/Ebanking'

import Admin from './components/Admin'


function App() {
	return (
		<Layout>
			<Switch>
				<PrivateRoute exact path="/" component={Home} />
				<Route exact path="/login" component={Login} />
				<PrivateRoute exact path="/logout" component={Logout} />
				<PrivateRoute exact path="/ebanking" component={Ebanking} />
				<AdminRoute exact path="/admin" component={Admin}/>
			</Switch>
		</Layout>
	);
}

export default App;
