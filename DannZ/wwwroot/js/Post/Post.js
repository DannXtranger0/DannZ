import { btnMultimedia, multimediaField, postForm, textContentField } from "./Create.js"
import { mainFeed,setUpPosts } from "./FeedPost.js"

btnMultimedia.addEventListener("click", () => {
    multimediaField.click();
})
document.addEventListener("DOMContentLoaded", () => {
    setUpPosts();

})