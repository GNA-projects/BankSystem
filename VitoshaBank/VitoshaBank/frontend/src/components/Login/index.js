import React, { useState } from "react";
import { loginUser, logoutUser, devLogin } from "../../Api/user";
import { LoginForm, InputGroup } from "./style";
import { faMountain } from "@fortawesome/free-solid-svg-icons";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleChangeUsername = (e) => {
    setUsername(e.target.value);
  };
  const handleChangePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleLogin = () => {
    loginUser(username, password);
  };

  return (
    <LoginForm>
      <LoginForm.Outer>
        <LoginForm.Inner>
          <LoginForm.Icon icon={faMountain}></LoginForm.Icon>
          <InputGroup>
            <InputGroup.Heading>username</InputGroup.Heading>
            <InputGroup.Input
              onChange={handleChangeUsername}
              value={username}
            ></InputGroup.Input>
          </InputGroup>
          <InputGroup>
            <InputGroup.Heading>password</InputGroup.Heading>
            <InputGroup.Input
              type="password"
              onChange={handleChangePassword}
              value={password}
            ></InputGroup.Input>
          </InputGroup>
          <LoginForm.Submit onClick={handleLogin}>Login</LoginForm.Submit>
          <LoginForm.Submit onClick={logoutUser}>LogOut</LoginForm.Submit>
          <LoginForm.Submit onClick={devLogin}>DevLogin</LoginForm.Submit>
        </LoginForm.Inner>
      </LoginForm.Outer>
    </LoginForm>
  );
}
