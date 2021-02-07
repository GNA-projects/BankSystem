import React from 'react'
import Input from "./Input";
import Button from "./Button";

export default function AdminForm(props) {
	return <React.Fragment {...props}>{props.children}</React.Fragment>;
}
AdminForm.Input = Input;
AdminForm.Button = Button;
