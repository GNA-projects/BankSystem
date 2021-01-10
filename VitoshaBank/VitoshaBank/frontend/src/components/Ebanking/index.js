import React from "react";
import Balance from "./Balance";
import Activity from "./Activity";
import { Body, HeadingGreet } from "../../style/eBankingStyle";

export default function Ebanking() {
  return (
    <div>
      <Body>
        <HeadingGreet>Hi, Sam!</HeadingGreet>
        <Balance />
        <Activity />
      </Body>
    </div>
  );
}
