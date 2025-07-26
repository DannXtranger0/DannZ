let profileId = document.getElementById("userIdHidden").dataset.id;
let logOutButton = document.getElementById("logOutButton");

document.addEventListener("DOMContentLoaded", () => {

    if(logOutButton!=null)
        logOutButton.addEventListener("click",logout)
})

async function logout(){
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

//Bring the user profile

export async function BringProfileData() {
    try {
        let response = await fetch(`https://localhost:7238/api/AccountApi/Profile/${profileId}`);
        if (!response.ok)
            throw new Error(response.status ?? "Unknown Message");

        let result = await response.json();
        console.log(result);
        return result;

    } catch (err) {
        console.log(err);
    }
}