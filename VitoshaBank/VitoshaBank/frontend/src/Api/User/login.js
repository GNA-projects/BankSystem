import { viaxios } from "../../axios";

export const loginUser = (username, password) => {
	return viaxios
		.post("api/users/login", {
			user: {
				username: username,
				password: password,
			},
		})
		.then((res) => {
			sessionStorage["jwt"] = res.data.message;
			alert("Welcome, You Are Logged In!");
			return true;
		})
		.catch(() => {
			sessionStorage.removeItem("jwt");
			alert("Check credentials, something went wrong!");
			return false;
		});
};
