import axios from "axios";
import { faMountain } from "@fortawesome/free-solid-svg-icons";
import React, { useState } from "react";
import {
  Container,
  Input,
  UserIcon,
  FormInner,
  FormOuter,
  Heading,
  Group,
  Submit
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
    axios.post("api/users/login", {
      username: username,
      password: password,
    });
  };
  return (
    <div>
      <Container>
        <FormOuter>
          <FormInner>
            <UserIcon icon={faMountain}></UserIcon>
            <Group>
              <Heading>username</Heading>
              <Input onChange={handleChangeUsername} value={username}></Input>
            </Group>
            <Group>
              <Heading>password</Heading>
              <Input type='password' onChange={handleChangePassword} value={password}></Input>
            </Group>
            <Submit onClick={handleSubmit}>Login</Submit>
          </FormInner>
        </FormOuter>
      </Container>
    </div>
  );
}
