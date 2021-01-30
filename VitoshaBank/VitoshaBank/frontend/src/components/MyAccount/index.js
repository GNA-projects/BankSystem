import React from "react";
import { useState } from "react/cjs/react.development";
import { changePass } from "../../Api/user";
import Input from "./Input";
import Button from "./Button";
import styled from "styled-components";

const Container = styled.div`
	display: flex;
	flex-direction: column;
	max-width: 500px;
	margin: auto;
`;

export default function MyAccount() {
	const [pass, setPass] = useState();
	const [confirm, setConfirm] = useState();
	const handleSubmit = () => {
		if (pass === confirm) changePass(pass);
		else alert("passwords do not mach");
	};
	return (
		<Container>
			<h1>change pass</h1>
			<p>new password</p>
			<Input
				type="password"
				color="teal"
				onChange={(e) => setPass(e.target.value)}
				value={pass}
			></Input>
			<p>confirm</p>
			<Input
				type="password"
				color={pass === confirm ? "teal" : "red"}
				onChange={(e) => setConfirm(e.target.value)}
				value={confirm}
			></Input>
			<Button onClick={() => handleSubmit()}>change</Button>
		</Container>
	);
}
