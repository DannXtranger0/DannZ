let postsContainer = document.getElementById("postsContainer");

document.addEventListener("DOMContentLoaded",  setUpPosts);

//Bringing the posts
export async function bringData(urlParams) {
    
    try {
        let response = await fetch(`https://localhost:7238/api/PostsApi?${urlParams.toString()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok)
            throw new Error(response.status ?? "Unknown Message");

        let res = await response.json();

        return res;

    } catch (err) {
        console.log(err);
    }
 
}


//Setup the posts
 async function setUpPosts() {

    let params = {
        userId: null,
        serach: null
    };
    let urlParams = new URLSearchParams(params)
    let allPosts = await bringData(params);
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
export function verifyMultimedia(multimedia) {
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

