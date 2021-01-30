import axios from "axios";

export const viaxios = axios.create({});

viaxios.interceptors.request.use((config) => {
	const token = "Bearer " + sessionStorage["jwt"];
	config.headers.Authorization = token;

	return config;
});
