import React, { useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import {
  Body,
  Form,
  InputBlock,
  Heading,
  Input,
  Button,
  BodyHeading,
  BackButton,
} from "../style";
import { TermOPContainer, Horizontal } from "./style";
import Dropdown from "./Dropdown";

export default function CreateAccountForm() {
  const history = useHistory();
  const [type, setType] = useState("BAccount");
  const [balance, setBalance] = useState(0.0);
  const [top, setTop] = useState(0);

  const handleCreate = () => {
    const config = {
      headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
    };

    axios.post(
      "/api/users/create",
      {
        type: type,
        balance: balance,
      },
      config
    );
  };

  return (
    <Body>
      <BodyHeading>Create Account!!!</BodyHeading>
      <Form>
        <InputBlock>
          <Heading>Account Type</Heading>
          <Dropdown type={[type, setType]}></Dropdown>
        </InputBlock>

        <InputBlock>
          <Heading>Balance: {balance}</Heading>
          <Input
            value={balance}
            onChange={(e) => {
              setBalance(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <TermOPContainer active={type === "Credit" || type === "Deposit"}>
          <Input
            readOnly
            value={top}
            onChange={(e) => {
              setTop(e.target.value);
            }}
          ></Input>
          <Horizontal>
            <Button onClick={() => setTop(1)}>1</Button>
            <Button onClick={() => setTop(3)}>3</Button>
            <Button onClick={() => setTop(6)}>6</Button>
            <Button onClick={() => setTop(12)}>12</Button>
            <Button onClick={() => setTop(24)}>24</Button>
          </Horizontal>
        </TermOPContainer>

        <Button onClick={handleCreate}>Create Account </Button>
      </Form>
      <BackButton onClick={() => history.goBack()}>Back</BackButton>
    </Body>
  );
}
