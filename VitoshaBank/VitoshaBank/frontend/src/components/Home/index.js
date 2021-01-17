import { Redirect } from "react-router-dom";

export default function Home() {
  return sessionStorage["jwt"] ? (
    <div>
      <h1>welcome, You are logged in</h1>
    </div>
  ) : (
    <div>
      <Redirect to="/login"></Redirect>
    </div>
  );
}
