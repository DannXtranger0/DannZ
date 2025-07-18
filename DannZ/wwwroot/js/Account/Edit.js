﻿let inputAvatar = document.querySelectorAll('input[type="file"]');
let avatar = document.getElementById("avatar");
let cover = document.getElementById("cover");

let CoverUrlField = document.querySelector("[name='CoverUrl']");
let AvatarUrlField = document.querySelector("[name='AvatarUrl']");
let NameField = document.querySelector("[name='Name']");
let EmailField = document.querySelector("[name='Email']");
let PasswordField = document.querySelector("[name='Password']");

let form = document.querySelector("form");

let route = (window.location.pathname).split("/");
let userId =  parseInt(route.at(route.length - 1));

document.addEventListener("DOMContentLoaded", () => {
    loadProfileData();
    inputAvatar.forEach(x => x.addEventListener("change", previewImage))


})

async function loadProfileData() {

    try {

        let response = await fetch(`https://localhost:7238/api/AccountApi/Profile/${userId}`, {
            method: 'GET',
            credentials: 'include'
        });
        if (!response.ok)
            throw new Error(response.text() ?? "Unknown Error");

        let data = await response.json();
        console.log(data);
        if (data["avatarUrl"] != null)
        avatar.src = data["avatarUrl"] ;

        if (data["coverUrl"] != null)
            cover.src = data["coverUrl"] 

        NameField.value = data["name"];
        EmailField.value= data["email"];

    }catch (err) {
        console.log(err);
    }
}

form.addEventListener("submit",saveChanges)

async function saveChanges(event) {
    event.preventDefault();

    let formData = new FormData(form);
    formData.append("Id", userId);

    try {
        let response = await fetch(`https://localhost:7238/api/AccountApi/Edit/${userId}`, {
            method: 'PUT',
            body: formData,
            credentials: 'include'
        });

        if (!response.ok)
            throw new Error(response.text() ?? "Unknown Error")

        let result = await response.json();
        console.log(result);

    } catch (err) {
        console.log(err);
    }
    

}
function previewImage(e) {
    let input = e.target;

    let img = input.files[0];
    let imgUrl = URL.createObjectURL(img);

    let imgPreview = input.previousElementSibling;

    imgPreview.src = imgUrl;

}