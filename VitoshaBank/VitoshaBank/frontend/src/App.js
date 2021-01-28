import { Route, Switch } from "react-router-dom";
import Home from "./components/Home";
import { PrivateRoute,AdminRoute } from "./Auth";

import Layout from "./components/Layout";
import MyAccount from "./components/MyAccount";

import Login from "./components/UserAuth/Login";
import Logout from "./components/UserAuth/Logout";

import Ebanking from './components/Ebanking'

import Admin from './components/Admin'
import CreateUser from './components/Admin/User/Create'
import BankAccount from './components/Admin/Account/Create/BankAccount'
import Credit from './components/Admin/Account/Create/Credit'
import DebitCard from './components/Admin/Account/Create/DebitCard'


function App() {
	return (
		<Layout>
			<Switch>
				<PrivateRoute exact path="/" component={Home} />
				<PrivateRoute exact path="/account" component={MyAccount} />
				<Route exact path="/login" component={Login} />
				<PrivateRoute exact path="/logout" component={Logout} />
				<PrivateRoute exact path="/ebanking" component={Ebanking} />
				<AdminRoute exact path="/admin" component={Admin}/>
				<AdminRoute exact path="/admin/create/user" component={CreateUser}/>
				<AdminRoute exact path="/admin/create/baccount" component={BankAccount}/>
				<AdminRoute exact path="/admin/create/credit" component={Credit}/>
				<AdminRoute exact path="/admin/create/debit" component={DebitCard}/>
			</Switch>
		</Layout>
	);
}

export default App;
