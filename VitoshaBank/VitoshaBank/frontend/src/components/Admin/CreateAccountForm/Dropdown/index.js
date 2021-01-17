import React from "react";
import { useState } from "react/cjs/react.development";
import {
  DropdownContainer,
  OptionContainer,
  ActiveOption,
  ActiveOptionText,
  Option,
  OptionText,
} from "./style";

export default function Dropdown(props) {
  const [type, setType] = props.type;
  const [dropdown, setDropdown] = useState(false);
  const BankAccount = "BAccount";
  const Credit = "Credit";
  const Wallet = "Wallet";
  const Deposit = "Deposit";
  const handleChange = (value) => {
    setDropdown(false);
    setType(value);
  };
  return (
    <DropdownContainer>
      <ActiveOption onClick={() => setDropdown(!dropdown)}>
        <ActiveOptionText>{type}</ActiveOptionText>
      </ActiveOption>
      <OptionContainer active={dropdown}>
        <Option onClick={() => handleChange(BankAccount)}>
          <OptionText>{BankAccount}</OptionText>
        </Option>
        <Option onClick={() => handleChange(Credit)}>
          <OptionText>{Credit}</OptionText>
        </Option>
        <Option onClick={() => handleChange(Wallet)}>
          <OptionText>{Wallet}</OptionText>
        </Option>
        <Option onClick={() => handleChange(Deposit)}>
          <OptionText>{Deposit}</OptionText>
        </Option>
      </OptionContainer>
    </DropdownContainer>
  );
}
