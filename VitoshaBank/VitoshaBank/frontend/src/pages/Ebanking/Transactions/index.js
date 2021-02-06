import React from "react";
import { useEffect, useState } from "react/cjs/react.development";
import Transaction from "../../../components/Ebanking/Transaction";
import { getAllTransactions } from "../../../Api/User/transactions";
export default function Transactions() {
	const [transactions, setTransactions] = useState([]);
	const getTransactions = async () => {
		let myTransactions = await getAllTransactions();
		console.log(myTransactions);
		setTransactions(myTransactions);
	};
	useEffect(() => {
		getTransactions();
	},[]);
	return (
		<div>
			{transactions.map((item, i) => {
				return <Transaction key={i} sender={item.senderInfo} receiver={item.reciverInfo} date={item.date} amount={item.amount} />;
			})}
		</div>
	);
}
