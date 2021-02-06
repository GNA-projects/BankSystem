import { Route, Switch } from "react-router-dom";
import Home from "./pages/Home";
import { PrivateRoute, AdminRoute } from "./Auth";

import Layout from "./components/Layout";
import MyAccount from "./pages/MyAccount";

import Login from "./pages/Auth/Login";
import Logout from "./pages/Auth/Logout";

import Ebanking from "./pages/Ebanking";
import ChargePage from "./pages/Ebanking/Charge";
import DepositPage from "./pages/Ebanking/Deposit";
import WalletPage from "./pages/Ebanking/Wallet";
import CreditPage from "./pages/Ebanking/Credit";

import Admin from "./pages/Admin";
import CreateUser from "./pages/Admin/User/Create";
import CreateCharge from "./pages/Admin/Account/Create/Charge";
import CreateCredit from "./pages/Admin/Account/Create/Credit";
import CreateDeposit from "./pages/Admin/Account/Create/Deposit";
import CreateWallet from "./pages/Admin/Account/Create/Wallet";
import CreateDebitCard from "./pages/Admin/Account/Create/DebitCard";
import Support from "./pages/Admin/User/Support";
import DeleteUser from "./pages/Admin/User/Delete";
import DeleteAccount from "./pages/Admin/Account/Delete";

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
				<AdminRoute
					exact
					path="/admin/support"
					component={Support}
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
