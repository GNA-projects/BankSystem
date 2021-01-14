import React from "react";
import { useState } from "react/cjs/react.development";
import { useHistory } from "react-router-dom";
import ActivityBlock from "./ActivityBlock";
import { ActivitySection, ActivityHeading, ActivityButton } from "./style";

export default function Activity(props) {
  const history = useHistory();
  const goAllActivity = () => {
    history.push('/ebanking/activity')
  }
  const [activity, setActivity] = useState([
    { source: "Georgi Trapov", date: "11 Sep", value: "52.33" },
    { source: "Dimo Dimitrov?", date: "32 Dec", value: "12.33" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
    { source: "Niki Hajoto", date: "69 Dimo", value: "00.00" },
  ]);
  const size = props.all ? 100 : 3;

  return (
    <ActivitySection>
      <ActivityHeading>Recent Activity</ActivityHeading>
      {activity.slice(0,size).map((item, i) => (
        <ActivityBlock
          key={i}
          source={item.source}
          date={item.date}
          value={item.value}
        />
      ))}
      <ActivityButton onClick={goAllActivity}>All Activity</ActivityButton>
    </ActivitySection>
  );
}
