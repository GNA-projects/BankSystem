import { viaxios } from "../axios";

export const createUser = (uname, fname, lname, password, mail, admin) => {
	viaxios
		.post("/api/admin/create/user", {
			user: {
				Username: uname, //must be > 5 symbols
				FirstName: fname, //must be min 1 symbol
				LastName: lname, //must be min 1 symbol
				Password: password, //must be min 6 symbols
				Email: mail, //must be min 1 symbol
				IsAdmin: admin,
			},
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createBankAccount = (uname, amount) => {
	viaxios
		.post("/api/admin/create/bankaccount", {
			BankAccount: {
				Amount: amount,
			},
			Username: uname,
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createCredit = (uname, amount, period) => {
	viaxios
		.post("/api/admin/create/credit", {
			Credit: {
				Amount: amount,
			},
			Username: uname,
			Period: period,
		})
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
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const createDebitCard = (iban) => {
	viaxios
		.post("/api/admin/create/debitcard", {
			BankAccount: {
				IBAN: iban,
			},
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const deleteUser = (uname) => {
	viaxios
		.delete("/api/admin/delete/user", {
			data: { username: uname },
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
export const deleteAccount = (uname, accountType) => {
	viaxios
		.delete(`/api/admin/delete/${accountType}`, {
			data: { username: uname },
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
