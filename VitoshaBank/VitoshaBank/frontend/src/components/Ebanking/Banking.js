import React from "react";
import Charge from './Account/Charge'
import Deposit from './Account/Deposit'
import Credit from './Account/Credit'
import Wallet from './Account/Wallet'

export default function Banking(props) {
	return <React.Fragment>{props.children}</React.Fragment>;
}
Banking.Charge = Charge;
Banking.Deposit = Deposit;
Banking.Wallet = Wallet;
Banking.Credit = Credit;
