import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../../AdminForm";

import { createDeposit } from "../../../../../Api/admin";

export default function CreateDeposit() {
	const [uname, setUname] = useState();
	const [amount, setAmount] = useState();
	const [period, setPeriod] = useState();

	const handleCreate = () => {
		createDeposit(uname, amount, period);
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
			<AdminForm.Input
				heading="Term of Payment"
				values={{ value: period, setValue: setPeriod }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>
				Create Deposit Account
			</AdminForm.Button>
		</AdminForm>
	);
}
