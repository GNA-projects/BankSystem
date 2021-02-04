import { Redirect, Route } from "react-router-dom";
import React, { useState } from "react";

export const AuthContext = React.createContext({});

export const logoutUser = () => {
	sessionStorage.removeItem("jwt");
};
export const devLogin = () => {
	sessionStorage["jwt"] = "123";
};

export const checkJwt = () => {
	return sessionStorage["jwt"] ? true : false;
};

export function PrivateRoute({ component: Component, ...rest }) {
	return (
		<Route
			{...rest}
			render={(props) =>
				checkJwt() ? <Component {...props} /> : <Redirect to="/login" />
			}
		/>
	);
}
export function AdminRoute({ component: Component, ...rest }) {
	return (
		<Route
			{...rest}
			render={(props) =>
				checkJwt() ? <Component {...props} /> : <Redirect to="/" />
			}
		/>
	);
}

export function AuthProvider(props) {
	const [logged, setLogged] = useState(checkJwt);
	return (
		<AuthContext.Provider value={{logged, setLogged}}>
			{props.children}
		</AuthContext.Provider>
	);
}
