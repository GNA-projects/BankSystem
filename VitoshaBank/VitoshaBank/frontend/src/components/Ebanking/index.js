import React from "react";
import Balance from "./Balance";
import Activity from "./Activity";
<<<<<<< HEAD
import { Body, HeadingGreet } from "../../style/eBankingStyle";
import { Redirect } from "react-router-dom";
=======
import { Body, HeadingGreet } from "./style";
>>>>>>> parent of 252463a... style refactor

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
