import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  width: 300px;
  margin: auto;
  box-shadow: 1px 1px 10px;
  padding: 10px 10px;
  margin-top: 20px;
  border-width: 1px;
  border-color: black;
  border-radius: 20px;
`;
export const FormOuter = styled.div`
  margin: auto;
  width: 100%;
  text-align: center;
`;

export const FormInner = styled.div`
  padding: 100px 10px;
  border-width: 1px;
  border-color: black;
  border-radius: 20px;
`;

export const InputContainer = styled.div`
  width: 100%;
`;

export const InputHeader = styled.p`
  font-size: 2rem;
  margin-bottom: 10px;
`;
export const Header = styled.p`
  font-size: 2.5rem;
  text-align: center;
`;

export const Input = styled.input`
  text-align: center;
  width: 100%;
  outline: none;
  border-width: 1px;
  border-color: black;
  border-radius: 20px;
  font-size: 1.5rem;
  padding: 5px 0;
  margin-bottom: 10px;
`;

export const Submit = styled.button`
  width: 100%;
  outline: none;
  border-width: 1px;
  border-color: black;
  background-color: white;
  border-radius: 20px;
  font-size: 1.5rem;
  margin-top: 30px;
  padding: 5px 0;
`;
