import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export const ActivityBlock = styled.p`
  display: flex;
  justify-content: space-between;
  padding: 20px;
  border-style: none none dotted none;
  border-width: 1px;
  margin: 20px;
`;
export const ActivityBlockIconBackground = styled.div`
  flex: 0.2fr;
  background-color: grey;
  display: flex;
  justify-content: center;
  text-align: center;
  width: 30px;
  height: 30px;
  border: 1px;
  border-radius: 50%;
  padding: 10px;
`;
export const ActivityBlockIcon = styled(FontAwesomeIcon)`
  font-size: 30px;
`;
export const ActivityBlockInfo = styled.div`
  flex: 0.5fr;
`;

export const ActivityBlockHeading = styled.p`
  margin-bottom: 10px;
`;
export const ActivityBlockDate = styled.p``;
export const ActivityBlockMoney = styled.p`
  flex: 0.3fr;
`;
