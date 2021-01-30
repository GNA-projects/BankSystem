import { useHistory } from "react-router-dom";
import styled from "styled-components";
import { InputGroup, BUTTON } from "./InputGroup";

const CONTAINER = styled.div``;

const LINK = styled.button`
	display: block;
	margin: auto;
	font-size: 16px;
	background-color: ${props => props.red ? "red" : "teal"};
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
	margin-top: 20px;
	&:hover {
		background-color: ${props => props.red ? "rebeccapurple" : "darkcyan"};
	}
`;

export const AdminPanel = (props) => (
	<CONTAINER {...props}>{props.children}</CONTAINER>
);
const Link = (props) => {
	const history = useHistory();
	return (
		<LINK {...props} onClick={() => history.push(props.to)}>
			{props.heading}
		</LINK>
	);
};
AdminPanel.Link = Link;

export const AdminForm = (props) => (
	<CONTAINER {...props}>{props.children}</CONTAINER>
);
AdminForm.Input = InputGroup;
AdminForm.Button = BUTTON;
