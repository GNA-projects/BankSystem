import React, { useState, useEffect } from "react";
import VitoshaForm from "../../../components/VitoshaForm";
import { depositInCredit } from "../../../Api/User/depositInAccount";
import Credit from "../../../components/Ebanking/Account/Credit";

export default function CreditPage() {
	const [depositAmount, setDepositAmount] = useState(0);
	return (
		<React.Fragment>
			<Credit />
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					heading="Amount"
					value={depositAmount}
					onChange={(e) => setDepositAmount(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => depositInCredit(depositAmount)}
					text="Deposit From Charge"
				/>
			</VitoshaForm>
		</React.Fragment>
	);
}
