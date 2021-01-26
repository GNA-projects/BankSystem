import React from "react";
import { Credit, BAccount, Deposit, Wallet } from "./Account";

export const Banking = (props) => (
	<React.Fragment>{props.children}</React.Fragment>
);
Banking.BAccount = BAccount;
Banking.Deposit = Deposit;
Banking.Wallet = Wallet;
Banking.Credit = Credit;
