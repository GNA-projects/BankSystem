import { viaxios } from "../../axios";

export const changePassword = (password) => {
	viaxios
		.put("api/users/changepass", {
			User: {
				Password: password, //must be min 6 symbols
			},
		})
		.then((res) => alert(res.data.message))
		.catch((err) => alert(err.response.data.message));
};
