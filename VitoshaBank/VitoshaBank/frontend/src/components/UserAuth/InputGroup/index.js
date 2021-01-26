import { Group } from "./style";

export default function InputGroup(props) {
	return (
		<Group>
			<Group.Heading>{props.heading}</Group.Heading>
			<Group.Input
				type={props.type}
				onChange={props.onChange}
				value={props.value}
			></Group.Input>
		</Group>
	);
}
