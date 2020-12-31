import React from "react";
import {
  Body,
  HeadingGreet,
  BalanceSection,
  BalanceHeading,
  BalanceMoney,
  BalanceMoneyHeading,
  ActivitySection,
  ActivityHeading,
  ActivityBlock,
  ActivityBlockIconBackground,
  ActivityBlockIcon,
  ActivityBlockInfo,
  ActivityBlockHeading,
  ActivityBlockDate,
  ActivityBlockMoney
} from "./style";
import { faStore } from "@fortawesome/free-solid-svg-icons";

export default function Ebanking() {
  return (
    <div>
      <Body>
        <HeadingGreet>Hi, Sam!</HeadingGreet>
        <BalanceSection>
          <BalanceHeading>Vitosha balance</BalanceHeading>
          <BalanceMoney>24,55 BGN</BalanceMoney>
          <BalanceMoneyHeading>Available</BalanceMoneyHeading>
        </BalanceSection>
        <ActivitySection>
            <ActivityHeading>Recent Activity</ActivityHeading>
            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>

            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>

            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>

            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>

            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>

            <ActivityBlock>
                <ActivityBlockIconBackground>
                	<ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
                </ActivityBlockIconBackground>
                <ActivityBlockInfo>
                    <ActivityBlockHeading>Netflix.com</ActivityBlockHeading>
                    <ActivityBlockDate>11 Sep</ActivityBlockDate>
                </ActivityBlockInfo>
                <ActivityBlockMoney> - 25,22 BGN</ActivityBlockMoney>
            </ActivityBlock>
        </ActivitySection>
      </Body>
    </div>
  );
}
