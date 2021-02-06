import React from "react";
import { useState } from "react/cjs/react.development";
import { changePassword } from "../../Api/User/changePass";
import VitoshaForm from "../../components/VitoshaForm";

export default function MyAccount() {
	const [pass, setPass] = useState();
	const [confirm, setConfirm] = useState();
	const handleSubmit = () => {
		if (pass === confirm) changePassword(pass);
		else alert("passwords do not mach");
	};
	return (
		<React.Fragment>
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					type="password"
					heading="password"
					value={pass}
					onChange={(e) => setPass(e.target.value)}
				/>
				<VitoshaForm.Input
					type="password"
					heading="confirm"
					value={confirm}
					onChange={(e) => setConfirm(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => handleSubmit()}
					text="Change Password"
				/>
			</VitoshaForm>
			<h1>Support Ticket</h1>
			<textarea  type="text"></textarea >
		</React.Fragment>
	);
}
