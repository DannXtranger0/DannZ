import { getJson } from "../Core/FetchUtils.js"
import { asignContentField,asignImage} from "../Core/AsignContentUtils.js"

let separatedRoute = (window.location.href).split("/");
export let profileId = separatedRoute.at(separatedRoute.length - 1);
 let userId = document.getElementById("userId").dataset.id;

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
        anchorEditProfile.style.display = 'block';
        profileContainer.style.display = 'inline-block';
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




////Setup the posts
//async function setUpPosts() {
//    let params = {
//        userId: profileId,
//        search: ""
//    };
//    let urlParams = new URLSearchParams()

//    if (params.userId) urlParams.append("userId", params.userId);
//    if (params.search) urlParams.append("search", params.search);

//    let allPosts = await bringData(urlParams);
//    console.log(allPosts);

//    allPosts.forEach(post => {
//        const multimediaHtml = verifyMultimedia(post['multimediaUrl'])

//        let postHtml = `<section style="border: 2px solid black;margin:2vh">
//            <div>
//                <div>
//                    <img src="${post['userAvatarUrl'] ?? '/images/userDefault.png'}" style="width:50px;height:50px" />
//                    <div>
//                        <p>${post['userName']}</p>
//                        <p>${post['uploadedDateTime'].split("T")[0]}</p>
//                    </div>
//                </div>

//                <div>
//                    <p>${post['textContent']}</p>
//                </div>
//                        ${multimediaHtml}
//                <div>
//                </div>
//            </div>
//        </section>`
//        postsContainer.innerHTML += postHtml;
//    });
//}