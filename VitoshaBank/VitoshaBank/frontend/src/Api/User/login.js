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
		.catch((err) => {
			sessionStorage.removeItem("jwt");
			alert(err.response.data.message);
			return false;
		});
};
