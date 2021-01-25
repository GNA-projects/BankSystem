import { Group } from "./style";

export default function InputGroup(props) {
	return (
		<Group>
			<Group.Heading>{props.heading}</Group.Heading>
			<Group.Input
				type={props.type}
				onClick={props.onClick}
				value={props.value}
			></Group.Input>
		</Group>
	);
}
