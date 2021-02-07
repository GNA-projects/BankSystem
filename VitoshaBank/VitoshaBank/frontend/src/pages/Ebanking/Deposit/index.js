import React, { useState } from "react";
import { depositInDeposit } from "../../../Api/User/depositInAccount";
import VitoshaForm from "../../../components/VitoshaForm";
import Deposit from "../../../components/Ebanking/Account/Deposit";

export default function DepositPage() {
	const [depositAmount, setDepositAmount] = useState(0);

	return (
		<React.Fragment>
			<Deposit />
			<VitoshaForm>
				<VitoshaForm.Icon />
				<VitoshaForm.Input
					heading="Amount"
					value={depositAmount}
					onChange={(e) => setDepositAmount(e.target.value)}
				/>
				<VitoshaForm.Submit
					onClick={() => depositInDeposit(depositAmount)}
					text="Deposit From Charge"
				/>
			</VitoshaForm>
		</React.Fragment>
	);
}
