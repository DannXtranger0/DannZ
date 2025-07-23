import { getJson} from "../Core/FetchUtils.js";
import { bringFormData } from "../Core/BringFormData.js";
export let form = document.querySelector("form");

export async function  registerAccount() {
    let isObject = false;
    let formData = bringFormData(form, isObject);

    let response = await getJson("https://localhost:7238/api/AuthApi/Create", {
        method: "POST",
        body: formData,
        credentials: 'include'
    });


    if (response) return true;
    else false;




}