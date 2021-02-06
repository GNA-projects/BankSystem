import React from "react";
import Banking from "../../components/Ebanking/Banking";


export default function Ebanking() {
	return (
		<Banking>
			<Banking.Charge />
			<Banking.Deposit />
			<Banking.Wallet />
			<Banking.Credit />
		</Banking>
	);
}
