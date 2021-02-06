import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faStore } from "@fortawesome/free-solid-svg-icons";

export const BLOCK = styled.p`
	display: flex;
	justify-content: space-between;
	padding: 20px;
	border-style: none none dotted none;
	border-width: 1px;
	margin: 20px;
`;
export const ICON_BG = styled.div`
	flex: 0.2fr;
	background-color: grey;
	display: flex;
	justify-content: center;
	text-align: center;
	width: 30px;
	height: 30px;
	border: 1px;
	border-radius: 50%;
	padding: 10px;
`;
export const ICON = styled(FontAwesomeIcon)`
	font-size: 30px;
`;
export const INFO = styled.div`
	flex: 0.5fr;
`;

export const SENDER = styled.p`
	margin-bottom: 10px;
`;
export const DATE = styled.p``;
export const AMOUNT = styled.p`
	flex: 0.3fr;
`;

export default function Transaction(props) {
	return (
		<BLOCK>
			<ICON_BG>
				<ICON icon={faStore}></ICON>
			</ICON_BG>
			<INFO>
				<SENDER>Sender: {props.sender}</SENDER>
				<SENDER>Receiver: {props.receiver}</SENDER>
				<DATE>{props.date}</DATE>
			</INFO>
			<AMOUNT>{props.amount}</AMOUNT>
		</BLOCK>
	);
}
