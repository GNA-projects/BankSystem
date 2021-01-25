import { useHistory } from "react-router-dom";
import styled from "styled-components";

const CONTAINER = styled.div``;
const HEADING = styled.p``;
const INPUT = styled.input``;
const LINK = styled.button`
	display: block;
	margin: auto;
    font-size: 16px;
	background-color: teal;
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
	margin-top: 20px;
	&:hover {
		background-color: darkcyan;
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
