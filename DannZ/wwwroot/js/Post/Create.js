//Create post script
let multimediaField = document.querySelector("[name='multimediaField']");
let btnMultimedia = document.querySelector("[name='btnMultimedia']");
let textContentField = document.querySelector("[name='TextContent']")
let postForm = document.querySelector("[name='postForm']");
let useravatar = document.getElementById("userAvatar");
//open the input File
btnMultimedia.addEventListener("click", (e) => {
    e.preventDefault();
    multimediaField.click();
})

//Upload the post
postForm.addEventListener("submit", savePost);
async function savePost(event) {
    event.preventDefault();
    let formData = new FormData(postForm);

    let files = multimediaField.files;
    for (let i = 0; i < files.length; i++) {
        formData.append("MultimediaList", files[i]);
    }

    try {

        let response = await fetch("https://localhost:7238/api/PostsApi/Create", {
            method: "POST",
            body: formData
        })

        if (!response.ok)
            throw new Error(response.text() ?? "Unknown Error");

        let res = await response.json();

        console.log(res);

    } catch (err) {
        console.log(err);
    }
}


