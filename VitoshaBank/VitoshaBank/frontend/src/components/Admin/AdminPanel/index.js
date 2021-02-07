import React from 'react'
import { useHistory } from "react-router-dom";
import styled from "styled-components";

const LINK = styled.button`
	display: block;
	margin: auto;
	font-size: 16px;
	background-color: ${(props) => (props.red ? "red" : "teal")};
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
	margin-top: 20px;
	&:hover {
		background-color: ${(props) => (props.red ? "rebeccapurple" : "darkcyan")};
	}
`;
const Link = (props) => {
	const history = useHistory();
	return (
		<LINK {...props} onClick={() => history.push(props.to)}>
			{props.heading}
		</LINK>
	);
};

export default function AdminPanel(props) {
	return <React.Fragment {...props}>{props.children}</React.Fragment>;
}
AdminPanel.Link = Link;
