import axios from "axios";

const config = {
	headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
};

export const createUser = (uname, fname, lname, password, mail, admin) => {
	axios
		.post(
			"api/admin/create/user",
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
			config
		)
		.then((res) => console.log(res))
		.catch((err) => console.log(err));
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
			config
		)
		.then((res) => console.log(res))
		.catch((err) => console.log(err));
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
			config
		)
		.then((res) => console.log(res))
		.catch((err) => console.log(err));
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
			config
		)
		.then((res) => console.log(res))
		.catch((err) => console.log(err));
};
