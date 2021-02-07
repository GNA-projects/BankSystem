import { viaxios } from "../../axios";

export const getCharge = (setAmount, setIban) => {
	viaxios.get("api/charge/").then(
		(res) => {
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			setIban(error.response.data.message);
		}
	);
};
export const getDeposit = (setAmount, setIban) => {
	viaxios.get("api/deposit/").then(
		(res) => {
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			setIban(error.response.data.message);
		}
	);
};
export const getCredit = (setAmount, setIban) => {
	viaxios.get("api/credit/").then(
		(res) => {
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			setIban(error.response.data.message);
		}
	);
};
export const getWallet = (setAmount, setIban) => {
	viaxios.get("api/wallet/").then(
		(res) => {
			setAmount(res.data.amount);
			setIban(res.data.iban);
		},
		(error) => {
			setIban(error.response.data.message);
		}
	);
};