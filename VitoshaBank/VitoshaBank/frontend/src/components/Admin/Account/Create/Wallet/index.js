import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../../AdminForm";

import { createWallet } from "../../../../../Api/admin";

export default function CreateDeposit() {
	const [uname, setUname] = useState();
	const [amount, setAmount] = useState();
	const handleCreate = () => {
		createWallet(uname, amount);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="Username"
				values={{ value: uname, setValue: setUname }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Amount"
				values={{ value: amount, setValue: setAmount }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>
				Create Wallet Account
			</AdminForm.Button>
		</AdminForm>
	);
}
