import { Route, Switch } from "react-router-dom";
import Home from "./components/Home";
import { PrivateRoute, AdminRoute } from "./Auth";

import Layout from "./components/Layout";
import MyAccount from "./components/MyAccount";

import Login from "./components/UserAuth/Login";
import Logout from "./components/UserAuth/Logout";

import Ebanking from "./components/Ebanking";
import ChargePage from "./components/Ebanking/AccountPages/Charge";
import DepositPage from "./components/Ebanking/AccountPages/Deposit";
import WalletPage from "./components/Ebanking/AccountPages/Wallet";
import CreditPage from "./components/Ebanking/AccountPages/Credit";

import Admin from "./components/Admin";
import CreateUser from "./components/Admin/User/Create";
import CreateCharge from "./components/Admin/Account/Create/Charge";
import CreateCredit from "./components/Admin/Account/Create/Credit";
import CreateDeposit from "./components/Admin/Account/Create/Deposit";
import CreateWallet from "./components/Admin/Account/Create/Wallet";
import CreateDebitCard from "./components/Admin/Account/Create/DebitCard";
import DeleteUser from "./components/Admin/User/Delete";
import DeleteAccount from "./components/Admin/Account/Delete";

function App() {
	return (
		<Layout>
			<Switch>
				<PrivateRoute exact path="/" component={Home} />
				<PrivateRoute exact path="/account" component={MyAccount} />
				<Route exact path="/login" component={Login} />
				<PrivateRoute exact path="/logout" component={Logout} />

				<PrivateRoute exact path="/ebanking" component={Ebanking} />
				<PrivateRoute exact path="/charge" component={ChargePage} />
				<PrivateRoute exact path="/deposit" component={DepositPage} />
				<PrivateRoute exact path="/wallet" component={WalletPage} />
				<PrivateRoute exact path="/credit" component={CreditPage} />

				<AdminRoute exact path="/admin" component={Admin} />
				<AdminRoute exact path="/admin/create/user" component={CreateUser} />
				<AdminRoute
					exact
					path="/admin/create/charge"
					component={CreateCharge}
				/>
				<AdminRoute
					exact
					path="/admin/create/credit"
					component={CreateCredit}
				/>
				<AdminRoute
					exact
					path="/admin/create/deposit"
					component={CreateDeposit}
				/>
				<AdminRoute
					exact
					path="/admin/create/wallet"
					component={CreateWallet}
				/>
				<AdminRoute
					exact
					path="/admin/create/debit"
					component={CreateDebitCard}
				/>
				<AdminRoute exact path="/admin/delete/user" component={DeleteUser} />
				<AdminRoute
					exact
					path="/admin/delete/account"
					component={DeleteAccount}
				/>
			</Switch>
		</Layout>
	);
}

export default App;
