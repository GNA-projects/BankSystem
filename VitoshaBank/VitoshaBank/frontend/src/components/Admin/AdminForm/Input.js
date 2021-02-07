import styled from "styled-components";

const GROUP = styled.div`
	display: flex;
	flex-direction: column;
	max-width: 400px;
	margin: auto;
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
	background-color: teal;
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
`;

export default function Input(props) {
	const { value, setValue } = props.values;
	return (
		<GROUP>
			<HEADING>{props.heading}</HEADING>
			<INPUT
				type={props.type}
				onChange={(e) => setValue(e.target.value)}
				value={value}
			></INPUT>
		</GROUP>
	);
}
