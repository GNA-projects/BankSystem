import React, { useState } from "react";
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
import {
  BalanceButonGroup,
  BalanceButtonCircle,
  BalanceButtonCircleValue,
} from "./style";

export default function CreateForm() {
  const history = useHistory();
  const [balance, setBalance] = useState(0);
  const handleBalance = (e) => {
    setBalance(e.target.value);
  };
  return (
    <Body>
      <BodyHeading>Create User!!!</BodyHeading>
      <Form>
        <InputBlock>
          <Heading>First Name</Heading>
          <Input></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Last Name</Heading>
          <Input></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Bank Name</Heading>
          <Input></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Bank Password</Heading>
          <Input></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Balance</Heading>
          <Input value={balance} onChange={handleBalance}></Input>
          <BalanceButonGroup>
            <BalanceButtonCircle onClick={() => setBalance(0)}>
              <BalanceButtonCircleValue>Res</BalanceButtonCircleValue>
            </BalanceButtonCircle>
          </BalanceButonGroup>
        </InputBlock>

        <InputBlock>
          <Heading>Birth Date</Heading>
          <Input type="datetime-local"></Input>
        </InputBlock>

        <Button>Create User</Button>
      </Form>
      <BackButton onClick={() => history.goBack()}>Back</BackButton>
    </Body>
  );
}
