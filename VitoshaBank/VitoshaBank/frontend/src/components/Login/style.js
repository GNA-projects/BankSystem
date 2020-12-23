import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import image from "./bg.jpg";

export const Container = styled.div`
  display: flex;
  height: 80vh;
`;

export const FormOuter = styled.div`
  display: flex;
  background-image: url(${image});
  background-color: rgb(40, 57, 101, 0.9);
  height: 90%;
  width: 100vw;
  max-width: 600px;
  margin: auto;
  box-shadow: 1px 0px 30px;
`;

export const FormInner = styled.div`
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
  background-color: rgb(255, 255, 255, .5);
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
  margin-top: 20px;
  &:hover{
  background-color: rgb(255, 255, 255, 0.2);
  }
`;

export const UserIcon = styled(FontAwesomeIcon)`
  font-size: 4rem;
  margin: auto;
  margin-bottom: 20px;
`;
