import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../AccountForm";
import { getBankAccount } from "../../../../Api/user";

export default function BankAccount() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		const interval = setInterval(() => {
			getBankAccount(setAmount, setIban);
		}, 1000);
		return () => clearInterval(interval);
	}, []);
	return (
		<AccountForm onClick={() => history.push("/baccount")}>
			<AccountForm.Heading>Bank Account</AccountForm.Heading>
			<AccountForm.Heading>{iban}</AccountForm.Heading>
			<AccountForm.Balance>{amount}</AccountForm.Balance>
		</AccountForm>
	);
}
