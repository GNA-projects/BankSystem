import React from "react";
import { Banking } from "./style";


export default function Ebanking() {
	return (
		<Banking>
			<Banking.Debit />
			<Banking.Deposit />
			<Banking.Wallet />
			<Banking.Credit />
		</Banking>
	);
}
