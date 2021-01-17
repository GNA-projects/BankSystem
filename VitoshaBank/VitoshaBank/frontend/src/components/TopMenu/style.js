import styled from "styled-components";
import image from "./Images/mountain.jpg";

import DropdownItem from "./DropdownItem";


export const StyledNavigation = styled.div`
  position: relative;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  box-shadow: 1px 1px 20px;
  background-image: url(${image});
  height: 80px;
`;

export const StyledDropdown = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  position: fixed;
  z-index: 1;
  overflow: hidden;
  background-image: url(${image});
  top: 0;
  height: ${(props) => (props.active ? "100vh" : "0vh")};
  transition: height 1s ease;
  width: 100vw;
`;

export const BurgerButton = styled.img`
  position: absolute;
  z-index: 2;
  height: 80px;
`;

export const LoggedInHeading = styled.h1`
  position: absolute;
  z-index: 2;
  right: 10px;
  top: 50%;
  transform: translate(0, -50%);
  color: white;
`;

export const Navigation = (props) => {return <StyledNavigation>{props.children}</StyledNavigation>}
Navigation.Logged = (props) => (<LoggedInHeading>{props.children}</LoggedInHeading>)
Navigation.Burger = (props) => (<BurgerButton src={props.src} onClick={props.onClick}></BurgerButton>)

export const Dropdown = (props) => {return <StyledDropdown active={props.active}>{props.children}</StyledDropdown>}
Dropdown.Item = (props) => (<DropdownItem onClick={props.onClick} icon={props.icon} heading={props.heading}>{props.children}</DropdownItem>)