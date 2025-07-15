let logOutButton = document.getElementById("logOutButton");

document.addEventListener("DOMContentLoaded", () => {
    if(logOutButton!=null)
        logOutButton.addEventListener("click",logout)
})

async function logout() {
    try {
        let response = await fetch("https://localhost:7238/api/AuthApi/Logout", {
            method: "POST",
            credentials: "include"
        });

        if (!response.ok)
            throw new Error(response.statusText ?? "Unknow error");

        let result = await response.json();
        window.location.href = "/";

    } catch (err) {
        console.log(err);
    }
    
}