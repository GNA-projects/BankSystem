import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import Admin from './components/Admin'
import AdminCreate from './components/Admin/CreateForm'
import AdminDelete from './components/Admin/DeleteForm'

import TopMenu from "./components/TopMenu";
import Home from "./components/Home";
import Ebanking from "./components/Ebanking";
import AllActivity from "./components/Ebanking//Activity/AllActivity";
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
              <Route exact path="/ebanking" component={Ebanking}></Route>
              <Route path="/ebanking/activity" component={AllActivity}></Route>
              <Route path="/login" component={Login}></Route>
              <Route path="/logout" component={Logout}></Route>
              <Route path="/about" component={About}></Route>

              <Route exact path="/admin" component={Admin}></Route>
              <Route path="/admin/create" component={AdminCreate}></Route>
              <Route path="/admin/delete" component={AdminDelete}></Route>

            </Switch>
          </JwtContext.Provider>
        </LoggedInContext.Provider>
      </Router>
    </div>
  );
}

export default App;
