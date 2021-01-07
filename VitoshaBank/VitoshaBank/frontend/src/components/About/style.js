import styled from "styled-components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
export const Body = styled.div`
  max-width: 1000px;
  margin: auto;
  text-align: center;
`;

export const ImageSection = styled.div`
  position: relative;
`;
export const Background = styled.img`
  width: 100%;
`;
export const ImageHeading = styled.div`
  position: absolute;
  bottom: 50%;
  left: 10%;
  font-size: 50px;
  text-shadow: -1px -1px 0 white, 1px -1px 0 white, -1px 1px 0 white,
    1px 1px 0 white;
`;
export const ImageText = styled.div`
  position: absolute;
  bottom: 35%;
  left: 10%;
  font-size: 30px;
  text-shadow: -1px -1px 0 white, 1px -1px 0 white, -1px 1px 0 white,
    1px 1px 0 white;
`;

export const Section = styled.div`
  margin: 100px 20px;
`;

export const HeadingIcon = styled(FontAwesomeIcon)`
  font-size: 35px;
  margin: 10px 0;
`;
export const Heading = styled.div`
  font-size: 24px;
  margin: 10px 0;
`;
export const Text = styled.div`
  font-size: 20px;
  line-height: 25px;
  margin: 5px 0;
  padding: 10px 40px;
`;
