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

export default function CreateUserForm() {
  const history = useHistory();
  const [fname, setFname] = useState("");
  const [lname, setLname] = useState("");
  const [uname, setUname] = useState("");
  const [bdate, setBdate] = useState(Date);
  const [password, setPassword] = useState("");
  const [isAdmin, setIsAdmin] = useState(false);

  const handleCreate = () => {
    const config = {
      headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
    };

    axios.post(
      "/api/users/create",
      {
        firstname: fname,
        lastname: lname,
        username: uname,
        password: password,
        birthDate: bdate,
        isAdmin: isAdmin,
      },
      config
    );
  };

  return (
    <Body>
      <BodyHeading>Create User!!!</BodyHeading>
      <Form>
        <InputBlock>
          <Heading>First Name {fname}</Heading>
          <Input
            value={fname}
            onChange={(e) => {
              setFname(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Last Name {lname}</Heading>
          <Input
            value={lname}
            onChange={(e) => {
              setLname(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Bank Name {uname}</Heading>
          <Input
            value={uname}
            onChange={(e) => {
              setUname(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Bank Password {password}</Heading>
          <Input
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Birth Date {bdate}</Heading>
          <Input
            type="datetime-local"
            value={bdate}
            onChange={(e) => {
              setBdate(e.target.value);
            }}
          ></Input>
        </InputBlock>

        <InputBlock>
          <Heading>Admin {isAdmin ? "true" : "False"}</Heading>
          <Button
            onClick={() => {
              setIsAdmin(!isAdmin);
            }}
          >
            User is {isAdmin ? "Admin" : "not Admin"}{" "}
          </Button>
        </InputBlock>

        <Button onClick={handleCreate}>Create User </Button>
      </Form>
      <BackButton onClick={() => history.goBack()}>Back</BackButton>
    </Body>
  );
}
