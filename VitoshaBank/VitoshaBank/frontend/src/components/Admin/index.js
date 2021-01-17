import React from "react";
import { useHistory } from "react-router-dom";
import { BodyHeading, Button } from "./style";
export default function Admin() {
  const history = useHistory();
  const goCreateUser = () => {
    history.push("/admin/create/user");
  };
  const goDeleteUser = () => {
    history.push("/admin/delete/user");
  };
  const goCreateAccount = () => {
    history.push("/admin/create/account");
  };
  return (
    <div>
      <BodyHeading>Welcome to the admin panel</BodyHeading>
      <Button onClick={goCreateUser}>Create a User</Button>
      <Button onClick={goDeleteUser}>Delete a User</Button>
      <Button onClick={goCreateAccount}>Create an Account</Button>
    </div>
  );
}
