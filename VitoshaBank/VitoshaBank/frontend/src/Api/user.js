import axios from "axios";

const config = {
	headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
};

export const loginUser = async (username, password, setLogged) => {
	await axios
		.post("api/users/login", {
			user: {
				username: username,
				password: password,
			},
		})
		.then(
			(res) => {
				sessionStorage["jwt"] = res.data.message;
				console.log("add token");

				setLogged(true);
			},
			(error) => {
				sessionStorage.removeItem("jwt");
				console.log(1);

				setLogged(false);
			}
		);
};

export const getBankAccount = async (setAmount, setIban) => {
	await axios.get("api/bankaccount/", config).then(
		(res) => {
			console.log(res.data)
			setAmount(res.data.amount)
			setIban(res.data.iban)
		},
		(error) => {
			console.log(error)
		}
	);
};
export const getDeposit = async (setAmount, setIban) => {
	await axios.get("api/deposit/", config).then(
		(res) => {
			console.log(res.data)
			setAmount(res.data.amount)
			setIban(res.data.iban)
		},
		(error) => {
			console.log(error)
		}
	);
};
export const getCredit = async (setAmount, setIban) => {
	await axios.get("api/credit/", config).then(
		(res) => {
			console.log(res.data)
			setAmount(res.data.amount)
			setIban(res.data.iban)
		},
		(error) => {
			console.log(error)
		}
	);
};
export const getWallet = async (setAmount, setIban) => {
	await axios.get("api/wallet/", config).then(
		(res) => {
			console.log(res.data)
			setAmount(res.data.amount)
			setIban(res.data.iban)
		},
		(error) => {
			console.log(error)
		}
	);
};


