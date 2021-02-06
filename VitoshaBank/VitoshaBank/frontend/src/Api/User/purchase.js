import { viaxios } from "../../axios";

export const purchaseAmazon = (iban) => {
	viaxios
		.put("api/charge/purchase", {
			BankAccount: {
				IBAN: iban,
			},
			Reciever: "Amazon America",
			Product: "Amazon Prime subscribtion",
			Amount: 36,
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
