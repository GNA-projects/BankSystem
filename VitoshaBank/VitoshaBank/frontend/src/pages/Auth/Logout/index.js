import { useContext } from "react";
import { useHistory } from "react-router-dom";
import { AuthContext, logoutUser } from "../../../Auth";
import VitoshaForm from "../../../components/VitoshaForm/index";

export default function Logout() {
	const { setLogged } = useContext(AuthContext);
	const history = useHistory();
	const logout = () => {
		history.push("/login");
		logoutUser();
		setLogged(false);
	};
	return (
		<VitoshaForm>
			<VitoshaForm.Icon />
			<VitoshaForm.Submit onClick={logout} text="Logout" />
		</VitoshaForm>
	);
}
