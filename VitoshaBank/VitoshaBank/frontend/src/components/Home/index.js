import React, { useContext } from "react";
import { Redirect } from "react-router-dom";
import {LoggedInContext, JwtContext} from "../../context/context";

export default function Home() {
  const { loggedIn, setLoggedIn } = useContext(LoggedInContext);
  const { jwtKey, setJwtKey } = useContext(JwtContext);
  return (jwtKey !== "") ? (
    <div>
      <h1>welcome, You are logged in {jwtKey}</h1>
    </div>
  ) : (
    <div>
      <Redirect to='/login'></Redirect>
    </div>
  );
}
