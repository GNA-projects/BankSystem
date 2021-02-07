import styled from "styled-components";
import home from "../../Images/home.jpg";
const Container = styled.div`
	position: relative;
`;
const WelcomeHeading = styled.h1`
	position: absolute;
	top: 30%;
	left: 50%;
	transform: translate(-50%, -50%);
	background-color: rgb(0, 0, 0, 0.5);
	color: white;
	padding: 15px 17px;
	width: 80vw;
`;
const WelcomeText = styled.h3`
	position: absolute;
	top: 75%;
	left: 50%;
	transform: translate(-50%, -50%);
	background-color: rgb(0, 0, 0, 0.5);
	color: white;
	padding: 15px 17px;
	width: 300px;
`;
const Image = styled.img`
	width: 100%;
	overflow: none;
	margin: auto;
`;
export default function Home() {
	return (
		<div>
			<Container>
				<Image src={home}></Image>
				<WelcomeHeading>Welcome, To Vitosha Bank</WelcomeHeading>
				<WelcomeText>Seeing this means you are one step closer to making the right choice</WelcomeText>
			</Container>
		</div>
	);
}
