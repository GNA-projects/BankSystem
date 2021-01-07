import styled from "styled-components";

export const Body = styled.div`
  width: 100vw;
  max-width: 1000px;
  margin: auto;
`;

export const BodyHeading = styled.h1`
  margin: 20px;
  text-align: center;
`;

export const Form = styled.div`
  max-width: 700px;
  margin: auto;
  padding: 20px;
`;

export const InputBlock = styled.div`
  margin: 10px auto;
  text-align: center;
`;

export const Heading = styled.p``;

export const Input = styled.input`
  font-size: 16px;
  background-color: rgb(0, 139, 139, 0.7);
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
`;

export const Button = styled.button`
  display: block;
  margin: auto;
  font-size: 16px;
  background-color: darkcyan;
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
  margin-top: 20px;
  &:hover {
    background-color: cyan;
  }
`;

export const BackButton = styled.button`
  display: block;
  margin: 10px auto;
  font-size: 16px;
  background-color: gray;
  padding: 15px 20px;
  color: white;
  outline: none;
  border: 1px;
  border-radius: 20px;
  margin-top: 20px;
  &:hover {
    background-color: cyan;
  }
`;
