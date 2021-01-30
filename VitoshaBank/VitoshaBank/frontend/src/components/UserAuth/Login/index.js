import { useContext, useState } from "react";
import { useHistory } from "react-router-dom";
import { AuthContext } from "../../../Auth";
import { loginUser } from "../../../Api/user";
import { Form } from "../style";

export default function Login() {
	const { setLogged } = useContext(AuthContext);
	const [username, setUsername] = useState();
	const [password, setPassword] = useState();
	const history = useHistory();
	const login = async () => {
		const logged = await loginUser(username, password);
		setLogged(logged)
		history.push("/ebanking");
	};
	return (
		<Form>
			<Form.Icon />
			<Form.Input
				heading="username"
				value={username}
				onChange={(e) => setUsername(e.target.value)}
			/>
			<Form.Input
				heading="password"
				type="password"
				value={password}
				onChange={(e) => setPassword(e.target.value)}
			/>
			<Form.Submit onClick={login} text="Login" />
		</Form>
	);
}
