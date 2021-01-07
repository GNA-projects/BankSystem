import axios from "axios";
import { faMountain } from "@fortawesome/free-solid-svg-icons";
import {LoggedInContext ,JwtContext} from '../../context/context'
import { useHistory } from "react-router-dom";
import React, { useContext, useState } from "react";
import {
  Container,
  Input,
  UserIcon,
  FormInner,
  FormOuter,
  Heading,
  Group,
  Submit,
} from "./style";

export default function Login() {
  const [adminCounter, setAdminCounter] = useState(0);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [jwt, setJwt] = useState("");

  const {loggedIn, setLoggedIn} = useContext(LoggedInContext)
  const {jwtKey, setJwtKey } = useContext(JwtContext);

  const history = useHistory()
  const handleAdmin = () => {
    setAdminCounter(adminCounter+1)
    if (adminCounter === 5) history.push('/admin')
  }

  const handleChangeUsername = (e) => {
    setUsername(e.target.value);
  };
  const handleChangePassword = (e) => {
    setPassword(e.target.value);
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .post("api/users/login", {
        username: username,
        password: password,
      })
      .then(
        (res) => {
          setLoggedIn(true);
          setJwtKey(res.data)
        },
        (error) => {
          setLoggedIn(false);
          setJwtKey("")
        }
      );
  };
  return (
    <div>
      <Container>
        <FormOuter>
          <FormInner>
            <UserIcon icon={faMountain} onClick={handleAdmin}></UserIcon>
            <Group>
              <Heading>username</Heading>
              <Input onChange={handleChangeUsername} value={username}></Input>
            </Group>
            <Group>
              <Heading>password</Heading>
              <Input
                type="password"
                onChange={handleChangePassword}
                value={password}
              ></Input>
            </Group>
            <Submit onClick={handleSubmit}>Login</Submit>
          </FormInner>
        </FormOuter>
      </Container>
    </div>
  );
}
