import React from "react";
import {
  Container,
  FormInner,
  FormOuter,
  Input,
  Submit,
  InputContainer,
  InputHeader,
} from "./style";

export default function Login() {
  return (
    <div>
      <Container>
        <FormOuter>
          <FormInner>
            <InputContainer>
              <InputHeader>Username</InputHeader>
              <Input></Input>
            </InputContainer>
            <InputContainer>
              <InputHeader>Password</InputHeader>
              <Input></Input>
            </InputContainer>

            <Submit>Log in</Submit>
          </FormInner>
        </FormOuter>
      </Container>
    </div>
  );
}
