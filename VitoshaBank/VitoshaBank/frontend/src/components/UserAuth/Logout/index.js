import { useContext } from "react";
import { useHistory } from "react-router-dom";
import { AuthContext, logoutUser } from "../../../Auth";
import { Form } from "../style";

export default function Logout() {
	const { setLogged } = useContext(AuthContext);
	const history = useHistory();
	const logout = () => {
		history.push("/login");
		logoutUser();
		setLogged(false);
	};
	return (
		<Form>
			<Form.Icon />
			<Form.Submit onClick={logout} text="Logout" />
		</Form>
	);
}
