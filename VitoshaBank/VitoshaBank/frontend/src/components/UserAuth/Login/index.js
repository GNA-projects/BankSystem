import { useContext, useState } from "react";
import { useHistory } from "react-router-dom";
import { AuthContext } from "../../../Auth";
import { loginUser } from "../../../Api/user";
import VitoshaForm from "../VitoshaForm/index";

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
		<VitoshaForm>
			<VitoshaForm.Icon />
			<VitoshaForm.Input
				heading="username"
				value={username}
				onChange={(e) => setUsername(e.target.value)}
			/>
			<VitoshaForm.Input
				heading="password"
				type="password"
				value={password}
				onChange={(e) => setPassword(e.target.value)}
			/>
			<VitoshaForm.Submit onClick={login} text="Login" />
		</VitoshaForm>
	);
}
