import { useContext } from "react";
import { useHistory } from "react-router-dom";
import { AuthContext, devLogin } from "../../../Auth";
import { Form } from "../style";

export default function Login() {
	const { setLogged } = useContext(AuthContext);
	const history = useHistory();
	const login = () => {
		history.push("/ebanking");
		devLogin();
		setLogged(true);
	};
	return (
		<Form>
			<Form.Icon />
			<Form.Input heading="username" />
			<Form.Input heading="password" type="password" />
			<Form.Submit onClick={login} text="Login" />
		</Form>
	);
}
