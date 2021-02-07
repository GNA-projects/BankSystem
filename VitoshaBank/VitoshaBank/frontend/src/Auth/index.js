import { Redirect, Route } from "react-router-dom";
import React, { useState } from "react";
import { authme } from "../Api/Admin/authme";

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

export const adminAuthMe = async () => {
	let isOk = false;
	isOk = await authme();
	if(isOk === false) window.location.href='/'
	return isOk;
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
				adminAuthMe() ? <Component {...props} /> : <Redirect to="/" />
			}
		/>
	);
}

export function AuthProvider(props) {
	const [logged, setLogged] = useState(checkJwt);
	return (
		<AuthContext.Provider value={{ logged, setLogged }}>
			{props.children}
		</AuthContext.Provider>
	);
}
