import { getJson } from "../Core/FetchUtils.js"
import {asignContentField,asignImage} from "../Core/AsignContentUtils.js"
import { bringFormData} from "../Core/BringFormData.js"

export let inputAvatar = document.querySelectorAll('input[type="file"]');
export let form = document.querySelector("form");

let route = (window.location.pathname).split("/");
let userId =  parseInt(route.at(route.length - 1));

export async function loadProfileData() {

        let data = await getJson(`https://localhost:7238/api/AccountApi/Profile/${userId}`, {
            method: 'GET',
            credentials: 'include'
        });

    asignImage("avatar", data["avatarUrl"]);
    asignImage("cover", data["coverUrl"]);

    asignContentField("Name", data["name"]);
    asignContentField("Email", data["email"]);
}

export async function saveChanges(event) {
    event.preventDefault();

    let isObject = false
    let formData = bringFormData(form, isObject);
    formData.append("Id", userId);
    await getJson(`https://localhost:7238/api/AccountApi/Edit/${userId}`, {
            method: 'PUT',
            body: formData,
            credentials: 'include'
        });

}

//Show the cover or avatar image at select it
export function previewImage(e) {
    let input = e.target;

    let img = input.files[0];
    let imgUrl = URL.createObjectURL(img);

    let imgPreview = input.previousElementSibling;

    imgPreview.src = imgUrl;
}