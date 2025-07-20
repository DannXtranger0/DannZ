//Create post script
let multimediaField = document.querySelector("[name='multimediaField']");
let btnMultimedia = document.querySelector("[name='btnMultimedia']");
let textContentField = document.querySelector("[name='TextContent']")
let postForm = document.querySelector("[name='postForm']");
let useravatar = document.getElementById("userAvatar");

let multimediaFiles = [];
let multimediaFilesForPreview = [];

//Modal
let uploadPostModal = document.querySelector("[name='uploadPostModal']");
let filesPreviewContainer = document.querySelector(".files-preview-container");
let textContentFieldModal = document.querySelector("[name='textContentFieldModal']");

//open the input File
btnMultimedia.addEventListener("click", (e) => {
    e.preventDefault();
     multimediaField.click();
})

//Obtaining the files
multimediaField.addEventListener("change", () => {
    for (let i = 0; i < multimediaField.files.length; i++) {
        multimediaFiles.push(multimediaField.files[i]);
        multimediaFilesForPreview.push(multimediaField.files[i]);
    }

    //Showing the preview of the file selected
    multimediaFilesForPreview.forEach(file => {
        let PreviewImageUrl = URL.createObjectURL(file);
        let previewImage = `
        <div style="width:100px;height:170px; overflow: hidden;" >
            <img src ="${PreviewImageUrl}" style="width: 100%; height: 100%; object-fit: fill;"   />                    
        </div>`

        filesPreviewContainer.innerHTML += previewImage;

    })
    
    multimediaFilesForPreview = [];

    //bring TextContent to the Modal
    textContentFieldModal.value= textContentField.value;

    uploadPostModal.style.display = "block";

})


//Upload the post
postForm.addEventListener("submit", savePost);
async function savePost(event) {
    event.preventDefault();

    let formData = new FormData(postForm);
    multimediaFiles.forEach(x => {
        formData.append("MultimediaList", x)
    });
    
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

