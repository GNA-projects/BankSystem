import React, { useState, useContext } from "react";
import { useHistory } from "react-router-dom";
import { Navigation, Dropdown } from "./style";

import { AuthContext } from "../../Auth/";

export default function TopMenu() {
	const { logged } = useContext(AuthContext);

	const [dropDown, setDropdown] = useState(false);
	const history = useHistory();

	const toPath = (path) => {
		history.push(`/${path}`);
		setDropdown(false);
	};

	return (
		<React.Fragment>
			<Navigation>
				<Navigation.Burger onClick={() => setDropdown(!dropDown)} />
				<Navigation.Logged>{logged ? "V" : "X"}</Navigation.Logged>
			</Navigation>

			<Dropdown active={dropDown}>
				<Dropdown.Item onClick={() => toPath("account")} about heading="My Account" />
				<Dropdown.Item
					onClick={() => toPath("ebanking")}
					bank
					heading="Ebanking"
				/>
				<Dropdown.Item onClick={() => toPath("")} home heading="Home" />
				<Dropdown.Item
					onClick={() => toPath("logout")}
					logout
					heading="Logout"
				/>
			</Dropdown>
		</React.Fragment>
	);
}
