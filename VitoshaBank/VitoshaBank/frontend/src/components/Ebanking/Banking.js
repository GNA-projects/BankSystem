import React from "react";
import styled from "styled-components";
import Charge from "./Account/Charge";
import Deposit from "./Account/Deposit";
import Credit from "./Account/Credit";
import Wallet from "./Account/Wallet";
import { Link } from "react-router-dom";

const TransactionsButton = styled(Link)`
	display: block;
	margin: auto;
	text-align: center;
`;
export default function Banking(props) {
	return <React.Fragment>{props.children}</React.Fragment>;
}
Banking.Charge = Charge;
Banking.Deposit = Deposit;
Banking.Wallet = Wallet;
Banking.Credit = Credit;
Banking.Transactions = TransactionsButton;
