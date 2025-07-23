import { loadProfileData,previewImage,inputAvatar,form, saveChanges } from "../features/Edit.js"

document.addEventListener("DOMContentLoaded", async () => {
    await loadProfileData();
    inputAvatar.forEach(x => x.addEventListener("change" , previewImage))
})

form.addEventListener("submit", saveChanges);