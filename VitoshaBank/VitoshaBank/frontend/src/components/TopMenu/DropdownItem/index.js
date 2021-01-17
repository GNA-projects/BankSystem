import React from "react";
import { Item } from "./style";

export default function DropdownItem(props) {
  return (
    <Item onClick={props.onClick}>
      <Item.Icon icon={props.icon} />
      <Item.Heading heading={props.heading}></Item.Heading>
    </Item>
  );
}
