import { setUpPosts,bringData} from "../features/FeedPost.js"
import { btnMultimedia, btnMultimediaModal,multimediaField,showFilePreview,postForm,savePost } from "../features/CreatePost.js"

document.addEventListener("DOMContentLoaded",async () => {
    setUpPosts(await bringData());

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

});



