import React from "react";
import img1 from "./money.jpg";
import { faHistory } from "@fortawesome/free-solid-svg-icons";
import { faHouseUser } from "@fortawesome/free-solid-svg-icons";
import "./icon.css";

import {
  Background,
  Body,
  ImageHeading,
  Text,
  Heading,
  Section,
  ImageSection,
  ImageText,
  HeadingIcon,
} from "./style";

export default function About() {
  return (
    <div>
      <Body>
        <ImageSection>
          <Background src={img1}></Background>
          <ImageHeading>Vitosha Bank</ImageHeading>
          <ImageText>Keep money interesting</ImageText>
        </ImageSection>

        <Section>
          <HeadingIcon icon={faHouseUser} className='gray'></HeadingIcon>
          <Heading>Our Company</Heading>
          <Text>
            Our company Our relationships are built on trust that we build every
            day through every interaction. Our employees are empowered to do the
            right thing to ensure they share our customers’ vision for success.
            We work as a partner to provide financial products and services that
            make banking safe, simple and convenient. We’re here to help
            navigate important milestones and strengthen futures together.
          </Text>
        </Section>

        <Section>
          <HeadingIcon icon={faHistory} className='gray'></HeadingIcon>
          <Heading>Our history</Heading>
          <Text>
            Our diverse business mix is fundamental in delivering a consistent,
            predictable and repeatable financial performance year after year.
            Our core businesses include Consumer & Business Banking, Corporate &
            Commercial Banking, Payment Services and Wealth Management &
            Investment Services. Through our “One U.S. Bank” philosophy, we are
            able to bring the power of the whole bank to every customer, every
            single day.
          </Text>
        </Section>
      </Body>
    </div>
  );
}
