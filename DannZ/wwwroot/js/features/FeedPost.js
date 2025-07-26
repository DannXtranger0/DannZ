import {getJson } from "../Core/FetchUtils.js";
import { getUserId} from "../Core/BringFormData.js";
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

        let postHtml =
        `<section  style="border: 2px solid black;margin:2vh" data-id="${post['postId']}" name="postContainer">
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
            
            <div>
                <button style="display:flex" name="btnOpenComment" onclick="viewComments(event)">
                    <img src="/images/commentWithoutComment.png" style="width:50px;height:50px"/>
                    <p>${post['commentsPost'].length}</p>
                </button>

                <section name="sectionComments" style="display:none">
                    <div>
                        <textarea name="CommentContent"></textarea>
                        <button onclick="saveComment(event)">Comment</button>
                    </div name="commentsContainer">
                    
                        ${loadComments(post['commentsPost'])}
                       
                    <div>
                        
                    </div>
                </section>
            </div>

        </section>`
        postsContainer.innerHTML += postHtml;
    });

}

 function loadComments(allComments) {
    let commentHtml = "";
    allComments.forEach(comment => {
        commentHtml +=
        `<div name="comment">
            <p>${comment['userName']}</p>
            <img src="${comment['userAvatarUrl']}" style="height:50px;width:50px">
                <p>${comment['uploadedDateTime']}</p>
                <p>${comment['commentContent']}</p>
        </div>`
    });
     return commentHtml;

}
window.viewComments = function (event) {
    let postContainer = event.currentTarget.closest("[name='postContainer']");
    let sectionComment = postContainer.querySelector("[name='sectionComments']");
    //en un futuro con toggle
    sectionComment.style.display = "block";
}

window.saveComment = async function (event) { 
    let postContainer = event.currentTarget.closest("[name='postContainer']");
    let postId = postContainer.dataset.id;
    let userId = getUserId();
    let commentContent = postContainer.querySelector("[name='CommentContent']").value;


    return await getJson(`https://localhost:7238/api/CommentsApi/Create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            PostId: postId,
            UserId: userId,
            CommentContent: commentContent})
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

