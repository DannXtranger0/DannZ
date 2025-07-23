import {login} from "../features/Login.js"
let form = document.querySelector("form");

form.addEventListener("submit", async (event) => {
    event.preventDefault();
    let succes = await login(form);
    if (succes) {
        window.location.pathname = "/Feed/MainFeed";
    } else {
        alert("Login failed");
    }
});

