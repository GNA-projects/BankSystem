import styled from "styled-components";

export const BalanceButonGroup = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: center;
  margin: 10px;

`;
export const BalanceButtonCircle = styled.div`
  display: flex;
  height: 40px;
  width: 40px;
  border: 1px;
  border-radius: 50%;
  background-color: darkcyan;
  justify-content: center;
  margin: 5px;
  &:hover{
      background-color: cyan;
  }
`;
export const BalanceButtonCircleValue = styled.p`
  margin: auto;
  color: white;
`;
