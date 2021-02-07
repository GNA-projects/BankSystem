import styled from "styled-components";

const BANK_ACCOUNT = styled.div`
	background-color: whitesmoke;
	box-shadow: 1px 2px 10px;
	width: 80vw;
	max-width: 500px;
	border: 1px;
	border-radius: 10px;
	margin: 40px auto;
	padding: 20px;
	&:hover {
		background-color: rgb(20, 20, 200, 0.6);
	}
`;
const HEADING = styled.div`
	font-size: 18px;
`;
const BALANCE = styled.div`
	font-size: 30px;
`;

export default function AccountForm(props) {
	return <BANK_ACCOUNT {...props}>{props.children}</BANK_ACCOUNT>;
}
AccountForm.Heading = HEADING;
AccountForm.Balance = (props) => <BALANCE>{props.children} BGN</BALANCE>;
