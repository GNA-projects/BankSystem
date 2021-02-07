import React from "react";
import { useState } from "react/cjs/react.development";
import AdminForm from "../../../../../components/Admin/AdminForm";

import { createDebitCard } from "../../../../../Api/Admin/create";

export default function CreateDebitCard() {
	const [iban, setIban] = useState();

	const handleCreate = () => {
		createDebitCard(iban);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="Iban"
				values={{ value: iban, setValue: setIban }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleCreate}>Create Debit Card</AdminForm.Button>
		</AdminForm>
	);
}
