import axios from "axios";
import React, { useState } from "react";
import {
  Container,
  FormInner,
  FormOuter,
  Input,
  Submit,
  InputContainer,
  InputHeader,
  Header,
} from "./style";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const handleChangeUsername = (e) => {
    setUsername(e.target.value);
  };
  const handleChangePassword = (e) => {
    setPassword(e.target.value);
  };
  const handleSubmit = () => {
    axios.post("api/users", {
      username: username,
      password: password,
    });
  };
  return (
    <div>
      <Header>Log in with your bank account</Header>
      <Container>
        <FormOuter>
          <FormInner>
            <InputContainer>
              <InputHeader>Username</InputHeader>
              <Input value={username} onChange={handleChangeUsername}></Input>
            </InputContainer>
            <InputContainer>
              <InputHeader>Password</InputHeader>
              <Input value={password} onChange={handleChangePassword}></Input>
            </InputContainer>

            <Submit onClick={handleSubmit}>Log in</Submit>
          </FormInner>
        </FormOuter>
      </Container>
    </div>
  );
}
