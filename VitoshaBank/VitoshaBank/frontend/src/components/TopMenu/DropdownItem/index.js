import React, { useEffect, useState } from "react";
import { Item } from "./style";

import { faPiggyBank } from "@fortawesome/free-solid-svg-icons";
import { faUserNinja } from "@fortawesome/free-solid-svg-icons";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

export default function DropdownItem(props) {
	const [icon, setIcon] = useState();
	useEffect(() => {
		evaluateIcon();
	});
	const evaluateIcon = () => {
		switch (true) {
			case props.home:
				setIcon(faHome);
				break;
			case props.about:
				setIcon(faUserNinja);
				break;
			case props.bank:
				setIcon(faPiggyBank);
				break;
			case props.logout:
				setIcon(faSignOutAlt);
				break;
		}
	};

	return (
		<Item onClick={props.onClick}>
			<Item.Icon icon={icon} />
			<Item.Heading heading={props.heading} />
		</Item>
	);
}
