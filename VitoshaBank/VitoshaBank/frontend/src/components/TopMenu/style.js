import styled from "styled-components";
import image from "./Images/mountain.jpg";
import logo from "../../Images/logo.png";

import DropdownItem from "./DropdownItem";

const NAVIGATION = styled.div`
	position: relative;
	display: flex;
	flex-direction: row;
	justify-content: space-between;
	box-shadow: 1px 1px 20px;
	background-image: url(${image});
	height: 80px;
`;

const DROPDOWN = styled.div`
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

const BURGER = styled.img`
	position: absolute;
	z-index: 2;
	height: 80px;
`;

const LOGGED = styled.h1`
	position: absolute;
	z-index: 2;
	right: 10px;
	top: 50%;
	transform: translate(0, -50%);
	color: ${(props) => props.theme.secondary};
`;

export const Navigation = (props) => <NAVIGATION>{props.children}</NAVIGATION>;
Navigation.Logged = LOGGED;
Navigation.Burger = (props) => <BURGER src={logo} {...props} />;

export const Dropdown = (props) => <DROPDOWN {...props}>{props.children}</DROPDOWN>;
Dropdown.Item = DropdownItem;
