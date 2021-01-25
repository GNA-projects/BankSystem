import React from "react";
import { Credit, Debit, Deposit, Wallet } from "./Account";

export const Banking = (props) => (
	<React.Fragment>{props.children}</React.Fragment>
);
Banking.Debit = Debit;
Banking.Deposit = Deposit;
Banking.Wallet = Wallet;
Banking.Credit = Credit;
