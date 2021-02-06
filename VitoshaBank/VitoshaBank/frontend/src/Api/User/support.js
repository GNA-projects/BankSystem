import { viaxios } from "../../axios";

export const sendTicket = (title, message) => {
	viaxios
		.post("api/support/create", {
			Ticket: {
				title: title,
				message: message
			},
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
