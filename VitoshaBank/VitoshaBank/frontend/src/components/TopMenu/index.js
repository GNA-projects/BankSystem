import React, { useContext, useState } from "react";
import { LoggedInContext } from "../../context/context";
import { useHistory } from "react-router-dom";
import {
  NavContainer,
  DropdownContainer,
  DropdownItem,
  BurgerButton,
  DropdownItemHeading,
  DropdownItemIcon,
  LoggedInHeading,
} from "./style";

import logo from "../../Images/logo.png";
import { faPiggyBank } from "@fortawesome/free-solid-svg-icons";
import { faUserNinja } from "@fortawesome/free-solid-svg-icons";
import { faHome } from "@fortawesome/free-solid-svg-icons";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

export default function TopMenu() {
  const [dropDown, setDropdown] = useState(false);
  const history = useHistory();

  const { loggedIn, setLoggedIn } = useContext(LoggedInContext);

  const goHome = () => {
    history.push("/");
    setDropdown(false);
  };
  const goLogin = () => {
    history.push("/login");
    setDropdown(false);
  };
  const goAbout = () => {
    history.push("/about");
    setDropdown(false);
  };
  const goEbanking = () => {
    history.push("/ebanking");
    setDropdown(false);
  };
  return (
    <div>
      <NavContainer>
        <BurgerButton
          src={logo}
          onClick={() => setDropdown(!dropDown)}
        ></BurgerButton>
        <LoggedInHeading>{sessionStorage['jwt'] ? "logged" : "not logged"}</LoggedInHeading>
      </NavContainer>
      <DropdownContainer active={dropDown}>
        <DropdownItem onClick={goHome}>
          <DropdownItemIcon icon={faHome}></DropdownItemIcon>
          <DropdownItemHeading>Home</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem onClick={goEbanking}>
          <DropdownItemIcon icon={faPiggyBank}></DropdownItemIcon>
          <DropdownItemHeading>Ebanking</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem onClick={goAbout}>
          <DropdownItemIcon icon={faUserNinja}></DropdownItemIcon>
          <DropdownItemHeading>About</DropdownItemHeading>
        </DropdownItem>
        <DropdownItem onClick={goLogin}>
          <DropdownItemIcon icon={faSignOutAlt}></DropdownItemIcon>
          <DropdownItemHeading>Logout</DropdownItemHeading>
        </DropdownItem>
      </DropdownContainer>
    </div>
  );
}
