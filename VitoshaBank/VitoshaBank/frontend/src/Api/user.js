import axios from "axios";

const getConfig = () => {
	return {
		headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
	};
};

export const loginUser = (username, password) => {
	return axios
		.post("api/users/login", {
			user: {
				username: username,
				password: password,
			},
		})
		.then((res) => {
			sessionStorage["jwt"] = res.data.message;
			return true;
		})
		.catch(() => {
			sessionStorage.removeItem("jwt");
			return false;
		});
};

export const getBankAccount = (setAmount, setIban) => {
	axios.get("api/bankaccount/", getConfig()).then(
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
	axios.get("api/deposit/", getConfig()).then(
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
	axios.get("api/credit/", getConfig()).then(
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
	axios.get("api/wallet/", getConfig()).then(
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
	axios
		.put(
			"api/users/changepass",
			{
				User: {
					Password: password, //must be min 6 symbols
				},
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
