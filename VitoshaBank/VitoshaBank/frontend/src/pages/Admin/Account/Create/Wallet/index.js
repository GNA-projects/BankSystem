import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../../../../components/Admin/AdminForm";

import { createWallet } from "../../../../../Api/Admin/create";

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
				type="number"
				heading="Amount"
				values={{ value: amount, setValue: setAmount }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>
				Create Wallet Account
			</AdminForm.Button>
		</AdminForm>
	);
}
