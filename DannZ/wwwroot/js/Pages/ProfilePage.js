import { setData, editBio,profileId} from "../features/Profile.js"
import { setUpPosts, bringData } from "../features/FeedPost.js"

import { btnMultimedia, btnMultimediaModal, multimediaField, showFilePreview, postForm, savePost } from "../features/CreatePost.js"

//Load the profile data
document.addEventListener("DOMContentLoaded", async () => {
    await setData();
    await setUpPosts(await bringData(profileId, ""));

    //open the input File between the modal and the principal input
    btnMultimedia.addEventListener("click", () => {
        multimediaField.click();
    })
    btnMultimediaModal.addEventListener("click", () => {
        multimediaField.click();
    })

    //when select an file to upload, show the preview of the selected file
    multimediaField.addEventListener("change", showFilePreview);

    postForm.addEventListener("submit", savePost);
})
//Update the biography
let btnEditBio = document.getElementById("btnEditBio");
btnEditBio.addEventListener("click", editBio);
