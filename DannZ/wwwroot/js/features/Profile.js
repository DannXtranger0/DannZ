import { getJson } from "../Core/FetchUtils.js"
import { asignContentField,asignImage} from "../Core/AsignContentUtils.js"

let separatedRoute = (window.location.href).split("/");
export let profileId = separatedRoute.at(separatedRoute.length - 1);
 let userId = document.getElementById("userIdHidden").dataset.id;

//Look and set for the data 
export async function BringProfileData() {
    let data = await getJson(`https://localhost:7238/api/AccountApi/Profile/${profileId}`);
    return data;
}
export async function setData() {

    let profileData = await BringProfileData();
    asignContentField("Name",profileData["name"]);
    asignContentField("Biography", profileData["biography"] || "There is not biography");
    asignImage("AvatarUrl", profileData["avatarUrl"]);
    asignImage("CoverUrl", profileData["coverUrl"]);

    //another method
    if (userId == profileId) {
        let anchorEditProfile = document.getElementById("anchorEditProfile");
        let profileContainer = document.getElementById("profileContainer");
        let uploadPostView = document.getElementById("uploadPostView");
        anchorEditProfile.style.display = 'block';
        profileContainer.style.display = 'inline-block';
        uploadPostView.style.display = 'block'
    }

}

////Edit Bio
let btnEditBio = document.getElementById("btnEditBio");
btnEditBio.addEventListener("click", editBio);

let biographyField = document.getElementById("Biography");


export async function editBio() {

    let editText = "Edit Bio";
    let saveText = "Save Changes";

    if (btnEditBio.textContent == editText) {
        biographyField.disabled = false;
        btnEditBio.textContent = saveText;
    }

    else if (btnEditBio.textContent == saveText) {
        btnEditBio.textContent = editText;
        biographyField.disabled = true;

     
        let response = await getJson(`https://localhost:7238/api/AccountApi/Edit/Bio/${userId}`, {
            method: 'PATCH',
            headers: {
                "Content-Type": "application/json"
            },
            credentials: 'include',
            body: JSON.stringify(biographyField.value)
        });

        console.log(response);
    }
}
