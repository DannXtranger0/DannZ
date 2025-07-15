//Obtain the id of the profile from the route
let route = window.location.pathname;
let separatedRoute = route.split("/");
let profileId = separatedRoute.at(separatedRoute.length - 1);

//Look for the data while the page is loading

async function BringProfileData() {
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


document.addEventListener("DOMContentLoaded", setData);
async function setData() {
    profileData = await BringProfileData();

    let nameField = document.getElementById("Name");
    let biographyField = document.getElementById("Biography");
    let avatarField = document.getElementById("AvatarUrl")


    nameField.textContent = profileData["name"];
   
    if (profileData["biography"] != null)
        biographyField.textContent = profileData["biography"];
    if (profileData["avatarUrl"] != null)
        avatarField.src = profileData["avatarUrl"];
    if (profileData["coverUrl"] != null)
        coverField.src = profileData["coverUrl"];


    let userId = document.getElementById("anchorEditProfile").dataset.id;

    console.log(userId + " " + profileId);
    console.log(userId== profileId);
    if (userId == profileId) {
        let anchorEditProfile = document.getElementById("anchorEditProfile");
        anchorEditProfile.style.display = 'inline-block';  
    }


}