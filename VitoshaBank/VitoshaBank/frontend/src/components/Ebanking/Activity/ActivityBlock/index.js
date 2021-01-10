import React from "react";
import {
  ActivityBlock,
  ActivityBlockIconBackground,
  ActivityBlockIcon,
  ActivityBlockInfo,
  ActivityBlockHeading,
  ActivityBlockDate,
  ActivityBlockMoney,
} from "../../../../style/activityBlockStyle";
import { faStore } from "@fortawesome/free-solid-svg-icons";

export default function Ebanking(props) {
  return (
    <ActivityBlock>
      <ActivityBlockIconBackground>
        <ActivityBlockIcon icon={faStore}></ActivityBlockIcon>
      </ActivityBlockIconBackground>
      <ActivityBlockInfo>
        <ActivityBlockHeading>{props.source}</ActivityBlockHeading>
        <ActivityBlockDate>{props.date}</ActivityBlockDate>
      </ActivityBlockInfo>
      <ActivityBlockMoney> - {props.value} BGN</ActivityBlockMoney>
    </ActivityBlock>
  );
}
