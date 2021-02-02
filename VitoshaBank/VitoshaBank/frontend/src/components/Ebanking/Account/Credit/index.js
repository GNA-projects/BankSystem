import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../AccountForm";
import { getCredit } from "../../../../Api/user";

export default function Credit() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		const interval = setInterval(() => {
			getCredit(setAmount, setIban);
		}, 1000);
		return () => clearInterval(interval);
	}, []);
	return (
		<AccountForm onClick={() => history.push("/credit")}>
			<AccountForm.Heading>Credit Account</AccountForm.Heading>
			<AccountForm.Heading>{iban}</AccountForm.Heading>
			<AccountForm.Balance>{amount}</AccountForm.Balance>
		</AccountForm>
	);
}
