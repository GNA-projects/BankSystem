import styled from "styled-components";

export const Container = styled.div`
  display: flex;
  background-color: blue;
  width: 300px;
  height: 500px;
  margin: auto;
  overflow: hidden;
`;
export const FormOuter = styled.div`
  margin: auto;
  background-color: green;
  height: 200px;
  width: 100%;
  text-align: center;
`;

export const FormInner = styled.div`
  padding: 0 10px;
`;

export const InputContainer = styled.div`
  width: 100%;
`;

export const InputHeader = styled.p`
  font-size: 1.5rem;
`;

export const Input = styled.input`
  width: 100%;
  outline: none;
  border-width: 1px;
  border-color: white;
  border-radius: 20px;
  font-size: 1.5rem;
`;

export const Submit = styled.button`
  width: 100%;
  outline: none;
  border-width: 1px;
  border-color: white;
  background-color: white;
  border-radius: 20px;
  font-size: 1.5rem;
  margin-top: 30px;
`;
