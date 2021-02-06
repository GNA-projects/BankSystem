import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../../../../components/Admin/AdminForm";
import { createCharge } from "../../../../../Api/Admin/create";

export default function CreateCharge() {
	const [uname, setUname] = useState();
	const [amount, setAmount] = useState();

	const handleCreate = () => {
		createCharge(uname, amount);
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
			<AdminForm.Button onClick={handleCreate}>Create Charge Account</AdminForm.Button>
		</AdminForm>
	);
}
