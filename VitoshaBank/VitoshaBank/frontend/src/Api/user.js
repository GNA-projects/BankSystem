import { viaxios } from "../axios";

export const loginUser = (username, password) => {
	return viaxios
		.post("api/users/login", {
			user: {
				username: username,
				password: password,
			},
		})
		.then((res) => {
			sessionStorage["jwt"] = res.data.message;
			alert("Welcome, You Are Logged In!");
			return true;
		})
		.catch(() => {
			sessionStorage.removeItem("jwt");
			alert("Check credentials, something went wrong!");
			return false;
		});
};

export const getBankAccount = (setAmount, setIban) => {
	viaxios.get("api/bankaccount/").then(
		(res) => {
			console.log(res.data);
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			console.log(error.response.data.message);
			setIban(error.response.data.message);
		}
	);
};
export const getDeposit = (setAmount, setIban) => {
	viaxios.get("api/deposit/").then(
		(res) => {
			console.log(res.data);
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			console.log(error.response.data.message);
			setIban(error.response.data.message);
		}
	);
};
export const getCredit = (setAmount, setIban) => {
	viaxios.get("api/credit/").then(
		(res) => {
			console.log(res.data);
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			console.log(error.response.data.message);
			setIban(error.response.data.message);
		}
	);
};
export const getWallet = (setAmount, setIban) => {
	viaxios.get("api/wallet/").then(
		(res) => {
			console.log(res.data);
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			console.log(error.response.data.message);
			setIban(error.response.data.message);
		}
	);
};
export const changePass = (password) => {
	viaxios
		.put("api/users/changepass", {
			User: {
				Password: password, //must be min 6 symbols
			},
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
