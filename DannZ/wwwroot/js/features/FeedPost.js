import {getJson } from "../Core/FetchUtils.js";
let postsContainer = document.getElementById("postsContainer");
//Bringing the posts
export async function bringData(userId,search) {
    let params = {
        userId: userId??"",
        search: search??""
    };
    let urlParams = new URLSearchParams(params);

        return await getJson(`https://localhost:7238/api/PostsApi?${urlParams.toString()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
}

//Setup the posts
 export async function setUpPosts(allPosts) {

     if (allPosts == null)
         return;

    allPosts.forEach(post => {
        const multimediaHtml = asignPostMultimedia(post['multimediaUrl']);

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
export function asignPostMultimedia(multimedia) {
    let filesHtml = "";
    if (multimedia.length > 0) {
        multimedia.forEach(file => {
            if (file.includes("mp4")) {
                filesHtml += `<video src="${file}" type="video/mp4" controls
                style="width:300px;height:300px;"></video>`
            } else {
                filesHtml += `<img src="${file}" style="width:300px;height:300px;"/>`
            }
        })
    }
    return filesHtml;
}

