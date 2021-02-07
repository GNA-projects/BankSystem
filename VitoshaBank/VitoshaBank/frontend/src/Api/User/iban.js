import { viaxios } from "../../axios";

export const getChargeIban = () => {
	return viaxios.get("api/charge/").then(
		(res) => {
			return res.data.iban;
		},
		(error) => {
			return error.response.data.message;
		}
	);
};

export const getDepositIban = () => {
	return viaxios.get("api/deposit/").then(
		(res) => {
			return res.data.iban;
		},
		(error) => {
			return error.response.data.message;
		}
	);
};
export const getCreditIban = () => {
	return viaxios.get("api/credit/").then(
		(res) => {
			return res.data.iban;
		},
		(error) => {
			return error.response.data.message;
		}
	);
};
export const getWalletIban = () => {
	return viaxios.get("api/wallet/").then(
		(res) => {
			return res.data.iban;
		},
		(error) => {
			return error.response.data.message;
		}
	);
};
