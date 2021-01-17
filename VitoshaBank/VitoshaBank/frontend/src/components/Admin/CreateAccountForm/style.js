import styled from "styled-components";

export const TermOPContainer = styled.div`
  margin: auto;
  text-align: center;
  overflow: hidden;
  transition: height 1s ease;
  height: ${(props) => (props.active ? "140px" : "0px")};
`;
export const Horizontal = styled.div`
  display: flex;
  flex-direction: row;
`;
