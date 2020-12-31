import React from 'react'
export const LoggedInContext = React.createContext({
  loggedIn: false,
  setLoggedIn: () => {},
});

export const JwtContext = React.createContext({
  jwtKey: false,
  setJwtKey: () => {},
});
