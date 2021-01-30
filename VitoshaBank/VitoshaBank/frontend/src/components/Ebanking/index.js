import React from "react";
import Banking from "./Banking";


export default function Ebanking() {
	return (
		<Banking>
			<Banking.BankAccount />
			<Banking.Deposit />
			<Banking.Wallet />
			<Banking.Credit />
		</Banking>
	);
}
