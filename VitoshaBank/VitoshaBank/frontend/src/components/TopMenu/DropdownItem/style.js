import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export const Icon = styled(FontAwesomeIcon)`
  font-size: 3rem;
  margin: 10px;
  color: white;
`;
export const Heading = styled.p`
  font-size: 3rem;
  margin: 10px;
  color: white;
`;

export const Item = styled.div`
  z-index: 0;
  margin: auto;
  display: flex;
  flex-direction: row;
  background-color: rgb(100, 100, 100, 0.8);
  &:hover {
    box-shadow: 1px 1px 10px white;
  }
`;

Item.Icon = (props) => (<Icon icon={props.icon}></Icon>)
Item.Heading = (props) =>(<Heading>{props.heading}</Heading>)
