import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMountain } from "@fortawesome/free-solid-svg-icons";
import { InputGroup, TextGroup } from "./InputGroup";

const FORM = styled.div`
	display: flex;
	height: 80vh;
`;
const OUTER = styled.div`
	display: flex;
	background-color: rgb(40, 57, 101, 0.9);
	height: 90%;
	width: 100vw;
	max-width: 600px;
	margin: auto;
	box-shadow: 1px 0px 30px;
`;
const INNER = styled.div`
	display: flex;
	flex-direction: column;
	margin: auto;
`;
const SUBMIT = styled.button`
	font-size: 16px;
	background-color: rgb(255, 255, 255, 0.5);
	padding: 15px 20px;
	color: white;
	outline: none;
	border: 1px;
	border-radius: 20px;
	margin-top: 20px;
	&:hover {
		background-color: rgb(255, 255, 255, 0.2);
	}
`;
const ICON = styled(FontAwesomeIcon)`
	font-size: 4rem;
	margin: auto;
	margin-bottom: 20px;
`;

export default function VitoshaForm(props) {
	return (
		<FORM>
			<OUTER>
				<INNER>{props.children}</INNER>
			</OUTER>
		</FORM>
	);
}
VitoshaForm.Icon = () => <ICON icon={faMountain} />;
VitoshaForm.Input = InputGroup;
VitoshaForm.TextArea = TextGroup;
VitoshaForm.Submit = (props) => <SUBMIT {...props}>{props.text}</SUBMIT>;
