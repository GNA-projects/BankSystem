import React from "react";
import { useState } from "react/cjs/react.development";
import { changePassword } from "../../Api/User/changePass";
import { sendTicket } from "../../Api/User/support";

import VitoshaForm from "../../components/VitoshaForm";

export default function MyAccount() {
	const [pass, setPass] = useState();
	const [confirm, setConfirm] = useState();
	const [ticketTitle, setTicketTitle] = useState();
	const [ticketMessage, setTicketMessage] = useState();
	const handlePassSubmit = () => {
		if (pass === confirm) changePassword(pass);
		else alert("passwords do not mach");
	};
	const handleTicketSubmit = () => {
		sendTicket(ticketTitle, ticketMessage);
	};
	return (
		<React.Fragment>
			<h1>Welcome to my Account</h1>
			<br></br>
			<br></br>
			<p>here you can change your account data and contact support</p>
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					type="password"
					heading="Password"
					value={pass}
					onChange={(e) => setPass(e.target.value)}
				/>
				<VitoshaForm.Input
					type="password"
					heading="Confirm"
					value={confirm}
					onChange={(e) => setConfirm(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => handlePassSubmit()}
					text="Change Password"
				/>
			</VitoshaForm>
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					heading="Title"
					value={ticketTitle}
					onChange={(e) => setTicketTitle(e.target.value)}
				/>
				<VitoshaForm.TextArea
					heading="Write a support ticket"
					value={ticketMessage}
					onChange={(e) => setTicketMessage(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => handleTicketSubmit()}
					text="Send Ticket"
				/>
			</VitoshaForm>
		</React.Fragment>
	);
}
