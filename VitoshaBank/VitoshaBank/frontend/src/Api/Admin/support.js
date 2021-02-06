import { viaxios } from "../../axios";

export const getAllTickets = () => {
	return viaxios
		.get("/api/admin/get/support")
		.then((res) => {
			alert(res.data.message);
			return res.data;
		})
		.catch((err) => alert(err.response.data.message));
};
