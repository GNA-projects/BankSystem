import React from "react";
import { useHistory } from "react-router-dom";
import { BodyHeading, Button } from "./style";
export default function Admin() {
  const history = useHistory();
  const goCreate = () => {
    history.push("/admin/create");
  };
  const goDelete = () => {
    history.push("/admin/delete");
  };
  return (
    <div>
      <BodyHeading>Welcome to the admin panel</BodyHeading>
      <Button onClick={goCreate}>Create a User</Button>
      <Button onClick={goDelete}>Delete a User</Button>
    </div>
  );
}
