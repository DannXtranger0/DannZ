let logOutButton = document.getElementById("logOutButton");

document.addEventListener("DOMContentLoaded", () => {
    if(logOutButton!=null)
        logOutButton.addEventListener("click",logout)
})

async function logout() {
    try {
        let response = await fetch("https://localhost:7238/api/Auth/Logout", {
            method: "POST",
            credentials: "include"
        });

        if(!response.ok)
            throw new Error(response.message ?? "Unknow error");

        let result = await response.json();
        console.log(result);

    } catch (err) {
        console.log(err);
    }
    
}