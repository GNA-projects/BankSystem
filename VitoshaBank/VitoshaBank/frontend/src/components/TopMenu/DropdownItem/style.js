import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const ICON = styled(FontAwesomeIcon)`
	font-size: 3rem;
	margin: 10px;
	color: white;
	cursor: pointer;
`;
const HEADING = styled.p`
	font-size: 3rem;
	margin: 10px;
	color: white;
	cursor: pointer;
`;
const ITEM = styled.div`
	z-index: 0;
	margin: auto;
	display: flex;
	flex-direction: row;
	background-color: rgb(100, 100, 100, 0.8);
	&:hover {
		box-shadow: 1px 1px 10px white;
	}
	cursor: pointer;
`;

export const Item = (props) => <ITEM {...props}>{props.children}</ITEM>;
Item.Icon = ICON;
Item.Heading = (props) => <HEADING>{props.heading}</HEADING>;
