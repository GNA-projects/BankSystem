import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../../Account/AccountForm";
import { getCharge, purchaseAmazon } from "../../../../Api/user";

export default function ChargePage() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		const interval = setInterval(() => {
			getCharge(setAmount, setIban);
		}, 1000);
		return () => clearInterval(interval);
	}, []);
	return (
		<AccountForm onClick={() => history.push("/charge")}>
			<AccountForm.Heading>Charge Account</AccountForm.Heading>
			<AccountForm.Heading>{iban}</AccountForm.Heading>
			<AccountForm.Balance>{amount}</AccountForm.Balance>
		</AccountForm>
	);
}
