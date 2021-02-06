import { viaxios } from "../../axios";
import { getChargeIban, getCreditIban, getDepositIban } from "./iban";

export const depositInCredit = async (amount) => {
	let creditIban = await getCreditIban();
	viaxios
		.put("api/credit/deposit", {
			Credit: {
				IBAN: creditIban,
			},
			Amount: amount,
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};

export const depositInDeposit = async (amount) => {
	let chargeIban = await getChargeIban();
	let depositIban = await getDepositIban();
	viaxios
		.put("api/deposit/deposit", {
			BankAccount: {
				IBAN: chargeIban,
			},
			Deposit: {
				IBAN: depositIban,
			},
			Amount: amount,
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
