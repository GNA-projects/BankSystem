import { viaxios } from "../../axios";

export const deleteUser = (uname) => {
    viaxios
        .delete("/api/admin/delete/user", {
            data: { username: uname },
        })
        .then((res) => alert(res.data.message))
        .catch((err) => alert(err.response.data.message));
};
export const deleteAccount = (uname, accountType) => {
    viaxios
        .delete(`/api/admin/delete/${accountType}`, {
            data: { username: uname },
        })
        .then((res) => alert(res.data.message))
        .catch((err) => alert(err.response.data.message));
};
