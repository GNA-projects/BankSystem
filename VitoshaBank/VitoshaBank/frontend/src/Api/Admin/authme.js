import { viaxios } from "../../axios";

export const authme = async () => {
	return await viaxios
		.get("/api/admin/authme")
		.then((res) => {
			return true;
		})
		.catch((err) => {
			return false;
		})
};
