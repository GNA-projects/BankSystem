import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { BankAccount } from "./style";

export function Debit() {
	const history = useHistory();

	return (
		<BankAccount onClick={() => history.push("/debit")}>
			<BankAccount.Heading>Debit Account</BankAccount.Heading>
			<BankAccount.Balance>22.55</BankAccount.Balance>
		</BankAccount>
	);
}
export function Deposit() {
	const history = useHistory();
	return (
		<BankAccount onClick={() => history.push("/deposit")}>
			<BankAccount.Heading>Deposit Account</BankAccount.Heading>
			<BankAccount.Balance>22.55</BankAccount.Balance>
		</BankAccount>
	);
}
export function Wallet() {
	const history = useHistory();
	return (
		<BankAccount onClick={() => history.push("/wallet")}>
			<BankAccount.Heading>Wallet Account</BankAccount.Heading>
			<BankAccount.Balance>22.55</BankAccount.Balance>
		</BankAccount>
	);
}
export function Credit() {
	const history = useHistory();
	return (
		<BankAccount onClick={() => history.push("/credit")}>
			<BankAccount.Heading>Credit Account</BankAccount.Heading>
			<BankAccount.Balance>22.55</BankAccount.Balance>
		</BankAccount>
	);
}
