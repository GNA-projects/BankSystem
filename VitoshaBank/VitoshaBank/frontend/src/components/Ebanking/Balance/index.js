import React from "react";
import {
  BalanceSection,
  BalanceHeading,
  BalanceMoney,
  BalanceMoneyHeading,
} from "../../../style/balanceStyle";

export default function Balance() {
  return (
    <BalanceSection>
      <BalanceHeading>Vitosha balance</BalanceHeading>
      <BalanceMoney>24,55 BGN</BalanceMoney>
      <BalanceMoneyHeading>Available</BalanceMoneyHeading>
    </BalanceSection>
  );
}
