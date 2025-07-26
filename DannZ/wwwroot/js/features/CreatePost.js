import {getJson } from "../Core/FetchUtils.js";

export let btnMultimedia = document.querySelector("[name='btnMultimedia']");
export let btnMultimediaModal = document.querySelector("[name='btnMultimediaModal']");
export let multimediaField = document.querySelector("[name='multimediaField']");

//Create post script
let uploadPost = document.querySelector("[name='uploadPost']");
let textContentField = document.getElementById("TextContent");
export let postForm = document.querySelector("[name='postForm']");

let multimediaFiles = [];
let multimediaFilesForPreview = [];

//Modal
let uploadPostModal = document.querySelector("[name='uploadPostModal']");
let filesPreviewBox = document.querySelector(".files-preview-box");
let textContentFieldModal = document.getElementById("textContentFieldModal");


//Obtaining the files
export async function showFilePreview() {
    for (let i = 0; i < multimediaField.files.length; i++) {
        multimediaFiles.push(multimediaField.files[i]);
        multimediaFilesForPreview.push(multimediaField.files[i]);
    }
    //Showing the preview of the file selected
    for (let i = 0; i < multimediaFilesForPreview.length; i++) {
        let PreviewImageUrl = URL.createObjectURL(multimediaFilesForPreview[i]);
        let previewImage = `
        <div style="width:100px;height:170px; overflow: hidden; position:relative; class="file-preview" data-id="${i}" >
            <button name="deletePreviewButton"  type="button" style="position:absolute;top:2px;right:2px;width:20px;height:20px; z-index:5;">
                <img src="/images/remove.png" style="width: 100%; height: 100%; object-fit: fill;">
            </button>
            <img src ="${PreviewImageUrl}" style="width: 100%; height: 100%; object-fit: cover;"   />                    
        </div>`

        filesPreviewBox.innerHTML += previewImage;
    }
    multimediaFilesForPreview = [];

    //enable the functionality to delete files from the post
    asignDeletePreview();

    //bring TextContent to the Modal
    textContentFieldModal.value = textContentField.value;

    uploadPost.style.display = "none";
    uploadPostModal.style.display = "block";

}
    
//give functionality to remove previewBotton
function asignDeletePreview() {
    let filePreviewButton = document.querySelectorAll("[name='deletePreviewButton']");
    
    filePreviewButton.forEach(x => {
        x.addEventListener("click", deletePreview);
    })
}

function deletePreview(event) {
    let filePreview = event.target.closest("div");
    multimediaFiles.splice(parseInt(filePreview.dataset.id), 1);
    filePreview.remove();
}

//Upload the post
export async function savePost(event) {
    event.preventDefault();
    let formData = new FormData(postForm);

    if (uploadPost.style.display == "none") {
        formData.set("TextContent", textContentFieldModal.value);
        formData.delete("textContentFieldModal");
    }
    multimediaFiles.forEach(x => {
        formData.append("MultimediaList", x)
    });

    let response = await getJson("https://localhost:7238/api/PostsApi/Create", {
        method: "POST",
        body: formData
    });

}

