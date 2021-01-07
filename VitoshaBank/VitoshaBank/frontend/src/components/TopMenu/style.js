import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import image from './mountain.jpg'

export const NavContainer = styled.div`
  position: relative;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  box-shadow: 1px 1px 20px;
  background-image: url(${image});
  height: 80px;
`;

export const DropdownContainer = styled.div`
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

export const DropdownItemIcon = styled(FontAwesomeIcon)`
  font-size: 3rem;
  margin: 10px;
  color: white;
`;
export const DropdownItemHeading = styled.p`
  font-size: 3rem;
  margin: 10px;
  color: white;
`;

export const DropdownItem = styled.div`
  z-index: 0;
  margin: auto;
  display: flex;
  flex-direction: row;
  &:hover{
    box-shadow: 1px 1px 10px white;
  }
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
  transform: translate(0,-50%);
  color: white;
`;
