import axios from "axios";

const config = {
  headers: { Authorization: "Bearer " + sessionStorage["jwt"] },
};

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
