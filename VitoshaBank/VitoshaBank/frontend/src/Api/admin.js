import axios from "axios";

const getDelConfig = () => {
	return { Authorization: "Bearer " + sessionStorage["jwt"] };
};
const getConfig = () => {
	return {
		headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
	};
};

export const createUser = (uname, fname, lname, password, mail, admin) => {
	axios
		.post(
			"/api/admin/create/user",
			{
				user: {
					Username: uname, //must be > 5 symbols
					FirstName: fname, //must be min 1 symbol
					LastName: lname, //must be min 1 symbol
					Password: password, //must be min 6 symbols
					Email: mail, //must be min 1 symbol
					IsAdmin: admin,
				},
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createBankAccount = (uname, amount) => {
	axios
		.post(
			"/api/admin/create/bankaccount",
			{
				BankAccount: {
					Amount: amount,
				},
				Username: uname,
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createCredit = (uname, amount, period) => {
	axios
		.post(
			"/api/admin/create/credit",
			{
				Credit: {
					Amount: amount,
				},
				Username: uname,
				Period: period,
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createDeposit = (uname, amount, period) => {
	axios
		.post(
			"/api/admin/create/deposit",
			{
				Deposit: {
					TermOfPayment: period,
					Amount: amount,
				},
				Username: uname,
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createDebitCard = (iban) => {
	axios
		.post(
			"/api/admin/create/debitcard",
			{
				BankAccount: {
					IBAN: iban,
				},
			},
			getConfig()
		)
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const deleteUser = (uname) => {
	axios
		.delete("/api/admin/delete/user", {
			data: { username: uname },
			headers: getDelConfig(),
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const deleteAccount = (uname, accountType) => {
	axios
		.delete(`/api/admin/delete/${accountType}`, {
			data: { username: uname },
			headers: getDelConfig(),
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
