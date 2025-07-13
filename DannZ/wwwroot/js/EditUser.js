document.addEventListener("DOMContentLoaded", () => {
    inputAvatar.forEach(x => x.addEventListener("change", previewImage))

})


let inputAvatar = document.querySelectorAll('input[type="file"]');

function previewImage(e) {
    let input = e.target;

    let img = input.files[0];
    let imgUrl = URL.createObjectURL(img);

    let imgPreview = input.previousElementSibling;

    imgPreview.src = imgUrl;

}