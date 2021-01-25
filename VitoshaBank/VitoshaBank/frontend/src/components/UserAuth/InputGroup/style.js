import styled from "styled-components";

const GROUP = styled.div`
	display: flex;
	flex-direction: column;
`;
const HEADING = styled.p`
	text-align: center;
	color: grey;
	font-size: 14px;
	border: 1px;
	margin: 10px 0;
`;
const INPUT = styled.input`
	font-size: 16px;
	background-color: rgb(255, 255, 255, 0.1);
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
`;

export const Group = (props) => <GROUP>{props.children}</GROUP>
Group.Heading = HEADING
Group.Input = INPUT
