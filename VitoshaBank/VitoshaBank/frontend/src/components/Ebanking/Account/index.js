import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { BankAccount } from "./style";
import {
	getBankAccount,
	getCredit,
	getDeposit,
	getWallet,
} from "../../../Api/user";

export function BAccount() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		getBankAccount(setAmount, setIban);
	}, []);
	return (
		<BankAccount onClick={() => history.push("/baccount")}>
			<BankAccount.Heading>Bank Account</BankAccount.Heading>
			<BankAccount.Heading>{iban}</BankAccount.Heading>
			<BankAccount.Balance>{amount}</BankAccount.Balance>
		</BankAccount>
	);
}
export function Deposit() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		getDeposit(setAmount, setIban);
	}, []);
	return (
		<BankAccount onClick={() => history.push("/deposit")}>
			<BankAccount.Heading>Deposit Account</BankAccount.Heading>
			<BankAccount.Heading>{iban}</BankAccount.Heading>
			<BankAccount.Balance>{amount}</BankAccount.Balance>
		</BankAccount>
	);
}
export function Wallet() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		getWallet(setAmount, setIban);
	}, []);
	return (
		<BankAccount onClick={() => history.push("/wallet")}>
			<BankAccount.Heading>Wallet Account</BankAccount.Heading>
			<BankAccount.Heading>{iban}</BankAccount.Heading>
			<BankAccount.Balance>{amount}</BankAccount.Balance>
		</BankAccount>
	);
}
export function Credit() {
	const history = useHistory();
	const [amount, setAmount] = useState(0);
	const [iban, setIban] = useState(0);
	useEffect(() => {
		getCredit(setAmount, setIban);
	}, []);
	return (
		<BankAccount onClick={() => history.push("/credit")}>
			<BankAccount.Heading>Credit Account</BankAccount.Heading>
			<BankAccount.Heading>{iban}</BankAccount.Heading>
			<BankAccount.Balance>{amount}</BankAccount.Balance>
		</BankAccount>
	);
}
