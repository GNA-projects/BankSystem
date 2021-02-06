import React, {useState} from "react";
import AdminForm from "../../../../components/Admin/AdminForm";
import { createUser } from "../../../../Api/Admin/create";

export default function CreateUser() {
	const [uname, setUname] = useState();
	const [fname, setFname] = useState();
	const [lname, setLname] = useState();
	const [password, setPassword] = useState();
	const [mail, setMail] = useState();
	const [admin, setAdmin] = useState(false);

	const handleCreate = () => {
		createUser(uname, fname, lname, password, mail, admin);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="First Name"
				values={{ value: fname, setValue: setFname }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Last Name"
				values={{ value: lname, setValue: setLname }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Username"
				values={{ value: uname, setValue: setUname }}
			></AdminForm.Input>
			<AdminForm.Input
				type="password"
				heading="Password"
				values={{ value: password, setValue: setPassword }}
			></AdminForm.Input>
			<AdminForm.Input
				heading="Mail"
				values={{ value: mail, setValue: setMail }}
			></AdminForm.Input>
			<AdminForm.Button onClick={() => setAdmin(!admin)}>
				{admin ? "User will be admin V" : "User won't be admin X"}
			</AdminForm.Button>
			<AdminForm.Button onClick={handleCreate}>Create User</AdminForm.Button>
		</AdminForm>
	);
}
