import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../AccountForm";
import { getWallet } from "../../../../Api/user";

export default function Wallet() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		getWallet(setAmount, setIban);
	}, []);
	return (
		<AccountForm onClick={() => history.push("/wallet")}>
			<AccountForm.Heading>Wallet Account</AccountForm.Heading>
			<AccountForm.Heading>{iban}</AccountForm.Heading>
			<AccountForm.Balance>{amount}</AccountForm.Balance>
		</AccountForm>
	);
}
