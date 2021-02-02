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
import Charge from './components/Admin/Account/Create/Charge'
import Credit from './components/Admin/Account/Create/Credit'
import Deposit from './components/Admin/Account/Create/Deposit'
import Wallet from './components/Admin/Account/Create/Wallet'
import DebitCard from './components/Admin/Account/Create/DebitCard'
import DeleteUser from './components/Admin/User/Delete'
import DeleteAccount from './components/Admin/Account/Delete'


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
				<AdminRoute exact path="/admin/create/charge" component={Charge}/>
				<AdminRoute exact path="/admin/create/credit" component={Credit}/>
				<AdminRoute exact path="/admin/create/deposit" component={Deposit}/>
				<AdminRoute exact path="/admin/create/wallet" component={Wallet}/>
				<AdminRoute exact path="/admin/create/debit" component={DebitCard}/>
				<AdminRoute exact path="/admin/delete/user" component={DeleteUser}/>
				<AdminRoute exact path="/admin/delete/account" component={DeleteAccount}/>
			</Switch>
		</Layout>
	);
}

export default App;
