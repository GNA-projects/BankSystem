import axios from "axios";

export const loginUser = (username, password) => {
  axios
    .post("api/users/login", {
      username: username,
      password: password,
    })
    .then(
      (res) => {
        sessionStorage["jwt"] = res.data;
      },
      (error) => {
        sessionStorage.removeItem("jwt");
      }
    );
};
export const logoutUser = () => {
  sessionStorage.removeItem("jwt");
};
export const devLogin = () => {
  sessionStorage["jwt"] = "123";
};
export const checkIfLogged = () => {
  return sessionStorage["jwt"] ? "V" : "X";
};
