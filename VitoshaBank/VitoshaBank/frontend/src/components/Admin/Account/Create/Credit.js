import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../AdminForm";

import { createCredit } from "../../../../Api/admin";

export default function CreateCredit() {
	const [uname, setUname] = useState();
	const [amount, setAmount] = useState();
	const [period, setPeriod] = useState();

	const handleCreate = () => {
		createCredit(uname, amount, period);
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
				heading="Period"
				values={{ value: period, setValue: setPeriod }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>Create Credit</AdminForm.Button>
		</AdminForm>
	);
}
