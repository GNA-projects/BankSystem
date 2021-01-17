import React from "react";
import Balance from "./Balance";
import Activity from "./Activity";

import { Redirect } from "react-router-dom";
import { Body, HeadingGreet } from "./style";

export default function Ebanking() {
  return sessionStorage["jwt"] ? (
    <div>
      <Body>
        <HeadingGreet>Hi, Sam!</HeadingGreet>
        <Balance />
        <Balance />
        <Balance />
        <Activity />
      </Body>
    </div>
  ) : (
    <Redirect to='/login'></Redirect>
  );
}
