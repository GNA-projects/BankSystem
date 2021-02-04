import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../../Account/AccountForm";
import { getWallet } from "../../../../Api/user";

export default function WalletPage() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		const interval = setInterval(() => {
			getWallet(setAmount, setIban);
		}, 1000);
		return () => clearInterval(interval);
	}, []);
	return (
		<AccountForm onClick={() => history.push("/wallet")}>
			<AccountForm.Heading>Wallet Account</AccountForm.Heading>
			<AccountForm.Heading>{iban}</AccountForm.Heading>
			<AccountForm.Balance>{amount}</AccountForm.Balance>
		</AccountForm>
	);
}
