import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import image from "./Images/bg.jpg";

export const StyledLoginForm = styled.div`
  display: flex;
  height: 80vh;
`;

export const Outer = styled.div`
  display: flex;
  background-image: url(${image});
  background-color: rgb(40, 57, 101, 0.9);
  height: 90%;
  width: 100vw;
  max-width: 600px;
  margin: auto;
  box-shadow: 1px 0px 30px;
`;

export const Inner = styled.div`
  display: flex;
  flex-direction: column;
  margin: auto;
`;

export const Group = styled.div`
  display: flex;
  flex-direction: column;
`;

export const Heading = styled.p`
  text-align: center;
  color: grey;
  font-size: 14px;
  border: 1px;
  margin: 10px 0;
`;

export const Input = styled.input`
  font-size: 16px;
  background-color: rgb(255, 255, 255, 0.1);
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
`;

export const Submit = styled.button`
  font-size: 16px;
  background-color: rgb(255, 255, 255, 0.5);
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
  margin-top: 20px;
  &:hover {
    background-color: rgb(255, 255, 255, 0.2);
  }
`;

export const UserIcon = styled(FontAwesomeIcon)`
  font-size: 4rem;
  margin: auto;
  margin-bottom: 20px;
`;

export const LoginForm = (props) => (
  <StyledLoginForm>{props.children}</StyledLoginForm>
);
LoginForm.Outer = (props) => <Outer>{props.children}</Outer>;
LoginForm.Inner = (props) => <Inner>{props.children}</Inner>;
LoginForm.Icon = (props) => <UserIcon icon={props.icon}></UserIcon>;
LoginForm.Submit = (props) => (
  <Submit onClick={props.onClick}>{props.children}</Submit>
);

export const InputGroup = (props) => <Group>{props.children}</Group>;
InputGroup.Heading = (props) => <Heading>{props.children}</Heading>;
InputGroup.Input = (props) => (
  <Input type={props.type} onChange={props.onChange} value={props.value}>
    {props.children}
  </Input>
);
