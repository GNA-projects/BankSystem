import styled, { ThemeProvider } from "styled-components";
import { AuthProvider } from "../../Auth/";
import TopMenu from "../TopMenu";
import { BrowserRouter as Router } from "react-router-dom";

const Body = styled.div`
	max-width: 1200px;
	margin: auto;
`;

const theme = {
	primary: "darkcyan",
	secondary: "white",
	text: "#384062",
};

export default function Layout(props) {
	return (
		<AuthProvider>
			<ThemeProvider theme={theme}>
				<Router>
					<TopMenu></TopMenu>
					<Body>{props.children}</Body>
				</Router>
			</ThemeProvider>
		</AuthProvider>
	);
}
