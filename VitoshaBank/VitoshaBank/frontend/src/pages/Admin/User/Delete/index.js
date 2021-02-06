import React, { useState } from "react";
import AdminForm from "../../../../components/Admin/AdminForm";
import { deleteUser } from "../../../../Api/Admin/delete";

export default function DeleteUser() {
	const [uname, setUname] = useState();

	const handleDelete = () => {
		deleteUser(uname);
	};
	return (
		<AdminForm>
			<AdminForm.Input
				heading="Username"
				values={{ value: uname, setValue: setUname }}
			></AdminForm.Input>
			<AdminForm.Button onClick={handleDelete}>Delete User</AdminForm.Button>
		</AdminForm>
	);
}
