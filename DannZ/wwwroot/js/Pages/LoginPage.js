import {login} from "../features/Login.js"
let form = document.querySelector("form");

form.addEventListener("submit", async (event) => {
    event.preventDefault();
    await login(form);

    window.location.pathname = "/Feed/MainFeed";
});

