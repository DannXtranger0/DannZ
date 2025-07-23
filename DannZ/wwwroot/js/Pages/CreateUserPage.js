import {form,registerAccount} from "../features/CreateUser.js"

form.addEventListener("submit", async (event) => {
    event.preventDefault();
    let succes = await registerAccount();
    if(succes)
        window.location.href = "/Feed/MainFeed";
    else
        alert("Create Account failed");
})


