import React from "react";
import { useState } from "react/cjs/react.development";
import { changePass } from "../../Api/user";

export default function MyAccount() {
	const [pass, setPass] = useState();
	return (
		<div>
			<h1>change pass</h1>
			<input onChange={(e) => setPass(e.target.value)} value={pass}></input>
			<button onClick={() => changePass(pass)}>cahnge</button>
		</div>
	);
}
