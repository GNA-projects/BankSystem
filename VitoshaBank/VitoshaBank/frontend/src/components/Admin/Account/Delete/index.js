import React from "react";
import { useState } from "react/cjs/react.development";
import { AdminForm } from "../../style";
import { deleteAccount } from "../../../../Api/admin";

export default function DeleteAccount() {
	const [uname, setUname] = useState();
	const [accountType, setAccountType] = useState();

	const handleDelete = () => {
		deleteAccount(uname, accountType);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="Username"
				values={{ value: uname, setValue: setUname }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Account Type 'deposit', 'credit', 'bankaccount', 'wallet'"
				values={{ value: accountType, setValue: setAccountType }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleDelete}>Delete Bank Account</AdminForm.Button>
		</AdminForm>
	);
}
