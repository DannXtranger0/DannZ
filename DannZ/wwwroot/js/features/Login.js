import { bringFormData } from "../Core/BringFormData.js"
import { getJson } from "../Core/FetchUtils.js"


export async function login(form) {
    let formData = bringFormData(form);

    let rememberMe = document.querySelector("[name='RememberMe']");
    formData["RememberMe"] = rememberMe.checked;

    const data = await getJson("https://localhost:7238/api/AuthApi", {
        method: 'POST',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(formData)
    })
    if (data) return true;
    else false;


}
