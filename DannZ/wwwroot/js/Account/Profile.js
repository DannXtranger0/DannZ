import {bringData,verifyMultimedia } from "../Post/FeedPost.js"
//Obtain the id of the profile from the route
let route = window.location.pathname;
let separatedRoute = route.split("/");
let profileId = separatedRoute.at(separatedRoute.length - 1);
let userId = document.getElementById("anchorEditProfile").dataset.id;
let postsContainer = document.getElementById("postsContainer");


//Look for the data 
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

//Set the data
document.addEventListener("DOMContentLoaded", setData);
async function setData() {
   
    let profileData = await BringProfileData();

    let nameField = document.getElementById("Name");
    let biographyField = document.getElementById("Biography");
    let avatarField = document.getElementById("AvatarUrl")
    let coverField = document.getElementById("CoverUrl")


    nameField.textContent = profileData["name"];
   
    if (profileData["biography"] != null)
        biographyField.value = profileData["biography"];
    if (profileData["avatarUrl"] != null)
        avatarField.src = profileData["avatarUrl"];
    if (profileData["coverUrl"] != null)
        coverField.src = profileData["coverUrl"];


    if (userId == profileId) {
        let anchorEditProfile = document.getElementById("anchorEditProfile");
        let profileContainer = document.getElementById("profileContainer");
        anchorEditProfile.style.display = 'block';  
        profileContainer.style.display = 'inline-block';  

    }

    setUpPosts();
    
}

//Edit Bio
let btnEditBio = document.getElementById("btnEditBio");
let biographyField = document.getElementById("Biography");

btnEditBio.addEventListener("click", editBio);

async function editBio() {

    let editText = "Edit Bio";
    let saveText = "Save Changes";

    if (btnEditBio.textContent == editText) {
        biographyField.disabled = false;
        btnEditBio.textContent = saveText;
    }

    else if (btnEditBio.textContent == saveText) {
        btnEditBio.textContent = editText;
        biographyField.disabled = true;

        try {
            let response = await fetch(`https://localhost:7238/api/AccountApi/Edit/Bio/${userId}`, {
                method: 'PATCH',
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: 'include',
                body: JSON.stringify(biographyField.value)
            });

            if (!response.ok)
                throw new Error(response.text.arguments ?? "Unknown Error");
            let res = await response.json();

            console.log(res);

        } catch (err) {
            console.log(err)
        }

    }

}


//Setup the posts
async function setUpPosts() {
    let params = {
        userId: profileId,
        search: ""
    };
    let urlParams = new URLSearchParams()

    if (params.userId) urlParams.append("userId", params.userId);
    if (params.search) urlParams.append("search", params.search);

    let allPosts = await bringData(urlParams);
    console.log(allPosts);

    allPosts.forEach(post => {
        const multimediaHtml = verifyMultimedia(post['multimediaUrl'])

        let postHtml = `<section style="border: 2px solid black;margin:2vh">
            <div>
                <div>
                    <img src="${post['userAvatarUrl'] ?? '/images/userDefault.png'}" style="width:50px;height:50px" />
                    <div>
                        <p>${post['userName']}</p>
                        <p>${post['uploadedDateTime'].split("T")[0]}</p>
                    </div>
                </div>

                <div>
                    <p>${post['textContent']}</p>
                </div>
                        ${multimediaHtml}
                <div>
                </div>
            </div>
        </section>`
        postsContainer.innerHTML += postHtml;
    });
}