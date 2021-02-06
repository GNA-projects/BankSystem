import { viaxios } from "../../axios";

export const getAllTickets = () => {
	return viaxios
		.get("/api/admin/get/support")
		.then((res) => {
			console.log(res.data);
			return res.data;
		})
		.catch((err) => {
			alert(err.response.data.message)
			return [];
		})
};

export const respondToTicket = async (ticketId) => {
	await viaxios
		.put("/api/admin/respond/support", { id: ticketId })
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
