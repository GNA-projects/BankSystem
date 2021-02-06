import React, { useState } from "react";
import { useEffect } from "react";
import { getAllTickets } from "../../../../Api/Admin/support";

export default function Support() {
	const [tickets, setTickets] = useState();
	useEffect(() => {
		setTickets(getAllTickets())
	}, []);

	return <div>123</div>;
}
