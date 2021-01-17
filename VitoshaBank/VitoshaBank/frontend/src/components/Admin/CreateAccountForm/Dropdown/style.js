import styled from "styled-components";

export const DropdownContainer = styled.div``;
export const ActiveOption = styled.div`
  text-align: center;
  outline: none;
  border: 1px;
  padding: 20px;
  width: 100px;
  margin: auto;
  background-color: gray;
  &:hover {
    background-color: lightgray;
  }
`;
export const ActiveOptionText = styled.p`
  color: white;
`;

export const OptionContainer = styled.div`
  overflow: hidden;
  transition: height 1s ease;
  height: ${(props) => (props.active ? "160px" : "0px")};
`;
export const Option = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  text-align: center;
  outline: none;
  border: 1px;
  padding: 10px;
  width: 100px;
  margin: auto;
  &:hover {
    background-color: turquoise;
  }
`;
export const OptionText = styled.p``;

export const TOfP = styled.div``;

