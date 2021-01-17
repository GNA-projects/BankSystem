import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import Admin from "./components/Admin";
import AdminUserCreate from "./components/Admin/CreateUserForm";
import AdminUserDelete from "./components/Admin/DeleteUserForm";
import AdminAccountCreate from "./components/Admin/CreateAccountForm";

import TopMenu from "./components/TopMenu";
import Home from "./components/Home";
import Ebanking from "./components/Ebanking";
import AllActivity from "./components/Ebanking//Activity/AllActivity";
import Login from "./components/Login";
import Logout from "./components/Logout";
import About from "./components/About";

function App() {
  return (
    <div>
      <Router>
        <TopMenu></TopMenu>
        <Switch>
          <Route exact path="/" component={Home}></Route>
          <Route exact path="/ebanking" component={Ebanking}></Route>
          <Route path="/ebanking/activity" component={AllActivity}></Route>
          <Route path="/login" component={Login}></Route>
          <Route path="/logout" component={Logout}></Route>
          <Route path="/about" component={About}></Route>

          <Route exact path="/admin" component={Admin}></Route>
          <Route path="/admin/create/user" component={AdminUserCreate}></Route>
          <Route path="/admin/create/account" component={AdminAccountCreate}></Route>
          <Route path="/admin/delete/user" component={AdminUserDelete}></Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
