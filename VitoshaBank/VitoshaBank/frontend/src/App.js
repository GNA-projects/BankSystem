import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import TopMenu from "./components/TopMenu";
import Home from "./components/Home";
import Ebanking from "./components/Ebanking";
import Login from "./components/Login";
import Logout from "./components/Logout";
import About from "./components/About";
import React, { useState } from "react";
import {LoggedInContext, JwtContext} from "./context/context";

function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  const loginValue = { loggedIn, setLoggedIn };

  const [jwtKey, setJwtKey] = useState("")
  const jwtValue = {jwtKey, setJwtKey};

  return (
    <div>
      <Router>
        <LoggedInContext.Provider value={loginValue}>
          <JwtContext.Provider value={jwtValue}>
            <TopMenu></TopMenu>
            <Switch>
              <Route exact path="/" component={Home}></Route>
              <Route path="/ebanking" component={Ebanking}></Route>
              <Route path="/login" component={Login}></Route>
              <Route path="/logout" component={Logout}></Route>
              <Route path="/about" component={About}></Route>
            </Switch>
          </JwtContext.Provider>
        </LoggedInContext.Provider>
      </Router>
    </div>
  );
}

export default App;
