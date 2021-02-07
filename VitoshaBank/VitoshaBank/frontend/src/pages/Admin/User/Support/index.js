import React, { useCallback, useState } from "react";
import { useEffect } from "react";
import { getAllTickets, respondToTicket } from "../../../../Api/Admin/support";

export default function Support() {
	const [tickets, setTickets] = useState([]);
	const respond = async (id) => {
		await respondToTicket(id);
		getTickets();
	};
	const getTickets = async () => {
		const array = await getAllTickets();
		setTickets(array);
		return array;
	};
	useEffect(() => {
		getTickets();
	}, []);

	return (
		<div>
			{tickets.map((item, i) => {
				return (
					<div key={i}>
						<h1>Ticket title: {item.title}</h1>
						<p>Ticket id: {item.id}</p>
						<p>Username: {item.username}</p>
						<p>Message: {item.message}</p>
						<button onClick={() => respond(item.id)}>Respond</button>
					</div>
				);
			})}
		</div>
	);
}
