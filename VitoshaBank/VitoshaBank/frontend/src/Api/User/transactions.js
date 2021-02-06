import { viaxios } from "../../axios";

export const getAllTransactions = () => {
	return viaxios
		.get("/api/transactions")
		.then((res) => {
			console.log(res.data);
			return res.data;
		})
		.catch((err) => {
			alert(err.response.data.message)
			return [];
		})
};