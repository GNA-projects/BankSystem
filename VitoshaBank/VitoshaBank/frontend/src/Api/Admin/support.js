import { viaxios } from "../../axios";

export const getAllTickets = () => {
	return viaxios
		.get("/api/admin/get/support")
		.then((res) => {
			console.log(res.data[0].id)
			return res.data;
		})
		.catch((err) => alert(err.response.data.message));
};

export const getAllTickets = () => {
	return viaxios
		.get("/api/admin/get/support")
		.then((res) => {
			console.log(res.data[0].id)
			return res.data;
		})
		.catch((err) => alert(err.response.data.message));
};

