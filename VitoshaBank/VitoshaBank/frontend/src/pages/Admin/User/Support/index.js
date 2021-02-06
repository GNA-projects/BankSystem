import React, { useCallback, useState } from "react";
import { useEffect } from "react";
import { getAllTickets } from "../../../../Api/Admin/support";

export default function Support() {
	const [tickets, setTickets] = useState([]);

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
					<div>
						<h1>{item.title}</h1>
						<p>{item.id}</p>
						<p>{item.userId}</p>
						<p>{item.message}</p>
						<button onClick={() => {}}></button>
					</div>
				);
			})}
			<button onClick={}>cc</button>
		</div>
	);
}
