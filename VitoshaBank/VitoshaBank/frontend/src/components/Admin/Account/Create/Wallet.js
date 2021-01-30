import React from "react";
import { useState } from "react/cjs/react.development";
import { AdminForm } from "../../style";
import { createBankAccount } from "../../../../Api/admin";

export default function CreateWallet() {
	const [uname, setUname] = useState();
	const [amount, setAmount] = useState();

	const handleCreate = () => {
		createBankAccount(uname, amount);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="Userna!!!!!!!!!!!!!!!!NOT DONEme"
				values={{ value: uname, setValue: setUname }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Amount"
				values={{ value: amount, setValue: setAmount }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>Create Wallet Account</AdminForm.Button>
		</AdminForm>
	);
}
