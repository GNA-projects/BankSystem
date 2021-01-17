import React, { useState } from "react";
import { checkIfLogged } from "../../Api/user";
import { useHistory } from "react-router-dom";
import { Navigation, Dropdown } from "./style";

import logo from "../../Images/logo.png";
import { faPiggyBank } from "@fortawesome/free-solid-svg-icons";
import { faUserNinja } from "@fortawesome/free-solid-svg-icons";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

export default function TopMenu() {
  const [dropDown, setDropdown] = useState(false);
  const history = useHistory();

  const toPath = (path) => {
    history.push(`/${path}`);
    setDropdown(false);
  };

  return (
    <React.Fragment>
      <Navigation>
        <Navigation.Burger src={logo} onClick={() => setDropdown(!dropDown)} />
        <Navigation.Logged>{checkIfLogged()}</Navigation.Logged>
      </Navigation>

      <Dropdown active={dropDown}>
        <Dropdown.Item
          onClick={() => toPath("")}
          icon={faHome}
          heading="Home"
        />
        <Dropdown.Item
          onClick={() => toPath("ebanking")}
          icon={faPiggyBank}
          heading="Ebanking"
        />
        <Dropdown.Item
          onClick={() => toPath("about")}
          icon={faUserNinja}
          heading="About"
        />
        <Dropdown.Item
          onClick={() => toPath("login")}
          icon={faSignOutAlt}
          heading="Logout"
        />
      </Dropdown>
    </React.Fragment>
  );
}
