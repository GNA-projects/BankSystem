import React from "react";
import { useState } from "react/cjs/react.development";
import { changePass } from "../../Api/user";

export default function MyAccount() {
	const [pass, setPass] = useState();
	const [confirm, setConfirm] = useState();
	const handleSubmit = () => {
		if (pass === confirm) changePass(pass);
		else alert("passwords do not mach");
	};
	return (
		<div>
			<h1>change pass</h1>
			<input
				type="password"
				onChange={(e) => setPass(e.target.value)}
				value={pass}
			></input>
			<input
				type="password"
				onChange={(e) => setConfirm(e.target.value)}
				value={confirm}
			></input>
			<button onClick={() => handleSubmit()}>change</button>
		</div>
	);
}
