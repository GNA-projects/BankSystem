import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import {
  NavContainer,
  DropdownContainer,
  DropdownItem,
  BurgerButton,
  DropdownItemHeading,
  DropdownItemIcon,
} from "./style";

import logo from "../../Images/logo.png";
import { faPiggyBank } from "@fortawesome/free-solid-svg-icons";
import { faUserNinja } from "@fortawesome/free-solid-svg-icons";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

export default function TopMenu() {
  const [dropDown, setDropdown] = useState(false);
  const history = useHistory();

  const goHome = () => {
      history.push('/home')
  }
  return (
    <div>
      <NavContainer>
        <BurgerButton
          src={logo}
          onClick={() => setDropdown(!dropDown)}
        ></BurgerButton>
      </NavContainer>
      <DropdownContainer active={dropDown}>
        <DropdownItem onClick={goHome}>
          <DropdownItemIcon icon={faHome}></DropdownItemIcon>
          <DropdownItemHeading>Home</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem>
          <DropdownItemIcon icon={faPiggyBank}></DropdownItemIcon>
          <DropdownItemHeading>Ebanking</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem>
          <DropdownItemIcon icon={faUserNinja}></DropdownItemIcon>
          <DropdownItemHeading>About</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem>
          <DropdownItemIcon icon={faSignOutAlt}></DropdownItemIcon>
          <DropdownItemHeading>Logout</DropdownItemHeading>
        </DropdownItem>
      </DropdownContainer>
    </div>
  );
}
