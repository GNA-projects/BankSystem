import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import AccountForm from "../../Account/AccountForm";
import VitoshaForm from "../VitoshaForm";
import { getCredit, depositInCredit } from "../../../../Api/user";

export default function CreditPage() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	const [depositAmount, setDepositAmount] = useState(0);
	useEffect(() => {
		const interval = setInterval(() => {
			getCredit(setAmount, setIban);
		}, 1000);
		return () => clearInterval(interval);
	}, []);
	return (
		<React.Fragment>
			<AccountForm onClick={() => history.push("/credit")}>
				<AccountForm.Heading>Credit Account</AccountForm.Heading>
				<AccountForm.Heading>{iban}</AccountForm.Heading>
				<AccountForm.Balance>{amount}</AccountForm.Balance>
			</AccountForm>
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					heading="Amount"
					value={depositAmount}
					onChange={(e) => setDepositAmount(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => depositInCredit(iban, depositAmount)}
					text="Deposit From Charge"
				/>
			</VitoshaForm>
		</React.Fragment>
	);
}
