import { setData, editBio,profileId} from "../features/Profile.js"
import { setUpPosts,bringData} from "../features/FeedPost.js"
//Load the profile data
document.addEventListener("DOMContentLoaded", async () => {
    await setData();
    await setUpPosts(await bringData(profileId, ""));
})
//Update the biography
let btnEditBio = document.getElementById("btnEditBio");
btnEditBio.addEventListener("click", editBio);
