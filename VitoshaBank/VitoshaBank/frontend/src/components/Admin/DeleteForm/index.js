import React from 'react'
import {useHistory} from 'react-router-dom'
import { Body, Form, InputBlock, Heading, Input, Button, BodyHeading, BackButton } from "../style";


export default function DeleteForm() {
    const history = useHistory()

    return (
        <Body>
            <BodyHeading>Delete User!!!</BodyHeading>
            <Form>
        <InputBlock>
          <Heading>Bank Name</Heading>
          <Input ></Input>
        </InputBlock>

        <Button>Delete User</Button>
      </Form>
            <BackButton onClick={() => history.goBack()}>go back</BackButton>
        </Body>
    )
}
